using System;
using static BankApp.Model.Constants;
using System.Collections.Generic;
namespace BankApp.Model
{
    public class AccountHolder : User
    {
        public AccountHolder()
        {
            this.Transactions = new List<Transaction>();
        }
        public string AccountNumber { get; set; }
        public UserType Type { get; set; }
        public double InitialBalance { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
