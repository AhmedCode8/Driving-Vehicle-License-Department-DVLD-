using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        // ========== استعلامات SQL ==========
        private const string AddNewApplicationQuery = @"
            INSERT INTO Applications 
            (
                ApplicantPersonID, 
                ApplicationDate, 
                ApplicationTypeID, 
                ApplicationStatus, 
                LastStatusDate, 
                PaidFees, 
                CreatedByUserID
            )
            VALUES 
            (
                @ApplicantPersonID, 
                @ApplicationDate, 
                @ApplicationTypeID, 
                @ApplicationStatus, 
                @LastStatusDate, 
                @PaidFees, 
                @CreatedByUserID
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string UpdateApplicationQuery = @"
            UPDATE Applications
            SET 
                ApplicantPersonID = @ApplicantPersonID,
                ApplicationDate = @ApplicationDate,
                ApplicationTypeID = @ApplicationTypeID,
                ApplicationStatus = @ApplicationStatus,
                LastStatusDate = @LastStatusDate,
                PaidFees = @PaidFees,
                CreatedByUserID = @CreatedByUserID
            WHERE ApplicationID = @ApplicationID;
        ";

        private const string UpdateApplicationStatusByLocalIDQuery = @"
         UPDATE Applications
         SET 
             ApplicationStatus = @ApplicationStatus,
             LastStatusDate = @LastStatusDate
         WHERE ApplicationID = (
             SELECT ApplicationID 
             FROM LocalDrivingLicenseApplications 
             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
         );";

        private const string DeleteApplicationQuery = @"
            DELETE FROM Applications 
            WHERE ApplicationID = @ApplicationID;
        ";

        private const string GetApplicationByIDQuery = @"
            SELECT 
                ApplicationID,
                ApplicantPersonID,
                ApplicationDate,
                ApplicationTypeID,
                ApplicationStatus,
                LastStatusDate,
                PaidFees,
                CreatedByUserID
            FROM Applications
            WHERE ApplicationID = @ApplicationID;
        ";

        private const string GetApplicationByLocalDrivingLicenseAppIDQuery = @"
         SELECT 
             ApplicationID,
             ApplicantPersonID,
             ApplicationDate,
             ApplicationTypeID,
             ApplicationStatus,
             LastStatusDate,
             PaidFees,
             CreatedByUserID
         FROM Applications
         WHERE ApplicationID = 
              (
             SELECT ApplicationID 
             FROM LocalDrivingLicenseApplications 
             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
         );";


        // ========== 1. إضافة طلب جديد ==========
        public static int AddNewApplication(clsApplicationDTO application)
        {
            int newApplicationID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewApplicationQuery;

                command.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = application.ApplicantPersonID;
                command.Parameters.Add("@ApplicationDate", SqlDbType.Date).Value = application.ApplicationDate;
                command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = application.ApplicationTypeID;
                command.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = application.ApplicationStatus;

                if (application.LastStatusDate == null)
                    command.Parameters.AddWithValue("@LastStatusDate", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);

                command.Parameters.Add("@PaidFees", SqlDbType.Decimal).Value = application.PaidFees;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = application.CreatedByUserID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newApplicationID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new application.", ex);
                }
            }

            return newApplicationID;
        }

        // ========== 2. تحديث بيانات الطلب ==========
        public static int UpdateApplication(clsApplicationDTO application)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateApplicationQuery;

                command.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = application.ApplicantPersonID;
                command.Parameters.Add("@ApplicationDate", SqlDbType.Date).Value = application.ApplicationDate;
                command.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = application.ApplicationTypeID;
                command.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = application.ApplicationStatus;
                command.Parameters.Add("@LastStatusDate", SqlDbType.Date).Value = application.LastStatusDate;
                command.Parameters.Add("@PaidFees", SqlDbType.Decimal).Value = application.PaidFees;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = application.CreatedByUserID;
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = application.ApplicationID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating application with ID {application.ApplicationID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 3. تحديث حالة الطلب فقط ==========
        public static int UpdateApplicationStatus(int localDrivingLicenseApplicationID, byte newStatus, DateTime lastStatusDate)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateApplicationStatusByLocalIDQuery;

                command.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = newStatus;
                command.Parameters.Add("@LastStatusDate", SqlDbType.Date).Value = lastStatusDate;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating status for application ID {localDrivingLicenseApplicationID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 4. حذف طلب ==========
        public static int DeleteApplication(int applicationID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DeleteApplicationQuery;
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // حالة انتهاك المفتاح الأجنبي (Foreign Key Violation)
                    if (ex.Number == 547)
                    {
                        throw new Exception("Cannot delete this application because it has related records (licenses or tests).", ex);
                    }
                    throw new Exception($"Error deleting application with ID {applicationID}.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting application with ID {applicationID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 5. جلب طلب محدد بالـ ID ==========
        public static clsApplicationDTO GetApplicationByID(int applicationID)
        {
            clsApplicationDTO application = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetApplicationByIDQuery;
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            application = new clsApplicationDTO
                            {
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                ApplicantPersonID = clsDataAccessHelper.ConvertToInt(reader["ApplicantPersonID"]),
                                ApplicationDate = clsDataAccessHelper.ConvertToDateTime(reader["ApplicationDate"]),
                                ApplicationTypeID = clsDataAccessHelper.ConvertToInt(reader["ApplicationTypeID"]),
                                ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]),
                                LastStatusDate = clsDataAccessHelper.ConvertToDateTime(reader["LastStatusDate"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving application with ID {applicationID}.", ex);
                }
            }

            return application;
        }



        public static clsApplicationDTO GetApplicationByLocalDrivingLicenseAppID(int localDrivingLicenseApplicationID)
        {
            clsApplicationDTO application = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetApplicationByLocalDrivingLicenseAppIDQuery;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            application = new clsApplicationDTO
                            {
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                ApplicantPersonID = clsDataAccessHelper.ConvertToInt(reader["ApplicantPersonID"]),
                                ApplicationDate = clsDataAccessHelper.ConvertToDateTime(reader["ApplicationDate"]),
                                ApplicationTypeID = clsDataAccessHelper.ConvertToInt(reader["ApplicationTypeID"]),
                                ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]),
                                LastStatusDate = clsDataAccessHelper.ConvertToDateTime(reader["LastStatusDate"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving application for Local Driving License Application ID {localDrivingLicenseApplicationID}.", ex);
                }
            }

            return application;
        }


    }
}