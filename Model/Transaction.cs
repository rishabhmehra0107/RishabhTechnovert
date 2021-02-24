using System;
using static Bank.Model.Constants;

namespace Bank.Model
{
    public class Transaction
    {
        public string ID { get; set; }

        public string CreatedBy { get; set; }

        public double Amount { get; set; }

        public TransactionType Type { get; set; }

        public string DestinationAccountNumber { get; set; }

        public string SourceAccountNumber { get; set; }

        public bool IsReverted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}