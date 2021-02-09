using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bank_Application.Services;
using Bank_Application.Utilities;

namespace Bank_Application
{
    public class Account
    {
		Bank Bank;
		private AccountService AccountService { get; set; }
		private Utility Utility { get; set; }
		public Account()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.AccountService = new AccountService();
		}
		
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();
		
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Role = "Account Holder";
		public double InitialBalance { get; set; }
		public string AccountId { get; set; }

		public void setupAccount()
		{
			string UserName = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Account Holder username");
			string Password = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password");
			
			InitialBalance = 1000;
			AccountId = UserName.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			User users = new User() { UserName = UserName, Password = Password, Type = "Account Holder", Id = AccountId };
			this.Bank.Users.Add(users);

			Console.WriteLine("Username: {0} ,AccountID:{1}, Balance: {2}", UserName, AccountId, InitialBalance);
			nextMenu();
		}

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
			Console.WriteLine("New Balance: {0}",InitialBalance);
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
