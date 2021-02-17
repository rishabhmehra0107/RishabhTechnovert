using System;
namespace BankApp.Model
{
    public static class Constants
    {
        public static double InitialBalance = 1000;
        public static int SameBankRtgs = 0;
        public static int SameBankImps = 5;
        public static int DifferentBankRtgs = 2;
        public static int DifferentBankImps = 6;

        public static class UserType
        {
            public static string AccountHolder = "AccountHolder";
            public static string Employee = "Employee";
            public static string Admin = "Admin";
        }

        public enum UserTypes
        {
            AccountHolder,
            Employee,
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
