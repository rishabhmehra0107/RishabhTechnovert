using System;
using System.ComponentModel.DataAnnotations;
using static Bank.Model.Constants;

namespace Bank.Model
{
    public class Transaction
    {
        [Key] public int Id { get; set; }

        public string TransactionID { get; set; }

        public string CreatedBy { get; set; }

        public double Amount { get; set; }

        public TransactionType Type { get; set; }

        public string DestinationAccountNumber { get; set; }

        public string SourceAccountNumber { get; set; }

        public bool IsReverted { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}