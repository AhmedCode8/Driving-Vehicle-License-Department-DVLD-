using DVLD_DataAccess;
using DVLD_DTOs;
using System;
using System.Data;

namespace DVDL_Logic_layer.License_Class
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int CreatedByUserID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.CreatedByUserID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(1);
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        public clsInternationalLicense(clsInternationalLicenseDTO dto)
        {
            this.InternationalLicenseID = dto.InternationalLicenseID;
            this.ApplicationID = dto.ApplicationID;
            this.DriverID = dto.DriverID;
            this.CreatedByUserID = dto.CreatedByUserID;
            this.IssuedUsingLocalLicenseID = dto.IssuedUsingLocalLicenseID;
            this.IssueDate = dto.IssueDate;
            this.ExpirationDate = dto.ExpirationDate;
            this.IsActive = dto.IsActive;

            Mode = enMode.Update;
        }

        public clsInternationalLicenseDTO ToDTO()
        {
            return new clsInternationalLicenseDTO
            {
                InternationalLicenseID = this.InternationalLicenseID,
                ApplicationID = this.ApplicationID,
                DriverID = this.DriverID,
                CreatedByUserID = this.CreatedByUserID,
                IssuedUsingLocalLicenseID = this.IssuedUsingLocalLicenseID,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                IsActive = this.IsActive
            };
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ToDTO());
            return (this.InternationalLicenseID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return false;
            }

            return false;
        }

        // ========== الدوال الساكنة (Static Methods) ==========

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public static clsInternationalLicenseDTO GetInternationalLicenseByID(int internationalLicenseID)
        {
            return clsInternationalLicenseData.GetInternationalLicenseByID(internationalLicenseID);
        }

        public static DataTable GetInternationalLicensesByDriverID(int driverID)
        {
            return clsInternationalLicenseData.GetInternationalLicensesByDriverID(driverID);
        }

        public static int GetActiveInternationalLicenseByLocalLicenseID(int localLicenseID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseByLocalLicenseID(localLicenseID);
        }

        public static bool DeactivateInternationalLicense(int internationalLicenseID)
        {
            return clsInternationalLicenseData.DeactivateInternationalLicense(internationalLicenseID) > 0;
        }
    }
}