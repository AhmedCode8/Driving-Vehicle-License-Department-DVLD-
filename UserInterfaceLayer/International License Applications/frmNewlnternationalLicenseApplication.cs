using DVDL_Logic_layer.Application_Types;
using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.License_Class;

using DVLD_DTOs;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.License;

namespace UserInterfaceLayer.International_License_Applications
{
    public partial class frmNewlnternationalLicenseApplication : Form
    {
        public frmNewlnternationalLicenseApplication()
        {
            InitializeComponent();
            // ربط حدث الكنترول مع الدالة في الـ Form
            ctrlFilterIicenseCard1.OnLicenseSelected += ctrlFilterIicenseCard1_OnLicenseSelected;
        }
        clsApplications _Application = new clsApplications();
        clsInternationalLicense internationalLicense = new clsInternationalLicense();

        private void ctrlFilterIicenseCard1_OnLicenseSelected(clsLicenseDTO selectedLicense)
        {
            // استدعاء دالة تعبئة البيانات الأولية فور وصول إشعار من الكنترول
            _LoadInitialApplicationData(selectedLicense);
        }
        public bool IsLicenseValid(clsLicenseDTO selectedLicense, out string errorMessage)
        {
            errorMessage = string.Empty;

            // 0. التحقق من أن الكائن غير فارغ
            if (selectedLicense == null)
            {
                errorMessage = "الرخصة غير موجودة أو لم يتم تحديدها!";
                return false;
            }

            // 1. الشرط الأول: هل الرخصة من الفئة الثالثة؟ (Class 3)
            if (selectedLicense.LicenseClass != 3)
            {
                errorMessage = "عذراً، الرخصة ليست من الفئة الثالثة!";
                return false;
            }

            // 2. الشرط الثاني: هل الرخصة غير منتهية الصلاحية؟
            if (selectedLicense.ExpirationDate < DateTime.Now)
            {
                errorMessage = "عذراً، الرخصة منتهية الصلاحية!";
                return false;
            }

            // 3. الشرط الثالث: هل الرخصة مفعلة؟
            if (!selectedLicense.IsActive)
            {
                errorMessage = "عذراً، الرخصة غير مفعلة!";
                return false;
            }

            // 4. الشرط الرابع: هل يمتلك رخصة دولية نشطة لنفس الرخصة المحلية؟
            int activeIntLicenseID = clsInternationalLicense.GetActiveInternationalLicenseByLocalLicenseID(selectedLicense.LicenseID);

            if (activeIntLicenseID > 0)
            {
                errorMessage = $"عذراً، هذا السائق يمتلك بالفعل رخصة دولية نشطة برقم [{activeIntLicenseID}]!";
                return false;
            }

            // استوفى جميع الشروط بنجاح
            return true;
        }
        private bool _IssueInternationalLicense(clsLicenseDTO selectedLicense, int applicationID)
        {

            internationalLicense.ApplicationID = applicationID;
            internationalLicense.DriverID = selectedLicense.DriverID;
            internationalLicense.IssuedUsingLocalLicenseID = selectedLicense.LicenseID;
            internationalLicense.IssueDate = DateTime.Now;
            internationalLicense.ExpirationDate = DateTime.Now.AddYears(1); // صالحة لسنة واحدة
            internationalLicense.IsActive = true;
            internationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (internationalLicense.Save())
            {
                // استدعاء الدالة الثانية لتحديث رقم الطلب ورقم الرخصة الدولية في الصفحة
                _UpdateIssuedApplicationData(applicationID, internationalLicense.InternationalLicenseID);
                MessageBox.Show($"تم إصدار الرخصة الدولية بنجاح! رقم الرخصة: {internationalLicense.InternationalLicenseID}",
                                "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            MessageBox.Show("فشل في إصدار الرخصة الدولية!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsLicenseDTO selectedLicense = ctrlFilterIicenseCard1.SelectedLicenseInfo;

            // 1. التحقق من الشروط
            if (!IsLicenseValid(selectedLicense, out string errorMsg))
            {
                MessageBox.Show(errorMsg, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. إنشاء الطلب أولاً، ثم إصدار الرخصة الدولية فور نجاحه
            if (_CreateNewApplication(selectedLicense))
            {
                _IssueInternationalLicense(selectedLicense, _Application.ApplicationID);
            }


        }
        private bool _CreateNewApplication(clsLicenseDTO selectedLicense)
        {


            // 2. جلب PersonID من DriverID
            clsDriverDTO driver = clsDrivers.GetDriverByID(selectedLicense.DriverID);
            if (driver == null)
            {
                MessageBox.Show("تعذر الحصول على بيانات السائق!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 3. جلب رسوم نوع الطلب رقم 6 (New International License)
            clsApplicationTypeDTO appType = clsApplicationTypes.GetApplicationTypeByID(6);

            // 4. تعبئة كائن الطلب
            _Application.ApplicantPersonID = driver.PersonID;
            _Application.ApplicationDate = DateTime.Now;
            _Application.ApplicationTypeID = 6; // نوع الطلب السادس
            _Application.ApplicationStatus = 3;  // حالة الطلب: مكتملة
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = appType != null ? appType.ApplicationFees : 50;
            _Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            // 5. حفظ الطلب
            if (_Application.Save())
            {
                MessageBox.Show($"تم إنشاء الطلب بنجاح برقم: {_Application.ApplicationID}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            MessageBox.Show("فشل في حفظ الطلب!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        // 1. الدالة الأولى: تعبئة البيانات الأولية قبل إكمال الطلب وإصدار الرخصة
        private void _LoadInitialApplicationData(clsLicenseDTO selectedLicense)
        {
            if (selectedLicense == null)
            {
                _ResetDefaultValues();
                return;
            }

            // تعيين المعرفات غير المصدرة بعد إلى [???]
            lblLRApplicationID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";

            // تعبئة البيانات العامة بما فيها رقم الرخصة المحلية
            lblLocalLicenseID.Text = selectedLicense.LicenseID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueData.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirtionData.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");

            // جلب رسوم نوع الطلب رقم 6
            clsApplicationTypeDTO appType = clsApplicationTypes.GetApplicationTypeByID(6);
            lblApplicationFees.Text = (appType != null ? appType.ApplicationFees : 50).ToString();

            // اسم المستخدم الحالي
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        // 2. الدالة الثانية: تحديث المعرفات بعد نجاح إنشاء الطلب وإصدار الرخصة الدولية
        private void _UpdateIssuedApplicationData(int applicationID, int internationalLicenseID)
        {
            lblLRApplicationID.Text = applicationID.ToString();
            lblReplacedLicenseID.Text = internationalLicenseID.ToString();
        }

        // دالة مساعدة لتفريغ الحقول في حال عدم تحديد رخصة
        private void _ResetDefaultValues()
        {
            lblLRApplicationID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueData.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblExpirtionData.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            lblApplicationFees.Text = "0";
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            clsLicenseDTO selectedLicense = ctrlFilterIicenseCard1.SelectedLicenseInfo;
            if (selectedLicense == null)
            {
                MessageBox.Show("الرجاء اختيار رخصة أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsDriverDTO driver = clsDrivers.GetDriverByID(selectedLicense.DriverID);
            if (driver != null)
            {
                frmLicenseHistory frm = new frmLicenseHistory(driver.PersonID);
                frm.ShowDialog();
            }

        }


        public static clsLicenseDTO ConvertToLicenseDTO(clsInternationalLicense intLicense)
        {
            if (intLicense == null) return null;

            return new clsLicenseDTO
            {
                LicenseID = intLicense.InternationalLicenseID, // نضع معرّف الرخصة الدولية
                ApplicationID = intLicense.ApplicationID,
                DriverID = intLicense.DriverID,
                CreatedByUserID = intLicense.CreatedByUserID,
                IssueDate = intLicense.IssueDate,
                ExpirationDate = intLicense.ExpirationDate,
                IsActive = intLicense.IsActive,

                // خصائص لا توجد في الرخصة الدولية، نضع لها قيماً افتراضية:
                LicenseClass = 3,             // أو الفئة المخصصة للدولي إذا كانت موجودة
                PaidFees = 0,                 // الرسوم
                IssueReason = 1,              // سبب افتراضي (First Time)
                Notes = "International License"
            };
        }
        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            if (internationalLicense != null && internationalLicense.InternationalLicenseID > 0)
            {
                clsLicenseDTO licenseInfo = ConvertToLicenseDTO(internationalLicense);
                frmLicenseInfo frm = new frmLicenseInfo(licenseInfo);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("لم يتم إصدار الرخصة الدولية بعد!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
