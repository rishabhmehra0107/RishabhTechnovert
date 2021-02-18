using System;
using BankApp.Model;

namespace BankApp.Services
{
	public class BankService
	{
		Bank Bank;

		public BankService(Bank bank)
		{
			this.Bank = bank;
		}

		public void AddBranch(Branch branch)
        {
			branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
			this.Bank.Branches.Add(branch);
		}

		public double Withdraw(double amount, AccountHolder accountHolder)
		{
			accountHolder.InitialBalance = accountHolder.InitialBalance - amount;

			return accountHolder.InitialBalance;
		}

		public double Deposit(double amount, AccountHolder accountHolder)
		{
			accountHolder.InitialBalance = accountHolder.InitialBalance + amount;

			return accountHolder.InitialBalance;
		}


		public void TransferAmount(double amount, AccountHolder accountHolder, AccountHolder accountHolder1)
		{
			accountHolder1.InitialBalance += amount;
			accountHolder.InitialBalance -= amount;
		}
	}
}
