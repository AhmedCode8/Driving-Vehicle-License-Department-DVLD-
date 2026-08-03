using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsInternationalLicenseData
    {
        // ========== استعلامات SQL ==========
        private const string AddNewInternationalLicenseQuery = @"
            INSERT INTO InternationalLicenses 
            (
                ApplicationID,
                DriverID,
                CreatedByUserID,
                IssuedUsingLocalLicenseID,
                IssueDate,
                ExpirationDate,
                IsActive
            )
            VALUES 
            (
                @ApplicationID,
                @DriverID,
                @CreatedByUserID,
                @IssuedUsingLocalLicenseID,
                @IssueDate,
                @ExpirationDate,
                @IsActive
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string GetAllInternationalLicensesQuery = @"
    SELECT 
        IL.InternationalLicenseID AS [Int.License ID],
        IL.ApplicationID,
        IL.DriverID,
        IL.IssuedUsingLocalLicenseID AS [L.License ID],               
        IL.IssueDate AS [Issue Date],
        IL.ExpirationDate AS [Expiration Date],
        IL.IsActive AS [Is Active]
    FROM InternationalLicenses IL
    INNER JOIN Drivers D ON IL.DriverID = D.DriverID
    INNER JOIN People P ON D.PersonID = P.PersonID
    ORDER BY IL.InternationalLicenseID DESC;
";

        private const string GetInternationalLicenseByIDQuery = @"
            SELECT 
                InternationalLicenseID,
                ApplicationID,
                DriverID,
                CreatedByUserID,
                IssuedUsingLocalLicenseID,
                IssueDate,
                ExpirationDate,
                IsActive
            FROM InternationalLicenses
            WHERE InternationalLicenseID = @InternationalLicenseID;
        ";

        private const string GetInternationalLicensesByDriverIDQuery = @"
            SELECT 
                InternationalLicenseID,
                ApplicationID,
                DriverID,
                CreatedByUserID,
                IssuedUsingLocalLicenseID,
                IssueDate,
                ExpirationDate,
                IsActive
            FROM InternationalLicenses
            WHERE DriverID = @DriverID
            ORDER BY IssueDate DESC;
        ";

        private const string GetActiveInternationalLicenseByLocalLicenseIDQuery = @"
            SELECT 
                InternationalLicenseID
            FROM InternationalLicenses
            WHERE IssuedUsingLocalLicenseID = @LocalLicenseID
            AND IsActive = 1;
        ";

        private const string DeactivateInternationalLicenseQuery = @"
            UPDATE InternationalLicenses
            SET IsActive = 0
            WHERE InternationalLicenseID = @InternationalLicenseID;
        ";

        // ========== 1. إضافة رخصة دولية جديدة ==========
        public static int AddNewInternationalLicense(clsInternationalLicenseDTO internationalLicense)
        {
            int newInternationalLicenseID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewInternationalLicenseQuery;

                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = internationalLicense.ApplicationID;
                command.Parameters.Add("@DriverID", SqlDbType.Int).Value = internationalLicense.DriverID;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = internationalLicense.CreatedByUserID;
                command.Parameters.Add("@IssuedUsingLocalLicenseID", SqlDbType.Int).Value = internationalLicense.IssuedUsingLocalLicenseID;
                command.Parameters.Add("@IssueDate", SqlDbType.Date).Value = internationalLicense.IssueDate;
                command.Parameters.Add("@ExpirationDate", SqlDbType.Date).Value = internationalLicense.ExpirationDate;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = internationalLicense.IsActive;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newInternationalLicenseID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new international license.", ex);
                }
            }

            return newInternationalLicenseID;
        }

        // ========== 2. جلب قائمة جميع الرخص الدولية ==========
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllInternationalLicensesQuery;

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
                    throw new Exception("Error retrieving international licenses list.", ex);
                }
            }

            return dt;
        }

        // ========== 3. جلب رخصة دولية محددة بالـ ID ==========
        public static clsInternationalLicenseDTO GetInternationalLicenseByID(int internationalLicenseID)
        {
            clsInternationalLicenseDTO internationalLicense = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetInternationalLicenseByIDQuery;
                command.Parameters.Add("@InternationalLicenseID", SqlDbType.Int).Value = internationalLicenseID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            internationalLicense = new clsInternationalLicenseDTO
                            {
                                InternationalLicenseID = clsDataAccessHelper.ConvertToInt(reader["InternationalLicenseID"]),
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                DriverID = clsDataAccessHelper.ConvertToInt(reader["DriverID"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                IssuedUsingLocalLicenseID = clsDataAccessHelper.ConvertToInt(reader["IssuedUsingLocalLicenseID"]),
                                IssueDate = clsDataAccessHelper.ConvertToDateTime(reader["IssueDate"]),
                                ExpirationDate = clsDataAccessHelper.ConvertToDateTime(reader["ExpirationDate"]),
                                IsActive = clsDataAccessHelper.ConvertToBool(reader["IsActive"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving international license with ID {internationalLicenseID}.", ex);
                }
            }

            return internationalLicense;
        }

        // ========== 4. جلب جميع الرخص الدولية لسائق محدد ==========
        public static DataTable GetInternationalLicensesByDriverID(int driverID)
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetInternationalLicensesByDriverIDQuery;
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
                    throw new Exception($"Error retrieving international licenses for driver ID {driverID}.", ex);
                }
            }

            return dt;
        }

        // ========== 5. التحقق من وجود رخصة دولية نشطة مرتبطة برخصة محلية ==========
        public static int GetActiveInternationalLicenseByLocalLicenseID(int localLicenseID)
        {
            int internationalLicenseID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetActiveInternationalLicenseByLocalLicenseIDQuery;
                command.Parameters.Add("@LocalLicenseID", SqlDbType.Int).Value = localLicenseID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    internationalLicenseID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking active international license for local license ID {localLicenseID}.", ex);
                }
            }

            return internationalLicenseID; // يعيد -1 إذا لم يتم العثور على شيء
        }

        // ========== 6. إيقاف تفعيل رخصة دولية ==========
        public static int DeactivateInternationalLicense(int internationalLicenseID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DeactivateInternationalLicenseQuery;
                command.Parameters.Add("@InternationalLicenseID", SqlDbType.Int).Value = internationalLicenseID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deactivating international license with ID {internationalLicenseID}.", ex);
                }
            }

            return rowsAffected;
        }
    }
}