using DVLD_DTOs;
using System.Windows.Forms;

namespace UserInterfaceLayer.People
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(clsPersonDTO Info)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonData(Info);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
