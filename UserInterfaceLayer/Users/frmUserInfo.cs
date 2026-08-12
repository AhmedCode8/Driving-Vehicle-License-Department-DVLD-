using DVDL_Logic_layer.Users;
using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Users
{
    public partial class frmUserInfo : Form
    {
        private int _userID = -1;
        private clsUserDTO _userInfo;

        // Default constructor
        public frmUserInfo()
        {
            InitializeComponent();
        }

        // Constructor accepting User ID
        public frmUserInfo(int userID) : this()
        {
            _userID = userID;
        }

        // Constructor accepting User DTO directly
        public frmUserInfo(clsUserDTO userInfo) : this()
        {
            _userInfo = userInfo;
            _userID = userInfo != null ? userInfo.UserID : -1;
        }

        private void frmUserInfo_Load_1(object sender, EventArgs e)
        {
            _userInfo = clsUser.GetUserByID(_userID);
            _LoadUserInfo();

        }

        private void _LoadUserInfo()
        {

            if (_userInfo != null)
            {
                ctrlUserCard.LoadUserData(_userInfo);
            }
            //else if (_userID != -1)
            //{
            //    ctrlUserCard.LoadUserData(_userID);
            //}
            else
            {
                MessageBox.Show("No valid user information provided!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void txtClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }


    }
}