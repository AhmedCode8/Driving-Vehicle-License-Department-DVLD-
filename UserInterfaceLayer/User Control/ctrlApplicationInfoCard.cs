using System;
using System.Windows.Forms;

namespace UserInterfaceLayer.User_Control
{
    public partial class ctrlApplicationInfoCard : UserControl
    {
        public ctrlApplicationInfoCard()
        {
            InitializeComponent();
        }
        // 1. دالة تستقبل كافة المعلومات الأساسية وتعرضها على عناصر الواجهة
        public void LoadApplicationInfo(DateTime applicationDate,
            decimal fees, string createdBy, int oldLicenseID)
        {
            //  lblLRApplicationID.Text = applicationID.ToString();
            lblApplicationDate.Text = applicationDate.ToShortDateString();
            lblApplicationFees.Text = fees.ToString();
            lblCreatedBy.Text = createdBy;
            //  lblReplacedLicenseID.Text = replacedLicenseID.ToString();
            lblOldLicenseID.Text = oldLicenseID.ToString();
        }

        // 2. دالة بسيطة تستقبل معرف الطلب ومعرف الرخصة فقط
        public void LoadInfoByApplicationAndLicenseID(int applicationID, int licenseID)
        {
            lblLRApplicationID.Text = applicationID.ToString();
            lblOldLicenseID.Text = licenseID.ToString();
        }
    }
}
