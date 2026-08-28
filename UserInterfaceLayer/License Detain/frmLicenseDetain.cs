using DVDL_Logic_layer.DetainedLicense;
using DVDL_Logic_layer.Global_Classes;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.License_Detain
{
    public partial class frmLicenseDetain : Form
    {
        private clsLicenseDTO _selectedLicenseInfo;

        public frmLicenseDetain()
        {
            InitializeComponent();
        }

        private void frmLicenseDetain_Load(object sender, System.EventArgs e)
        {
            ctrlFilterIicenseCard1.OnLicenseSelected += CtrlFilterIicenseCard1_OnLicenseSelected;
        }
        public event Action OnLicenseDetain;
        private bool _ValidateLicenseForDetain(clsLicenseDTO licenseInfo)
        {
            if (licenseInfo == null)
            {
                btnDetain.Enabled = false;
                return false;
            }

            // 1. التأكد من أن الرخصة ليست محتجزة بالفعل
            if (clsDetainedLicense.IsLicenseDetained(licenseInfo.LicenseID))
            {
                btnDetain.Enabled = false;
                MessageBox.Show(
                    "Selected License is ALREADY detained, please choose another license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            btnDetain.Enabled = true;
            return true;
        }

        private void CtrlFilterIicenseCard1_OnLicenseSelected(clsLicenseDTO licenseInfo)
        {
            _selectedLicenseInfo = licenseInfo;

            if (_selectedLicenseInfo == null)
            {
                btnDetain.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
                return;
            }

            // تفعيل روابط عرض تفاصيل الرخصة والسجل فور اختيار رخصة صحيحة
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            // التحقق من صلاحية الرخصة للاحتجاز ثم ملء البيانات المبدئية
            if (_ValidateLicenseForDetain(_selectedLicenseInfo))
            {
                _FillDefaultDetainData();
            }
        }

        private void _FillDefaultDetainData()
        {
            if (_selectedLicenseInfo == null)
                return;

            // ملء بيانات مجموعة Detain Info حسب العناصر الموجودة في الواجهة
            lblDetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblLicenseID.Text = _selectedLicenseInfo.LicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName; // أو UserID حسب نظامك

            lblDetainID.Text = "[???]";
            txtFineFees.Text = "";

            // نقل التركيز المباشر لمربع نص الغرامة لسهولة الإدخال
            txtFineFees.Focus();
        }
        private void _CreatDetentionLicenseRecord()
        {
            // 1. الحماية من التحويل الخاطئ للغرامة (حل الخطأ القاتل الأول)
            if (!decimal.TryParse(txtFineFees.Text.Trim(), out decimal fineFees) || fineFees < 0)
            {
                MessageBox.Show("Please enter a valid fine fee amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFineFees.Focus();
                return;
            }

            // 2. الحماية من كائن الرخصة الفارغ
            if (_selectedLicenseInfo == null)
            {
                MessageBox.Show("Please select a valid license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDetainedLicenseDTO detainedLicenseDTO = new clsDetainedLicenseDTO
            {
                LicenseID = _selectedLicenseInfo.LicenseID,
                DetainDate = DateTime.Now,
                FineFees = fineFees,
                CreatedByUserID = (clsGlobal.CurrentUser != null) ? clsGlobal.CurrentUser.UserID : 1,
                IsReleased = false,
                ReleaseDate = null,
                ReleasedByUserID = null,
                ReleaseApplicationID = null
            };

            int result = clsDetainedLicense.DetainLicense(detainedLicenseDTO);

            if (result != -1)
            {

                // تحديث الواجهة فور النجاح
                lblDetainID.Text = result.ToString();
                btnDetain.Enabled = false;
                txtFineFees.Enabled = false;
                OnLicenseDetain?.Invoke();

                MessageBox.Show($"License Detained Successfully with ID = {result}", "License Detained", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to Detain License. Please try again or check system log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _CreatDetentionLicenseRecord();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
