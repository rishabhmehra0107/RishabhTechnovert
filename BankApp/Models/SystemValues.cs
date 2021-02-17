using System;
namespace BankApp
{
    public class SystemValues
    {
        public double InitialBalance = 1000;
        public int SbRtgs = 0;
        public int SbImps = 5;
        public int DbRtgs = 2;
        public int DbImps = 6;
        public string[] UserType  = { "Admin", "Employee", "AccountHolder" };
        public string AccountType = "SavingsAccount";
        public string[] TransactionType = { "Deposit", "Withdraw" };
    }
}
