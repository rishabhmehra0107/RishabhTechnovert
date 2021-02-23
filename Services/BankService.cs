using System;
using BankApp.Model;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
	public interface IBankService
	{
		public bool AddBranch(Branch branch);

		public double Withdraw(double amount, string accountNumber);

		public double Deposit(double amount, string accountNumber);

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2);
	}

	public class BankService : IBankService
	{
		public Bank Bank { get; set; }

		public BankService(Bank bank)
		{
			this.Bank = bank;
		}

        public BankService()
        {
        }

        public bool AddBranch(Branch branch)
        {
            try
            {
				branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
				this.Bank.Branches.Add(branch);
				return true;
			}
            catch (Exception)
            {
				return false;
            }
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
				return (double)TransactionStatus.InsufficientBalance;
            }

			return (double)TransactionStatus.Null;
		}

		public double Deposit(double amount, string accountNumber)
		{
			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
			if (accountHolder != null)
			{
				accountHolder.AvailableBalance = accountHolder.AvailableBalance + amount;
				return accountHolder.AvailableBalance;
			}

			return (double)TransactionStatus.Null;
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
