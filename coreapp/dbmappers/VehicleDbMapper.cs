using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using core.models;
using core.xmlmappers;

namespace core.dbmappers
{
    public class VehicleDbMapper
    {
        private static string selectAll = "SELECT rowid, * FROM Vehicles";
        private static string selectById = "SELECT rowid, * FROM Vehicles WHERE rowid=@Id";
        private static string selectByAdminId = "SELECT COUNT(rowid) FROM Vehicles WHERE AdminId=@AdminId";

        private static string selectDriversCountByAdminId =
            "SELECT COUNT(*) FROM (SELECT DriverId, COUNT(DriverId) FROM Vehicles WHERE AdminId=@AdminId GROUP BY DriverId)";

        private static string insert =
            "INSERT INTO Vehicles(VehicleTypeId, VehicleBrandId, Title, Vin, LicensePlate, " +
            "Vintage, PurchasedOn, Price, DriverId, AdminId) values (@VehicleTypeId, @VehicleBrandId, @Title, @Vin, @LicensePlate, " +
            "@Vintage, @PurchasedOn, @Price, @DriverId, @AdminId)";

        private static string update = "UPDATE Vehicles SET VehicleTypeId=@VehicleTypeId, VehicleBrandId=@VehicleBrandId, " +
                                       "Title=@Title, Vin=@Vin, LicensePlate=@LicensePlate, Vintage=@Vintage, " +
                                       "PurchasedOn=@PurchasedOn, Price=@Price, DriverId=@DriverId, AdminId=@AdminId " +
                                       "WHERE rowid=@Id";

        private static string delete = "DELETE FROM Vehicles WHERE rowid=@Id";


        public static List<Vehicle> SelectAll(SQLiteConnection conn)
        {
            List<Vehicle> result = new List<Vehicle>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Vehicle vm = new Vehicle()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            VehicleType = VehicleTypeXmlMapper.SelectById(Convert.ToInt32(reader["VehicleTypeId"])),
                            VehicleBrand = VehicleBrandXmlMapper.SelectById(Convert.ToInt32(reader["VehicleBrandId"])),
                            Title = reader["Title"].ToString(),
                            Vin = reader["Vin"].ToString(),
                            LicensePlate = reader["LicensePlate"].ToString(),
                            Vintage = Convert.ToInt16(reader["Vintage"]),
                            PurchasedOn = Convert.ToDateTime(reader["PurchasedOn"]),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Boss = UserBossDbMapper.SelectById(conn, Convert.ToInt32(reader["AdminId"])),
                            Driver = UserDriverDbMapper.SelectById(conn, Convert.ToInt32(reader["DriverId"]))
                        };
                        result.Add(vm);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static Vehicle SelectById(SQLiteConnection conn, int id)
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
                        Vehicle vm = new Vehicle()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            VehicleType = VehicleTypeXmlMapper.SelectById(Convert.ToInt32(reader["VehicleTypeId"])),
                            VehicleBrand = VehicleBrandXmlMapper.SelectById(Convert.ToInt32(reader["VehicleBrandId"])),
                            Title = reader["Title"].ToString(),
                            Vin = reader["Vin"].ToString(),
                            LicensePlate = reader["LicensePlate"].ToString(),
                            Vintage = Convert.ToInt16(reader["Vintage"]),
                            PurchasedOn = Convert.ToDateTime(reader["PurchasedOn"]),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Boss = UserBossDbMapper.SelectById(conn, Convert.ToInt32(reader["AdminId"])),
                            Driver = UserDriverDbMapper.SelectById(conn, Convert.ToInt32(reader["DriverId"]))
                        };
                        reader.Close();
                        return vm;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static int Insert(SQLiteConnection conn, Vehicle vm)
        {
            if (vm == null) return -2;

            using (SQLiteCommand cmd = new SQLiteCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@VehicleTypeId", vm.VehicleType.Id);
                cmd.Parameters.AddWithValue("@VehicleBrandId", vm.VehicleBrand.Id);
                cmd.Parameters.AddWithValue("@Title", vm.Title);
                cmd.Parameters.AddWithValue("@Vin", vm.Vin);
                cmd.Parameters.AddWithValue("@LicensePlate", vm.LicensePlate);
                cmd.Parameters.AddWithValue("@Vintage", vm.Vintage);
                cmd.Parameters.AddWithValue("@PurchasedOn", vm.PurchasedOn);
                cmd.Parameters.AddWithValue("@Price", vm.Price);
                cmd.Parameters.AddWithValue("@AdminId", vm.Boss.Id);
                cmd.Parameters.AddWithValue("@DriverId", vm.Driver.Id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Update(SQLiteConnection conn, Vehicle vm)
        {
            if (vm == null) return -2;
            if (vm.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@Id", vm.Id);
                cmd.Parameters.AddWithValue("@VehicleTypeId", vm.VehicleType.Id);
                cmd.Parameters.AddWithValue("@VehicleBrandId", vm.VehicleBrand.Id);
                cmd.Parameters.AddWithValue("@Title", vm.Title);
                cmd.Parameters.AddWithValue("@Vin", vm.Vin);
                cmd.Parameters.AddWithValue("@LicensePlate", vm.LicensePlate);
                cmd.Parameters.AddWithValue("@Vintage", vm.Vintage);
                cmd.Parameters.AddWithValue("@PurchasedOn", vm.PurchasedOn);
                cmd.Parameters.AddWithValue("@Price", vm.Price);
                cmd.Parameters.AddWithValue("@AdminId", vm.Boss.Id);
                cmd.Parameters.AddWithValue("@DriverId", vm.Driver.Id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.Update: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Delete(SQLiteConnection conn, Vehicle vm)
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
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static bool VinExists(SQLiteConnection conn, string vin)
        {
            if (String.IsNullOrEmpty(vin)) return false;

            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT COUNT(Vin) FROM Vehicles WHERE Vin = {vin}", conn))
            {
                try
                {
                    long count = (long) cmd.ExecuteScalar();
                    if (count > 0) return true;
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.Delete: {e.Message}");
                    return false;
                }
            }
            return false;
        }

        public static long SelectVehiclesByAdminId(SQLiteConnection conn, int adminId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(selectByAdminId, conn))
            {
                cmd.Parameters.AddWithValue("@AdminId", adminId);
                try
                {
                    long count = (long)cmd.ExecuteScalar();
                    return count;
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.SelectVehiclesByAdminId: {e.Message}");
                }
            }
            return -1;
        }

        public static long SelectDriversCountByAdminId(SQLiteConnection conn, int adminId)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(selectDriversCountByAdminId, conn))
            {
                cmd.Parameters.AddWithValue("@AdminId", adminId);
                try
                {
                    long count = (long)cmd.ExecuteScalar();
                    return count;
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: VehicleDbMapper.SelectDriversCountByAdminId: {e.Message}");
                }
            }
            return -1;
        }
    }
}
