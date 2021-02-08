using System;
using System.Collections.Generic;

namespace BankApplication
{
    public class Staff
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string userType { get; set; }
        public string Id { get; set; }
        public Staff(string username, string password, string type, string Id)
        {
            this.userName = username;
            this.password = password;
            this.userType = type;
            this.Id = Id;
        }

    }
}