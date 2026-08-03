
using DVDL_Logic_layer.License_Class;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlFilterIicenseCard : UserControl
    {
        // 1. تعريف الـ Event الذي سيستمع إليه الـ Form
        public event Action<clsLicenseDTO> OnLicenseSelected;

        public int LicenseID { get; private set; } = -1;
        public clsLicenseDTO SelectedLicenseInfo
        {
            get
            {
                return ctrlDriverLicenseInfoCard1.LicenseInfo;
            }
        }
        public ctrlFilterIicenseCard()
        {
            InitializeComponent();
        }
        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            FindLicense();
        }
        public void FindLicense()
        {
            if (string.IsNullOrWhiteSpace(txtLicenseID.Text))
            {
                MessageBox.Show("يرجى إدخال رقم الرخصة أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLicenseID.Focus();
                return;
            }

            if (!int.TryParse(txtLicenseID.Text.Trim(), out int licenseID))
            {
                MessageBox.Show("يرجى إدخال رقم رخصة صحيح!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            this.LicenseID = licenseID;

            clsLicenseDTO license = clsLicenses.GetLicenseByID(this.LicenseID);

            if (license == null)
            {
                MessageBox.Show($"لم يتم العثور على رخصة تحمل الرقم [{this.LicenseID}]", "غير موجود", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlDriverLicenseInfoCard1.ResetDefaultValues();

                // إعادة تعيين الحقول في الـ Form عند عدم العثور على رخصة
                OnLicenseSelected?.Invoke(null);
                return;
            }

            // عرض البيانات في كارت الرخصة
            ctrlDriverLicenseInfoCard1.LoadInfo(license);

            // 2. إطلاق الحدث وإرسال بيانات الرخصة إلى الـ Form
            OnLicenseSelected?.Invoke(license);
        }
        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnFindLicense.PerformClick();
            }
        }

        // دالة تُستدعى من خارج الـ UserControl لتحميل رخصة برقم محدد فوراً
        public void LoadLicenseInfo(int licenseID)
        {
            txtLicenseID.Text = licenseID.ToString();
            FindLicense(); // ستتولى دالة البحث جلب البيانات وإطلاق الحدث OnLicenseSelected
        }
    }
}