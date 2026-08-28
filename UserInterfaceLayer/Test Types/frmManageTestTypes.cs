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
            // منع المستخدم من الإضافة أو الحذف اليدوي من الـ Grid
            dgvTestTypes.AllowUserToAddRows = false;
            dgvTestTypes.AllowUserToDeleteRows = false;

            // تفعيل تحديد السطر بالكامل بدلاً من الخلايا الفردية
            dgvTestTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestTypes.MultiSelect = false;

            // جعل الجدول للقراءة فقط
            dgvTestTypes.ReadOnly = true;

            // 🌟 التعديل الجذري: ضبط العرض اليدوي للأعمدة ليتطابق مع الصورة تماماً
            if (dgvTestTypes.Columns.Count > 0)
            {
                dgvTestTypes.Columns["TestTypeID"].Width = 110;
                dgvTestTypes.Columns["TestTypeTitle"].Width = 200;
                dgvTestTypes.Columns["TestTypeDescription"].Width = 450;
                dgvTestTypes.Columns["TestTypeFees"].Width = 110;
            }
        }

        private void ssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int applicationTypeID = (int)dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value;

            frmUpdateTestTypes frm = new frmUpdateTestTypes(applicationTypeID);
            frm.TestTypesUpdated += _RefreshPersonList;
            frm.ShowDialog();

        }



        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshPersonList();
            _LayoutDataGridView();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
