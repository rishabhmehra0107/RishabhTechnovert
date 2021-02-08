using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Bank_Application
{
    public class Account
    {

		Bank Bank = new Bank();
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();
		public Account()
        {
        }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Role = "Account Holder";
		public double InitialBalance { get; set; }
		public string AccountId { get; set; }

		public void setupAccount()
		{
			Console.WriteLine("Enter Account Holder username");
			UserName = Console.ReadLine();
			Console.WriteLine("Enter Account Holder password");
			Password = Console.ReadLine();
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
			Console.WriteLine("Withdraw Amount: ");
			double withdrawAmt = Convert.ToDouble(Console.ReadLine());
			WithdrawList.Add(withdrawAmt);
			InitialBalance = InitialBalance -= withdrawAmt;
			Console.WriteLine("New Balance: {0}",InitialBalance);
			nextMenu();
			return withdrawAmt;
		}

		public double deposit()
		{
			Console.WriteLine("Available Balance: {0}", InitialBalance);
			Console.WriteLine("Deposit Amount: ");
			double depositAmt = Convert.ToDouble(Console.ReadLine());


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
