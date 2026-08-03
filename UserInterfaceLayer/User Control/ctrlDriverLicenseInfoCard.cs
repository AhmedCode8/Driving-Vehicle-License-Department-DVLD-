//using DVDL_Logic_layer.driver;
//using DVDL_Logic_layer.License_Class;
//using DVDL_Logic_layer.Person;
//using DVLD_DTOs;
//using System.ComponentModel;
//using System.Windows.Forms;

//namespace UserInterfaceLayer.User_Control
//{
//    public partial class ctrlDriverLicenseInfoCard : UserControl
//    {
//        clsLicenseDTO _License;

//        public ctrlDriverLicenseInfoCard()
//        {
//            InitializeComponent();
//            LoadInfo(_License);
//        }
//        // Constructor يستقبل رقم الرخصة

//        public void LoadInfo(clsLicenseDTO license)
//        {
//            _License = license;

//            if (_License == null)
//            {
//                MessageBox.Show("No License info2222!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;

//            }




//            // 1. تعبئة بيانات الرخصة مباشرة من الكائن التابع لك
//            lblLicenseID.Text = _License.LicenseID.ToString();
//            lblDriverID.Text = _License.DriverID.ToString();
//            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
//            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
//            lblNotes.Text = string.IsNullOrEmpty(_License.Notes) ? "No Notes" : _License.Notes;
//            lblIsActive.Text = _License.IsActive ? "Yes" : "No";

//            // تحويل سبب الإصدار لنص
//            switch (_License.IssueReason)
//            {
//                case 1: lblIssueReason.Text = "First Time"; break;
//                case 2: lblIssueReason.Text = "Renew"; break;
//                case 3: lblIssueReason.Text = "Replacement for Damaged"; break;
//                case 4: lblIssueReason.Text = "Replacement for Lost"; break;
//                default: lblIssueReason.Text = "First Time"; break;
//            }

//            // 2. تعبئة اسم صنف الرخصة
//            clsLicenseClassDTO licenseClass = clsLicenseClass.GetLicenseClassByID(_License.LicenseClass);
//            lblClass.Text = (licenseClass != null) ? licenseClass.ClassName : "[???]";

//            // 3. تعبئة بيانات السائق والشخص المرتبط بها
//            clsDriverDTO driver = clsDrivers.GetDriverByID(_License.DriverID);
//            if (driver != null)
//            {
//                clsPersonDTO person = clsPerson.GetPersonById(driver.PersonID);
//                if (person != null)
//                {
//                    lblFullName.Text = person.FullName;
//                    lblNationalNo.Text = person.NationalNo;
//                    lblGender.Text = (person.Gendor == 0) ? "Male" : "Female";
//                    lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();

//                    // تحميل الصورة بناءً على الجنس والمسار
//                    pbDriverImage.Image = (person.Gendor == 0)
//                        ? Properties.Resources.Male_512
//                        : Properties.Resources.Female_512;

//                }
//            }

//        }


//    }
//}
using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.License_Class;
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.IO;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlDriverLicenseInfoCard : UserControl
    {
        private clsLicenseDTO _license;

        /// <summary>
        /// الحصول على كائن الرخصة المعروض حالياً
        /// </summary>
        public clsLicenseDTO LicenseInfo => _license;

        public ctrlDriverLicenseInfoCard()
        {
            InitializeComponent();

            // تهيئة الواجهة بالقيم الافتراضية دون استدعاء رسائل خطأ أثناء التصميم
            ResetDefaultValues();
        }

        /// <summary>
        /// إعادة تعيين جميع عناصر الواجهة إلى القيم الافتراضية
        /// </summary>
        public void ResetDefaultValues()
        {
            _license = null;

            lblLicenseID.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblNotes.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblClass.Text = "[???]";
            lblFullName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGender.Text = "[???]";
            lblDateOfBirth.Text = "[???]";

            pbDriverImage.Image = Properties.Resources.Male_512;
        }

        /// <summary>
        /// تحميل وتعبئة بيانات الرخصة في عناصر الواجهة
        /// </summary>
        /// <param name="license">كائن الرخصة المراد عرض بياناتها</param>
        public void LoadInfo(clsLicenseDTO license)
        {
            _license = license;

            if (_license == null)
            {
                MessageBox.Show("لم يتم العثور على بيانات الرخصة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetDefaultValues();
                return;
            }

            try
            {
                // 1. تعبئة بيانات الرخصة الأساسية
                _FillLicenseInfo();

                // 2. تعبئة اسم صنف الرخصة
                _FillLicenseClassInfo();

                // 3. تعبئة بيانات السائق والشخص المرتبط بها
                _FillDriverAndPersonInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل بيانات الرخصة: {ex.Message}", "خطأ في النظام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Helper Private Methods

        private void _FillLicenseInfo()
        {
            lblLicenseID.Text = _license.LicenseID.ToString();
            lblDriverID.Text = _license.DriverID.ToString();
            lblIssueDate.Text = _license.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _license.ExpirationDate.ToShortDateString();
            lblNotes.Text = string.IsNullOrWhiteSpace(_license.Notes) ? "No Notes" : _license.Notes;
            lblIsActive.Text = _license.IsActive ? "Yes" : "No";

            lblIssueReason.Text = _GetIssueReasonText(_license.IssueReason);
        }

        private string _GetIssueReasonText(byte issueReason)
        {
            switch (issueReason)
            {
                case 1: return "First Time";
                case 2: return "Renew";
                case 3: return "Replacement for Damaged";
                case 4: return "Replacement for Lost";
                default: return "First Time";
            }
        }

        private void _FillLicenseClassInfo()
        {
            clsLicenseClassDTO licenseClass = clsLicenseClass.GetLicenseClassByID(_license.LicenseClass);
            lblClass.Text = licenseClass?.ClassName ?? "[???]";
        }

        private void _FillDriverAndPersonInfo()
        {
            clsDriverDTO driver = clsDrivers.GetDriverByID(_license.DriverID);
            if (driver == null)
            {
                _SetPersonDefaultInfo();
                return;
            }

            clsPersonDTO person = clsPerson.GetPersonById(driver.PersonID);
            if (person == null)
            {
                _SetPersonDefaultInfo();
                return;
            }

            lblFullName.Text = person.FullName;
            lblNationalNo.Text = person.NationalNo;
            lblGender.Text = (person.Gendor == 0) ? "Male" : "Female";
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();

            _LoadPersonImage(person);
        }

        private void _LoadPersonImage(clsPersonDTO person)
        {
            // التحقق مما إذا كان هناك مسار صورة خاص وموجود على الجهاز
            if (!string.IsNullOrEmpty(person.ImagePath) && File.Exists(person.ImagePath))
            {
                pbDriverImage.ImageLocation = person.ImagePath;
            }
            else
            {
                // الصورة الافتراضية بحسب الجنس
                pbDriverImage.Image = (person.Gendor == 0)
                    ? Properties.Resources.Male_512
                    : Properties.Resources.Female_512;
            }
        }

        private void _SetPersonDefaultInfo()
        {
            lblFullName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGender.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            pbDriverImage.Image = Properties.Resources.Male_512;
        }

        #endregion
    }
}

