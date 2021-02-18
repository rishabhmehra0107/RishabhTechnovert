using System;
using System.Collections.Generic;
namespace BankApp.Model
{
    public class Bank
    {
        public Bank()
        {
            this.AccountHolders = new List<AccountHolder>();
            this.Branches = new List<Branch>();
            this.Users = new List<User>();
            this.Staffs = new List<Staff>();
            this.Currency = new List<Currency>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int SameBankRTGS { get; set; }
        public int SameBankIMPS { get; set; }
        public int DifferentBankRTGS { get; set; }
        public int DifferentBankIMPS { get; set; }
        public List<AccountHolder> AccountHolders { get; set; }
        public List<User> Users { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Staff> Staffs { get; set; }
        public List<Currency> Currency { get; set; }
    }
}