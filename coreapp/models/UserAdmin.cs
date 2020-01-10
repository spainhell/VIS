using System;
using System.Collections.Generic;
using System.Text;

namespace coreapp.models
{
    public class UserAdmin : UserBossModel
    {
        public bool CanManageUsers { get; set; }
        public bool CanManageStations { get; set; }
        public bool CanManageNotifications { get; set; }
    }
}
