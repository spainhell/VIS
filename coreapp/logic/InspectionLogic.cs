using System;
using System.Collections.Generic;
using System.Text;
using core.dbmappers;
using core.models;

namespace core.logic
{
    public class InspectionLogic
    {
        public static void Delete(int id)
        {
            List<Notification> notifications = NotificationDbMapper.SelectByInspectionId(id);
            foreach (Notification notify in notifications)
            {
                NotificationDbMapper.Delete(notify.Id);
            }
        }
    }
}
