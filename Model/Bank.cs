﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    public class Bank
    {
        public Bank()
        {
            this.AccountHolders = new List<AccountHolder>();
            this.Branches = new List<Branch>();
            this.Employees = new List<Employee>();
            this.Currency = new List<Currency>();
        }

        [Key] public string BankId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int SameBankRTGS { get; set; }

        public int SameBankIMPS { get; set; }

        public int DiffBankRTGS { get; set; }

        public int DiffBankIMPS { get; set; }

        public List<AccountHolder> AccountHolders { get; set; }

        public List<Branch> Branches { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Currency> Currency { get; set; }
    }
}