using DVDL_Logic_layer.Application_Types;
using DVDL_Logic_layer.Applications;
using DVDL_Logic_layer.driver;
using DVDL_Logic_layer.Global_Classes;
using DVDL_Logic_layer.License_Class;
using DVLD_DTOs;
using System;
using System.Windows.Forms;
using UserInterfaceLayer.License;

namespace UserInterfaceLayer.Application_Type
{
    public partial class frmRenewLicenseApplication : Form
    {
        private clsLicenseDTO _selectedLicenseInfo;
        private clsApplicationTypeDTO _InfoApplicationTyp;
        private clsLicenseClassDTO _InfoLicenseClass;

        public frmRenewLicenseApplication()
        {
            InitializeComponent();
        }
        private bool CheckLicenseExpiration(clsLicenseDTO licenseInfo)
        {
            if (licenseInfo == null)
            {
                btnRenew.Enabled = false;
                return false;
            }

            if (licenseInfo.IsActive == false)
            {
                btnRenew.Enabled = false;
                MessageBox.Show(
                  $"The license is not activated",
                  "Not allowed",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error
              );
                return false;
            }


            // فحص ما إذا كانت الرخصة منتهية (تاريخ الانتهاء أصغر من تاريخ اليوم الحالي)
            if (licenseInfo.ExpirationDate < DateTime.Now)
            {
                // الرخصة منتهية: تفعيل زر التجديد وبدون إظهار أي رسالة
                btnRenew.Enabled = true;
                return true; // تعبر عن أن الرخصة منتهية وجاهزة للتجديد
            }
            else
            {
                // الرخصة غير منتهية: تعطيل زر التجديد وإظهار الرسالة
                btnRenew.Enabled = false;

                // تحويل التاريخ لنفس الصيغة الظاهرة بالصورة (مثال: 09/Oct/2033)
                string formattedDate = licenseInfo.ExpirationDate.ToString("dd/MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                MessageBox.Show(
                    $"Selected License is not yet expiared, it will expire on:\n{formattedDate}",
                    "Not allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }
        }
        private void frmRenewLicenseApplication_Load(object sender, EventArgs e)
        {
            // 1. الاشتراك في الحدث عند تحميل الفورم
            ctrlFilterLicenseCard1.OnLicenseSelected += CtrlFilterIicenseCard1_OnLicenseSelected;
            _InfoApplicationTyp = clsApplicationTypes.GetApplicationTypeByID(2);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CtrlFilterIicenseCard1_OnLicenseSelected(clsLicenseDTO licenseInfo)
        {
            if (licenseInfo != null)
            {
                _selectedLicenseInfo = licenseInfo;
            }

            _InfoLicenseClass = clsLicenseClass.GetLicenseClassByID(_selectedLicenseInfo.LicenseClass);
            if (CheckLicenseExpiration(_selectedLicenseInfo))
            {
                // 5. تعبئة البيانات في الواجهة
                _FillDefaultApplicationData();
            }

            _FillDefaultApplicationData();
        }
        private void _FillDefaultApplicationData()
        {
            if (_selectedLicenseInfo == null)
                return;

            // 1. التواريخ (تاريخ الطلب والإصدار هو تاريخ اليوم)
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            // تاريخ الانتهاء الجديد (إضافة 10 سنوات على تاريخ اليوم)
            // إن كان كائن _InfoLicenseClass يحتوي على خاصية فترة الصلاحية DefaultValidityLength يمكنك استبدال الـ 10 بها
            int validityYears = (_InfoLicenseClass != null) ? _InfoLicenseClass.DefaultValidityLength : 10;
            lblExpirationDate.Text = DateTime.Now.AddYears(validityYears).ToString("dd/MMM/yyyy");

            // 2. رقم الرخصة القديمة
            lblOldLicenseID.Text = _selectedLicenseInfo.LicenseID.ToString();

            // 3. احتساب وتعبئة الرسوم
            decimal applicationFees = (_InfoApplicationTyp != null) ? _InfoApplicationTyp.ApplicationFees : 0;
            decimal licenseFees = (_InfoLicenseClass != null) ? _InfoLicenseClass.ClassFees : 0;

            lblApplicationFees.Text = applicationFees.ToString("0.##");
            lblLicenseFees.Text = licenseFees.ToString("0.##");
            lblTotalFees.Text = (applicationFees + licenseFees).ToString("0.##");

            // 4. اسم / رقم المستخدم الحالي
            // يمكنك استبدالها باسم المستخدم المسجل في النظام (مثال: clsGlobal.CurrentUser.UserName)
            lblCreatedBy.Text = _selectedLicenseInfo.CreatedByUserID.ToString();

            // 5. القيم الافتراضية قبل إتمام التجديد
            lblRenewApplicationID.Text = "[???]";

            // ملاحظة: في ملف الـ Designer يظهر عنوان هذا الخيار باسم label8 (أو lblReplacedLicenseID إذا قمت بتغيير اسمه)
            lblReplacedLicenseID.Text = "[???]";
        }
        private void _UpdateRenewedInfo(int newApplicationID, int newRenewedLicenseID)
        {
            // 1. عرض رقم طلب التجديد الجديد
            lblRenewApplicationID.Text = newApplicationID.ToString();

            // 2. عرض رقم الرخصة الجديدة الصادرة
            // (استخدم label8 أو قم بتغيير اسمه لـ lblReplacedLicenseID من خصائص Control)
            lblReplacedLicenseID.Text = newRenewedLicenseID.ToString();
        }
        private void Createtheapplicationandcreatethelicense()
        {
            // 1. فحص أمان للبيانات الأساسية
            if (_selectedLicenseInfo == null || _InfoApplicationTyp == null)
            {
                MessageBox.Show("بيانات الرخصة أو نوع الطلب غير مكتملة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDriverDTO driverInfo = clsDrivers.GetDriverByID(_selectedLicenseInfo.DriverID);
            if (driverInfo == null)
            {
                MessageBox.Show("لم يتم العثور على بيانات السائق!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. إلغاء تفعيل الرخصة القديمة (خطوة أساسية في التجديد)
            if (!clsLicenses.DeactivateLicense(_selectedLicenseInfo.LicenseID))
            {
                MessageBox.Show("فشل في إلغاء تفعيل الرخصة القديمة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. إنشاء طلب التجديد الأساسي (Base Application)
            clsApplications baseApplication = new clsApplications()
            {
                ApplicantPersonID = driverInfo.PersonID,
                CreatedByUserID = clsGlobal.CurrentUser.UserID,
                ApplicationTypeID = _InfoApplicationTyp.ApplicationTypeID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3, // Completed (مكتمل)
                LastStatusDate = DateTime.Now,
                PaidFees = _InfoApplicationTyp.ApplicationFees
            };

            // حفظ الطلب أولاً والتأكد من نجاحه
            if (!baseApplication.Save())
            {
                MessageBox.Show("حدث خطأ أثناء حفظ طلب التجديد!", "فشل الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. تحديد مدة الصلاحية بناءً على فئة الرخصة
            int validityYears = (_InfoLicenseClass != null) ? _InfoLicenseClass.DefaultValidityLength : 10;

            // 5. إنشاء الرخصة الجديدة
            clsLicenseDTO newLicense = new clsLicenseDTO
            {
                ApplicationID = baseApplication.ApplicationID,
                DriverID = driverInfo.DriverID,
                LicenseClass = _selectedLicenseInfo.LicenseClass,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(validityYears),
                Notes = txtNotes.Text.Trim(),
                PaidFees = (_InfoLicenseClass != null) ? _InfoLicenseClass.ClassFees : 0,
                IsActive = true,
                IssueReason = 2, // 2 = Renew (تجديد)
                CreatedByUserID = clsGlobal.CurrentUser.UserID
            };

            int newLicenseID = clsLicenses.AddNewLicense(newLicense);

            // 6. التحقق من نجاح إنشاء الرخصة وتحديث الواجهة
            if (newLicenseID != 0)
            {
                MessageBox.Show($"تم تجديد الرخصة بنجاح!\nرقم الرخصة الجديدة: [{newLicenseID}]", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //// تحديث أرقام الطلب والرخصة الجديدة على الواجهة
                //lblRenewApplicationID.Text = baseApplication.ApplicationID.ToString();
                //lblReplacedLicenseID.Text = newLicenseID.ToString(); // Renewed License ID
                _UpdateRenewedInfo(baseApplication.ApplicationID, newLicenseID);


                // تأمين الواجهة لمنع إعادة الضغط أو التعديل
                btnRenew.Enabled = false;
                llShowNewLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("فشل في إصدار الرخصة الجديدة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRenew_Click(object sender, EventArgs e)
        {
            Createtheapplicationandcreatethelicense();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selectedLicenseInfo == null)
                return;
            clsDriverDTO driverInfo = clsDrivers.GetDriverByID(_selectedLicenseInfo.DriverID);

            frmLicenseHistory frm = new frmLicenseHistory(driverInfo.PersonID);
            frm.Show();
        }
    }
}
