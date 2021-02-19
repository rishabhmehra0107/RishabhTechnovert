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
			if (accountHolder != null && accountHolder.AvailableBalance >= amount)
            {
				accountHolder.AvailableBalance = accountHolder.AvailableBalance - amount;

				return accountHolder.AvailableBalance;
			}
			else if (accountHolder != null && accountHolder.AvailableBalance < amount)
            {
				return -1;
            }

			return -2;
		}

		public double Deposit(double amount, string accountNumber)
		{
			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
			if (accountHolder != null)
			{
				accountHolder.AvailableBalance = accountHolder.AvailableBalance + amount;

				return accountHolder.AvailableBalance;
			}

			return -1;
		}

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2)
		{
			var accountHolder1 = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber1));
			var accountHolder2 = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber2));
            if (accountHolder1 != null && accountHolder2 != null)
            {
				accountHolder1.AvailableBalance -= amount;
				accountHolder2.AvailableBalance += amount;
				return true;
			}
			return false;
		}
	}
}