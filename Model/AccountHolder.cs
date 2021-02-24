using System;
using static Bank.Model.Constants;
using System.Collections.Generic;
namespace Bank.Model
{
    public class AccountHolder : User
    {
        public AccountHolder()
        {
            this.Transactions = new List<Transaction>();
        }

        public string AccountNumber { get; set; }

        public double AvailableBalance { get; set; }

        public AccountType AccountType { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}