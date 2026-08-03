using DVDL_Logic_layer.License_Class;
using DVLD_DTOs;
using System.Windows.Forms;


namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlLicenseApplicationInInfoCard : UserControl
    {
        public ctrlLicenseApplicationInInfoCard()
        {
            InitializeComponent();
        }
        public clsApplicationDTO infoApplication;

        public void LoadData(clsApplicationDTO infoApplication, clsLocalDrivingLicenseApplicationDTO infoLocalDrivingLicenseApplication, int _PassedTestCount = 0)
        {
            clsLicenseClassDTO info = clsLicenseClass.GetLicenseClassByID(infoLocalDrivingLicenseApplication.LicenseClassID);
            // Guard Clause: التأكد من أن الكائنات ليست فارغة لمنع الـ NullReferenceException
            if (infoApplication == null || infoLocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: Cannot load details, data is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1. تعبئة الجزء الخاص ببيانات الطلب المحلي (Driving License Application Info)
            lblLocalDrivingLicenseAppID.Text = infoLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            // ملاحظة: لو كان الـ DTO لديك يحتوي على اسم الفئة النصي وعدد الاختبارات مباشرة استخدمهما، وإلا مرر الـ ID أو دالة الجلب
            lblAppliedForLicenseClass.Text = info.ClassName;
            lblPassedTestsCount.Text = $"{_PassedTestCount}/3"; // يمكنك استبدالها بدالة جلب عدد الاختبارات الناجحة لاحقاً

            // 2. تعبئة الجزء الخاص بالبيانات الأساسية للطلب (Application Basic Info)
            lblApplicationID.Text = infoApplication.ApplicationID.ToString();
            lblApplicationFees.Text = infoApplication.PaidFees.ToString("0.00");
            lblApplicationDate.Text = infoApplication.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = infoApplication.LastStatusDate?.ToShortDateString() ?? "N/A";
            // إذا كان الـ DTO يحتوي على المعرفات الرقمية فقط (IDs)، يمكنك عرضها مباشرة أو استبدالها بأسماء نصية عبر دوال الجلب لديك
            lblCreatedByUserName.Text = infoApplication.CreatedByUserID.ToString();
            lblApplicantFullName.Text = infoApplication.ApplicantPersonID.ToString();
            lblApplicationType.Text = infoApplication.ApplicationTypeID.ToString();

            // 3. تحويل حالة الطلب الرقمية إلى نص مفهوم للمستخدم
            switch (infoApplication.ApplicationStatus)
            {
                case 1:
                    lblApplicationStatus.Text = "New";
                    break;
                case 2:
                    lblApplicationStatus.Text = "Cancelled";
                    break;
                case 3:
                    lblApplicationStatus.Text = "Completed";
                    break;
                default:
                    lblApplicationStatus.Text = "Unknown";
                    break;
            }
        }
    }
}
