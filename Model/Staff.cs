﻿using System;

namespace BankApp.Model
{
    public class Staff : User
    {
        public string BranchId { get; set; }
        public string EmployeeId { get; set; }
    }
}