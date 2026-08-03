
using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
        // ========== استعلامات SQL ==========
        private const string AddNewLicenseQuery = @"
            INSERT INTO Licenses 
            (
                ApplicationID,
                DriverID,
                LicenseClass,
                IssueDate,
                ExpirationDate,
                Notes,
                PaidFees,
                IsActive,
                IssueReason,
                CreatedByUserID
            )
            VALUES 
            (
                @ApplicationID,
                @DriverID,
                @LicenseClass,
                @IssueDate,
                @ExpirationDate,
                @Notes,
                @PaidFees,
                @IsActive,
                @IssueReason,
                @CreatedByUserID
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string GetLicenseByIDQuery = @"
            SELECT 
                LicenseID,
                ApplicationID,
                DriverID,
                LicenseClass,
                IssueDate,
                ExpirationDate,
                Notes,
                PaidFees,
                IsActive,
                IssueReason,
                CreatedByUserID
            FROM Licenses
            WHERE LicenseID = @LicenseID;
        ";

        private const string DeactivateLicenseQuery = @"
            UPDATE Licenses
            SET IsActive = 0
            WHERE LicenseID = @LicenseID;
        ";

        //private const string GetLicensesByDriverIDQuery = @"
        //    SELECT 
        //        LicenseID,
        //        ApplicationID,
        //        DriverID,
        //        LicenseClass,
        //        IssueDate,
        //        ExpirationDate,
        //        Notes,
        //        PaidFees,
        //        IsActive,
        //        IssueReason,
        //        CreatedByUserID
        //    FROM Licenses
        //    WHERE DriverID = @DriverID
        //    ORDER BY IssueDate DESC;
        //";
        private const string GetLicensesByDriverIDQuery = @"
         SELECT 
             Licenses.LicenseID AS [Lic.ID],
             Licenses.ApplicationID AS [App.ID],
             LicenseClasses.ClassName AS [Class Name],
             Licenses.IssueDate AS [Issue Date],
             Licenses.ExpirationDate AS [Expiration Date],
             Licenses.IsActive AS [Is Active]
         FROM Licenses 
         INNER JOIN LicenseClasses 
             ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
         WHERE Licenses.DriverID = @DriverID
         ORDER BY Licenses.IssueDate DESC;
         ";

        private const string GetActiveLicenseIDByPersonIDAndClassQuery = @"
            SELECT 
                L.LicenseID
            FROM Licenses L
            INNER JOIN Drivers D ON L.DriverID = D.DriverID
            WHERE D.PersonID = @PersonID
            AND L.LicenseClass = @LicenseClass
            AND L.IsActive = 1;
        ";

        private const string GetActiveLicenseByLicenseClassAndPersonIDQuery = @"
            SELECT 
                L.*
            FROM Licenses L
            INNER JOIN Drivers D ON L.DriverID = D.DriverID
            WHERE D.PersonID = @PersonID
            AND L.LicenseClass = @LicenseClass
            AND L.IsActive = 1;
        ";
        private const string GetLicenseByApplicationIDQuery = @"
         SELECT 
             LicenseID,
             ApplicationID,
             DriverID,
             LicenseClass,
             IssueDate,
             ExpirationDate,
             Notes,
             PaidFees,
             IsActive,
             IssueReason,
             CreatedByUserID
         FROM Licenses
         WHERE ApplicationID = @ApplicationID;
         ";

        // ========== 1. إضافة رخصة جديدة ==========
        public static int AddNewLicense(clsLicenseDTO license)
        {
            int newLicenseID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewLicenseQuery;

                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = license.ApplicationID;
                command.Parameters.Add("@DriverID", SqlDbType.Int).Value = license.DriverID;
                command.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = license.LicenseClass;
                command.Parameters.Add("@IssueDate", SqlDbType.Date).Value = license.IssueDate;
                command.Parameters.Add("@ExpirationDate", SqlDbType.Date).Value = license.ExpirationDate;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = string.IsNullOrEmpty(license.Notes) ? (object)DBNull.Value : license.Notes;
                command.Parameters.Add("@PaidFees", SqlDbType.Decimal).Value = license.PaidFees;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = license.IsActive;
                command.Parameters.Add("@IssueReason", SqlDbType.TinyInt).Value = license.IssueReason;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = license.CreatedByUserID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newLicenseID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new license.", ex);
                }
            }

            return newLicenseID;
        }

        // ========== 2. جلب رخصة محددة بالـ ID ==========
        public static clsLicenseDTO GetLicenseByID(int licenseID)
        {
            clsLicenseDTO license = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLicenseByIDQuery;
                command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            license = new clsLicenseDTO
                            {
                                LicenseID = clsDataAccessHelper.ConvertToInt(reader["LicenseID"]),
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                DriverID = clsDataAccessHelper.ConvertToInt(reader["DriverID"]),
                                LicenseClass = clsDataAccessHelper.ConvertToInt(reader["LicenseClass"]),
                                IssueDate = clsDataAccessHelper.ConvertToDateTime(reader["IssueDate"]),
                                ExpirationDate = clsDataAccessHelper.ConvertToDateTime(reader["ExpirationDate"]),
                                Notes = clsDataAccessHelper.ConvertToString(reader["Notes"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                IsActive = clsDataAccessHelper.ConvertToBool(reader["IsActive"]),
                                IssueReason = Convert.ToByte(reader["IssueReason"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving license with ID {licenseID}.", ex);
                }
            }

            return license;
        }

        // ========== 3. إيقاف تفعيل رخصة ==========
        public static int DeactivateLicense(int licenseID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DeactivateLicenseQuery;
                command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deactivating license with ID {licenseID}.", ex);
                }
            }

            return rowsAffected;
        }



        // ========== 4. جلب جميع رخص سائق محدد ==========
        public static DataTable GetLicensesByDriverID(int driverID)
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLicensesByDriverIDQuery;
                command.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverID;

                try
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving licenses for driver ID {driverID}.", ex);
                }
            }

            return dt;
        }

        // ========== 5. التحقق من وجود رخصة نشطة لشخص معين وصنف معين (ترجع LicenseID) ==========
        public static int GetActiveLicenseIDByPersonIDAndClass(int personID, int licenseClassID)
        {
            int licenseID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetActiveLicenseIDByPersonIDAndClassQuery;
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
                command.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = licenseClassID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    licenseID = clsDataAccessHelper.ConvertToInt(result); // إذا لم يوجد يعيد 0
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking active license for person ID {personID} and class {licenseClassID}.", ex);
                }
            }

            return licenseID;
        }

        // ========== 6. جلب رخصة نشطة كاملة لشخص معين وصنف معين ==========
        public static clsLicenseDTO GetActiveLicenseByLicenseClassAndPersonID(int personID, int licenseClassID)
        {
            clsLicenseDTO license = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetActiveLicenseByLicenseClassAndPersonIDQuery;
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;
                command.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = licenseClassID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            license = new clsLicenseDTO
                            {
                                LicenseID = clsDataAccessHelper.ConvertToInt(reader["LicenseID"]),
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                DriverID = clsDataAccessHelper.ConvertToInt(reader["DriverID"]),
                                LicenseClass = clsDataAccessHelper.ConvertToInt(reader["LicenseClass"]),
                                IssueDate = clsDataAccessHelper.ConvertToDateTime(reader["IssueDate"]),
                                ExpirationDate = clsDataAccessHelper.ConvertToDateTime(reader["ExpirationDate"]),
                                Notes = clsDataAccessHelper.ConvertToString(reader["Notes"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                IsActive = clsDataAccessHelper.ConvertToBool(reader["IsActive"]),
                                IssueReason = Convert.ToByte(reader["IssueReason"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving active license for person ID {personID} and class {licenseClassID}.", ex);
                }
            }

            return license;
        }

        // ========== 7. جلب رخصة محددة بواسطة ApplicationID ==========
        public static clsLicenseDTO GetLicenseByApplicationID(int applicationID)
        {
            clsLicenseDTO license = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLicenseByApplicationIDQuery;
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            license = new clsLicenseDTO
                            {
                                LicenseID = clsDataAccessHelper.ConvertToInt(reader["LicenseID"]),
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                DriverID = clsDataAccessHelper.ConvertToInt(reader["DriverID"]),
                                LicenseClass = clsDataAccessHelper.ConvertToInt(reader["LicenseClass"]),
                                IssueDate = clsDataAccessHelper.ConvertToDateTime(reader["IssueDate"]),
                                ExpirationDate = clsDataAccessHelper.ConvertToDateTime(reader["ExpirationDate"]),
                                Notes = clsDataAccessHelper.ConvertToString(reader["Notes"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                IsActive = clsDataAccessHelper.ConvertToBool(reader["IsActive"]),
                                IssueReason = Convert.ToByte(reader["IssueReason"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving license for application ID {applicationID}.", ex);
                }
            }

            return license;
        }
    }
}