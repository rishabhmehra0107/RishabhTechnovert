using System;
using static Bank.Model.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    public class AccountHolder : User
    {
        public AccountHolder()
        {
            this.Transactions = new List<Transaction>();
        }

        public double AvailableBalance { get; set; }

        public string BankId { get; set; }

        public AccountType AccountType { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}