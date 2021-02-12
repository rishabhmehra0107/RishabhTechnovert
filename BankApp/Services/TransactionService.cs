using System;
using System.Collections.Generic;
using Bank_Application.Utilities;
namespace Bank_Application.Services
{
	public class TransactionService
	{
		private Utility Utility { get; set; }
		public TransactionService()
		{
			this.Utility = new Utility();
		}
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();

		public double InitialBalance = 1000;
	



		public double withdraw()
		{
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			double withdrawAmt = this.Utility.getIntegerInput("Enter Withdraw Amount");
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			WithdrawList.Add(withdrawAmt);
			InitialBalance = InitialBalance -= withdrawAmt;
			Console.WriteLine("New Balance: {0}", InitialBalance);
			return withdrawAmt;
		}

		public double deposit()
		{
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			double depositAmt = this.Utility.getIntegerInput("Enter Deposit Amount");


			DepositList.Add(depositAmt);

			InitialBalance = InitialBalance + depositAmt;
			Console.WriteLine("New Balance: {0}", InitialBalance);
			return depositAmt;
		}

		public void depositHistory()
		{
			foreach (double i in DepositList)
			{
				Console.WriteLine("Deposit History: " + i);
			}
		}

		public void withdrawHistory()
		{
			foreach (double i in WithdrawList)
			{
				Console.WriteLine("Withdraw History: " + i);
			}
		}

		public void logout()
		{
			Console.WriteLine("Goodbye");
		}
	}
}