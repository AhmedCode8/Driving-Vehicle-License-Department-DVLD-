//using DVDL_Logic_layer.Global_Classes;
//using DVDL_Logic_layer.Test_Types;
//using DVDL_Logic_layer.tests;
//using DVLD_DTOs;
//using System;
//using System.Windows.Forms;

//namespace UserInterfaceLayer.Test_Types.Tests
//{
//    public partial class frmScheduleTest : Form
//    {
//        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

//        clsApplicationDTO _AppDTO;
//        clsLocalDrivingLicenseApplicationDTO _LocalAppDTO;
//        clsTestAppointmentDTO TAppointmentDTO;
//        clsTestAppointment _TestAppointment = new clsTestAppointment();


//        clsTestTypeDTO TTypeDTO;
//        public frmScheduleTest()
//        {
//            InitializeComponent();
//        }

//        public frmScheduleTest(clsTestTypes.enTestType TestTypeID, clsApplicationDTO AppDTO, clsLocalDrivingLicenseApplicationDTO LocalAppDTO)
//        {
//            InitializeComponent();
//            _TestTypeID = TestTypeID;
//            _AppDTO = AppDTO;
//            _LocalAppDTO = LocalAppDTO;
//        }
//        private void _LoadTestTypeHeaderInfo()
//        {
//            switch (_TestTypeID)
//            {
//                case clsTestTypes.enTestType.VisionTest:
//                    groupBox1.Text = "Vision Test";
//                    lblTitle.Text = "Schedule Vision Test";
//                    pictureBox2.Image = Properties.Resources.Vision_512;
//                    this.Text = "Schedule Vision Test";
//                    break;

//                case clsTestTypes.enTestType.WrittenTest:
//                    groupBox1.Text = "Written Test";
//                    lblTitle.Text = "Schedule Written Test";
//                    pictureBox2.Image = Properties.Resources.Written_Test_512;
//                    this.Text = "Schedule Written Test";
//                    break;

//                case clsTestTypes.enTestType.StreetTest:
//                    groupBox1.Text = "Street Test";
//                    lblTitle.Text = "Schedule Street Test";
//                    pictureBox2.Image = Properties.Resources.Street_Test_32;
//                    this.Text = "Schedule Street Test";
//                    break;
//            }
//        }
//        private void btmClose_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }
//        private void frmScheduleTest_Load(object sender, EventArgs e)
//        {
//            // 1. تحديث شكل وتفاصيل الهيدر
//            _LoadTestTypeHeaderInfo();
//            _FillAppointmentObjectFromUI();
//            _LodeDataIntoTheUIElements();
//        }
//        private void _LodeDataIntoTheUIElements()
//        {
//            lblLocalDrivingLicenseAppID.Text = _TestAppointment.TestAppointmentID.ToString();
//            lblLicenseClass.Text = _TestAppointment.GitLicenseClassName(_LocalAppDTO.LicenseClassID);
//            lblApplicantName.Text = _TestAppointment.GitFullName(_AppDTO.ApplicantPersonID);
//            lblTrialCount.Text = clsTest.GetFailedTestsCount(_LocalAppDTO.LocalDrivingLicenseApplicationID, _LocalAppDTO.LicenseClassID).ToString();
//            lblTestFees.Text = TTypeDTO.TestTypeFees.ToString();


//        }

//        private void _FillAppointmentObjectFromUI()
//        {
//            TTypeDTO = clsTestTypes.GetTestTypeByID(1);

//            _TestAppointment.TestTypeID = TTypeDTO.TestTypeID;
//            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalAppDTO.LocalDrivingLicenseApplicationID;
//            _TestAppointment.AppointmentDate = dtpAppointmentDate.Value;
//            _TestAppointment.PaidFees = TTypeDTO.TestTypeFees;
//            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
//            _TestAppointment.IsLocked = false;



//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (dtpAppointmentDate.Value < DateTime.Now.Date)
//            {
//                MessageBox.Show("You cannot select a date in the past!", "Wrong Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }
//            _FillAppointmentObjectFromUI();
//            if (_TestAppointment.Save())
//            {
//                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                lblTitle.Text = "Edit Test Appointment";
//            }
//            else
//            {
//                MessageBox.Show("Failed to Save Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }

//        private void lblTrialCount_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}

////TAppointmentDTO.TestTypeID =   ;
////TAppointmentDTO.LocalDrivingLicenseApplicationID =    ;
////TAppointmentDTO.AppointmentDate =  ;
////TAppointmentDTO.PaidFees =     ;
////TAppointmentDTO.CreatedByUserID =   ;
////TAppointmentDTO.IsLocked =    ;



//// عليك ان تكمل عمل هذا الكائن كشروط وحمايه الخ لقد نجحت مبدئن في توصيله الى قواعد البيانات
//// لديك مشلكه في حفض التارخ فهو يحفض التاريخ فقط الذي يحمل في شاشه التحمل اي انه لا يقوم باستقبال تاريخك المختار 
////حل المشكله علما اضن هو تغير مكان او وضع في كلا المانين لداله LodesDataIntotheclsTestAppointment


//// بعد ان تكمل عمل ميزه الاضافه اريد منك ان تتجعل الى هذه الميزه هو انشاء الشبكه تحتتوي الشبكه على قم الموعد، تاريخ الموعد، الرسوم المدفوعة، وحالة الإقفال
//// وبعد هذا انتق الى زر التعيدل و تقديم الفحص
//// ملاحضه زر التعديل يعمل مباشهر مع هذا الاكلاس فان كان الديك التدقف باكمله اعمل مع ا
///
/// لاضافه ولا ترمي لغير وفت ...
/// 


using DVDL_Logic_layer.Application_Types;
using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.Test_Types;
using DVDL_Logic_layer.tests;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Test_Types.Tests
{
    public partial class frmScheduleTest : Form
    {
        // Enum to define mode state
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private int _TestAppointmentID = -1;

        private clsApplicationDTO _AppDTO;
        private clsLocalDrivingLicenseApplicationDTO _LocalAppDTO;
        private clsTestAppointment _TestAppointment;
        private clsTestTypeDTO _TTypeDTO;

        // Constructor for Add New Mode
        public frmScheduleTest(clsTestTypes.enTestType TestTypeID, clsApplicationDTO AppDTO, clsLocalDrivingLicenseApplicationDTO LocalAppDTO)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _AppDTO = AppDTO;
            _LocalAppDTO = LocalAppDTO;
            _TestAppointmentID = -1;
            _Mode = enMode.AddNew;
        }

        // Overloaded Constructor for Edit/Update Mode
        public frmScheduleTest(clsTestTypes.enTestType TestTypeID, clsApplicationDTO AppDTO, clsLocalDrivingLicenseApplicationDTO LocalAppDTO, int TestAppointmentID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _AppDTO = AppDTO;
            _LocalAppDTO = LocalAppDTO;
            _TestAppointmentID = TestAppointmentID;
            _Mode = enMode.Update;
        }

        // Load test header title and image based on test type
        private void _LoadTestTypeHeaderInfo()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    groupBox1.Text = "Vision Test";
                    lblTitle.Text = (_Mode == enMode.AddNew) ? "Schedule Vision Test" : "Edit Vision Test";
                    pictureBox2.Image = Properties.Resources.Vision_512;
                    this.Text = "Schedule Vision Test";
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    groupBox1.Text = "Written Test";
                    lblTitle.Text = (_Mode == enMode.AddNew) ? "Schedule Written Test" : "Edit Written Test";
                    pictureBox2.Image = Properties.Resources.Written_Test_512;
                    this.Text = "Schedule Written Test";
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    groupBox1.Text = "Street Test";
                    lblTitle.Text = (_Mode == enMode.AddNew) ? "Schedule Street Test" : "Edit Street Test";
                    pictureBox2.Image = Properties.Resources.Street_Test_32;
                    this.Text = "Schedule Street Test";
                    break;
            }
        }

        // Load test appointment data based on Mode
        private void _LoadAppointmentData()
        {
            // Get test type fees and info dynamically
            _TTypeDTO = clsTestTypes.GetTestTypeByID((int)_TestTypeID);

            if (_TTypeDTO == null)
            {
                MessageBox.Show("Error: Could not load test type information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                dtpAppointmentDate.MinDate = DateTime.Now;
            }
            else
            {
                // Find existing appointment for update
                _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

                if (_TestAppointment == null)
                {
                    MessageBox.Show("Error: Test appointment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Protection: Check if appointment is locked
                if (_TestAppointment.IsLocked)
                {
                    MessageBox.Show("This appointment is locked. You cannot edit it!", "Locked Appointment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = false;
                    dtpAppointmentDate.Enabled = false;
                }
            }
        }

        // Fill UI elements with current data
        private void _LoadDataIntoUIElements()
        {
            int trialCount = clsTest.GetFailedTestsCount(_LocalAppDTO.LocalDrivingLicenseApplicationID, (int)_TestTypeID);
            clsApplicationTypeDTO info = clsApplicationTypes.GetApplicationTypeByID(7);

            lblLocalDrivingLicenseAppID.Text = _LocalAppDTO.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = _TestAppointment.GitLicenseClassName(_LocalAppDTO.LicenseClassID);
            lblApplicantName.Text = _TestAppointment.GitFullName(_AppDTO.ApplicantPersonID);
            lblTrialCount.Text = trialCount.ToString();
            lblTestFees.Text = _TTypeDTO.TestTypeFees.ToString();
            if (trialCount > 0)
            {
                lblRetakeAppFees.Text = info.ApplicationFees.ToString();
                lblTotalFees.Text = (info.ApplicationFees + _TTypeDTO.TestTypeFees).ToString();
            }


            // Set date control value in Update mode
            if (_Mode == enMode.Update)
            {
                dtpAppointmentDate.Value = _TestAppointment.AppointmentDate;
            }
        }
        // Read UI inputs into object before saving
        private void _FillAppointmentObjectFromUI()
        {
            _TestAppointment.TestTypeID = (int)_TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalAppDTO.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpAppointmentDate.Value;
            _TestAppointment.PaidFees = _TTypeDTO.TestTypeFees;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            _LoadTestTypeHeaderInfo();
            _LoadAppointmentData();
            _LoadDataIntoUIElements();
        }
        // Save button click handler
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Date validation for new appointments
            if (_Mode == enMode.AddNew && dtpAppointmentDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("You cannot select a date in the past!", "Wrong Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Map UI values to logic object
            _FillAppointmentObjectFromUI();

            // Save object to database
            if (_TestAppointment.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update; // Change mode to update after successful insert
                lblTitle.Text = "Edit Test Appointment";
            }
            else
            {
                MessageBox.Show("Failed to Save Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}