using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreapp.models;
using testapp.models;

namespace testapp.dbmappers
{
    public class UserDriverDbMapper
    {
        private static string selectAll = "SELECT rowid, * FROM Users";
        private static string selectById = "SELECT rowid, * FROM Users WHERE rowid=@Id";

        private static string insert =
            "INSERT INTO Users(FirstName, LastName, Email, Phone, Company, Street, City, Zip, Active) " +
            "VALUES (@FirstName, @LastName, @Email, @Phone, @Company, @Street, @City, @Zip, @Active)";

        private static string update = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email, " +
                                       "Phone=@Phone, Company=@Company, Street=@Street, City=@City, Zip=@Zip, Active=@Active " +
                                       "WHERE rowid=@Id";

        private static string delete = "DELETE FROM Users WHERE rowid=@Id";


        public static List<UserDriverModel> SelectAll(SQLiteConnection conn)
        {
            List<UserDriverModel> result = new List<UserDriverModel>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserDriverModel um = new UserDriverModel()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Street = reader["Street"].ToString(),
                            City = reader["City"].ToString(),
                            Zip = reader["Zip"].ToString(),
                            Active = Convert.ToBoolean(reader["Active"])
                        };
                        result.Add(um);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserAdminDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static UserDriverModel SelectById(SQLiteConnection conn, int id)
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
                        UserDriverModel um = new UserDriverModel()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Street = reader["Street"].ToString(),
                            City = reader["City"].ToString(),
                            Zip = reader["Zip"].ToString(),
                            Active = Convert.ToBoolean(reader["Active"])
                        };
                        reader.Close();
                        return um;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: UserAdminDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static int Insert(SQLiteConnection conn, UserModel um)
        {
            if (um == null) return -2;

            using (SQLiteCommand cmd = new SQLiteCommand(insert, conn))
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
                    Trace.WriteLine($"EXCEPTION: UserAdminDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Update(SQLiteConnection conn, UserDriverModel um)
        {
            if (um == null) return -2;
            if (um.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(update, conn))
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
                    Trace.WriteLine($"EXCEPTION: UserAdminDbMapper.Update: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Delete(SQLiteConnection conn, UserDriverModel vm)
        {
            if (vm == null) return -2;
            if (vm.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", vm.Id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: UserAdminDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }
    }
}
