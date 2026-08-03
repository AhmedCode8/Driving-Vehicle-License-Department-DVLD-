using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseData
    {
        // ========== استعلامات SQL ==========
        private const string DetainLicenseQuery = @"
            INSERT INTO DetainedLicenses 
            (
                LicenseID,
                CreatedByUserID,
                DetainDate,
                FineFees,
                IsReleased
            )
            VALUES 
            (
                @LicenseID,
                @CreatedByUserID,
                @DetainDate,
                @FineFees,
                0
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string ReleaseLicenseQuery = @"
            UPDATE DetainedLicenses
            SET 
                ReleaseApplicationID = @ReleaseApplicationID,
                ReleasedByUserID = @ReleasedByUserID,
                ReleaseDate = @ReleaseDate,
                IsReleased = 1
            WHERE DetainID = @DetainID;
        ";

        private const string IsLicenseDetainedQuery = @"
            SELECT COUNT(*)
            FROM DetainedLicenses
            WHERE LicenseID = @LicenseID
            AND IsReleased = 0;
        ";

        private const string GetAllDetainedLicensesQuery = @"
    SELECT 
        DL.DetainID AS [D.ID],
        DL.LicenseID AS [L.ID],
        DL.DetainDate AS [D.Date],
        DL.IsReleased AS [Is Released],
        DL.FineFees AS [Fine Fees],
        DL.ReleaseDate AS [Release Date],
        P.NationalNo AS [N.No.],
        P.FirstName + ' ' + P.SecondName + ' ' + ISNULL(P.ThirdName + ' ', '') + P.LastName AS [Full Name],
        DL.ReleaseApplicationID AS [Release App.ID]
        FROM DetainedLicenses DL
        INNER JOIN Licenses L ON DL.LicenseID = L.LicenseID
           INNER JOIN Drivers D ON L.DriverID = D.DriverID
         INNER JOIN People P ON D.PersonID = P.PersonID
       ORDER BY DL.DetainID DESC;
";

        private const string GetDetainedLicenseByIDQuery = @"
            SELECT 
                DetainID,
                LicenseID,
                ReleaseApplicationID,
                CreatedByUserID,
                ReleasedByUserID,
                DetainDate,
                FineFees,
                IsReleased,
                ReleaseDate
            FROM DetainedLicenses
            WHERE DetainID = @DetainID;
        ";

        private const string GetDetainedLicenseByLicenseIDQuery = @"
            SELECT 
                DetainID,
                LicenseID,
                ReleaseApplicationID,
                CreatedByUserID,
                ReleasedByUserID,
                DetainDate,
                FineFees,
                IsReleased,
                ReleaseDate
            FROM DetainedLicenses
            WHERE LicenseID = @LicenseID
            AND IsReleased = 0;
        ";

        // ========== 1. حجز رخصة جديدة ==========
        public static int DetainLicense(clsDetainedLicenseDTO detainedLicense)
        {
            int newDetainID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DetainLicenseQuery;

                command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = detainedLicense.LicenseID;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = detainedLicense.CreatedByUserID;
                command.Parameters.Add("@DetainDate", SqlDbType.Date).Value = detainedLicense.DetainDate;
                command.Parameters.Add("@FineFees", SqlDbType.Decimal).Value = detainedLicense.FineFees;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newDetainID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error detaining license.", ex);
                }
            }

            return newDetainID;
        }

        // ========== 2. فك حجز رخصة ==========
        public static int ReleaseLicense(int detainID, int releaseApplicationID, int releasedByUserID, DateTime releaseDate)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = ReleaseLicenseQuery;

                command.Parameters.Add("@ReleaseApplicationID", SqlDbType.Int).Value = releaseApplicationID;
                command.Parameters.Add("@ReleasedByUserID", SqlDbType.Int).Value = releasedByUserID;
                command.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = releaseDate;
                command.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error releasing license with Detain ID {detainID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 3. التحقق من أن الرخصة محجوزة ==========
        public static bool IsLicenseDetained(int licenseID)
        {
            bool isDetained = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = IsLicenseDetainedQuery;
                command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseID;

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    isDetained = count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking if license {licenseID} is detained.", ex);
                }
            }

            return isDetained;
        }

        // ========== 4. جلب قائمة جميع الرخص المحجوزة ==========
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllDetainedLicensesQuery;

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
                    throw new Exception("Error retrieving detained licenses list.", ex);
                }
            }

            return dt;
        }

        // ========== 5. جلب سجل حجز محدد بالـ ID ==========
        public static clsDetainedLicenseDTO GetDetainedLicenseByID(int detainID)
        {
            clsDetainedLicenseDTO detainedLicense = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetDetainedLicenseByIDQuery;
                command.Parameters.Add("@DetainID", SqlDbType.Int).Value = detainID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detainedLicense = new clsDetainedLicenseDTO
                            {
                                DetainID = clsDataAccessHelper.ConvertToInt(reader["DetainID"]),
                                LicenseID = clsDataAccessHelper.ConvertToInt(reader["LicenseID"]),
                                ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? (int?)null : clsDataAccessHelper.ConvertToInt(reader["ReleaseApplicationID"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? (int?)null : clsDataAccessHelper.ConvertToInt(reader["ReleasedByUserID"]),
                                DetainDate = clsDataAccessHelper.ConvertToDateTime(reader["DetainDate"]),
                                FineFees = Convert.ToDecimal(reader["FineFees"]),
                                IsReleased = clsDataAccessHelper.ConvertToBool(reader["IsReleased"]),
                                ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? (DateTime?)null : clsDataAccessHelper.ConvertToDateTime(reader["ReleaseDate"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving detained license with ID {detainID}.", ex);
                }
            }

            return detainedLicense;
        }

        // ========== 6. جلب سجل حجز بواسطة LicenseID (للمحجوزة وغير المفرج عنها) ==========
        public static clsDetainedLicenseDTO GetDetainedLicenseByLicenseID(int licenseID)
        {
            clsDetainedLicenseDTO detainedLicense = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetDetainedLicenseByLicenseIDQuery;
                command.Parameters.Add("@LicenseID", SqlDbType.Int).Value = licenseID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detainedLicense = new clsDetainedLicenseDTO
                            {
                                DetainID = clsDataAccessHelper.ConvertToInt(reader["DetainID"]),
                                LicenseID = clsDataAccessHelper.ConvertToInt(reader["LicenseID"]),
                                ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? (int?)null : clsDataAccessHelper.ConvertToInt(reader["ReleaseApplicationID"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? (int?)null : clsDataAccessHelper.ConvertToInt(reader["ReleasedByUserID"]),
                                DetainDate = clsDataAccessHelper.ConvertToDateTime(reader["DetainDate"]),
                                FineFees = Convert.ToDecimal(reader["FineFees"]),
                                IsReleased = clsDataAccessHelper.ConvertToBool(reader["IsReleased"]),
                                ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? (DateTime?)null : clsDataAccessHelper.ConvertToDateTime(reader["ReleaseDate"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving detained license for license ID {licenseID}.", ex);
                }
            }

            return detainedLicense;
        }
    }
}