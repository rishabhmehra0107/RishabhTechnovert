using System;
using System.ComponentModel.DataAnnotations;
using static Bank.Model.Constants;
namespace Bank.Model
{
    public class User
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        [Key] public string UserName { get; set; }

        [Key] public string Password { get; set; }

        public UserType Type { get; set; }
    }
}