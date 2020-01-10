using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreapp;
using testapp.models;
using testapp.xmlmappers;

namespace testapp.dbmappers
{
    public class VehicleDbMapper
    {
        private static string selectAll = "SELECT rowid, * FROM Vehicles";
        private static string selectById = "SELECT rowid, * FROM Vehicles WHERE rowid=@Id";

        private static string insert =
            "INSERT INTO Vehicles(VehicleTypeId, VehicleBrandId, Title, Vin, LicensePlate, " +
            "Vintage, PurchasedOn, Price) values (@VehicleTypeId, @VehicleBrandId, @Title, @Vin, @LicensePlate, " +
            "@Vintage, @PurchasedOn, @Price)";

        private static string update = "UPDATE Vehicles SET VehicleTypeId=@VehicleTypeId, VehicleBrandId=@VehicleBrandId, " +
                                       "Title=@Title, Vin=@Vin, LicensePlate=@LicensePlate, Vintage=@Vintage, " +
                                       "PurchasedOn=@PurchasedOn, Price=@Price WHERE rowid=@Id";

        private static string delete = "DELETE FROM Vehicles WHERE rowid=@Id";


        public static List<VehicleModel> SelectAll(SQLiteConnection conn)
        {
            List<VehicleModel> result = new List<VehicleModel>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        VehicleModel vm = new VehicleModel()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            VehicleType = VehicleTypeXmlMapper.SelectById(appconfig.xmlTypes, Convert.ToInt32(reader["VehicleTypeId"])),
                            VehicleBrand = VehicleBrandXmlMapper.SelectById(appconfig.xmlBrands, Convert.ToInt32(reader["VehicleBrandId"])),
                            Title = reader["Title"].ToString(),
                            Vin = reader["Vin"].ToString(),
                            LicensePlate = reader["LicensePlate"].ToString(),
                            Vintage = Convert.ToInt16(reader["Vintage"]),
                            PurchasedOn = Convert.ToDateTime(reader["PurchasedOn"]),
                            Price = Convert.ToDecimal(reader["Price"].ToString())
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

        public static VehicleModel SelectById(SQLiteConnection conn, int id)
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
                        VehicleModel vm = new VehicleModel()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            VehicleType = VehicleTypeXmlMapper.SelectById(appconfig.xmlTypes, Convert.ToInt32(reader["VehicleTypeId"])),
                            VehicleBrand = VehicleBrandXmlMapper.SelectById(appconfig.xmlBrands, Convert.ToInt32(reader["VehicleBrandId"])),
                            Title = reader["Title"].ToString(),
                            Vin = reader["Vin"].ToString(),
                            LicensePlate = reader["LicensePlate"].ToString(),
                            Vintage = Convert.ToInt16(reader["Vintage"]),
                            PurchasedOn = Convert.ToDateTime(reader["PurchasedOn"]),
                            Price = Convert.ToDecimal(reader["Price"].ToString())
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

        public static int Insert(SQLiteConnection conn, VehicleModel vm)
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

        public static int Update(SQLiteConnection conn, VehicleModel vm)
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

        public static int Delete(SQLiteConnection conn, VehicleModel vm)
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
    }
}
