using DVDL_Logic_layer.Person;
using DVLD_DTOs;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard(clsUserDTO Info)
        {
            InitializeComponent();
            LoadUserData(Info);
        }
        //  الشكل الصحيح الذي يريده الـ Designer
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        public void LoadUserData(clsUserDTO userInfo)
        {

            clsPersonDTO personinfo = clsPerson.GetPersonById(userInfo.PersonID);
            ctrlPersonCard1.LoadPersonData(personinfo);
            lblUserName.Text = userInfo.UserName;
            lblUserID.Text = userInfo.PersonID.ToString();

            lblIsActive.Text = (userInfo.IsActive) ? "Yes" : "No";



        }


    }
}
