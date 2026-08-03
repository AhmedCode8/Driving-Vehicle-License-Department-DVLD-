using DVDL_Logic_layer.Users;
using DVLD_DTOs;
using System.Windows.Forms;
namespace UserInterfaceLayer.Users
{
    public partial class frmChangePassword : Form
    {
        clsUserDTO _UserInfo;
        public frmChangePassword(clsUserDTO userInfo)
        {
            InitializeComponent();
            ctrlUserCard.LoadUserData(userInfo);
            _UserInfo = userInfo;
        }

        public frmChangePassword()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int Result = clsUser.UpdatePassword(_UserInfo.UserID, txtConfirmPassword.Text);
            if (Result != 0)
            {
                MessageBox.Show("The password has been successfully changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void txtCurrentPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtCurrentPassword.Text != _UserInfo.Password)
            {
                errorProvider.SetError(txtCurrentPassword, "The Password is not the same");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtCurrentPassword, "");
                e.Cancel = false;

            }
        }

        private void txtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.AutoValidate = AutoValidate.EnableAllowFocusChange; // ✨ أضف هذا السطر هنا
            // إذا كان الحقل فارغاً، أو كان النص لا يطابق كلمة المرور الأولى
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || txtConfirmPassword.Text != txtNewPassword.Text)
            {
                e.Cancel = true; // منع الفورم من الحفظ
                errorProvider.SetError(txtConfirmPassword, "Passwords do not match or field is empty!");
            }
            else
            {
                e.Cancel = false; // السماح بالحفظ
                errorProvider.SetError(txtConfirmPassword, ""); // مسح الخطأ
            }
        }
    }
}
