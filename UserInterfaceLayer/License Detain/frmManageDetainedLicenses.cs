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
            frmReleaseDetainedLicense frmManageDetainedLicense = new frmReleaseDetainedLicense();
            frmManageDetainedLicense.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, System.EventArgs e)
        {
            frmLicenseDetain frmLicenseDetain = new frmLicenseDetain();
            frmLicenseDetain.ShowDialog();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        DataTable _dtSource;
        private void _RefreshList()
        {
            // جلب البيانات وتخزينها في المتغير العام أولاً
            _dtSource = clsDetainedLicense.GetAllDetainedLicenses();

            // ربط الـ Grid بالمتغير العام مباشرة لتتضح الرؤية
            dgvDetainedLicenses.DataSource = _dtSource;
            lblRecordCount.Text = _dtSource.Rows.Count.ToString();
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
            frm.ShowDialog();

            // 5. تحديث جدول الرخص المحتجزة بعد إغلاق الشاشة لإنعاش البيانات
            _RefreshList();

        }
    }
}
