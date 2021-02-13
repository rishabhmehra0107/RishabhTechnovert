using System;
using System.Collections.Generic;
using BankApp.Utilities;
namespace BankApp.Services
{
	public class TransactionService
	{
		Bank Bank;
		private Utility Utility { get; set; }
		public TransactionService(Bank bank)
		{
			this.Bank = bank;
			this.Utility = new Utility();
		}
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();

		public double InitialBalance = 1000;
	



		public double Withdraw(double withdrawAmt, string PresentUser)
		{
			Transaction transaction = new Transaction();
			transaction.Type = "Withdraw";
			transaction.CreateDate= DateTime.UtcNow;
			transaction.DoneBy = PresentUser;
			transaction.ID = "TXN" + this.Bank.Id+ this.Bank.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = withdrawAmt;
			InitialBalance = InitialBalance -= withdrawAmt;
			this.Bank.Transactions.Add(transaction);
			Console.WriteLine("New Balance: {0}", InitialBalance);

			return InitialBalance;
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
			InitialBalance = InitialBalance + depositAmt;
			this.Bank.Transactions.Add(transaction);
			Console.WriteLine("New Balance: {0}", InitialBalance);

			return InitialBalance;
		}

		public void DepositHistory()
		{
			Console.WriteLine("Deposit History");
			foreach (Transaction transaction in this.Bank.Transactions)
			{
				if (transaction.Type.Equals("Deposit"))

				{
					Console.WriteLine("Deposit Amount: {0}\nTransaction Date: {1}\nTransaction ID: {2}", transaction.Amount, transaction.CreateDate, transaction.ID);
				}
			}
		}

		public void WithdrawHistory()
		{
			Console.WriteLine("Withdraw History");
			foreach(Transaction transaction in this.Bank.Transactions)
            {
				if (transaction.Type.Equals("Withdraw"))

				{
					Console.WriteLine("Withdraw Amount: {0}\nTransaction Date: {1}\nTransaction ID: {2}",transaction.Amount,transaction.CreateDate,transaction.ID);
				}
			}
		
		}

		public void Logout()
		{
			Console.WriteLine("Goodbye");
		}
	}
}