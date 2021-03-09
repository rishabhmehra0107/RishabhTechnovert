using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    public class Employee : User
    {
        public string BranchId { get; set; }

        public string BankId { get; set; }
    }
}