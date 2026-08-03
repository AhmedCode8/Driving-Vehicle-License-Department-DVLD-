using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.License_Class;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Local_Driving_License_Application
{
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        private clsLocalDrivingLicenseApplicationDTO _InfoLocalDrivingLicenseApplication;
        private clsApplicationDTO _InfoApplication;
        private int _PassedTestCount;

        public frmIssueDriverLicenseForTheFirstTime()
        {
            InitializeComponent();
        }

        public frmIssueDriverLicenseForTheFirstTime(clsApplicationDTO infoApplication,
            clsLocalDrivingLicenseApplicationDTO infoLocalDrivingLicenseApplication, int PassedTestCount)
        {
            InitializeComponent();
            _InfoApplication = infoApplication;
            _InfoLocalDrivingLicenseApplication = infoLocalDrivingLicenseApplication;
            _PassedTestCount = PassedTestCount;
        }

        private void frmIssueDriverLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            ctrlApplicationInInfoCard.LoadData(_InfoApplication, _InfoLocalDrivingLicenseApplication, _PassedTestCount);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the driving license for this application?",
                        "Confirm Issue", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return; // Exit if the user clicks No
            }

            // ==========================================
            // 1. الترقية: فحص وجود السائق أو إنشائه
            // ==========================================
            clsDriverDTO driver = clsDrivers.GetDriverByPersonID(_InfoApplication.ApplicantPersonID);
            int driverID = -1;

            if (driver == null)
            {
                // إنشاء سائق جديد لأول مرة
                clsDriverDTO newDriver = new clsDriverDTO
                {
                    PersonID = _InfoApplication.ApplicantPersonID,
                    CreatedByUserID = clsGlobal.CurrentUser.UserID, // أو _InfoApplication.CreatedByUserID
                    CreatedDate = DateTime.Now
                };

                driverID = clsDrivers.AddNewDriver(newDriver);
            }
            else
            {
                // السائق موجود مسبقاً
                driverID = driver.DriverID;
            }

            if (driverID == -1)
            {
                MessageBox.Show("Failed to create or retrieve Driver record!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ==========================================
            // 2. إنشاء بيانات الرخصة الجديدة
            // ==========================================
            clsLicenseDTO newLicense = new clsLicenseDTO
            {
                ApplicationID = _InfoApplication.ApplicationID,
                DriverID = driverID,
                LicenseClass = _InfoLocalDrivingLicenseApplication.LicenseClassID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(10), // الصلاحية الافتراضية 10 سنوات
                Notes = txtNotes.Text.Trim(),
                PaidFees = _InfoApplication.PaidFees,
                IsActive = true,
                IssueReason = 1, // First Time (أول مرة)
                CreatedByUserID = clsGlobal.CurrentUser.UserID // أو _InfoApplication.CreatedByUserID
            };

            int newLicenseID = clsLicenses.AddNewLicense(newLicense);

            if (newLicenseID == -1)
            {
                MessageBox.Show("Failed to issue the driving license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ==========================================
            // 3. إغلاق الطلب وتغيير حالته إلى Completed
            // ==========================================
            int isStatusUpdated = clsApplications.UpdateApplicationStatus(
                _InfoLocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, 3, DateTime.Now);

            if (isStatusUpdated != 0)
            {
                // 4. Success Message
                MessageBox.Show($"🎉 Congratulations! The license has been issued successfully with License ID = {newLicenseID} and the application is now Completed!",
                                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. Updating UI controls for keeping the data safe
                btnIssue.Enabled = false;
                txtNotes.Enabled = false;
            }
            else
            {
                // 5. Failure Message
                MessageBox.Show("Failed to update application status. Please try again or check system logs.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}