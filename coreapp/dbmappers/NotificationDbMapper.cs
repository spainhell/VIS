﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testapp.models;

namespace testapp.dbmappers
{
    public class NotificationDbMapper
    {
        private static string selectAll = "SELECT * FROM Notifications";
        private static string selectById = "SELECT * FROM Notifications WHERE Id=@Id";
        private static string selectByInspectionId = "SELECT * FROM Notifications WHERE InspectionId=@InspectionId";

        private static string insert =
            "INSERT INTO Notifications(InspectionId, Destination, SentOn, Delivered) VALUES (@InspectionId, @Destination, @SentOn, @Delivered)";

        private static string update = "UPDATE Notifications SET InspectionId=@InspectionId, Destination=@Destination, SentOn=@SentOn, Delivered=@Delivered WHERE Id=@Id";

        private static string delete = "DELETE FROM Notifications WHERE Id=@Id";


        public static List<NotificationModel> SelectAll(SQLiteConnection conn)
        {
            List<NotificationModel> result = new List<NotificationModel>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NotificationModel nm = new NotificationModel()
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

        public static NotificationModel SelectById(SQLiteConnection conn, int id)
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
                        NotificationModel nm = new NotificationModel()
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

        public static NotificationModel SelectByInspectionId(SQLiteConnection conn, int inspectionId)
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
                        NotificationModel nm = new NotificationModel()
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

        public static int Insert(SQLiteConnection conn, NotificationModel nm)
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

        public static int Update(SQLiteConnection conn, NotificationModel nm)
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

        public static int Delete(SQLiteConnection conn, NotificationModel nm)
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
    }
}