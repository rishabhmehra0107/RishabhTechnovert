﻿using System;
using System.Collections.Generic;
namespace BankApp.Model
{
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public List<Transaction> Transactions = new List<Transaction>();
    }
}