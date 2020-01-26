using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using core.dbmappers;
using core.models;

namespace bl
{
    public class UserBossLogic
    {
        public static List<UserBoss> FindAll(SQLiteConnection conn)
        {
            return UserBossDbMapper.SelectAll(conn);
        }

        public static UserBoss FindById(SQLiteConnection conn, int id)
        {
            return UserBossDbMapper.SelectById(conn, id);
        }
    }
}
