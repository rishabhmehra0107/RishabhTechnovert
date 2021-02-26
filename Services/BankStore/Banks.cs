using System;
using System.Collections.Generic;

namespace Bank.Services.BankStore
{
    public class Banks
    {
        public Banks()
        {
            this.Bank = new List<Bank.Model.Bank>();
        }
        public List<Bank.Model.Bank> Bank { get; set; }
    }
}
