using DVDL_Logic_layer.Application_Types;
using System;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.Application_Type
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        private DataTable _dtSource;

        private void _RefreshPersonList()
        {
            _dtSource = clsApplicationTypes.GetAllApplicationTypes();

            if (_dtSource == null)
            {
                MessageBox.Show("error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvApplicationTypes.DataSource = _dtSource;
            lblRecordsCount.Text = _dtSource.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually
            dgvApplicationTypes.AllowUserToAddRows = false;
            dgvApplicationTypes.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvApplicationTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvApplicationTypes.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvApplicationTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvApplicationTypes.ReadOnly = true;
        }

        private void ssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int applicationTypeID = (int)dgvApplicationTypes.CurrentRow.Cells["ApplicationTypeID"].Value;

            frmUpdateApplicationType frm = new frmUpdateApplicationType(applicationTypeID);
            frm.ApplicationTypeUpdated += _RefreshPersonList;
            frm.ShowDialog();

        }



        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshPersonList();
            _LayoutDataGridView();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
