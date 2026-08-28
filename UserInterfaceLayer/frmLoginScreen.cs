using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.Users;
using DVLD_DTOs;
using Microsoft.Win32; // 👈 إضافة مساحة الأسماء
using System;
using System.Windows.Forms;

// مسار تخزين إعدادات تطبيقك في الـ Registry
namespace UserInterfaceLayer
{
    public partial class frmLoginScreen : Form
    {
        private string _registryPath = @"HKEY_CURRENT_USER\Software\DVLD";

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



            // حفظ أو مسح البيانات من الـ Registry بناءً على خيار "تذكرني"
            if (chkRememberMe.Checked)
            {
                Registry.SetValue(_registryPath, "UserName", txtUserName.Text.Trim(), RegistryValueKind.String);
                Registry.SetValue(_registryPath, "Password", txtPassword.Text.Trim(), RegistryValueKind.String);
                Registry.SetValue(_registryPath, "RememberMe", "True", RegistryValueKind.String);
            }
            else
            {
                // إذا شال علامة الصح نلغي التذكر
                Registry.SetValue(_registryPath, "RememberMe", "False", RegistryValueKind.String);
                Registry.SetValue(_registryPath, "UserName", "", RegistryValueKind.String);
                Registry.SetValue(_registryPath, "Password", "", RegistryValueKind.String);
            }

            frMain frm = new frMain();
            frm.Show();
            this.Hide();




            //// إذا كان المستخدم واضعاً علامة صح على "تذكرني"
            //if (chkRememberMe.Checked)
            //{
            //    // نحفظ البيانات في إعدادات التطبيق المحلية
            //    Properties.Settings.Default.UserName = txtUserName.Text.Trim();
            //    Properties.Settings.Default.Password = txtPassword.Text.Trim();
            //    Properties.Settings.Default.RememberMe = true;
            //}
            //else
            //{
            //    // إذا شال علامة الصح، نمسح البيانات القديمة لكي لا يتذكره المرة القادمة
            //    Properties.Settings.Default.UserName = "";
            //    Properties.Settings.Default.Password = "";
            //    Properties.Settings.Default.RememberMe = false;
            //}

            //// ⚠️ سطر مهم جداً لتأكيد الحفظ على الهارد ديسك
            //Properties.Settings.Default.Save();

            //frMain frm = new frMain();
            //frm.Show();
            //this.Hide(); // إخفاء شاشة تسجيل الدخول



        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";

            // 1. قراءة حالة "تذكرني" (القيمة الافتراضية "False" في حال عدم وجود المفتاح)
            string rememberMe = Registry.GetValue(keyPath, "RememberMe", "False") as string;

            if (rememberMe == "True")
            {
                // 2. قراءة اسم المستخدم وكلمة المرور
                txtUserName.Text = Registry.GetValue(keyPath, "UserName", "") as string;
                txtPassword.Text = Registry.GetValue(keyPath, "Password", "") as string;
                chkRememberMe.Checked = true;
            }
            else
            {
                // 3. إذا لم يطلب التذكر، نترك الخانات فارغة
                txtUserName.Text = "";
                txtPassword.Text = "";
                chkRememberMe.Checked = false;
            }
        }

        //private void frmLoginScreen_Load(object sender, EventArgs e)
        //{
        //    // نفحص هل المستخدم طلب تذكره في المرة السابقة؟
        //    if (Properties.Settings.Default.RememberMe)
        //    {
        //        // نملأ الخانات تلقائياً بالبيانات المحفوظة
        //        txtUserName.Text = Properties.Settings.Default.UserName;
        //        txtPassword.Text = Properties.Settings.Default.Password;
        //        chkRememberMe.Checked = true;
        //    }
        //    else
        //    {
        //        // إذا لم يطلب، نترك الخانات فارغة
        //        txtUserName.Text = "";
        //        txtPassword.Text = "";
        //        chkRememberMe.Checked = false;
        //    }
        //}
    }
}
