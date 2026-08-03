using DVDL_Logic_layer.Application_Types;
using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.License_Class;
using DVLD_DTOs;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.License;

namespace UserInterfaceLayer.Application_Type
{
    public partial class frmReplacementforDamagedLicense : Form
    {
        private clsLicenseDTO _selectedLicenseInfo;
        private clsApplicationTypeDTO _InfoApplicationTyp;
        private clsLicenseClassDTO _InfoLicenseClass;

        public frmReplacementforDamagedLicense()
        {
            InitializeComponent();
        }

        private void frmReplacementforDamagedLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterIicenseCard1.OnLicenseSelected += CtrlFilterIicenseCard1_OnLicenseSelected;

            // تحديد خيار "بدل تالف" كخيار افتراضي وتحديث نوع المعاملة والرسوم
            rbDamagedLicense.Checked = true;
            rbDamagedLicense_CheckedChanged(null, null);
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            // 4 = Replacement for Damaged, 3 = Replacement for Lost
            int applicationTypeID = rbDamagedLicense.Checked ? 4 : 3;
            _InfoApplicationTyp = clsApplicationTypes.GetApplicationTypeByID(applicationTypeID);

            // تحديث عرض الرسوم فوراً في الواجهة عند تغيير الخيار
            if (_InfoApplicationTyp != null)
            {
                lblApplicationFees.Text = _InfoApplicationTyp.ApplicationFees.ToString("0.##");
            }
        }

        // فحص صلاحية الرخصة لاستبدالها (يجب أن تكون نشطة وغيييير منتهية)
        private bool _ValidateLicenseForReplacement(clsLicenseDTO licenseInfo)
        {
            if (licenseInfo == null)
            {
                btnIssueReplacement.Enabled = false;
                return false;
            }

            // 1. التأكد من أن الرخصة نشطة
            if (!licenseInfo.IsActive)
            {
                btnIssueReplacement.Enabled = false;
                MessageBox.Show(
                    "Selected License is NOT Active, please choose an active license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            // 2. التأكد من أن الرخصة غير منتهية الصلاحية (لأن المنتهية تتطلب تجديدًا وليس استبدالًا)
            if (licenseInfo.ExpirationDate < DateTime.Now)
            {
                btnIssueReplacement.Enabled = false;
                string formattedDate = licenseInfo.ExpirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                MessageBox.Show(
                    $"Selected License is expired on: {formattedDate}.\nYou must RENEW it instead of replacement.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            btnIssueReplacement.Enabled = true;
            return true;
        }

        private void CtrlFilterIicenseCard1_OnLicenseSelected(clsLicenseDTO licenseInfo)
        {
            _selectedLicenseInfo = licenseInfo;

            if (_selectedLicenseInfo == null)
            {
                btnIssueReplacement.Enabled = false;
                return;
            }

            _InfoLicenseClass = clsLicenseClass.GetLicenseClassByID(_selectedLicenseInfo.LicenseClass);

            if (_ValidateLicenseForReplacement(_selectedLicenseInfo))
            {
                _FillDefaultApplicationData();
            }
        }

        private void _FillDefaultApplicationData()
        {
            if (_selectedLicenseInfo == null)
                return;

            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblOldLicenseID.Text = _selectedLicenseInfo.LicenseID.ToString();

            decimal applicationFees = (_InfoApplicationTyp != null) ? _InfoApplicationTyp.ApplicationFees : 0;
            lblApplicationFees.Text = applicationFees.ToString("0.##");
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserID.ToString();

            lblLRApplicationID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";
        }

        private void _UpdateReplacedInfo(int newApplicationID, int newReplacedLicenseID)
        {
            lblLRApplicationID.Text = newApplicationID.ToString();
            lblReplacedLicenseID.Text = newReplacedLicenseID.ToString();
        }

        private void Createtheapplicationandcreatethelicense()
        {
            if (_selectedLicenseInfo == null || _InfoApplicationTyp == null)
            {
                MessageBox.Show("بيانات الرخصة أو نوع الطلب غير مكتملة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDriverDTO driverInfo = clsDrivers.GetDriverByID(_selectedLicenseInfo.DriverID);
            if (driverInfo == null)
            {
                MessageBox.Show("لم يتم العثور على بيانات السائق!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1. إلغاء تفعيل الرخصة القديمة
            if (!clsLicenses.DeactivateLicense(_selectedLicenseInfo.LicenseID))
            {
                MessageBox.Show("فشل في إلغاء تفعيل الرخصة القديمة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. إنشاء الطلب في النظام
            clsApplications baseApplication = new clsApplications()
            {
                ApplicantPersonID = driverInfo.PersonID,
                CreatedByUserID = clsGlobal.CurrentUser.UserID,
                ApplicationTypeID = _InfoApplicationTyp.ApplicationTypeID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3, // Completed
                LastStatusDate = DateTime.Now,
                PaidFees = _InfoApplicationTyp.ApplicationFees
            };

            if (!baseApplication.Save())
            {
                MessageBox.Show("حدث خطأ أثناء حفظ الطلب!", "فشل الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. تحديد سبب الإصدار (3 = Damaged Replacement, 4 = Lost Replacement)
            byte issueReason = (byte)(rbDamagedLicense.Checked ? 3 : 4);

            // 4. إنشاء الرخصة الجديدة ونقل نفس تاريخ الانتهاء والرسوم القديمة
            clsLicenseDTO newLicense = new clsLicenseDTO
            {
                ApplicationID = baseApplication.ApplicationID,
                DriverID = driverInfo.DriverID,
                LicenseClass = _selectedLicenseInfo.LicenseClass,
                IssueDate = DateTime.Now,
                ExpirationDate = _selectedLicenseInfo.ExpirationDate, // نحتفظ بنفس تاريخ الانتهاء القديم
                Notes = _selectedLicenseInfo.Notes,
                PaidFees = 0, // لا توجد رسوم رخصة إضافية، تم دفع رسوم الطلب فقط
                IsActive = true,
                IssueReason = issueReason,
                CreatedByUserID = clsGlobal.CurrentUser.UserID
            };

            int newLicenseID = clsLicenses.AddNewLicense(newLicense);

            if (newLicenseID != 0)
            {
                MessageBox.Show($"تم استبدال الرخصة بنجاح!\nرقم الرخصة الجديدة: [{newLicenseID}]", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _UpdateReplacedInfo(baseApplication.ApplicationID, newLicenseID);

                // إغلاق عناصر التحكم لمنع التكرار
                btnIssueReplacement.Enabled = false;
                gbReplacementFor.Enabled = false; // تعطيل الـ RadioButtons
                llShowNewLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("فشل في إصدار الرخصة الجديدة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            Createtheapplicationandcreatethelicense();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selectedLicenseInfo == null)
                return;

            clsDriverDTO driverInfo = clsDrivers.GetDriverByID(_selectedLicenseInfo.DriverID);
            if (driverInfo != null)
            {
                frmLicenseHistory frm = new frmLicenseHistory(driverInfo.PersonID);
                frm.ShowDialog();
            }
        }
    }
}