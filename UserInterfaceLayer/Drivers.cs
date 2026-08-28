using DVDL_Logic_layer.driver;
using System;
using System.Data;
using System.Windows.Forms;
// تأكد من استدعاء مجال طبقة العمليات لديك هنا
// using BusinessLayer; 

namespace UserInterfaceLayer
{
    public partial class frmManageDrivers : Form
    {
        private DataTable _dtAllDrivers;

        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            // إعداد القائمة المنسدلة للفلتر
            cbFilterBy.SelectedIndex = 0;

            // جلب البيانات من الدالة التي أخبرتني عنها
            // قم بتعديل اسم الكلاس clsDriverData إلى الكلاس الصحيح في مشروعك
            _dtAllDrivers = clsDrivers.GetAllDrivers();

            dgvDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

            // ضبط أسماء وأحجام الأعمدة (اختياري حسب استرجاع الداتابيس)
            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 110;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 110;

                dgvDrivers.Columns[2].HeaderText = "National No";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 350;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 150;

                dgvDrivers.Columns[5].HeaderText = "Active Drives";
                dgvDrivers.Columns[5].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            // تصفير الفلتر عند اختيار None
            if (_dtAllDrivers != null)
                _dtAllDrivers.DefaultView.RowFilter = "";

            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // تحديد اسم العمود بناءً على الاختيار
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID"; // تأكد أن هذه الأسماء تطابق قاعدة البيانات لديك
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            // إعادة ضبط الفلتر إذا كان مربع النص فارغاً
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            // تطبيق الفلتر بناءً على نوع البيانات (رقم أم نص)
            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
                // فلتر الأرقام
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                // فلتر النصوص
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // منع إدخال الحروف إذا كان الفلتر على الأرقام
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}