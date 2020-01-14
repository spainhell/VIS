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
    public class InspectionStationDbMapper
    {
        private static string selectAll = "SELECT rowid, * FROM InspectionStations";
        private static string selectById = "SELECT rowid, * FROM InspectionStations WHERE rowid=@Id";

        private static string insert =
            "INSERT INTO InspectionStations(Active, StationNumber, ZipCode, City, Street, Company, Phone, Email, Orp, District, Region, Url) " +
            "values (@Active, @StationNumber, @ZipCode, @City, @Street, @Company, @Phone, @Email, @Orp, @District, @Region, @Url)";

        private static string update = "UPDATE InspectionStations SET Active=@Active, StationNumber=@StationNumber, ZipCode=@ZipCode, " +
                                       "City=@City, Street=@Street, Company=@Company, Phone=@Phone, Email=@Email, Orp=@Orp, " +
                                       "District=@District, Region=@Region, Url=@Url WHERE rowid=@Id";

        private static string delete = "DELETE FROM InspectionStations WHERE rowid=@Id";


        public static List<InspectionStation> SelectAll(SQLiteConnection conn)
        {
            List<InspectionStation> result = new List<InspectionStation>();
            using (SQLiteCommand cmd = new SQLiteCommand(selectAll, conn))
            {
                SQLiteDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        InspectionStation ism = new InspectionStation()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"]),
                            StationNumber = reader["StationNumber"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            City = reader["City"].ToString(),
                            Street = reader["Street"].ToString(),
                            Company = reader["Company"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Orp = reader["Orp"].ToString(),
                            District = reader["District"].ToString(),
                            Region = reader["Region"].ToString(),
                            Url = reader["Url"].ToString()
                        };
                        result.Add(ism);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionStationDbMapper.SelectAll: {e.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }

            return result;
        }

        public static InspectionStation SelectById(SQLiteConnection conn, int id)
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
                        InspectionStation ism = new InspectionStation()
                        {
                            Id = Convert.ToInt32(reader["rowid"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"]),
                            StationNumber = reader["StationNumber"].ToString(),
                            ZipCode = reader["ZipCode"].ToString(),
                            City = reader["City"].ToString(),
                            Street = reader["Street"].ToString(),
                            Company = reader["Company"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Orp = reader["Orp"].ToString(),
                            District = reader["District"].ToString(),
                            Region = reader["Region"].ToString(),
                            Url = reader["Url"].ToString()
                        };
                        reader.Close();
                        return ism;
                    }
                }
                catch (Exception e)
                {
                    reader?.Close();
                    Trace.WriteLine($"EXCEPTION: InspectionStationDbMapper.SelectById: {e.Message}");
                }
            }
            return null;
        }

        public static int Insert(SQLiteConnection conn, InspectionStation im)
        {
            if (im == null) return -2;

            using (SQLiteCommand cmd = new SQLiteCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@Active", im.Active);
                cmd.Parameters.AddWithValue("@StationNumber", im.StationNumber);
                cmd.Parameters.AddWithValue("@ZipCode", im.ZipCode);
                cmd.Parameters.AddWithValue("@City", im.City);
                cmd.Parameters.AddWithValue("@Street", im.Street);
                cmd.Parameters.AddWithValue("@Company", im.Company);
                cmd.Parameters.AddWithValue("@Phone", im.Phone);
                cmd.Parameters.AddWithValue("@Email", im.Email);
                cmd.Parameters.AddWithValue("@Orp", im.Orp);
                cmd.Parameters.AddWithValue("@District", im.District);
                cmd.Parameters.AddWithValue("@Region", im.Region);
                cmd.Parameters.AddWithValue("@Url", im.Url);



                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionStationDbMapper.Insert: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Update(SQLiteConnection conn, InspectionStation im)
        {
            if (im == null) return -2;
            if (im.Id < 0) return -3;

            using (SQLiteCommand cmd = new SQLiteCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@Id", im.Id);
                cmd.Parameters.AddWithValue("@Active", im.Active);
                cmd.Parameters.AddWithValue("@StationNumber", im.StationNumber);
                cmd.Parameters.AddWithValue("@ZipCode", im.ZipCode);
                cmd.Parameters.AddWithValue("@City", im.City);
                cmd.Parameters.AddWithValue("@Street", im.Street);
                cmd.Parameters.AddWithValue("@Company", im.Company);
                cmd.Parameters.AddWithValue("@Phone", im.Phone);
                cmd.Parameters.AddWithValue("@Email", im.Email);
                cmd.Parameters.AddWithValue("@Orp", im.Orp);
                cmd.Parameters.AddWithValue("@District", im.District);
                cmd.Parameters.AddWithValue("@Region", im.Region);
                cmd.Parameters.AddWithValue("@Url", im.Url);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"EXCEPTION: InspectionStationDbMapper.Update: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }

        public static int Delete(SQLiteConnection conn, InspectionStation im)
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
                    Trace.WriteLine($"EXCEPTION: InspectionStationDbMapper.Delete: {e.Message}");
                    return -1;
                }
            }
            return 0;
        }
    }
}
