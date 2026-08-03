using DVDL_Logic_layer.License_Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlLicenseHistoryCard : UserControl
    {
        public int _DriverID = -1;
        DataTable _dtSourceLicenses;
        DataTable _dtSourceInternationalLicense;
        public ctrlLicenseHistoryCard()
        {
            InitializeComponent();
        }



        public void LoadData(int driverID)
        {
            _DriverID = driverID;
            _RefreshList();
        }

        private void _RefreshList()
        {
            _dtSourceLicenses = clsLicenses.GetLicensesByDriverID(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtSourceLicenses;
            lblRecordCount.Text = _dtSourceLicenses.Rows.Count.ToString();

            _dtSourceInternationalLicense = clsInternationalLicense.GetInternationalLicensesByDriverID(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtSourceInternationalLicense;
            lblRecordCounInterntional.Text = _dtSourceInternationalLicense.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually
            dgvLocalLicensesHistory.AllowUserToAddRows = false;
            dgvLocalLicensesHistory.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvLocalLicensesHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLocalLicensesHistory.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvLocalLicensesHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvLocalLicensesHistory.ReadOnly = true;
        }

        private void ctrlLicenseHistoryCard_Load(object sender, EventArgs e)
        {
            _LayoutDataGridView();

        }
    }
}
