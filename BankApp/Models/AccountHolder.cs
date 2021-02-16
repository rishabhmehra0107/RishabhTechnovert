using System;
using System.Collections.Generic;
namespace BankApp
{
    public class AccountHolder : User
    {
        public string AccountNumber { get; set; }
        public double InitialBalance = 0;
        public string AccountType { get; set; }
    }
}
