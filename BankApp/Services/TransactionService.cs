using System;
using System.Linq;
using BankApp.Utilities;
namespace BankApp.Services
{
	public class TransactionService
	{
		Bank Bank;
		private Utility Utility { get; set; }

		public TransactionService(Bank bank, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
		}

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

			return InitialBalance;
		}
		/*
		public Tuple<double,DateTime,string> DepositHistory()
		{
			foreach (Transaction transaction in this.Bank.Transactions)
			{
				if (transaction.Type.Equals("Deposit"))

				{
					return new Tuple<double, DateTime, string>(transaction.Amount, transaction.CreateDate, transaction.ID);
				}
			}
			DateTime date = new DateTime();
			return new Tuple<double, DateTime, string>(0, date, "Invalid");
		}*/

		public void WithdrawHistory()
		{
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