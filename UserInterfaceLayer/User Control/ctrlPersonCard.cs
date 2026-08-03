using DVLD_DTOs;
using System.Windows.Forms;
namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlPersonCard : UserControl
    {
        public ctrlPersonCard()
        {
            InitializeComponent();
        }


        public void LoadPersonData(clsPersonDTO Info)
        {
            // 1. حارس الأمان: التحقق من أن الكائن ليس Null ومنع إجهاض البرنامج
            if (Info == null)
            {
                MessageBox.Show("No person data found to display.", "Data Unavailable",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ResetDefaultValues(); // دالة اختيارية لتنظيف الشاشة من البيانات السابقة
                return; // الخروج فوراً لحماية الأسطر التالية من الانهيار
            }

            lblPersonID.Text = Info.PersonID.ToString();
            lblNationalNo.Text = Info.NationalNo;
            lblPhone.Text = Info.Phone;
            lblAddress.Text = Info.Address;
            lblDateOfBirth.Text = Info.DateOfBirth.ToShortDateString();
            lblCountry.Text = Info.NationalityCountryID.ToString();

            lblFullName.Text = Info.FullName; ;
            lblEmail.Text = string.IsNullOrEmpty(Info.Email) ? "No Email Provided" : Info.Email;

            lblGender.Text = (Info.Gendor == 0) ? "Male" : "Female";
            pbPersonImage.Image = (Info.Gendor == 0) ? Properties.Resources.Man_32 : Properties.Resources.Female_512;
            pbPersonImage.ImageLocation = Info.ImagePath;

            //if (!string.IsNullOrEmpty(Info.ImagePath))
            //{
            //    if (System.IO.File.Exists(Info.ImagePath))
            //        pbPersonImage.ImageLocation = Info.ImagePath;
            //    else
            //        MessageBox.Show("Could not find the person's profile image on disk.", "Image Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

    }


}
