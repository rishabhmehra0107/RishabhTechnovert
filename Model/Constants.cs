using System;
namespace BankApp.Model
{
    public static class Constants
    {
        public static double InitialBalance = 1000;
        public static int SameBankRTGS = 0;
        public static int SameBankIMPS = 5;
        public static int DifferentBankRTGS = 2;
        public static int DifferentBankIMPS = 6;

        public static class UserType
        {
            public static string AccountHolder = "AccountHolder";
            public static string Employee = "Employee";
        }

        public enum UserTypes
        {
            AccountHolder,
            Employee
        }

        public static class EmployeeType
        {
            public static string Staff = "Staff";
            public static string Admin = "Admin";
        }

        public enum EmployeeTypes
        {
            Staff,
            Admin
        }

        public static class AccountType
        {
            public static string Savings = "Savings";
            public static string Current = "Current";
        }

        public enum AccountTypes
        {
            Savings,
            Current
        }

        public static class TransactionType
        {
            public static string Deposit = "Deposit";
            public static string Withdraw = "Withdraw";
        }

        public enum TransactionTypes
        {
            Deposit,
            Withdraw
        }
    }
}
