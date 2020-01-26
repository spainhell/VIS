using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using core.dbmappers;
using core.models;

namespace bl
{
    public class InspectionStationLogic
    {
        public static List<InspectionStation> FindAll(SQLiteConnection conn)
        {
            return InspectionStationDbMapper.SelectAll(conn);
        }

        public static InspectionStation FindById(SQLiteConnection conn, int id)
        {
            return InspectionStationDbMapper.SelectById(conn, id);
        }
    }
}
