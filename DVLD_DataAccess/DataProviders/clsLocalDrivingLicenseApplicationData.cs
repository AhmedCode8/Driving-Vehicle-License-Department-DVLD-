using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        // ========== استعلامات SQL ==========
        private const string GetAllLocalDrivingLicenseApplicationsQuery = @"
            SELECT 
                LDL.LocalDrivingLicenseApplicationID AS [L.D.L.AppID],
                LC.ClassName AS [Driving Class],
                P.NationalNo AS [National No.],
                P.FirstName + ' ' + P.SecondName + ' ' + ISNULL(P.ThirdName + ' ', '') + P.LastName AS [Full Name],
                A.ApplicationDate AS [Application Date],
                CASE 
                    WHEN A.ApplicationStatus = 1 THEN 'New'
                    WHEN A.ApplicationStatus = 2 THEN 'Cancelled'
                    WHEN A.ApplicationStatus = 3 THEN 'Completed'
                    ELSE 'Unknown'
                END AS [Status],
                (
                    SELECT COUNT(*)
                    FROM TestAppointments TA
                    INNER JOIN Tests T ON TA.TestAppointmentID = T.TestAppointmentID
                    WHERE TA.LocalDrivingLicenseApplicationID = LDL.LocalDrivingLicenseApplicationID
                    AND T.TestResult = 1
                ) AS [Passed Tests]
            FROM LocalDrivingLicenseApplications LDL
            INNER JOIN Applications A ON LDL.ApplicationID = A.ApplicationID
            INNER JOIN People P ON A.ApplicantPersonID = P.PersonID
            INNER JOIN LicenseClasses LC ON LDL.LicenseClassID = LC.LicenseClassID
            ORDER BY LDL.LocalDrivingLicenseApplicationID DESC;
        ";

        private const string GetLocalDrivingLicenseApplicationByIDQuery = @"
            SELECT 
                LocalDrivingLicenseApplicationID,
                ApplicationID,
                LicenseClassID
            FROM LocalDrivingLicenseApplications
            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
        ";

        private const string AddLocalDrivingLicenseApplicationQuery = @"
            INSERT INTO LocalDrivingLicenseApplications 
            (
                ApplicationID, 
                LicenseClassID
            )
            VALUES 
            (
                @ApplicationID, 
                @LicenseClassID
            );
            SELECT SCOPE_IDENTITY();
        ";

        private const string UpdateLocalDrivingLicenseApplicationQuery = @"
            UPDATE LocalDrivingLicenseApplications
            SET 
                LicenseClassID = @LicenseClassID
            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
        ";

        private const string DeleteLocalDrivingLicenseApplicationQuery = @"
            DELETE FROM LocalDrivingLicenseApplications 
            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
        ";

        private const string GetLocalDrivingLicenseApplicationByApplicationIDQuery = @"
            SELECT 
                LocalDrivingLicenseApplicationID,
                ApplicationID,
                LicenseClassID
            FROM LocalDrivingLicenseApplications
            WHERE ApplicationID = @ApplicationID;
        ";

        private const string GetPassedTestCountQuery = @"
              SELECT COUNT(*)
              FROM TestAppointments TA
              INNER JOIN Tests T ON TA.TestAppointmentID = T.TestAppointmentID
              WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                AND T.TestResult = 1; -- 1 تعني ناجح (Pass) ";

        private const string CheckApplicationStatusQuery = @"
            SELECT 1 
            FROM Applications 
            INNER JOIN LocalDrivingLicenseApplications
                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
            WHERE Applications.ApplicantPersonID = @ApplicantPersonID 
              AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID 
              AND Applications.ApplicationStatus = @ApplicationStatus;
           ";

        // ========== 1. جلب قائمة جميع الطلبات المحلية ==========
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllLocalDrivingLicenseApplicationsQuery;

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
                    throw new Exception("Error retrieving local driving license applications list.", ex);
                }
            }

            return dt;
        }

        // ========== 2. جلب طلب محلي محدد بالـ ID ==========
        public static clsLocalDrivingLicenseApplicationDTO GetLocalDrivingLicenseApplicationByID(int localDrivingLicenseApplicationID)
        {
            clsLocalDrivingLicenseApplicationDTO localApp = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLocalDrivingLicenseApplicationByIDQuery;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            localApp = new clsLocalDrivingLicenseApplicationDTO
                            {
                                LocalDrivingLicenseApplicationID = clsDataAccessHelper.ConvertToInt(reader["LocalDrivingLicenseApplicationID"]),
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                LicenseClassID = clsDataAccessHelper.ConvertToInt(reader["LicenseClassID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving local driving license application with ID {localDrivingLicenseApplicationID}.", ex);
                }
            }

            return localApp;
        }

        // ========== 3. جلب طلب محلي بواسطة ApplicationID ==========
        public static clsLocalDrivingLicenseApplicationDTO GetLocalDrivingLicenseApplicationByApplicationID(int applicationID)
        {
            clsLocalDrivingLicenseApplicationDTO localApp = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetLocalDrivingLicenseApplicationByApplicationIDQuery;
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            localApp = new clsLocalDrivingLicenseApplicationDTO
                            {
                                LocalDrivingLicenseApplicationID = clsDataAccessHelper.ConvertToInt(reader["LocalDrivingLicenseApplicationID"]),
                                ApplicationID = clsDataAccessHelper.ConvertToInt(reader["ApplicationID"]),
                                LicenseClassID = clsDataAccessHelper.ConvertToInt(reader["LicenseClassID"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving local driving license application with Application ID {applicationID}.", ex);
                }
            }

            return localApp;
        }

        // ========== 4. إضافة طلب محلي جديد ==========
        public static int AddLocalDrivingLicenseApplication(clsLocalDrivingLicenseApplicationDTO localApp)
        {
            int newLocalAppID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddLocalDrivingLicenseApplicationQuery;

                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = localApp.ApplicationID;
                command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = localApp.LicenseClassID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newLocalAppID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new local driving license application.", ex);
                }
            }

            return newLocalAppID;
        }

        // ========== 5. تحديث طلب محلي (تغيير صنف الرخصة) ==========
        public static int UpdateLocalDrivingLicenseApplication(clsLocalDrivingLicenseApplicationDTO localApp)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateLocalDrivingLicenseApplicationQuery;

                command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = localApp.LicenseClassID;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localApp.LocalDrivingLicenseApplicationID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating local driving license application with ID {localApp.LocalDrivingLicenseApplicationID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 6. حذف طلب محلي (الابن فقط) ==========
        public static int DeleteLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DeleteLocalDrivingLicenseApplicationQuery;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) // Foreign Key Violation
                    {
                        throw new Exception("Cannot delete this local application because it has related test appointments.", ex);
                    }
                    throw new Exception($"Error deleting local driving license application with ID {localDrivingLicenseApplicationID}.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting local driving license application with ID {localDrivingLicenseApplicationID}.", ex);
                }
            }

            return rowsAffected;
        }

        private static bool _CheckApplicationStatus(int applicantPersonID, int licenseClassID, byte applicationStatus)
        {
            bool exists = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = CheckApplicationStatusQuery;

                // تمرير البارامترات الثلاثة
                command.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicantPersonID;
                command.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = licenseClassID;
                command.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = applicationStatus; // 🌟 الحالة هنا ديناميكية

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    exists = (result != null);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking application status for Person ID {applicantPersonID}.", ex);
                }
            }

            return exists;
        }

        // ========== 1. التحقق من وجود طلب نشط (حالة 1) ==========
        public static bool DoesPersonHaveActiveApplicationForLicenseClass(int applicantPersonID, int licenseClassID)
        {
            // نستدعي الدالة الكبيرة ونمرر لها الحالة 1 (New / Active)
            return _CheckApplicationStatus(applicantPersonID, licenseClassID, 1);
        }

        // ========== 2. التحقق مما إذا كانت الرخصة ممتلكة مسبقاً (حالة 3) ==========
        public static bool IsLicenseClassAlreadyObtained(int applicantPersonID, int licenseClassID)
        {
            // نستدعي الدالة الكبيرة ونمرر لها الحالة 3 (Completed)
            return _CheckApplicationStatus(applicantPersonID, licenseClassID, 3);
        }

        // ========== جلب عدد الاختبارات المجتازة بنجاح ==========
        public static int GetPassedTestCount(int localDrivingLicenseApplicationID)
        {
            int passedTestCount = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetPassedTestCountQuery;
                command.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = localDrivingLicenseApplicationID;

                try
                {
                    connection.Open();

                    // نستخدم ExecuteScalar لأننا نريد الحصول على قيمة واحدة فقط وهي الـ Count
                    object result = command.ExecuteScalar();

                    // نستخدم كلاس المساعد الخاص بك لتحويل القيمة بأمان لـ int
                    passedTestCount = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving passed tests count for Local Driving License Application ID {localDrivingLicenseApplicationID}.", ex);
                }
            }

            return passedTestCount;
        }


    }
}
