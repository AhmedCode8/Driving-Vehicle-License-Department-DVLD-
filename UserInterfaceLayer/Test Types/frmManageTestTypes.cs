using DVDL_Logic_layer.Test_Types;
using System;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }
        private DataTable _dtSource;

        private void _RefreshPersonList()
        {
            _dtSource = clsTestTypes.GetAllTestTypes();

            if (_dtSource == null)
            {
                MessageBox.Show("error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvTestTypes.DataSource = _dtSource;
            lblRecordsCount.Text = _dtSource.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually
            dgvTestTypes.AllowUserToAddRows = false;
            dgvTestTypes.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvTestTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestTypes.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvTestTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvTestTypes.ReadOnly = true;
        }

        private void ssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int applicationTypeID = (int)dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value;

            frmUpdateTestTypes frm = new frmUpdateTestTypes(applicationTypeID);
            frm.ShowDialog();
            _RefreshPersonList();
            _LayoutDataGridView();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshPersonList();
            _LayoutDataGridView();
        }
    }
}
