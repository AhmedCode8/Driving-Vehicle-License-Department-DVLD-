using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.Local_Driving_License_Application;
using DVDL_Logic_layer.Test_Types;
using DVDL_Logic_layer.tests;
using DVLD_DTOs;
using System;
using System.Data;

using System.Windows.Forms;

namespace UserInterfaceLayer.Test_Types.Tests
{
    public partial class frmListTestAppointments : Form
    {
        public frmListTestAppointments()
        {
            InitializeComponent();
        }
        public frmListTestAppointments(int LDLAppID, clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;

            _AppDTO = clsApplications.GetApplicationByLocalDrivingLicenseAppID(LDLAppID);
            if (_AppDTO == null)
            {
                MessageBox.Show("_AppDTO =null");
            }
            _LocalAppDTO = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LDLAppID);
            if (_LocalAppDTO == null)
            {
                MessageBox.Show("_LocalAppDTO =null");
            }
        }

        private clsTestTypes.enTestType _TestTypeID;
        DataTable dtSouces;
        clsApplicationDTO _AppDTO;
        clsLocalDrivingLicenseApplicationDTO _LocalAppDTO;


        private void _LoadTestTypeHeaderInfo()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Vision_512;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Written_Test_512;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    pbTestTypeImage.Image = Properties.Resources.Street_Test_32;
                    break;
            }
        }
        private void _RefreshAppointmentsList()
        {
            // جلب جدول المواعيد الخاص بهذا الطلب ونوع الفحص
            dtSouces = clsTestAppointment.GetTestAppointmentsByApplicationIDAndTestType
               (_LocalAppDTO.LocalDrivingLicenseApplicationID, (int)_TestTypeID);

            dgvTestAppointments.DataSource = dtSouces;

            // 🎯 إعادة تسمية وتنظيم أعمدة الشبكة بشكل احترافي
            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns["TestAppointmentID"].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns["TestAppointmentID"].Width = 120;

                dgvTestAppointments.Columns["AppointmentDate"].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns["AppointmentDate"].Width = 160;

                dgvTestAppointments.Columns["PaidFees"].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns["PaidFees"].Width = 120;

                dgvTestAppointments.Columns["IsLocked"].HeaderText = "Is Locked";
                dgvTestAppointments.Columns["IsLocked"].Width = 100;
            }
            dgvTestAppointments.AllowUserToAddRows = false;
            dgvTestAppointments.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvTestAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestAppointments.MultiSelect = false;
            dgvTestAppointments.ReadOnly = true;

        }
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            int passedTestCount = clsLocalDrivingLicenseApplication.GetPassedTestCount(_LocalAppDTO.LocalDrivingLicenseApplicationID);
            ctrlApplicationInInfoCard.LoadData(_AppDTO, _LocalAppDTO, passedTestCount);
            _LoadTestTypeHeaderInfo();
            _RefreshAppointmentsList();
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            // 🛡️ حماية حتمية: التأكد من وجود بيانات الطلب ونوع الفحص
            if (_LocalAppDTO == null) return;

            // 🎯 1. الحماية الأولى: هل اجتاز المتقدم هذا الفحص سابقاً بنجاح؟
            // (فحص النجاح أولاً لتوفير استعلامات قاعدة البيانات وإيقاف العملية فوراً)
            if (clsTest.DoesPassTestType(_LocalAppDTO.LocalDrivingLicenseApplicationID, (int)_TestTypeID))
            {
                MessageBox.Show("This person has already passed this test successfully. You cannot schedule another appointment.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // منع العملية مباشرة
            }

            // 🎯 2. جلب آخر موعد ثبت لهذا الطلب ونوع الفحص من قاعدة البيانات
            clsTestAppointment lastAppointment = clsTestAppointment.GetLastTestAppointmentByApplicationIDAndTestType
            (
                _LocalAppDTO.LocalDrivingLicenseApplicationID,
                (int)_TestTypeID
            );

            // 🎯 3. الحماية الثانية: هل يوجد موعد فعال حالياً ولم يُقفل بعد؟ (IsLocked == false)
            if (lastAppointment != null && !lastAppointment.IsLocked)
            {
                MessageBox.Show("This person already has an effective appointment for this screening. You cannot add another appointment.",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // منع الإضافة
            }

            // 🎯 4. فتح شاشة حجز الموعد
            // ملاحظة: إذا كان (lastAppointment != null) فهذا يعني أن المتقدم أجرى الاختبار ورسب،
            // والشاشة ستتعامل مع الحالة كـ Retake Test تلقائياً.
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_TestTypeID, _AppDTO, _LocalAppDTO);
            frmScheduleTest.ShowDialog();

            // 🎯 5. Refreshing DataGridView لتحديث قائمة المواعيد فور الخروج
            _RefreshAppointmentsList();


        }
        private void tsmiEditAppointment_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.CurrentRow == null) return;

            int appointmentID = (int)dgvTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value;

            // Open form in Edit Mode using the overloaded constructor
            frmScheduleTest frm = new frmScheduleTest(_TestTypeID, _AppDTO, _LocalAppDTO, appointmentID);
            frm.ShowDialog();

            // Refresh list after edit
            _RefreshAppointmentsList();
        }
        private void tsmiTakeTest_Click(object sender, EventArgs e)
        {
            int appointmentID = (int)dgvTestAppointments.CurrentRow.Cells["TestAppointmentID"].Value;

            frmTakeTest frmTakeTest = new frmTakeTest(appointmentID, _TestTypeID);
            frmTakeTest.ShowDialog();
            _RefreshAppointmentsList();

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}
