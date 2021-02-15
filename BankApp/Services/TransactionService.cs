using System;
using BankApp.Utilities;

namespace BankApp.Services
{
    public class TransactionService
	{
		Bank Bank;
		Constants Constants;
		private Utility Utility { get; set; }

		public TransactionService(Bank bank, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
			this.Constants = new Constants();
		}

		public double Withdraw(double withdrawAmt, string PresentUser)
		{
			Transaction transaction = new Transaction();
			transaction.Type = "Withdraw";
			transaction.CreateDate= DateTime.UtcNow;
			transaction.DoneBy = PresentUser;
			transaction.ID = "TXN" + this.Bank.Id+ this.Bank.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = withdrawAmt;
			Constants.InitialBalance = Constants.InitialBalance -= withdrawAmt;
			this.Bank.Transactions.Add(transaction);
			
			return Constants.InitialBalance;
		}

		public double Deposit(double depositAmt, string PresentUser)
		{
			Transaction transaction = new Transaction();
			transaction.Type = "Deposit";
			transaction.CreateDate = DateTime.UtcNow;
			transaction.DoneBy = PresentUser;
			transaction.ID = "TXN" + this.Bank.Id + this.Bank.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = depositAmt;
			Constants.InitialBalance = Constants.InitialBalance + depositAmt;
			this.Bank.Transactions.Add(transaction);

			return Constants.InitialBalance;
		}
	}
}