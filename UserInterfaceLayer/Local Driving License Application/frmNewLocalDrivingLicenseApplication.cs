using DVDL_Logic_layer.Application_Types;
using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.License_Class;
using DVDL_Logic_layer.Local_Driving_License_Application;
using DVLD_DTOs;
using System;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.Local_Driving_License_Application
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        #region Private Fields

        private const int _LocalDrivingLicenseApplicationTypeID = 1;
        private clsApplicationTypeDTO _applicationType;

        private clsApplications _baseApplication = new clsApplications();
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

        #endregion

        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        public frmNewLocalDrivingLicenseApplication(int LDLAppID)
        {
            InitializeComponent();

            // 1. جلب بيانات الـ DTO أولاً
            clsApplicationDTO appDTO = clsApplications.GetApplicationByLocalDrivingLicenseAppID(LDLAppID);
            clsLocalDrivingLicenseApplicationDTO localAppDTO = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(LDLAppID);

            if (localAppDTO != null)
            {
                // 2. صب البيانات داخل كائن الكلاس العام للشاشة لتجنب خطأ التحويل
                _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID = localAppDTO.LocalDrivingLicenseApplicationID;
                _localDrivingLicenseApplication.ApplicationID = localAppDTO.ApplicationID;
                _localDrivingLicenseApplication.LicenseClassID = localAppDTO.LicenseClassID;
            }
            if (appDTO != null)
            {
                // 🎯 التعديل الجوهري: صب البيانات داخل كائن الكلاس العام ليحفظ الـ ID في الذاكرة
                _baseApplication.ApplicationID = appDTO.ApplicationID;
                _baseApplication.ApplicantPersonID = appDTO.ApplicantPersonID;
                _baseApplication.ApplicationDate = appDTO.ApplicationDate;
                _baseApplication.ApplicationTypeID = appDTO.ApplicationTypeID;
                _baseApplication.ApplicationStatus = appDTO.ApplicationStatus;
                _baseApplication.LastStatusDate = appDTO.LastStatusDate;
                _baseApplication.PaidFees = appDTO.PaidFees;
                _baseApplication.CreatedByUserID = appDTO.CreatedByUserID;

                // تحويل الوضع إلى تعديل ليطلق جملة UPDATE
                _baseApplication.Mode = clsApplications.enMode.Update;
                _localDrivingLicenseApplication.Mode = clsLocalDrivingLicenseApplication.enMode.Update;
            }

            // 2. تعبئة عناصر الواجهة
            //  ctrlPersonFinder.LoadPersonData(-1, NationalNo);
            _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID = LDLAppID;
            ctrlPersonFinder.LoadPersonData(_baseApplication.ApplicantPersonID);
            lblLocalDrivingLicenseApplication.Text = "Update Local Driving License Application";

        }

        #region Form Events

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _InitializeForm();
        }

        #endregion

        #region Initialization & Data Loading

        private void _InitializeForm()
        {
            // Load application type details safely
            _applicationType = clsApplicationTypes.GetApplicationTypeByID(_LocalDrivingLicenseApplicationTypeID);

            if (_applicationType == null)
            {
                MessageBox.Show("Critical Error: Application type metadata could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _FillLicenseClassesComboBox();
            // 2. التعديل: اختيار فئة الرخصة القديمة وتحديث الواجهة لو كنا في وضع التعديل
            if (_baseApplication.Mode == clsApplications.enMode.Update && _localDrivingLicenseApplication != null)
            {
                cbLicenseClasses.SelectedValue = _localDrivingLicenseApplication.LicenseClassID;
                _UpdateApplicationSummaryUI();
            }
        }

        private void _FillLicenseClassesComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();
            cbLicenseClasses.DataSource = dtLicenseClasses;
            cbLicenseClasses.DisplayMember = "ClassName";
            cbLicenseClasses.ValueMember = "LicenseClassID";

        }

        private void _PopulateBaseApplicationObject(int personID)
        {
            // 🌟 تعيين رقم الشخص المرتبط بهذا الطلب
            _baseApplication.ApplicantPersonID = personID;
            _baseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _baseApplication.ApplicationTypeID = _LocalDrivingLicenseApplicationTypeID;
            // 🎯 التعديل: التواريخ والحالة والرسوم يتم تعيينهم فقط عند إنشاء طلب جديد
            if (_baseApplication.Mode == clsApplications.enMode.AddNew)
            {
                _baseApplication.ApplicationDate = DateTime.Now;
                _baseApplication.ApplicationStatus = 1; // 1 = New
                _baseApplication.LastStatusDate = DateTime.Now;
                _baseApplication.PaidFees = _applicationType.ApplicationFees;
            }
        }

        private void _UpdateApplicationSummaryUI()
        {
            lblLocalDrivingLicenseAppID.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationFees.Text = _applicationType.ApplicationFees.ToString();

            lblCreatedBy.Text = clsGlobal.CurrentUser.UserID.ToString();
            lblApplicationDate.Text = _baseApplication.ApplicationDate.ToShortDateString();
        }

        #endregion

        #region Validation Logic

        private bool _ValidateApplicationRequirements(int personID, int licenseClassID)
        {
            if (personID == -1)
            {
                MessageBox.Show("Please select a person first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveLicenseForThisClass(personID, licenseClassID))
            {
                MessageBox.Show("This person already holds an active license for this class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (clsLocalDrivingLicenseApplication.IsThereAnActiveApplication(personID, licenseClassID))
            {
                MessageBox.Show("This person already has a pending or active application for this class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion

        #region Controls Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbLicenseClasses.SelectedValue == null || cbLicenseClasses.SelectedValue is DataRowView)
            {
                MessageBox.Show("Please select a valid license class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedLicenseClassID = Convert.ToInt32(cbLicenseClasses.SelectedValue);
            int currentPersonID = ctrlPersonFinder.PersonID;

            if (!_ValidateApplicationRequirements(currentPersonID, selectedLicenseClassID))
                return;
            if (_baseApplication.Mode == clsApplications.enMode.AddNew)
            {
                _PopulateBaseApplicationObject(currentPersonID);
            }

            // Guard Clause: Save parent application first
            if (!_baseApplication.Save())
            {
                MessageBox.Show("Failed to save the base application details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Link parent application ID and selected class to the sub-application
            _localDrivingLicenseApplication.ApplicationID = _baseApplication.ApplicationID;
            _localDrivingLicenseApplication.LicenseClassID = selectedLicenseClassID;

            if (_localDrivingLicenseApplication.Save())
            {
                MessageBox.Show("Application saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //btnSave.Enabled = false;
                //cbLicenseClasses.Enabled = false;

                lblLocalDrivingLicenseApplication.Text = "Updata Local Driving License Application";

                _UpdateApplicationSummaryUI();
            }
            else
            {
                MessageBox.Show("Failed to save the local driving license application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tcApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcApplication.SelectedIndex == 1)
            {
                // Prevent navigating to summary tab if application is not saved yet
                //if (_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID == -1)
                //{
                //    MessageBox.Show("Please save the application first to unlock the summary tab.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    tcApplication.SelectedIndex = 0;
                //    return;
                //}

                _UpdateApplicationSummaryUI();
            }
        }
        private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLicenseClasses.SelectedValue != null && int.TryParse(cbLicenseClasses.SelectedValue.ToString(), out int licenseClassID))
            {
                _localDrivingLicenseApplication.LicenseClassID = licenseClassID;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}

