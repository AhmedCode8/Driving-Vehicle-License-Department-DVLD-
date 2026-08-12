using DVLD_DTOs;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        public int UserID => _UserID;

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public ctrlUserCard(clsUserDTO userInfo)
        {
            InitializeComponent();
            LoadUserData(userInfo);
        }

        public void LoadUserData(clsUserDTO userInfo)
        {
            if (userInfo == null)
            {
                MessageBox.Show("User data cannot be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetUserCard();
                return;
            }

            _UserID = userInfo.UserID;

            /// تمرير بيانات الشخص مباشرة لأنها أصبحت جزءاً من كائن المستخدم
            if (userInfo.PersonInfo != null)
            {
                ctrlPersonCard1.LoadPersonData(userInfo.PersonInfo);
            }

            // 2. عرض بيانات الحساب الخاصة بالمستخدم بدقة
            lblUserID.Text = userInfo.UserID.ToString(); // تم التصحيح ليعرض UserID وليس PersonID
            lblUserName.Text = userInfo.UserName;
            lblIsActive.Text = userInfo.IsActive ? "Yes" : "No";
        }

        public void ResetUserCard()
        {
            _UserID = -1;
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
        }

        private void ctrlPersonCard1_Load(object sender, System.EventArgs e)
        {

        }
    }
}