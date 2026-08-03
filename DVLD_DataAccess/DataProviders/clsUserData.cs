using DVLD_DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsUserData
    {
        // ========== استعلامات SQL ==========
        private const string GetUserByUsernameAndPasswordQuery = @"
            SELECT Users.*, People.FirstName, People.SecondName, People.ThirdName, People.LastName
            FROM Users
            INNER JOIN People ON Users.PersonID = People.PersonID
            WHERE Users.UserName = @UserName AND Users.Password = @Password;
        ";

        private const string AddNewUserQuery = @"
            INSERT INTO Users (PersonID, UserName, Password, IsActive)
            VALUES (@PersonID, @UserName, @Password, @IsActive);
            SELECT SCOPE_IDENTITY();
        ";

        private const string GetAllUsersQuery = @"
            SELECT 
                Users.UserID,
                Users.PersonID,
                People.FirstName + ' ' + People.SecondName + ' ' + ISNULL(People.ThirdName + ' ', '') + People.LastName AS FullName,
                Users.UserName,
                Users.IsActive
            
            FROM Users
            INNER JOIN People ON Users.PersonID = People.PersonID;
        ";

        private const string GetUserByIDQuery = @"
            SELECT Users.*, People.FirstName, People.SecondName, People.ThirdName, People.LastName
            FROM Users
            INNER JOIN People ON Users.PersonID = People.PersonID
            WHERE Users.UserID = @UserID;
        ";

        private const string UpdateUserQuery = @"
            UPDATE Users
            SET 
                PersonID = @PersonID,
                UserName = @UserName,
                Password = @Password,
                IsActive = @IsActive
            WHERE UserID = @UserID;
        ";

        private const string UpdatePasswordQuery = @"
            UPDATE Users
            SET Password = @NewPassword
            WHERE UserID = @UserID;
        ";

        private const string DeleteUserQuery = @"
            DELETE FROM Users WHERE UserID = @UserID;
        ";

        private const string IsPersonLinkedToUserQuery = @"
         SELECT 1 FROM Users WHERE PersonID = @PersonID;
         ";

        private const string IsUserExistsQuery = @"
         SELECT 1 FROM Users WHERE UserName = @UserName;
         ";

        // ========== 1. التحقق من تسجيل الدخول ==========
        public static clsUserDTO GetUserByUsernameAndPassword(string userName, string password)
        {
            clsUserDTO user = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetUserByUsernameAndPasswordQuery;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 255).Value = userName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = password;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new clsUserDTO
                            {
                                UserID = clsDataAccessHelper.ConvertToInt(reader["UserID"]),
                                PersonID = clsDataAccessHelper.ConvertToInt(reader["PersonID"]),
                                UserName = clsDataAccessHelper.ConvertToString(reader["UserName"]),
                                Password = clsDataAccessHelper.ConvertToString(reader["Password"]),
                                IsActive = clsDataAccessHelper.ConvertToBool(reader["IsActive"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during login validation.", ex);
                }
            }

            return user;
        }

        // ========== 2. إضافة مستخدم جديد ==========
        public static int AddNewUser(clsUserDTO user)
        {
            int newUserID = -1;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = AddNewUserQuery;
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = user.PersonID;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 255).Value = user.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = user.Password;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    newUserID = clsDataAccessHelper.ConvertToInt(result);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding new user.", ex);
                }
            }

            return newUserID;
        }

        // ========== 3. جلب قائمة جميع المستخدمين ==========
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetAllUsersQuery;

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
                    throw new Exception("Error retrieving users list.", ex);
                }
            }

            return dt;
        }

        // ========== 4. جلب مستخدم محدد بالـ ID ==========
        public static clsUserDTO GetUserByID(int userID)
        {
            clsUserDTO user = null;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = GetUserByIDQuery;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new clsUserDTO
                            {
                                UserID = clsDataAccessHelper.ConvertToInt(reader["UserID"]),
                                PersonID = clsDataAccessHelper.ConvertToInt(reader["PersonID"]),
                                UserName = clsDataAccessHelper.ConvertToString(reader["UserName"]),
                                Password = clsDataAccessHelper.ConvertToString(reader["Password"]),
                                IsActive = clsDataAccessHelper.ConvertToBool(reader["IsActive"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving user with ID {userID}.", ex);
                }
            }

            return user;
        }

        // ========== 5. تحديث بيانات المستخدم ==========
        public static int UpdateUser(clsUserDTO user)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdateUserQuery;
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = user.PersonID;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 255).Value = user.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = user.Password;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating user with ID {user.UserID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 6. تغيير كلمة المرور فقط ==========
        public static int UpdatePassword(int userID, string newPassword)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = UpdatePasswordQuery;
                command.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 255).Value = newPassword;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error updating password for user ID {userID}.", ex);
                }
            }

            return rowsAffected;
        }

        // ========== 7. حذف مستخدم ==========
        public static int DeleteUser(int userID)
        {
            int rowsAffected = 0;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = DeleteUserQuery;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error deleting user with ID {userID}.", ex);
                }
            }

            return rowsAffected;
        }
        // ========== التحقق من ارتباط الشخص بمستخدم ==========
        public static bool IsPersonLinkedToUser(int personID)
        {
            bool isLinked = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = IsPersonLinkedToUserQuery;
                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = personID;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    // إذا كانت النتيجة ليست Null، فهذا يعني أن الشخص مرتبط بمستخدم فعلاً
                    if (result != null)
                    {
                        isLinked = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during checking if person is linked to user.", ex);
                }
            }

            return isLinked;
        }
        // ========== التحقق من وجود اسم المستخدم ==========
        public static bool IsUserExists(string userName)
        {
            bool isFound = false;
            string connectionString = clsDataAccessSettings._connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = IsUserExistsQuery;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar, 255).Value = userName;

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    // إذا كانت النتيجة لا تساوي null، فهذا يعني أن اسم المستخدم موجود بالفعل
                    isFound = (result != null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error checking if user exists.", ex);
                }
            }

            return isFound;
        }
    }
}