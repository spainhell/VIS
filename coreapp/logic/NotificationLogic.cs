using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using core.dbmappers;
using core.models;

namespace core.logic
{
    public class NotificationLogic
    {
        public static void Delete(SQLiteConnection conn, int id)
        {
            NotificationDbMapper.Delete(conn, id);
        }

        public static List<Notification> GetNotifications(SQLiteConnection conn, int days)
        {
            return NotificationDbMapper.GenerateNotifications(conn, 30);
        }

        public static bool SaveNotifications(SQLiteConnection conn, List<Notification> notifications)
        {
            SQLiteTransaction transaction;
            transaction = conn.BeginTransaction();

            try
            {
                foreach (Notification notification in notifications)
                {
                    int r = NotificationDbMapper.Insert(conn, notification);
                    if (r != 0) throw new Exception("error inserting nofitication to DB");
                }
                transaction.Commit();
            }

            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
            
            return true;
        }
    }
}
