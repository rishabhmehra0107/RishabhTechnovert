using System;
using static BankApp.Model.Constants;

namespace BankApp.Model
{
    public class Transaction
    {
        public string ID { get; set; }

        public string CreatedBy { get; set; }

        public double Amount { get; set; }

        public TransactionType Type { get; set; }

        public string AccountNumber { get; set; }

        public bool IsReverted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}