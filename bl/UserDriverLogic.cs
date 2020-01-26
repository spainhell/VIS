using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using core.dbmappers;
using core.models;

namespace bl
{
    public class UserDriverLogic
    {
        public static List<UserDriver> FindAll(SQLiteConnection conn)
        {
            return UserDriverDbMapper.SelectAll(conn);
        }

        public static UserDriver FindById(SQLiteConnection conn, int id)
        {
            return UserDriverDbMapper.SelectById(conn, id);
        }

        public static int Insert(SQLiteConnection conn, UserDriver um)
        {
            return UserDriverDbMapper.Insert(conn, um);
        }

        public static int Update(SQLiteConnection conn, UserDriver um)
        {
            return UserDriverDbMapper.Update(conn, um);
        }
    }
}
