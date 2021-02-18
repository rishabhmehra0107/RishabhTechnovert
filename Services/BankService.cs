using System;
using BankApp.Model;

namespace BankApp.Services
{
	public class BankService
	{
		public Bank Bank { get; set; }

		public BankService(Bank bank)
		{
			this.Bank = bank;
		}

		public void AddBranch(Branch branch)
        {
			branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
			this.Bank.Branches.Add(branch);
		}

		public double Withdraw(double amount, string accountNumber)
		{
			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
			if (accountHolder != null && accountHolder.InitialBalance>=amount)
            {
				accountHolder.InitialBalance = accountHolder.InitialBalance - amount;

				return accountHolder.InitialBalance;
			}

			return -1;
		}

		public double Deposit(double amount, string accountNumber)
		{
			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
			if (accountHolder != null)
			{
				accountHolder.InitialBalance = accountHolder.InitialBalance + amount;

				return accountHolder.InitialBalance;
			}

			return -1;
		}

		public void TransferAmount(double amount, AccountHolder accountHolder, AccountHolder accountHolder1)
		{
			accountHolder1.InitialBalance += amount;
			accountHolder.InitialBalance -= amount;
		}
	}
}