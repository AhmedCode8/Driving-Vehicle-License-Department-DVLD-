using DVLD_DataAccess; // تأكد من مطابقة الـ Namespace لطبقة البيانات لديك
using DVLD_DTOs;
using System;

namespace DVDL_Logic_layer.Applications
{
    public class clsApplications
    {
        #region Object State & Properties

        // 1. تحديد حالة الكائن (إضافة جديد أم تعديل)
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // 2. خصائص كائن الطلب (Properties)
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime? LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        #endregion

        #region Constructors (المشيدات)

        // المشيد الافتراضي: يستخدم لإنشاء طلب جديد بالكامل
        public clsApplications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 1; // القيمة الافتراضية للطلب الجديد (مثلاً 1 تعني New)
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            // تحديد الحالة كإضافة جديد
            this.Mode = enMode.AddNew;
        }
        public clsApplications(clsApplicationDTO applicationInfo)
        {
            if (applicationInfo == null)
            {
                throw new ArgumentNullException(nameof(applicationInfo), "Application DTO cannot be null.");
            }

            this.ApplicationID = applicationInfo.ApplicationID;
            this.ApplicantPersonID = applicationInfo.ApplicantPersonID;
            this.ApplicationDate = applicationInfo.ApplicationDate;
            this.ApplicationTypeID = applicationInfo.ApplicationTypeID;
            this.ApplicationStatus = applicationInfo.ApplicationStatus;
            this.LastStatusDate = applicationInfo.LastStatusDate;
            this.PaidFees = applicationInfo.PaidFees;
            this.CreatedByUserID = applicationInfo.CreatedByUserID;

            this.Mode = enMode.Update;
        }

        #endregion

        #region Private Saving Methods (الدوال الخاصة بالحفظ)


        private int _AddNew()
        {
            clsApplicationDTO applicationDTO = new clsApplicationDTO
            {

                ApplicantPersonID = this.ApplicantPersonID,
                ApplicationDate = this.ApplicationDate,
                ApplicationTypeID = this.ApplicationTypeID,
                ApplicationStatus = this.ApplicationStatus,
                LastStatusDate = this.LastStatusDate,
                PaidFees = this.PaidFees,
                CreatedByUserID = this.CreatedByUserID
            };

            this.ApplicationID = clsApplicationData.AddNewApplication(applicationDTO);

            return this.ApplicationID;
        }
        private bool _Update()
        {
            clsApplicationDTO applicationDTO = new clsApplicationDTO
            {
                ApplicationID = this.ApplicationID, // 💡 ضروري جداً في التعديل
                ApplicantPersonID = this.ApplicantPersonID,
                ApplicationDate = this.ApplicationDate,
                ApplicationTypeID = this.ApplicationTypeID,
                ApplicationStatus = this.ApplicationStatus,
                LastStatusDate = this.LastStatusDate,
                PaidFees = this.PaidFees,
                CreatedByUserID = this.CreatedByUserID
            };

            return (clsApplicationData.UpdateApplication(applicationDTO) > 0);
        }

        #endregion

        #region Public Save Method (الدالة العامة للحفظ)

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew() != -1)
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }
        #endregion

        public static int UpdateApplicationStatus(int applicationID, byte newStatus, DateTime lastStatusDate)
        {
            return clsApplicationData.UpdateApplicationStatus(applicationID, newStatus, lastStatusDate);
        }
        public static clsApplicationDTO GetApplicationByID(int applicationID)
        {
            return clsApplicationData.GetApplicationByID(applicationID);
        }
        public static int DeleteApplication(int applicationID)
        {
            return clsApplicationData.DeleteApplication(applicationID);
        }

        public static clsApplicationDTO GetApplicationByLocalDrivingLicenseAppID(int localDrivingLicenseApplicationID)
        {
            return clsApplicationData.GetApplicationByLocalDrivingLicenseAppID(localDrivingLicenseApplicationID);
        }



    }
}