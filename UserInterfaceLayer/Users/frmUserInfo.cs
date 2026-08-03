using DVLD_DTOs;
using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.Users
{
    public partial class frmUserInfo : Form
    {
        public frmUserInfo(clsUserDTO userInfo)
        {
            InitializeComponent();
            ctrlUserCard.LoadUserData(userInfo);
        }

        public frmUserInfo()
        {
            InitializeComponent();
        }


        private void txtClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
