using DVLD_DataAccess;
using DVLD_DTOs;
using System;
using System.Data;

namespace DVDL_Logic_layer.tests
{
    public class clsTestAppointment
    {
        #region Object State & Properties

        // Enum to manage object state (AddNew or Update)
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // Appointment properties
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public clsLicenseClassData licenseClassData = new clsLicenseClassData();

        // Property to convert current object state into DTO
        public clsTestAppointmentDTO AppointmentDTO
        {
            get
            {
                return new clsTestAppointmentDTO
                {
                    TestAppointmentID = this.TestAppointmentID,
                    TestTypeID = this.TestTypeID,
                    LocalDrivingLicenseApplicationID = this.LocalDrivingLicenseApplicationID,
                    AppointmentDate = this.AppointmentDate,
                    PaidFees = this.PaidFees,
                    CreatedByUserID = this.CreatedByUserID,
                    IsLocked = this.IsLocked
                };
            }
        }

        #endregion
        public string GitLicenseClassName(int ID)
        {
            clsLicenseClassDTO info = clsLicenseClassData.GetLicenseClassByID(ID);
            return info.ClassName;
        }
        public string GitFullName(int ID)
        {
            clsPersonDTO info = clsPersonData.GetPersonByID(ID);
            return info.FullName;
        }


        #region Constructors

        // Default constructor for creating a new appointment
        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;

            this.Mode = enMode.AddNew;
        }

        // Constructor to populate object data from DTO
        public clsTestAppointment(clsTestAppointmentDTO appointmentDTO)
        {
            if (appointmentDTO == null) return;

            this.TestAppointmentID = appointmentDTO.TestAppointmentID;
            this.TestTypeID = appointmentDTO.TestTypeID;
            this.LocalDrivingLicenseApplicationID = appointmentDTO.LocalDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDTO.AppointmentDate;
            this.PaidFees = appointmentDTO.PaidFees;
            this.CreatedByUserID = appointmentDTO.CreatedByUserID;
            this.IsLocked = appointmentDTO.IsLocked;

            this.Mode = enMode.Update;
        }

        #endregion

        #region Add and Edit Operations

        // Private method to add new appointment to database
        private bool _AddNew()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(this.AppointmentDTO);
            return (this.TestAppointmentID != -1);
        }

        // Private method to update existing appointment details
        private bool _Update()
        {
            int rowsAffectedDate = clsTestAppointmentData.UpdateTestAppointmentDate(this.TestAppointmentID, this.AppointmentDate);
            int rowsAffectedFees = clsTestAppointmentData.UpdateTestAppointmentFees(this.TestAppointmentID, this.PaidFees);

            if (this.IsLocked)
            {
                clsTestAppointmentData.LockAppointment(this.TestAppointmentID);
            }

            return (rowsAffectedDate > 0 || rowsAffectedFees > 0);
        }

        // Public method to save object (Handles AddNew or Update based on Mode)
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
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

        #region Static Methods (Operations)

        // Lock a test appointment by ID
        public static bool LockAppointment(int appointmentID)
        {
            return clsTestAppointmentData.LockAppointment(appointmentID) > 0;
        }

        // Get all test appointments for a specific application and test type
        public static DataTable GetTestAppointmentsByApplicationIDAndTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            return clsTestAppointmentData.GetTestAppointmentsByApplicationIDAndTestType(localDrivingLicenseApplicationID, testTypeID);
        }

        // Get test appointment object by ID
        public static clsTestAppointment Find(int appointmentID)
        {
            clsTestAppointmentDTO dto = clsTestAppointmentData.GetTestAppointmentByID(appointmentID);

            if (dto != null)
                return new clsTestAppointment(dto);
            else
                return null;
        }

        // Get the last test appointment for a specific application and test type
        public static clsTestAppointment GetLastTestAppointmentByApplicationIDAndTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            clsTestAppointmentDTO dto = clsTestAppointmentData.GetLastTestAppointmentByApplicationIDAndTestType(localDrivingLicenseApplicationID, testTypeID);

            if (dto != null)
                return new clsTestAppointment(dto);
            else
                return null;
        }

        // Update appointment date directly
        public static bool UpdateTestAppointmentDate(int appointmentID, DateTime newDate)
        {
            return clsTestAppointmentData.UpdateTestAppointmentDate(appointmentID, newDate) > 0;
        }

        // Update appointment fees directly
        public static bool UpdateTestAppointmentFees(int appointmentID, decimal newFees)
        {
            return clsTestAppointmentData.UpdateTestAppointmentFees(appointmentID, newFees) > 0;
        }

        public static bool IsTheTestLocked(int testAppointmentID)
        {
            // 1. استدعاء الدالة من كلاس طبقة البيانات (clsTestAppointmentData)
            clsTestAppointmentDTO info = clsTestAppointmentData.GetTestAppointmentByID(testAppointmentID);

            // 2. التحقق من أن الكائن ليس null وإرجاع قيمة IsLocked
            if (info != null)
            {
                return info.IsLocked;
            }

            return false; // إرجاع قيمة افتراضية في حال عدم وجود الموعد
        }



        #endregion
    }
}