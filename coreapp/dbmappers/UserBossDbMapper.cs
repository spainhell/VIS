using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.models;


namespace core.dbmappers
{
    public class UserBossDbMapper
    {
        private static string selectAll = "SELECT UB.rowid, * FROM UserBosses UB INNER JOIN Users U " +
                                          "WHERE UB.UserId = U.rowid AND CanManageUsers IS NULL and CanManageNotifications IS NULL and CanManageStations IS NULL";
        private static string selectById = selectAll + " AND UB.rowid=@Id";

        private static string insertU =
            "INSERT INTO Users(FirstName, LastName, Email, Phone, Street, City, Zip, Active) " +
            "VALUES (@FirstName, @LastName, @Email, @Phone, @Street, @City, @Zip, @Active)";

        private static string insertUB =
            "INSERT INTO UserBosses(UserId, Login, Password) VALUES (@UserId, @Login, @Password)";

        private static string updateU = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email, " +
                                       "Phone=@Phone, Street=@Street, City=@City, Zip=@Zip, Active=@Active " +
                                       "WHERE rowid=@Id";

        private static string updateUB = "UPDATE UserBosses SET UserId=@UserId, Login=@Login, Password=@Password " +
                                        "WHERE rowid=@Id";

        private static string deleteU = "DELETE FROM Users WHERE rowid=@Id";

        private static string deleteUB = "DELETE FROM UserBosses WHERE rowid=@Id";


        public static List<UserBoss> SelectAll(SQLiteConnection conn)
        {
            List<UserBoss> result = new List<UserBoss>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserBoss um = new UserBoss()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Street = reader["Street"].ToString(),
                            City = reader["City"].ToString(),
                            Zip = reader["Zip"].ToString(),
                            Active = Convert.ToBoolean(reader["Active"]),
                            Login = reader["Login"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                        result.Add(um);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static UserBoss SelectById(SQLiteConnection conn, int id)
        {
            if (id < 0) return null;
            using (SQLiteCommand cmd = new SQLiteCommand(selectById, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserBoss um = new UserBoss()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Street = reader["Street"].ToString(),
                            City = reader["City"].ToString(),
                            Zip = reader["Zip"].ToString(),
                            Active = Convert.ToBoolean(reader["Active"]),
                            Login = reader["Login"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                        reader.Close();
                        return um;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static int Insert(SQLiteConnection conn, UserBoss um)
        {
            if (um == null) return -2;

            using (SQLiteCommand cmd = new SQLiteCommand(insertU, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", um.FirstName);
                cmd.Parameters.AddWithValue("@LastName", um.LastName);
                cmd.Parameters.AddWithValue("@Email", um.Email);
                cmd.Parameters.AddWithValue("@Phone", um.Phone);
                cmd.Parameters.AddWithValue("@Street", um.Street);
                cmd.Parameters.AddWithValue("@City", um.City);
                cmd.Parameters.AddWithValue("@Zip", um.Zip);
                cmd.Parameters.AddWithValue("@Active", um.Active);
                
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }

            long? lastId;
            using (SQLiteCommand cmd = new SQLiteCommand(@"select last_insert_rowid()", conn))
            {
                lastId = (long?)cmd.ExecuteScalar();
            }

            if (lastId == null)
            {
                return -1;
            }

            using (SQLiteCommand cmd = new SQLiteCommand(insertUB, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", lastId);
                cmd.Parameters.AddWithValue("@Login", um.Login);
                cmd.Parameters.AddWithValue("@Password", um.Password);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }

            return 0;
        }

        public static int Update(SQLiteConnection conn, UserBoss um)
        {
            if (um == null) return -2;
            if (um.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(updateU, conn))
            {
                cmd.Parameters.AddWithValue("@Id", um.Id);
                cmd.Parameters.AddWithValue("@FirstName", um.FirstName);
                cmd.Parameters.AddWithValue("@LastName", um.LastName);
                cmd.Parameters.AddWithValue("@Email", um.Email);
                cmd.Parameters.AddWithValue("@Phone", um.Phone);
                cmd.Parameters.AddWithValue("@Street", um.Street);
                cmd.Parameters.AddWithValue("@City", um.City);
                cmd.Parameters.AddWithValue("@Zip", um.Zip);
                cmd.Parameters.AddWithValue("@Active", um.Active);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.Update: {e.Message}");
                    return -1;
                }
            }

            using (SQLiteCommand cmd = new SQLiteCommand(updateUB, conn))
            {
                cmd.Parameters.AddWithValue("@Login", um.Login);
                cmd.Parameters.AddWithValue("@Password", um.Password);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.Update: {e.Message}");
                    return -1;
                }
            }

            return 0;
        }

        public static int Delete(SQLiteConnection conn, UserBoss ub)
        {
            if (ub == null) return -2;
            if (ub.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(deleteUB, conn))
            {
                cmd.Parameters.AddWithValue("@Id", ub.Id);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }

            using (SQLiteCommand cmd = new SQLiteCommand(deleteU, conn))
            {
                cmd.Parameters.AddWithValue("@Id", GetUserIdFromBossId(conn, ub.Id));
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserBossDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int GetUserIdFromBossId(SQLiteConnection conn, int BossId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT UserId FROM UserBosses WHERE rowid={BossId}", conn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
