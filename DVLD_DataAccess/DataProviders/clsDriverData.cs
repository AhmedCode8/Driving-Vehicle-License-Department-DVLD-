using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DTOs;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
        // ========== استعلامات SQL ==========
        private const string AddNewDriverQuery = @"
            INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
            VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
            SELECT SCOPE_IDENTITY();
        ";

        private const string GetAllDriversQuery = @"
            SELECT 
                Drivers.DriverID,
                Drivers.PersonID,
                People.NationalNo,
                People.FirstName + ' ' + People.SecondName + ' ' + 
                ISNULL(People.ThirdName + ' ', '') + People.LastName AS FullName,
                Drivers.CreatedDate AS [Date],
                (
                    SELECT COUNT(*) 
                    FROM Licenses 
                    WHERE Licenses.DriverID = Drivers.DriverID 
                    AND Licenses.IsActive = 1
                ) AS [Active Licenses]
            FROM Drivers
            INNER JOIN People ON Drivers.PersonID = People.PersonID
            ORDER BY Drivers.DriverID;
        ";

        private const string GetDriverByPersonIDQuery = @"
            SELECT 
                DriverID,
                PersonID,
                CreatedByUserID,
                CreatedDate
            FROM Drivers
            WHERE PersonID = @PersonID;
        ";

        private const string GetDriverByIDQuery = @"
            SELECT 
                DriverID,
                PersonID,
                CreatedByUserID,
                CreatedDate
            FROM Drivers
            WHERE DriverID = @DriverID;
        ";

        // ========== 1. إضافة سائق جديد ==========
        public static int AddNewDriver(clsDriverDTO driver)
        {
            int newDriverID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewDriverQuery;

                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = driver.PersonID;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = driver.CreatedByUserID;
                command.Parameters.Add("@CreatedDate", SqlDbType.Date).Value = driver.CreatedDate;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newDriverID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new driver.", ex);
                }
            }

            return newDriverID;
        }

        // ========== 2. جلب قائمة جميع السائقين ==========
        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllDriversQuery;

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
                    throw new Exception("Error retrieving drivers list.", ex);
                }
            }

            return dt;
        }

        // ========== 3. البحث عن سائق بواسطة PersonID ==========
        public static clsDriverDTO GetDriverByPersonID(int personID)
        {
            clsDriverDTO driver = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetDriverByPersonIDQuery;
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driver = new clsDriverDTO
                            {
                                DriverID = clsDataAccessHelper.ConvertToInt(reader["DriverID"]),
                                PersonID = clsDataAccessHelper.ConvertToInt(reader["PersonID"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                CreatedDate = clsDataAccessHelper.ConvertToDateTime(reader["CreatedDate"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving driver with Person ID {personID}.", ex);
                }
            }

            return driver;
        }

        // ========== 4. البحث عن سائق بواسطة DriverID ==========
        public static clsDriverDTO GetDriverByID(int driverID)
        {
            clsDriverDTO driver = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetDriverByIDQuery;
                command.Parameters.Add("@DriverID", SqlDbType.Int).Value = driverID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driver = new clsDriverDTO
                            {
                                DriverID = clsDataAccessHelper.ConvertToInt(reader["DriverID"]),
                                PersonID = clsDataAccessHelper.ConvertToInt(reader["PersonID"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                CreatedDate = clsDataAccessHelper.ConvertToDateTime(reader["CreatedDate"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving driver with ID {driverID}.", ex);
                }
            }

            return driver;
        }
    }
}