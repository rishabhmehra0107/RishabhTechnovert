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

        public enum UserType
        {
            AccountHolder,
            Employee
        }

        public enum EmployeeType
        {
            Staff,
            Admin
        }

        public enum AccountType
        {
            Savings,
            Current
        }

        public enum TransactionType
        {
            Deposit,
            Withdraw
        }
    }
}
