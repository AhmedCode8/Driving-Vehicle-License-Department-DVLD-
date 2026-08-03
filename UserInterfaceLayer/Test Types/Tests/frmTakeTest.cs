using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.Test_Types;
using DVDL_Logic_layer.tests;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Test_Types.Tests
{
    public partial class frmTakeTest : Form
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private int _AppointmentID = -1;
        private clsTestAppointment _TestAppointment;
        private clsTest _Test;

        // المشيد الافتراضي
        public frmTakeTest()
        {
            InitializeComponent();
        }

        // المشيد الرئيسي - يستقبل رقم الموعد + نوع الفحص
        public frmTakeTest(int AppointmentID, clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _TestTypeID = TestTypeID;
        }

        // 🎯 تغيير عنوان الشاشة وأيقونتها بناءً على نوع الفحص
        private void _SetFormTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    groupBox1.Text = "Vision Test";
                    lblTitle.Text = "Take Vision Test";
                    pbTestTypeIcon.Image = Properties.Resources.Vision_512;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    groupBox1.Text = "Written Test";
                    lblTitle.Text = "Take Written Test";
                    pbTestTypeIcon.Image = Properties.Resources.Written_Test_512;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    groupBox1.Text = "Street Test";
                    lblTitle.Text = "Take Street Test";
                    pbTestTypeIcon.Image = Properties.Resources.Street_Test_32;
                    break;
            }
        }

        // 📥 تحميل بيانات الموعد وتعبئة الكنترولات بها
        private void _LoadAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_AppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No appointment found with ID = " + _AppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }



            // Fill UI controls with loaded data from appointment
            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            // lblLicenseClass.Text = _TestAppointment.GitLicenseClassName(_TestAppointment.LocalDrivingLicenseApplication.LicenseClassID);
            //lblApplicantName.Text = _TestAppointment.GitFullName(_TestAppointment.Application.ApplicantPersonID);
            lblTrialCount.Text = clsTest.GetFailedTestsCount(_TestAppointment.LocalDrivingLicenseApplicationID, (int)_TestTypeID).ToString();
            lblTestDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblTestFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = "Not Taken Yet";
        }

        // 📤 سحب البيانات من عناصر التحكم وتعبئة كائن clsTest
        private void _FillTestObjectFromUI()
        {
            _Test = new clsTest();
            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _SetFormTitle();
            _LoadAppointmentData();
        }

        // 💾 زر الحفظ (Save Event)
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation: Check if test result is selected
            if (!rbPass.Checked && !rbFail.Checked)
            {
                MessageBox.Show("Please select the test result (Pass or Fail)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirming save action with the user
            if (MessageBox.Show("Are you sure you want to save this test result? You cannot edit it later!", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // Map UI inputs to business object
            _FillTestObjectFromUI();

            // Save test result to database
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update UI after successful save
                lblTestID.Text = _Test.TestID.ToString();

                // Lock controls since updating is not allowed
                btnSave.Enabled = false;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                txtNotes.Enabled = false;
                clsTestAppointment.LockAppointment(_AppointmentID);// اختبر هذا السنتكس
            }
            else
            {
                MessageBox.Show("Error: Data was not saved successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _SetFormTitle();
            _LoadAppointmentData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}