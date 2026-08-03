using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.License_Class;
using DVDL_Logic_layer.Local_Driving_License_Application;
using DVDL_Logic_layer.Test_Types;
using DVLD_DTOs;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using UserInterfaceLayer.License;
using UserInterfaceLayer.Test_Types.Tests;
namespace UserInterfaceLayer.Local_Driving_License_Application
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {

        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        #region Functions and auxiliary elements
        private void _RefreshList()
        {
            // جلب البيانات وتخزينها في المتغير العام أولاً
            _dtSource = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            // ربط الـ Grid بالمتغير العام مباشرة لتتضح الرؤية
            dgvLocalDrivingLicenseApplications.DataSource = _dtSource;
            lblRecordCount.Text = _dtSource.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually
            dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;
            dgvLocalDrivingLicenseApplications.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvLocalDrivingLicenseApplications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalDrivingLicenseApplications.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvLocalDrivingLicenseApplications.ReadOnly = true;
        }
        private void frmManageLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _RefreshList();
            _LayoutDataGridView();
        }

        DataTable _dtSource;
        private clsTestTypes.enTestType _TestTypeID;

        #endregion
        private void btnAddNewLocalApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            frmNewLocalDrivingLicenseApplication.ShowDialog();
        }
        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {

            // 1. Protection: Check if CurrentRow is selected
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            // 2. Get localAppID from DataGridView (Make sure to use column Name or Index)
            int localAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value);

            // 3. First step: Get Local Application DTO
            clsLocalDrivingLicenseApplicationDTO localAppDTO = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(localAppID);

            if (localAppDTO == null)
            {
                MessageBox.Show("Error: Could not retrieve Local Application data from database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Second step: Get Base Application DTO by using ApplicationID from localAppDTO
            clsApplicationDTO appDTO = clsApplications.GetApplicationByID(localAppDTO.ApplicationID);

            if (appDTO == null)
            {
                MessageBox.Show("Error: Could not retrieve Base Application data from database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5. Get passed tests count
            int passedTestCount = clsLocalDrivingLicenseApplication.GetPassedTestCount(localAppID);

            // 6. Pass data to the form and display it
            frmLocalDrivingLicenseApplicationInfo frmLocal = new frmLocalDrivingLicenseApplicationInfo(appDTO, localAppDTO, passedTestCount);
            frmLocal.ShowDialog();
        }
        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;

            frmListTestAppointments frmVisionTestAppointmentscs = new frmListTestAppointments(LDLAppID, clsTestTypes.enTestType.VisionTest);
            frmVisionTestAppointmentscs.ShowDialog();
            _RefreshList();

        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;

            frmListTestAppointments frmVisionTestAppointmentscs = new frmListTestAppointments(LDLAppID, clsTestTypes.enTestType.WrittenTest);
            frmVisionTestAppointmentscs.ShowDialog();
            _RefreshList();


        }
        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;

            frmListTestAppointments frmVisionTestAppointmentscs = new frmListTestAppointments(LDLAppID, clsTestTypes.enTestType.StreetTest);
            frmVisionTestAppointmentscs.ShowDialog();
            _RefreshList();

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // 1. Protection: Check if CurrentRow is selected
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            // 2. Get localAppID from DataGridView (Make sure to use column Name or Index)
            int localAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value);

            // 3. First step: Get Local Application DTO
            clsLocalDrivingLicenseApplicationDTO localAppDTO = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(localAppID);

            if (localAppDTO == null)
            {
                MessageBox.Show("Error: Could not retrieve Local Application data from database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Second step: Get Base Application DTO by using ApplicationID from localAppDTO
            clsApplicationDTO appDTO = clsApplications.GetApplicationByID(localAppDTO.ApplicationID);

            if (appDTO == null)
            {
                MessageBox.Show("Error: Could not retrieve Base Application data from database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5. Get passed tests count
            int passedTestCount = clsLocalDrivingLicenseApplication.GetPassedTestCount(localAppID);
            frmIssueDriverLicenseForTheFirstTime frmIssueDriverLicenseForTheFirstTime =
                new frmIssueDriverLicenseForTheFirstTime(appDTO, localAppDTO, passedTestCount);
            frmIssueDriverLicenseForTheFirstTime.ShowDialog();
            _RefreshList();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {


            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;
            clsApplicationDTO Application = clsApplications.GetApplicationByLocalDrivingLicenseAppID(LDLAppID);

            clsLicenseDTO license = clsLicenses.GetLicenseByApplicationID(Application.ApplicationID);

            if (license != null)
            {
                frmLicenseInfo frm = new frmLicenseInfo(license);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!0000", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {

            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;
            clsApplicationDTO Application = clsApplications.GetApplicationByLocalDrivingLicenseAppID(LDLAppID);

            int personID = Application.ApplicantPersonID;

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(personID);
            frmLicenseHistory.ShowDialog();
        }
        //Functions: Cancel the Applications
        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            // 1. Guard Clause: إلغاء فتح القائمة إذا كان الجدول فارغاً
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
            {
                e.Cancel = true;
                return;
            }

            // 2. قراءة البيانات من الصف المحدد
            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;
            int passedTestCount = clsLocalDrivingLicenseApplication.GetPassedTestCount(LDLAppID);
            string status = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["Status"].Value.ToString();

            bool isNew = (status == "New");
            bool passedAllTests = (passedTestCount == 3); // 🎯 هل تخطى الاختبارات الثلاثة؟

            // 3. منع التعديل والحذف والإلغاء إذا تخطى 3 اختبارات أو كانت الحالة غير New
            tsmiEditApplication.Enabled = isNew && (passedTestCount == 0); // من الفضل منع التعديل بعد بدء الفحوصات
            tsmiDeleteApplication.Enabled = isNew && !passedAllTests;      // ❌ يمنع الحذف إذا اجتاز 3 اختبارات
            tsmiCancelApplication.Enabled = isNew && !passedAllTests;      // ❌ يمنع الإلغاء إذا اجتاز 3 اختبارات

            // 4. خيار إصدار رخصة القيادة لأول مرة
            // يفعل فقط إذا كان الطلب جديداً وأكمل الفحوصات الثلاثة
            tsmiIssueDrivingLicense.Enabled = isNew && passedAllTests;

            // 5. ضبط خيارات جدولة الاختبارات (Schedule Tests)
            bool canScheduleTests = isNew && !passedAllTests;
            tsmiScheduleTests.Enabled = canScheduleTests;

            if (canScheduleTests)
            {
                tsmiScheduleVisionTest.Enabled = (passedTestCount == 0);
                tsmiScheduleWrittenTest.Enabled = (passedTestCount == 1);
                tsmiScheduleStreetTest.Enabled = (passedTestCount == 2);
            }
            else
            {
                // تعطيل جميع خيارات الاختبارات إذا كان الطلب ملغى، مكتمل، أو اجتاز الـ 3 فحوصات
                tsmiScheduleVisionTest.Enabled = false;
                tsmiScheduleWrittenTest.Enabled = false;
                tsmiScheduleStreetTest.Enabled = false;
            }
        }
        private void tsmiCancelApplication_Click(object sender, EventArgs e)
        {


            // Guard Clause: Ensure a row is actually selected
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int localAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value);

            // 2 = Cancelled status
            if (clsApplications.UpdateApplicationStatus(localAppID, 2, DateTime.Now) > 0)
            {
                MessageBox.Show("Application cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshList();
                // 💡 Note: Call your grid refresh method here (e.g., _RefreshApplicationsList();)
            }
            else
            {
                MessageBox.Show("Failed to cancel the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Function: Close current form...
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsmiEditApplication_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            //  string nationalNo = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["National No."].Value.ToString();
            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;

            frmNewLocalDrivingLicenseApplication frmNewLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication(LDLAppID);
            frmNewLocalDrivingLicenseApplication.ShowDialog();
            _RefreshList();
        }
        private void tsmiDeleteApplication_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;

            // 1️⃣ خطوة حارسة: جلب بيانات الطلب المحلي لمعرفة الـ ApplicationID المحدد بدقة
            var localAppDTO = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LDLAppID);

            if (localAppDTO == null)
            {
                MessageBox.Show("Error: Application data could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int baseApplicationID = localAppDTO.ApplicationID;

            // 2️⃣ استخدام try-catch لالتقاط استثناءات القيود التي تطلقها طبقة البيانات
            try
            {
                // احذف سجل الابن أولاً من جدول الطلبات المحلية
                int rowsAffectedLocal = clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(LDLAppID);

                if (rowsAffectedLocal > 0)
                {
                    // احذف سجل الأب المحدد بدقة باستخدام الـ ID الخاص به
                    int rowsAffectedBase = clsApplications.DeleteApplication(baseApplicationID);

                    if (rowsAffectedBase > 0)
                    {
                        MessageBox.Show("Application deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshList(); // تحديث الجدول فوراً بعد النجاح
                        return;
                    }
                }

                MessageBox.Show("Deletion was not successful.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // 🛡️ هنا سيتم عرض رسالتك المخصصة (Cannot delete this application...) بشكل لائق للمستخدم
                MessageBox.Show(ex.Message, "Deletion Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




            //// Guard Clause: If grid is empty, do nothing
            //if (dgvLocalDrivingLicenseApplications.CurrentRow == null) return;

            ////  string nationalNo = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["National No."].Value.ToString();
            //int LDLAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["L.D.L.AppID"].Value;
            //string NationalNo = (string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells["National No."].Value;

            //int RowsAffected = clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(LDLAppID);
            //if (RowsAffected > 0)
            //{
            //    // clsApplications.DeleteApplicationByNationalNo(NationalNo);
            //    MessageBox.Show("Deletion was not successful");

            //}
            ////  MessageBox.Show("Deletion was not successful");
        }



    }
}
