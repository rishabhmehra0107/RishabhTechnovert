using System;
using BankApp.Model;
using BankApp.Services.Utilities;

namespace BankApp.Services
{
    public class TransactionService
	{
		Bank Bank;
		SystemValues Account;
		private Utility Utility { get; set; }

		public TransactionService(Bank bank, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
			this.Account = new SystemValues();
		}

		public double Withdraw(double withdrawAmt, string PresentUser)
		{
			Transaction transaction = new Transaction();
			transaction.Type = Account.TransactionType[1];
			transaction.CreateDate= DateTime.UtcNow;
			transaction.DoneBy = PresentUser;
			transaction.ID = "TXN" + this.Bank.Id+ this.Bank.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = withdrawAmt;
			Account.InitialBalance = Account.InitialBalance -= withdrawAmt;
			this.Bank.Transactions.Add(transaction);
			
			return Account.InitialBalance;
		}

		public double Deposit(double depositAmt, string PresentUser)
		{
			Transaction transaction = new Transaction();
			transaction.Type = Account.TransactionType[0];
			transaction.CreateDate = DateTime.UtcNow;
			transaction.DoneBy = PresentUser;
			transaction.ID = "TXN" + this.Bank.Id + this.Bank.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = depositAmt;
			Account.InitialBalance = Account.InitialBalance + depositAmt;
			this.Bank.Transactions.Add(transaction);

			return Account.InitialBalance;
		}
	}
}