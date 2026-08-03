using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DTOs;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        // ========== استعلامات SQL ==========
        private const string AddNewTestQuery = @"
            INSERT INTO Tests 
            (
                TestAppointmentID,
                TestResult,
                Notes,
                CreatedByUserID
            )
            VALUES 
            (
                @TestAppointmentID,
                @TestResult,
                @Notes,
                @CreatedByUserID
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string GetTestByIDQuery = @"
            SELECT 
                TestID,
                TestAppointmentID,
                TestResult,
                Notes,
                CreatedByUserID
            FROM Tests
            WHERE TestID = @TestID;
        ";

        private const string GetTestsByAppointmentIDQuery = @"
            SELECT 
                TestID,
                TestAppointmentID,
                TestResult,
                Notes,
                CreatedByUserID
            FROM Tests
            WHERE TestAppointmentID = @TestAppointmentID
            ORDER BY TestID DESC;
        ";

        private const string DoesPassTestTypeQuery = @"
            SELECT COUNT(*)
            FROM Tests T
            INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
            WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            AND TA.TestTypeID = @TestTypeID
            AND T.TestResult = 1;
        ";

        private const string GetLastTestResultByApplicationIDAndTestTypeQuery = @"
            SELECT TOP 1
                T.TestResult,
                T.Notes,
                T.TestID,
                T.CreatedByUserID,
                TA.AppointmentDate,
                TA.IsLocked
            FROM Tests T
            INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
            WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            AND TA.TestTypeID = @TestTypeID
            ORDER BY TA.AppointmentDate DESC;
        ";

        private const string GetFailedTestsCountQuery = @"
            SELECT COUNT(*)
            FROM Tests T
            INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
            WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            AND TA.TestTypeID = @TestTypeID
            AND T.TestResult = 0;
        ";

        private const string HasPassedAllTestsQuery = @"
            SELECT COUNT(DISTINCT TA.TestTypeID)
            FROM TestAppointments TA
            INNER JOIN Tests T ON TA.TestAppointmentID = T.TestAppointmentID
            WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            AND T.TestResult = 1;
        ";

        // ========== 1. إضافة نتيجة اختبار جديدة ==========
        public static int AddNewTest(clsTestDTO test)
        {
            int newTestID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewTestQuery;

                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = test.TestAppointmentID;
                command.Parameters.Add("@TestResult", SqlDbType.Bit).Value = test.TestResult;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = string.IsNullOrEmpty(test.Notes) ? (object)DBNull.Value : test.Notes;
                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = test.CreatedByUserID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newTestID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new test result.", ex);
                }
            }

            return newTestID;
        }

        // ========== 2. جلب اختبار محدد بالـ ID ==========
        public static clsTestDTO GetTestByID(int testID)
        {
            clsTestDTO test = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetTestByIDQuery;
                command.Parameters.Add("@TestID", SqlDbType.Int).Value = testID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            test = new clsTestDTO
                            {
                                TestID = clsDataAccessHelper.ConvertToInt(reader["TestID"]),
                                TestAppointmentID = clsDataAccessHelper.ConvertToInt(reader["TestAppointmentID"]),
                                TestResult = clsDataAccessHelper.ConvertToBool(reader["TestResult"]),
                                Notes = clsDataAccessHelper.ConvertToString(reader["Notes"]),
                                CreatedByUserID = clsDataAccessHelper.ConvertToInt(reader["CreatedByUserID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving test with ID {testID}.", ex);
                }
            }

            return test;
        }

        // ========== 3. جلب جميع الاختبارات لموعد محدد ==========
        public static DataTable GetTestsByAppointmentID(int appointmentID)
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetTestsByAppointmentIDQuery;
                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = appointmentID;

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
                    throw new Exception($"Error retrieving tests for appointment ID {appointmentID}.", ex);
                }
            }

            return dt;
        }

        // ========== 4. التحقق من نجاح المتقدم في نوع اختبار محدد ==========
        public static bool DoesPassTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            bool passed = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DoesPassTestTypeQuery;

                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;
                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    passed = count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking if applicant passed test type {testTypeID}.", ex);
                }
            }

            return passed;
        }

        // ========== 5. جلب آخر نتيجة اختبار لنوع اختبار محدد ==========
        public static DataTable GetLastTestResultByApplicationIDAndTestType(int localDrivingLicenseApplicationID, int testTypeID)
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLastTestResultByApplicationIDAndTestTypeQuery;

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
                    throw new Exception($"Error retrieving last test result for application {localDrivingLicenseApplicationID} and test type {testTypeID}.", ex);
                }
            }

            return dt;
        }

        // ========== 6. حساب عدد مرات الرسوب في اختبار معين ==========
        public static int GetFailedTestsCount(int localDrivingLicenseApplicationID, int testTypeID)
        {
            int failedCount = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetFailedTestsCountQuery;

                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;
                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

                try
                {
                    connection.Open();
                    failedCount = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error counting failed tests for application {localDrivingLicenseApplicationID} and test type {testTypeID}.", ex);
                }
            }

            return failedCount;
        }

        // ========== 7. التحقق من نجاح المتقدم في جميع الاختبارات الثلاثة ==========
        public static bool HasPassedAllTests(int localDrivingLicenseApplicationID)
        {
            bool passedAll = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = HasPassedAllTestsQuery;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                try
                {
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    // يجب أن يكون نجح في الأنواع الثلاثة (1=Vision, 2=Written, 3=Practical)
                    passedAll = count == 3;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking if applicant passed all tests for application {localDrivingLicenseApplicationID}.", ex);
                }
            }

            return passedAll;
        }
    }
}