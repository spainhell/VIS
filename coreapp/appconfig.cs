using System;
using System.Collections.Generic;
using System.Text;

namespace core
{
    public static class appconfig
    {
        //public static string path = @"C:\Users\spanhel\Disk Google\VŠB\VIS\vis\data\";
        public static string path = @"C:\SHARE\vis\data\";
        public static string xmlTypes = path + @"VehicleTypes.xml";
        public static string xmlBrands = path + @"VehicleBrands.xml";
        public static string sqlite = "Data Source=" + path + @"db.sqlite";
    }
}
