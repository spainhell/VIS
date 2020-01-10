using System;
using System.Collections.Generic;
using System.Text;
using testapp.models;

namespace coreapp.models
{
    public class UserBossModel : UserModel
    {
        //public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
