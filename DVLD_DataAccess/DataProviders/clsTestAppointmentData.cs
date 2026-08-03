using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        // ========== استعلامات SQL ==========
        private const string AddNewTestAppointmentQuery = @"
            INSERT INTO TestAppointments 
            (
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked
            )
            VALUES 
            (
                @TestTypeID,
                @LocalDrivingLicenseApplicationID,
                @AppointmentDate,
                @PaidFees,
                @CreatedByUserID,
                0
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string UpdateTestAppointmentDateQuery = @"
            UPDATE TestAppointments
            SET AppointmentDate = @NewDate
            WHERE TestAppointmentID = @TestAppointmentID
            AND IsLocked = 0;
        ";

        private const string LockAppointmentQuery = @"
            UPDATE TestAppointments
            SET IsLocked = 1
            WHERE TestAppointmentID = @TestAppointmentID;
        ";

        private const string GetTestAppointmentsByApplicationIDAndTestTypeQuery = @"
            SELECT 
                TestAppointmentID,
                AppointmentDate,
                PaidFees,
                IsLocked
            FROM TestAppointments
            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            AND TestTypeID = @TestTypeID
            ORDER BY AppointmentDate DESC;
        ";

        private const string GetTestAppointmentByIDQuery = @"
            SELECT 
                TestAppointmentID,
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked
            FROM TestAppointments
            WHERE TestAppointmentID = @TestAppointmentID;
        ";

        private const string GetLastTestAppointmentByApplicationIDAndTestTypeQuery = @"
    SELECT TOP 1
        TestAppointmentID,
        TestTypeID,
        LocalDrivingLicenseApplicationID,
        AppointmentDate,
        PaidFees,
        CreatedByUserID,
        IsLocked
    FROM TestAppointments
    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
      AND TestTypeID = @TestTypeID
    ORDER BY TestAppointmentID DESC; -- 👈 الترتيب برقم الموعد هو الأدق دائماً
";

        private const string UpdateTestAppointmentFeesQuery = @"
            UPDATE TestAppointments
            SET PaidFees = @PaidFees
            WHERE TestAppointmentID = @TestAppointmentID;
        ";

        // ========== 1. إضافة موعد اختبار جديد ==========
        public static int AddNewTestAppointment(clsTestAppointmentDTO appointment)
        {
            int newAppointmentID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewTestAppointmentQuery;

                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = appointment.TestTypeID;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = appointment.LocalDrivingLicenseApplicationID;
                command.Parameters.Add("@AppointmentDate", SqlDbType.Date).Value = appointment.AppointmentDate;
                command.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = appointment.PaidFees;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = appointment.CreatedByUserID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newAppointmentID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new test appointment.", ex);
                }
            }

            return newAppointmentID;
        }

        // ========== 2. تحديث تاريخ الموعد (فقط إذا كان غير مقفل) ==========
        public static int UpdateTestAppointmentDate(int appointmentID, DateTime newDate)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateTestAppointmentDateQuery;

                command.Parameters.Add("@NewDate", SqlDbType.Date).Value = newDate;
                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = appointmentID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating appointment date for ID {appointmentID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 3. قفل الموعد ==========
        public static int LockAppointment(int appointmentID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = LockAppointmentQuery;
                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = appointmentID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error locking appointment with ID {appointmentID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 4. جلب مواعيد اختبار محددة لنوع اختبار معين ==========
        public static DataTable GetTestAppointmentsByApplicationIDAndTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetTestAppointmentsByApplicationIDAndTestTypeQuery;

                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;
                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

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
                    throw new Exception($"Error retrieving test appointments for application {localDrivingLicenseApplicationID} and test type {testTypeID}.", ex);
                }
            }

            return dt;
        }

        // ========== 5. جلب موعد محدد بالـ ID ==========
        public static clsTestAppointmentDTO GetTestAppointmentByID(int appointmentID)
        {
            clsTestAppointmentDTO appointment = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetTestAppointmentByIDQuery;
                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = appointmentID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            appointment = new clsTestAppointmentDTO
                            {
                                TestAppointmentID = clsDataAccessHelper.ConvertToInt(reader["TestAppointmentID"]),
                                TestTypeID = clsDataAccessHelper.ConvertToInt(reader["TestTypeID"]),
                                LocalDrivingLicenseApplicationID = clsDataAccessHelper.ConvertToInt(reader["LocalDrivingLicenseApplicationID"]),
                                AppointmentDate = clsDataAccessHelper.ConvertToDateTime(reader["AppointmentDate"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                IsLocked = clsDataAccessHelper.ConvertToBool(reader["IsLocked"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving test appointment with ID {appointmentID}.", ex);
                }
            }

            return appointment;
        }

        // ========== 6. جلب آخر موعد لنوع اختبار معين ==========
        public static clsTestAppointmentDTO GetLastTestAppointmentByApplicationIDAndTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            clsTestAppointmentDTO appointment = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLastTestAppointmentByApplicationIDAndTestTypeQuery;

                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;
                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            appointment = new clsTestAppointmentDTO
                            {
                                TestAppointmentID = clsDataAccessHelper.ConvertToInt(reader["TestAppointmentID"]),
                                TestTypeID = clsDataAccessHelper.ConvertToInt(reader["TestTypeID"]),
                                LocalDrivingLicenseApplicationID = clsDataAccessHelper.ConvertToInt(reader["LocalDrivingLicenseApplicationID"]),
                                AppointmentDate = clsDataAccessHelper.ConvertToDateTime(reader["AppointmentDate"]),
                                PaidFees = Convert.ToDecimal(reader["PaidFees"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"]),
                                IsLocked = clsDataAccessHelper.ConvertToBool(reader["IsLocked"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving last test appointment for application {localDrivingLicenseApplicationID} and test type {testTypeID}.", ex);
                }
            }

            return appointment;
        }

        // ========== 7. تحديث رسوم الموعد (لإعادة الاختبار) ==========
        public static int UpdateTestAppointmentFees(int appointmentID, decimal newFees)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateTestAppointmentFeesQuery;

                command.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = newFees;
                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = appointmentID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating fees for appointment ID {appointmentID}.", ex);
                }
            }

            return rowsAffected;
        }
    }
}
//private const string GetTestAppointmentsByApplicationIDAndTestTypeQuery = @"
//            SELECT 
//                TestAppointmentID,
//                TestTypeID,
//                LocalDrivingLicenseApplicationID,
//                AppointmentDate,
//                PaidFees,
//                CreatedByUserID,
//                IsLocked
//            FROM TestAppointments
//            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
//            AND TestTypeID = @TestTypeID
//            ORDER BY AppointmentDate DESC;
//        ";
