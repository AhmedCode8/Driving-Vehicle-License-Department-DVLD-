using DVDL_Logic_layer.Users;
using DVLD_DTOs;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UserInterfaceLayer.Users
{
    public partial class frmAddNewUser : Form
    {
        private int _UserID = -1;
        public clsUser User = new clsUser();

        public int UserID
        {
            get => _UserID;
            set => _UserID = value;
        }

        private int _PersonID => ctrlFindUserCard1.PersonID;

        public frmAddNewUser()
        {
            InitializeComponent();
            UpdateTitleStatus();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            if (_UserID != -1)
            {
                clsUserDTO iefo = clsUser.GetUserByID(_UserID);
                User = new clsUser(iefo);

                // Fill UI controls with data
                txtUserName.Text = User.UserName;
                txtPassword.Text = User.Password;
                txtConfirmPassword.Text = User.Password;
                chkIsActive.Checked = User.IsActive;
                lblUserID.Text = User.UserID.ToString();

                ctrlFindUserCard1.LoadPersonData(User.PersonID);
                UpdateTitleStatus();
            }
        }

        private void UpdateTitleStatus()
        {
            // Shortened using Ternary Operator
            lblAddOrUpdate.Text = (User.Mode == clsUser.enMode.AddNew) ? "Add New User" : "Update User";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            User.PersonID = _PersonID;
            User.UserName = txtUserName.Text;
            User.Password = txtPassword.Text;
            User.IsActive = chkIsActive.Checked;


            int savedUserID = User.Save();
            if (savedUserID != -1)
            {
                MessageBox.Show("Data Saved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _UserID = User.UserID;
                User.Mode = clsUser.enMode.Update;
                lblAddOrUpdate.Text = "Update User";
                lblUserID.Text = savedUserID.ToString();
                UpdateTitleStatus();
            }
            else
            {
                MessageBox.Show("Error: Data was not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                MessageBox.Show("Please select a person first.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (User.Mode == clsUser.enMode.AddNew && clsUser.IsPersonLinkedToUser(_PersonID))
            {
                MessageBox.Show("This person is already associated with a user. Please select another person.",
                                "Person Already Linked", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
            {
                tabControl1.SelectedIndex++;
            }
        }
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string username = txtUserName.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be empty.");
                return;
            }

            if (IsSameUsernameInUpdateMode(username))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
                return;
            }

            if (clsUser.IsUserExists(username))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username is already taken, please choose another one.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }
        private bool IsSameUsernameInUpdateMode(string username)
        {
            if (_UserID == -1) return false;

            clsUserDTO currentUser = clsUser.GetUserByID(_UserID);
            return currentUser != null && currentUser.UserName == username;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            this.AutoValidate = AutoValidate.EnableAllowFocusChange; // ✨ أضف هذا السطر هنا
            // إذا كان الحقل فارغاً، أو كان النص لا يطابق كلمة المرور الأولى
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true; // منع الفورم من الحفظ
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match or field is empty!");
            }
            else
            {
                e.Cancel = false; // السماح بالحفظ
                errorProvider1.SetError(txtConfirmPassword, ""); // مسح الخطأ
            }
        }



    }
}