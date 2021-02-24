using System;

namespace Bank.Model
{
    public class Employee : User
    {
        public string BranchId { get; set; }

        public string EmployeeId { get; set; }
    }
}