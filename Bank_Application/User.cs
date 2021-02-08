using System;
namespace BankApplication
{
    public class User
    {
     
        public string userName { get; set; }
        public string password { get; set; }
        public string userType { get; set; }
        public string Id { get; set; }
        public User(string username, string password, string type, string Id)
        {
            this.userName = username;
            this.password = password;
            this.userType = type;
            this.Id = Id;
        }
        
    }
}
