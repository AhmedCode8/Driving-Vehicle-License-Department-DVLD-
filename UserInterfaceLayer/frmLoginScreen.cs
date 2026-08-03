using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.Users;
using DVLD_DTOs;
using System;
using System.Windows.Forms;
namespace UserInterfaceLayer
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();


            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your username and password first.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsUserDTO user = clsUser.GetUserByUsernameAndPassword(userName, password);

            if (user == null)
            {
                MessageBox.Show("The username or password is incorrect!", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return; //
            }

            if (!user.IsActive)
            {
                MessageBox.Show("Sorry, This account is currently disabled . Please check with the administrator.", "Inactive account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // الخروج ومنع الدخول
            }

            clsGlobal.CurrentUser = user;
            // الحالة الثالثة والأخيرة: البيانات صحيحة والحساب مفعّل بنجاح!
            MessageBox.Show($"Welcome back, Hey {user.UserName} in the system!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // إذا كان المستخدم واضعاً علامة صح على "تذكرني"
            if (chkRememberMe.Checked)
            {
                // نحفظ البيانات في إعدادات التطبيق المحلية
                Properties.Settings.Default.UserName = txtUserName.Text.Trim();
                Properties.Settings.Default.Password = txtPassword.Text.Trim();
                Properties.Settings.Default.RememberMe = true;
            }
            else
            {
                // إذا شال علامة الصح، نمسح البيانات القديمة لكي لا يتذكره المرة القادمة
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.RememberMe = false;
            }

            // ⚠️ سطر مهم جداً لتأكيد الحفظ على الهارد ديسك
            Properties.Settings.Default.Save();

            frMain frm = new frMain();
            frm.Show();
            this.Hide(); // إخفاء شاشة تسجيل الدخول
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            // نفحص هل المستخدم طلب تذكره في المرة السابقة؟
            if (Properties.Settings.Default.RememberMe)
            {
                // نملأ الخانات تلقائياً بالبيانات المحفوظة
                txtUserName.Text = Properties.Settings.Default.UserName;
                txtPassword.Text = Properties.Settings.Default.Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                // إذا لم يطلب، نترك الخانات فارغة
                txtUserName.Text = "";
                txtPassword.Text = "";
                chkRememberMe.Checked = false;
            }
        }
    }
}
