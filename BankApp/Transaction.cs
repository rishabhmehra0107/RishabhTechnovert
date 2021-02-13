using System;
using System.Collections.Generic;

namespace BankApp
{
    public class Transaction
    {
        public string ID { get; set; }
        public string DoneBy { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public bool isReverted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
