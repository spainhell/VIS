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
    public class InspectionDbMapper
    {
        private static string selectAll = "SELECT rowid, * FROM Inspections";
        private static string selectById = "SELECT rowid, * FROM Inspections WHERE rowid=@Id";

        private static string selectAllByVehicleId = "SELECT rowid, * FROM Inspections WHERE VehicleId=@Id";

        private static string selectAllByVehicleAdminId =
            "SELECT * FROM Inspections WHERE VehicleId IN (SELECT rowid FROM Vehicles WHERE AdminId=@AdminId)";

        private static string insert =
            "INSERT INTO Inspections(VehicleId, InspectionDate, ValidTo, InspectionStationId, ProtocolNumber, " +
            "Tachometer, Price, Defects) values (@VehicleId, @InspectionDate, @ValidTo, @InspectionStationId, " +
            "@ProtocolNumber, @Tachometer, @Price, @Defects)";

        private static string update = "UPDATE Inspections SET VehicleId=@VehicleId, InspectionDate=@InspectionDate, " +
                                       "ValidTo=@ValidTo, InspectionStationId=@InspectionStationId, ProtocolNumber=@ProtocolNumber, " +
                                       "Tachometer=@Tachometer, Price=@Price, Defects=@Defects WHERE rowid=@Id";

        private static string delete = "DELETE FROM Inspections WHERE rowid=@Id";

        public static List<Inspection> SelectAll(SQLiteConnection conn)
        {
            List<Inspection> result = new List<Inspection>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Inspection im = new Inspection()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            Vehicle = VehicleDbMapper.SelectById(conn, Convert.ToInt32(reader["VehicleId"])),
                            InspectionDate = Convert.ToDateTime(reader["InspectionDate"]),
                            ValidTo = Convert.ToDateTime(reader["ValidTo"]),
                            InspectionStation = InspectionStationDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionStationId"])),
                            ProtocolNumber = reader["ProtocolNumber"].ToString(),
                            Tachometer = Convert.ToInt32(reader["Tachometer"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Defects = reader["Defects"].ToString()
                        };
                        result.Add(im);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static Inspection SelectById(SQLiteConnection conn, int id)
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
                        Inspection im = new Inspection()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            Vehicle = VehicleDbMapper.SelectById(conn, Convert.ToInt32(reader["VehicleId"])),
                            InspectionDate = Convert.ToDateTime(reader["InspectionDate"]),
                            ValidTo = Convert.ToDateTime(reader["ValidTo"]),
                            InspectionStation = InspectionStationDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionStationId"])),
                            ProtocolNumber = reader["ProtocolNumber"].ToString(),
                            Tachometer = Convert.ToInt32(reader["Tachometer"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Defects = reader["Defects"].ToString()
                        };
                        reader.Close();
                        return im;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static List<Inspection> SelectAllByVehicleId(SQLiteConnection conn, int id)
        {
            List<Inspection> result = new List<Inspection>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAllByVehicleId, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Inspection im = new Inspection()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            Vehicle = VehicleDbMapper.SelectById(conn, Convert.ToInt32(reader["VehicleId"])),
                            InspectionDate = Convert.ToDateTime(reader["InspectionDate"]),
                            ValidTo = Convert.ToDateTime(reader["ValidTo"]),
                            InspectionStation = InspectionStationDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionStationId"])),
                            ProtocolNumber = reader["ProtocolNumber"].ToString(),
                            Tachometer = Convert.ToInt32(reader["Tachometer"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Defects = reader["Defects"].ToString()
                        };
                        result.Add(im);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.SelectAllByVehicleId: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static int Insert(SQLiteConnection conn, Inspection im)
        {
            if (im == null) return -2;

            using (SQLiteCommand cmd = new SQLiteCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@VehicleId", im.Vehicle.Id);
                cmd.Parameters.AddWithValue("@InspectionDate", im.InspectionDate);
                cmd.Parameters.AddWithValue("@ValidTo", im.ValidTo);
                cmd.Parameters.AddWithValue("@InspectionStationId", im.InspectionStation.Id);
                cmd.Parameters.AddWithValue("@ProtocolNumber", im.ProtocolNumber);
                cmd.Parameters.AddWithValue("@Tachometer", im.Tachometer);
                cmd.Parameters.AddWithValue("@Price", im.Price);
                cmd.Parameters.AddWithValue("@Defects", im.Defects);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Update(SQLiteConnection conn, Inspection im)
        {
            if (im == null) return -2;
            if (im.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@Id", im.Id);
                cmd.Parameters.AddWithValue("@VehicleId", im.Vehicle.Id);
                cmd.Parameters.AddWithValue("@InspectionDate", im.InspectionDate);
                cmd.Parameters.AddWithValue("@ValidTo", im.ValidTo);
                cmd.Parameters.AddWithValue("@InspectionStationId", im.InspectionStation.Id);
                cmd.Parameters.AddWithValue("@ProtocolNumber", im.ProtocolNumber);
                cmd.Parameters.AddWithValue("@Tachometer", im.Tachometer);
                cmd.Parameters.AddWithValue("@Price", im.Price);
                cmd.Parameters.AddWithValue("@Defects", im.Defects);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.Update: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Delete(SQLiteConnection conn, Inspection im)
        {
            if (im == null) return -2;
            if (im.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Id", im.Id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static List<Inspection> SelectAllByVehicleAdminId(SQLiteConnection conn, int adminId)
        {
            List<Inspection> result = new List<Inspection>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAllByVehicleAdminId, conn))
            {
                SQLiteDataReader reader = null;
                cmd.Parameters.AddWithValue("@AdminId", adminId);
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Inspection im = new Inspection()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            Vehicle = VehicleDbMapper.SelectById(conn, Convert.ToInt32(reader["VehicleId"])),
                            InspectionDate = Convert.ToDateTime(reader["InspectionDate"]),
                            ValidTo = Convert.ToDateTime(reader["ValidTo"]),
                            InspectionStation = InspectionStationDbMapper.SelectById(conn, Convert.ToInt32(reader["InspectionStationId"])),
                            ProtocolNumber = reader["ProtocolNumber"].ToString(),
                            Tachometer = Convert.ToInt32(reader["Tachometer"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Defects = reader["Defects"].ToString()
                        };
                        result.Add(im);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionDbMapper.SelectAll: {e.Message}");
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
