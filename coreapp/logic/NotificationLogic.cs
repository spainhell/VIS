using System;
using System.Collections.Generic;
using System.Text;
using core.dbmappers;

namespace core.logic
{
    public class NotificationLogic
    {
        public static void Delete(int id)
        {
            NotificationDbMapper.Delete(id);
        }

    }
}
