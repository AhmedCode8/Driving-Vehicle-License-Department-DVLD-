using DVDL_Logic_layer.Users;
using DVLD_DTOs;
using System;
using System.Data;
using System.Windows.Forms;

namespace UserInterfaceLayer.Users
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
            _RefreshPersonList();
            _LayoutDataGridView();
        }

        private DataTable _dtSource;

        private void _RefreshPersonList()
        {
            // جلب البيانات وتخزينها في المتغير العام أولاً
            _dtSource = clsUser.GetAllUsers();

            // ربط الـ Grid بالمتغير العام مباشرة لتتضح الرؤية
            dgvUsers.DataSource = _dtSource;
            lblRecordsCount.Text = _dtSource.Rows.Count.ToString();
        }
        private void _LayoutDataGridView()
        {
            // Prevent user from adding or deleting rows manually
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;

            // Enable full row selection instead of individual cell selection
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;

            // Adjust column widths automatically to fill the grid
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Make the grid read-only
            dgvUsers.ReadOnly = true;
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            clsUserDTO usrsInfo = clsUser.GetUserByID(UserID);



            frmUserInfo frmUserInfo = new frmUserInfo(usrsInfo);
            frmUserInfo.ShowDialog();
        }

        private void tsmiAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddNewUser addNewUser = new frmAddNewUser();
            addNewUser.ShowDialog();
            _RefreshPersonList();
            _LayoutDataGridView();
        }

        private void tsmiEditPerson_Click(object sender, EventArgs e)
        {

            frmAddNewUser addNewUser = new frmAddNewUser();
            addNewUser.UserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            addNewUser.ShowDialog();
            _RefreshPersonList();
            _LayoutDataGridView();
        }

        private void tsmiDeletePerson_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            int Result = clsUser.DeleteUser(UserID);
            if (Result > 0)
            {
                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshPersonList();
                _LayoutDataGridView();
            }
            else
            {
                MessageBox.Show("Error: User could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;
            clsUserDTO usrsInfo = clsUser.GetUserByID(UserID);


            frmChangePassword frmChangePassword = new frmChangePassword(usrsInfo);
            frmChangePassword.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddNewUser addNewUser = new frmAddNewUser();
            addNewUser.ShowDialog();
            _RefreshPersonList();
            _LayoutDataGridView();
        }


        private void FilterUsers()
        {
            if (_dtSource == null) return;

            // إعادة تعيين الفلتر إذا كان عمود البحث هو None
            if (cbFilterBy.Text == "None")
            {
                _dtSource.DefaultView.RowFilter = "";
                return;
            }

            // إذا كان الفلتر ليس Is Active، وتكست بوكس الفلترة فارغ.. امسح الفلتر واخرج
            if (cbFilterBy.Text != "Is Active" && string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                _dtSource.DefaultView.RowFilter = "";
                return;
            }

            // ربط عناصر الـ ComboBox بأسماء الأعمدة الحقيقية في قاعدة البيانات
            string columnName = "";
            switch (cbFilterBy.Text)
            {
                case "User ID": columnName = "UserID"; break;
                case "UserName": columnName = "UserName"; break;
                case "Person ID": columnName = "PersonID"; break;
                case "Full Name": columnName = "FullName"; break;
                case "Is Active": columnName = "IsActive"; break;
                default: columnName = "None"; break;
            }

            // تطبيق الفلترة بناءً على نوع البيانات (Data Type)
            if (columnName == "UserID" || columnName == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text.Trim(), out int id))
                {
                    _dtSource.DefaultView.RowFilter = $"{columnName} = {id}";
                }
                else
                {
                    _dtSource.DefaultView.RowFilter = "";
                }
            }
            else if (columnName == "IsActive")
            {
                // ✨ الفلترة هنا تقرأ الآن من الـ ComboBox الخاص بـ cbIsActive مباشرة!
                string selectedActive = cbIsActive.Text;

                if (selectedActive == "Yes")
                {
                    _dtSource.DefaultView.RowFilter = $"{columnName} = true";
                }
                else if (selectedActive == "No")
                {
                    _dtSource.DefaultView.RowFilter = $"{columnName} = false";
                }
                else // في حال كان الخيار "All" مثلاً
                {
                    _dtSource.DefaultView.RowFilter = "";
                }

            }
            else
            {
                // فلترة النصوص (LIKE)
                _dtSource.DefaultView.RowFilter = $"{columnName} LIKE '{txtFilterValue.Text.Trim()}%'";
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            FilterUsers();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // دائماً عند تغيير نوع الفلترة نقوم بتنظيف الفلاتر والنصوص القديمة لتبدأ بداية نظيفة
            _dtSource.DefaultView.RowFilter = "";
            txtFilterValue.Clear();

            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0; // اجعله يختار أول عنصر تلقائياً (مثلاً All أو Yes)
            }
            else if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = false;
            }
            else
            {
                txtFilterValue.Visible = true;
                cbIsActive.Visible = false;
                txtFilterValue.Focus(); // وضع مؤشر الكتابة داخل التكست بوكس فوراً
            }

            FilterUsers();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterUsers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
