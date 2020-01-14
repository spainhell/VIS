using System;
using System.Collections.Generic;
using System.Text;

namespace core.models
{
    public class UserAdmin : UserBoss
    {
        public bool CanManageUsers { get; set; }
        public bool CanManageStations { get; set; }
        public bool CanManageNotifications { get; set; }
    }
}
