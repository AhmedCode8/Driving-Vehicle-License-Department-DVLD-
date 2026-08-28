using DVDL_Logic_layer.Application_Types;
using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.DetainedLicense;
using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.Global_Classes;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.License_Detain
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }
        private int _licenseID = -1; // متغير لحفظ الرقم المؤقت
        public frmReleaseDetainedLicense(int licenseID)
        {
            InitializeComponent();
            _licenseID = licenseID; // نحفظ الرقم فقط ولا نستدعي التحميل هنا!
        }



        private clsLicenseDTO _selectedLicenseInfo;
        private clsDetainedLicenseDTO _detainedLicenseInfo;
        clsApplicationTypeDTO _applicationTypeDTO = clsApplicationTypes.GetApplicationTypeByID(5);
        public event Action OnReleaseDetainedLicense;


        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterIicenseCard1.OnLicenseSelected += CtrlFilterIicenseCard1_OnLicenseSelected;
            // 2. نستدعي التحميل بعد ضمان وجود المستمع (Event Listener)
            if (_licenseID != -1)
            {
                ctrlFilterIicenseCard1.LoadLicenseInfo(_licenseID);
            }
        }

        // 1. التحقق من أن الرخصة محتجزة بالفعل (عكس شرط الاحتجاز)
        private bool _ValidateLicenseForRelease(clsLicenseDTO licenseInfo)
        {
            if (licenseInfo == null)
            {
                btnRelease.Enabled = false;
                return false;
            }

            // التأكد من أن الرخصة محتجزة في النظام
            if (!clsDetainedLicense.IsLicenseDetained(licenseInfo.LicenseID))
            {
                btnRelease.Enabled = false;
                MessageBox.Show(
                    "Selected License is NOT detained. Please choose a detained license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            btnRelease.Enabled = true;
            return true;
        }

        // 2. عند اختيار الرخصة من الكنترول
        private void CtrlFilterIicenseCard1_OnLicenseSelected(clsLicenseDTO licenseInfo)
        {
            _selectedLicenseInfo = licenseInfo;

            if (_selectedLicenseInfo == null)
            {
                btnRelease.Enabled = false;
                llShowLicenseHistory.Enabled = false;
                llShowLicenseInfo.Enabled = false;
                return;
            }

            // تفعيل روابط العرض
            llShowLicenseHistory.Enabled = true;
            llShowLicenseInfo.Enabled = true;

            // إذا كانت الرخصة محتجزة، قم بملء بيانات الإفراج والماليات
            if (_ValidateLicenseForRelease(_selectedLicenseInfo))
            {
                _FillReleaseData();
            }
        }

        // 3. قراءة بيانات الاحتجاز السابقة وحساب المبالغ المالية
        private void _FillReleaseData()
        {
            if (_selectedLicenseInfo == null)
                return;

            // جلب سجل الاحتجاز من طبقة المنطق بواسطة LicenseID
            _detainedLicenseInfo = clsDetainedLicense.GetDetainedLicenseByLicenseID(_selectedLicenseInfo.LicenseID);

            if (_detainedLicenseInfo == null)
            {
                MessageBox.Show("Could not find detention record for this license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            // ملء بيانات الواجهة حسب الصورة
            lblDetainID.Text = _detainedLicenseInfo.DetainID.ToString();
            lblLicenseID.Text = _selectedLicenseInfo.LicenseID.ToString();
            lblDetainDate.Text = _detainedLicenseInfo.DetainDate.ToString("dd/MMM/yyyy");
            lblCreatedBy.Text = _detainedLicenseInfo.CreatedByUserID.ToString();

            // المبالغ المالية
            decimal applicationFees = _applicationTypeDTO.ApplicationFees;
            decimal fineFees = _detainedLicenseInfo.FineFees;
            decimal totalFees = applicationFees + fineFees;

            lblApplicationFees.Text = applicationFees.ToString("0.00");
            lblFineFees.Text = fineFees.ToString("0.00");
            lblTotalFees.Text = totalFees.ToString("0.00");

            lblApplicationID.Text = "[???]";
        }

        // 1. الدالة الرئيسية (منظمة وتصف تسلسل العمليات بوضوح)
        private void _ReleaseDetainedLicense()
        {
            // أ. التحقق من جاهزية البيانات
            if (!_IsValidForRelease())
                return;

            // ب. جلب معرّف الشخص (السائق)
            int personID = _GetApplicantPersonID();
            if (personID == -1)
            {
                MessageBox.Show("لم يتم العثور على بيانات السائق!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int currentUserID = (clsGlobal.CurrentUser != null) ? clsGlobal.CurrentUser.UserID : 1;

            // ج. إنشاء طلب فك الاحتجاز
            int applicationID = _CreateReleaseApplication(personID, currentUserID);
            if (applicationID == -1)
            {
                MessageBox.Show("حدث خطأ أثناء حفظ الطلب!", "فشل الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // د. فك الاحتجاز وتحديث الواجهة
            _ExecuteReleaseAndHandleResult(applicationID, currentUserID);

        }

        // ==================== الدوال المساعدة (Helper Methods) ====================

        // دالة التحقق من المدخلات الرئيسية
        private bool _IsValidForRelease()
        {
            if (_selectedLicenseInfo == null || _detainedLicenseInfo == null)
            {
                MessageBox.Show("No license selected or detained record found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_applicationTypeDTO == null)
            {
                MessageBox.Show("Application type information is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // دالة جلب رقم الشخص المرتبط بالسائق
        private int _GetApplicantPersonID()
        {
            clsDriverDTO driverInfo = clsDrivers.GetDriverByID(_selectedLicenseInfo.DriverID);
            return driverInfo?.PersonID ?? -1;
        }

        // دالة إنشاء وحفظ كائن الطلب (Application)
        private int _CreateReleaseApplication(int personID, int userID)
        {
            clsApplications baseApplication = new clsApplications()
            {
                ApplicantPersonID = personID,
                CreatedByUserID = userID,
                ApplicationTypeID = _applicationTypeDTO.ApplicationTypeID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3, // Completed
                LastStatusDate = DateTime.Now,
                PaidFees = _applicationTypeDTO.ApplicationFees
            };

            return baseApplication.Save() ? baseApplication.ApplicationID : -1;
        }

        // دالة تنفيذ العملية النهائية وتحديث الشاشة والرسائل
        private void _ExecuteReleaseAndHandleResult(int applicationID, int userID)
        {
            int result = clsDetainedLicense.ReleaseLicense
                (
                _detainedLicenseInfo.DetainID,
                applicationID,
                userID,
                DateTime.Now
            );

            if (result != -1)
            {
                _UpdateUIOnReleaseSuccess(applicationID);
                OnReleaseDetainedLicense?.Invoke();

                MessageBox.Show("License Released Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to Release License. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // دالة تحديث كائنات الشاشة بعد النجاح
        private void _UpdateUIOnReleaseSuccess(int applicationID)
        {
            lblApplicationID.Text = applicationID.ToString();
            btnRelease.Enabled = false;
            ctrlFilterIicenseCard1.Enabled = false; // اختيار تحسيني لمنع البحث عن رخصة جديدة دون إغلاق/إعادة فتح الشاشة
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _ReleaseDetainedLicense();
        }
    }
}

