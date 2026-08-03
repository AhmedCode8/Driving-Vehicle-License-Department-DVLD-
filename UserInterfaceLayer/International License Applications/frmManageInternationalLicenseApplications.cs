using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.License_Class;
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Data;
using System.Windows.Forms;
using UserInterfaceLayer.License;
using UserInterfaceLayer.People;


namespace UserInterfaceLayer.International_License_Applications
{
    public partial class frmManageInternationalLicenseApplications : Form
    {
        public frmManageInternationalLicenseApplications()
        {
            InitializeComponent();
        }
        DataTable _dtSource;
        private void _RefreshList()
        {
            // جلب البيانات وتخزينها في المتغير العام أولاً
            _dtSource = clsInternationalLicense.GetAllInternationalLicenses();

            // ربط الـ Grid بالمتغير العام مباشرة لتتضح الرؤية
            dgvInternationalLicenses.DataSource = _dtSource;
            lblRecordCount.Text = _dtSource.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually

            dgvInternationalLicenses.AllowUserToAddRows = false;
            dgvInternationalLicenses.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvInternationalLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInternationalLicenses.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvInternationalLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvInternationalLicenses.ReadOnly = true;
        }
        public static clsLicenseDTO ConvertToLicenseDTO(clsInternationalLicenseDTO intLicense)
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
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvInternationalLicenses.CurrentRow == null) return;

            int intLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells["Int.License ID"].Value;
            clsInternationalLicenseDTO intLicenseInfo = clsInternationalLicense.GetInternationalLicenseByID(intLicenseID);

            if (intLicenseInfo != null)
            {
                // 1. تحويل الكائن إلى clsLicenseDTO
                clsLicenseDTO licenseInfo = ConvertToLicenseDTO(intLicenseInfo);

                frmLicenseInfo frm = new frmLicenseInfo(licenseInfo);
                frm.ShowDialog();
            }
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvInternationalLicenses.CurrentRow == null) return;

            //  string nationalNo = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["National No."].Value.ToString();
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells["DriverID"].Value;
            clsDriverDTO info = clsDrivers.GetDriverByID(DriverID);
            clsPersonDTO iefoPerson = clsPerson.GetPersonById(info.PersonID);

            frmPersonDetails personDetailsForm = new frmPersonDetails(iefoPerson);
            personDetailsForm.ShowDialog();

        }

        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {
            // Guard Clause: If grid is empty, do nothing
            if (dgvInternationalLicenses.CurrentRow == null) return;

            //  string nationalNo = dgvLocalDrivingLicenseApplications.CurrentRow.Cells["National No."].Value.ToString();
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells["DriverID"].Value;
            clsDriverDTO info = clsDrivers.GetDriverByID(DriverID);

            frmLicenseHistory frmLicenseHistory = new frmLicenseHistory(info.PersonID);
            frmLicenseHistory.ShowDialog();
        }

        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _LayoutDataGridView();
            _RefreshList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
