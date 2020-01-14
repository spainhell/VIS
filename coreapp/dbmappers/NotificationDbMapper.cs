using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.models;



namespace core.dbmappers
{
    public class NotificationDbMapper
    {
        private static string selectAll = "SELECT * FROM Notifications";
        private static string selectById = "SELECT * FROM Notifications WHERE Id=@Id";
        private static string selectByInspectionId = "SELECT * FROM Notifications WHERE InspectionId=@InspectionId";

        private static string insert =
            "INSERT INTO Notifications(InspectionId, Destination, SentOn, Delivered) VALUES (@InspectionId, @Destination, @SentOn, @Delivered)";

        private static string update = 
            "UPDATE Notifications SET InspectionId=@InspectionId, Destination=@Destination, SentOn=@SentOn, Delivered=@Delivered WHERE Id=@Id";

        private static string delete = "DELETE FROM Notifications WHERE Id=@Id";

        private static string generateNotifications =
            "SELECT I.rowid AS 'InspectionId', V.rowid AS 'VehicleId', V.title, V.LicensePlate, I.ValidTo, " +
            "CAST(julianday(I.ValidTo) - julianday('now') AS Integer) AS 'Days' FROM Inspections I " +
            "JOIN Vehicles V ON I.VehicleId = V.rowid WHERE julianday(I.ValidTo) - julianday('now') < @days " +
            "AND I.rowid NOT IN (SELECT InspectionId FROM Notifications)";

        public static List<Notification> SelectAll(SQLiteConnection conn)
        {
            List<Notification> result = new List<Notification>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification nm = new Notification()
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Inspection = InspectionDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionId"])),
                            Destination = reader["Destination"].ToString(),
                            SentOn = Convert.ToDateTime(reader["SentOn"]),
                            Delivered = Convert.ToDateTime(reader["Delivered"])
                        };
                        result.Add(nm);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static Notification SelectById(SQLiteConnection conn, int id)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(selectById, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification nm = new Notification()
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Inspection = InspectionDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionId"])),
                            Destination = reader["Destination"].ToString(),
                            SentOn = Convert.ToDateTime(reader["SentOn"]),
                            Delivered = Convert.ToDateTime(reader["Delivered"])
                        };
                        reader.Close();
                        return nm;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static Notification SelectByInspectionId(SQLiteConnection conn, int inspectionId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(selectByInspectionId, conn))
            {
                cmd.Parameters.AddWithValue("@InspectionId", inspectionId);
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification nm = new Notification()
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Inspection = InspectionDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionId"])),
                            Destination = reader["Destination"].ToString(),
                            SentOn = Convert.ToDateTime(reader["SentOn"]),
                            Delivered = Convert.ToDateTime(reader["Delivered"])
                        };
                        reader.Close();
                        return nm;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static int Insert(SQLiteConnection conn, Notification nm)
        {
            if (nm == null) return -2;

            using (SQLiteCommand cmd = new SQLiteCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@InspectionId", nm.Inspection.Id);
                cmd.Parameters.AddWithValue("@Destination", nm.Destination);
                cmd.Parameters.AddWithValue("@SentOn", nm.SentOn);
                cmd.Parameters.AddWithValue("@Delivered", nm.Delivered);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Update(SQLiteConnection conn, Notification nm)
        {
            if (nm == null) return -2;
            if (nm.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@Id", nm.Id);
                cmd.Parameters.AddWithValue("@InspectionId", nm.Inspection.Id);
                cmd.Parameters.AddWithValue("@Destination", nm.Destination);
                cmd.Parameters.AddWithValue("@SentOn", nm.SentOn);
                cmd.Parameters.AddWithValue("@Delivered", nm.Delivered);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.Update: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Delete(SQLiteConnection conn, Notification nm)
        {
            if (nm == null) return -2;
            if (nm.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", nm.Id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static List<Notification> GenerateNotifications(SQLiteConnection conn, int days)
        {
            List<Notification> result = new List<Notification>();
            using (SQLiteCommand cmd = new SQLiteCommand(generateNotifications, conn))
            {
                cmd.Parameters.AddWithValue("@days", days);
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Notification nm = new Notification()
                        {
                            Id = -1,
                            Inspection = InspectionDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionId"])),
                            Destination = "",
                            SentOn = DateTime.Now,
                            Delivered = DateTime.Now
                        };

                        var vehicle = VehicleDbMapper.SelectById(conn, Convert.ToInt32(reader["VehicleId"]));
                        var boss = UserBossDbMapper.SelectById(conn, vehicle.Boss.Id);
                        var email = boss.Email;
                        nm.Destination = email;

                        result.Add(nm);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: NotificationDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }
    }
}
