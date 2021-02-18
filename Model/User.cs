﻿using System;
using static BankApp.Model.Constants;
namespace BankApp.Model
{
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
        public UserType Type { get; set; }
    }
}
