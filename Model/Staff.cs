using System;
using static BankApp.Model.Constants;

namespace BankApp.Model
{
    public class Staff : User
    {
        public string BranchId { get; set; }
        public EmployeeType Type { get; set; }
    }
}