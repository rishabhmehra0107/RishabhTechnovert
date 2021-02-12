using System;
using System.Collections.Generic;
namespace Bank_Application
{
    public class Bank
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<AccountHolder> AccountHolders = new List<AccountHolder>();
        public List<User> Users = new List<User>();
        public List<Branch> Branches = new List<Branch>();
        public List<Admin> Admins = new List<Admin>();
        public List<Staff> Staffs = new List<Staff>();
        public List<Currency> Currency = new List<Currency>();
    }
}