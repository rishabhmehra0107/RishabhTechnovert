using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Model
{
    public class Branch
    {
        [Key] public int Id { get; set; }

        public string BranchId { get; set; }

        public string BankId { get; set; }

        public string Location { get; set; }
    }
}