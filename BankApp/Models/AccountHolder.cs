﻿using System;
using System.Collections.Generic;
namespace BankApp
{
    public class AccountHolder : User
    {
        public string AccountNumber { get; set; }
        public double InitialBalance { get; set; }
        public string AccountType { get; set; }
    }
}