using System;
namespace BankApplication
{
    public class Admin
    {

        public string userName { get; set; }
        public string password { get; set; }
        public string userType { get; set; }
        
        public Admin(string username, string password, string type)
        {
            this.userName = username;
            this.password = password;
            this.userType = type;
           
        }

    }
}
