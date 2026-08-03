using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_DTOs;

namespace DVLD_DataAccess
{
    public class clsTestTypeData
    {
        // ========== استعلامات SQL ==========
        private const string GetAllTestTypesQuery = @"
            SELECT 
                TestTypeID,
                TestTypeTitle,
                TestTypeDescription,
                TestTypeFees
            FROM TestTypes
            ORDER BY TestTypeID;
        ";

        private const string UpdateTestTypeQuery = @"
            UPDATE TestTypes
            SET 
                TestTypeTitle = @TestTypeTitle,
                TestTypeDescription = @TestTypeDescription,
                TestTypeFees = @TestTypeFees
            WHERE TestTypeID = @TestTypeID;
        ";

        private const string GetTestTypeByIDQuery = @"
            SELECT 
                TestTypeID,
                TestTypeTitle,
                TestTypeDescription,
                TestTypeFees
            FROM TestTypes
            WHERE TestTypeID = @TestTypeID;
        ";

        // ========== 1. جلب قائمة جميع أنواع الاختبارات ==========
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllTestTypesQuery;

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
                    throw new Exception("Error retrieving test types list.", ex);
                }
            }

            return dt;
        }

        // ========== 2. جلب نوع اختبار محدد بالـ ID ==========
        public static clsTestTypeDTO GetTestTypeByID(int testTypeID)
        {
            clsTestTypeDTO testType = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetTestTypeByIDQuery;
                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testTypeID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testType = new clsTestTypeDTO
                            {
                                TestTypeID = clsDataAccessHelper.ConvertToInt(reader["TestTypeID"]),
                                TestTypeTitle = clsDataAccessHelper.ConvertToString(reader["TestTypeTitle"]),
                                TestTypeDescription = clsDataAccessHelper.ConvertToString(reader["TestTypeDescription"]),
                                TestTypeFees = Convert.ToDecimal(reader["TestTypeFees"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving test type with ID {testTypeID}.", ex);
                }
            }

            return testType;
        }

        // ========== 3. تحديث بيانات نوع اختبار ==========
        public static int UpdateTestType(clsTestTypeDTO testType)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateTestTypeQuery;

                // إضافة المعاملات
                command.Parameters.Add("@TestTypeTitle", SqlDbType.NVarChar, 255).Value = testType.TestTypeTitle;
                command.Parameters.Add("@TestTypeDescription", SqlDbType.NVarChar, -1).Value = testType.TestTypeDescription ?? (object)DBNull.Value;
                command.Parameters.Add("@TestTypeFees", SqlDbType.Decimal).Value = testType.TestTypeFees;
                command.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = testType.TestTypeID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating test type with ID {testType.TestTypeID}.", ex);
                }
            }

            return rowsAffected;
        }
    }
}