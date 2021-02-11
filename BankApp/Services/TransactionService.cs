using System;
using System.Collections.Generic;
using Bank_Application.Utilities;
namespace Bank_Application.Services
{
	public class TransactionService
	{
		Bank Bank;
		private Utility Utility { get; set; }
		public TransactionService()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
		}
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();

		public string UserName { get; set; }
		public string Password { get; set; }
		public double InitialBalance = 1000;
		public string AccountId { get; set; }


		public void nextMenu()
		{
			double balance = InitialBalance;
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Deposit History\n4.Withdraw History\n5.Logout");
			string menuList = Console.ReadLine();
			int nextChoice = Convert.ToInt32(menuList);

			switch (nextChoice)
			{
				case 1:
					withdraw();
					break;
				case 2:
					deposit();
					break;

				case 3:
					depositHistory();
					break;
				case 4:
					withdrawHistory();
					break;
				case 5:
					logout();
					break;
			}
		}

		public double withdraw()
		{
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			double withdrawAmt = this.Utility.getIntegerInput("Enter Withdraw Amount");
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			WithdrawList.Add(withdrawAmt);
			InitialBalance = InitialBalance -= withdrawAmt;
			Console.WriteLine("New Balance: {0}", InitialBalance);
			nextMenu();
			return withdrawAmt;
		}

		public double deposit()
		{
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			double depositAmt = this.Utility.getIntegerInput("Enter Deposit Amount");


			DepositList.Add(depositAmt);

			InitialBalance = InitialBalance + depositAmt;
			Console.WriteLine("New Balance: {0}", InitialBalance);
			nextMenu();
			return depositAmt;
		}

		public void depositHistory()
		{
			foreach (double i in DepositList)
			{
				Console.WriteLine("Deposit History: " + i);
			}
			nextMenu();
		}

		public void withdrawHistory()
		{
			foreach (double i in WithdrawList)
			{
				Console.WriteLine("Withdraw History: " + i);
			}
			nextMenu();
		}

		public void logout()
		{
			Console.WriteLine("Goodbye: " + UserName);
		}
	}
}