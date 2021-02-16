using System;
namespace BankApp
{
    public class SystemValues
    {
        public double InitialBalance = 1000;
        public string[] UserType  = { "Admin", "Employee", "AccountHolder" };
        public string AccountType = "SavingsAccount";
        public string[] TransactionType = { "Deposit", "Withdraw" };
    }
}
