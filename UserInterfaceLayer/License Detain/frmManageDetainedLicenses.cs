using DVDL_Logic_layer.DetainedLicense;
using DVDL_Logic_layer.License_Class;
using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System;
using System.Data;
using System.Windows.Forms;
using UserInterfaceLayer.License;
using UserInterfaceLayer.People;

namespace UserInterfaceLayer.License_Detain
{
    public partial class frmManageDetainedLicenses : Form
    {
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnReleaseDetainedLicense_Click(object sender, System.EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.OnReleaseDetainedLicense += _RefreshList;
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, System.EventArgs e)
        {
            frmLicenseDetain frm = new frmLicenseDetain();
            frm.OnLicenseDetain += _RefreshList;
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        DataTable _dtDetainedLicenses;
        private void _RefreshList()
        {
            // جلب البيانات وتخزينها في المتغير العام أولاً
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            // ربط الـ Grid بالمتغير العام مباشرة لتتضح الرؤية
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            lblRecordCount.Text = _dtDetainedLicenses.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually

            dgvDetainedLicenses.AllowUserToAddRows = false;
            dgvDetainedLicenses.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvDetainedLicenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetainedLicenses.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvDetainedLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvDetainedLicenses.ReadOnly = true;
        }

        private void frmManageDetainedLicenses_Load(object sender, System.EventArgs e)
        {
            _LayoutDataGridView();
            _RefreshList();
        }

        private void tsmiShowPersonDetails_Click(object sender, System.EventArgs e)
        {
            // 1. الحماية: التأكد من وجود صف محدد
            if (dgvDetainedLicenses.CurrentRow == null) return;

            // 2. قراءة قيمة الخلية بأمان مع التأكد أنها ليست فارغة
            object cellValue = dgvDetainedLicenses.CurrentRow.Cells["N.No."].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("National No is missing for this row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nationalNo = cellValue.ToString();

            // 3. جلب بيانات الشخص من طبقة المنطق
            clsPersonDTO personInfo = clsPerson.GetPersonByNationalNo(nationalNo);

            // 4. الحماية: التأكد من العثور على الشخص قبل فتح الشاشة
            if (personInfo == null)
            {
                MessageBox.Show($"No person found with National No = {nationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5. فتح الشاشة بنجاح
            frmPersonDetails personDetailsForm = new frmPersonDetails(personInfo);
            personDetailsForm.ShowDialog();
        }

        private void tsmiShowLicenseDetails_Click(object sender, System.EventArgs e)
        {
            // 1. الحماية: التأكد من وجود صف محدد
            if (dgvDetainedLicenses.CurrentRow == null) return;

            // 2. قراءة قيمة الخلية كـ object أولاً لحمايتها قبل التحويل
            object rawValue = dgvDetainedLicenses.CurrentRow.Cells["L.ID"].Value;

            if (rawValue == null || rawValue == DBNull.Value)
            {
                MessageBox.Show("License ID is missing for this row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int licenseID = Convert.ToInt32(rawValue);

            // 3. جلب بيانات الرخصة من طبقة المنطق
            clsLicenseDTO license = clsLicenses.GetLicenseByID(licenseID);

            // 4. الحماية: التأكد من وجود الرخصة فعلياً قبل فتح الشاشة
            if (license == null)
            {
                MessageBox.Show($"No license found with ID = {licenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 5. فتح شاشة تفاصيل الرخصة
            frmLicenseInfo frm = new frmLicenseInfo(license);
            frm.ShowDialog();
        }


        private void tsmiShowLicenseHistory_Click(object sender, System.EventArgs e)
        {
            // 1. الحماية: التأكد من وجود صف محدد
            if (dgvDetainedLicenses.CurrentRow == null) return;

            // 2. قراءة قيمة الخلية بأمان مع التأكد أنها ليست فارغة
            object cellValue = dgvDetainedLicenses.CurrentRow.Cells["N.No."].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("National No is missing for this row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nationalNo = cellValue.ToString();

            // 3. جلب بيانات الشخص من طبقة المنطق
            clsPersonDTO personInfo = clsPerson.GetPersonByNationalNo(nationalNo);

            // 4. الحماية: التأكد من العثور على الشخص قبل فتح الشاشة
            if (personInfo == null)
            {
                MessageBox.Show($"No person found with National No = {nationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmLicenseHistory LicenseHistory = new frmLicenseHistory(personInfo.PersonID);
            LicenseHistory.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. الحماية: التأكد من وجود صف محدد
            if (dgvDetainedLicenses.CurrentRow == null) return;

            // 2. التأكد من أن الرخصة محتجزة حالياً (ولم يتم الإفراج عنها مسبقاً)
            bool isReleased = Convert.ToBoolean(dgvDetainedLicenses.CurrentRow.Cells["Is Released"].Value);
            if (isReleased)
            {
                MessageBox.Show("Selected License is ALREADY released!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. قراءة رقم الرخصة (L.ID) بأمان
            object cellValue = dgvDetainedLicenses.CurrentRow.Cells["L.ID"].Value;
            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("License ID is missing for this row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int licenseID = Convert.ToInt32(cellValue);

            // 4. فتح شاشة فك الاحتجاز وتمرين رقم الرخصة لها
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(licenseID);
            frm.OnReleaseDetainedLicense += _RefreshList;
            frm.ShowDialog();


        }

        //code of Filter
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            // إعادة ضبط الفلتر وإعادة حساب عدد السجلات عند تغيير نوع الفلتر
            if (_dtDetainedLicenses != null)
                _dtDetainedLicenses.DefaultView.RowFilter = "";

            lblRecordCount.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }
        //-------------------
        private string _GetSelectedFilterColumn()
        {
            switch (cbFilterBy.Text)
            {
                case "Detain ID": return "D.ID";
                case "Is Released": return "Is Released";
                case "National No.": return "N.No.";
                case "Full Name": return "Full Name";
                case "Release Application ID": return "Release App.ID";
                default: return "None";
            }
        }


        private void _ApplyFilterToTable(string filterColumn, string filterValue)
        {
            // 1. فلترة الأعمدة الرقمية
            if (filterColumn == "D.ID" || filterColumn == "Release App.ID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, filterValue);
            }
            // 2. فلترة عمود الإفراج (Boolean / Bit)
            else if (filterColumn == "Is Released")
            {
                string val = filterValue.ToLower();
                if (val == "1" || val == "true" || val == "yes" || val == "نعم")
                    _dtDetainedLicenses.DefaultView.RowFilter = "[Is Released] = true";
                else if (val == "0" || val == "false" || val == "no" || val == "لا")
                    _dtDetainedLicenses.DefaultView.RowFilter = "[Is Released] = false";
                else
                    _dtDetainedLicenses.DefaultView.RowFilter = "1 = 0"; // إخفاء الكل إذا أدخل قيمة غير منطقية
            }
            // 3. فلترة الأعمدة النصية
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterColumn, filterValue);
            }
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = _GetSelectedFilterColumn();
            string filterValue = txtFilterValue.Text.Trim();

            // إذا كان الحقل فارغاً أو العمود غير معرّف، قم بإلغاء الفلتر
            if (string.IsNullOrWhiteSpace(filterValue) || filterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            }
            else
            {
                _ApplyFilterToTable(filterColumn, filterValue);
            }

            // تحديث عداد السجلات في النهاية دائماً
            lblRecordCount.Text = dgvDetainedLicenses.Rows.Count.ToString();


        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // منع كتابة غير الأرقام إذا كان الفلتر على المعرفات الرقمية
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
