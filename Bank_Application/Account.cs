using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Bank_Application
{
    public class Account
    {

		Bank bank = new Bank();
		User user;
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();
		public Account()
        {
        }
		public string userName { get; set; }
		public string password { get; set; }
		public string role = "Account Holder";
		public double initialBalance { get; set; }
		public string accountId { get; set; }

		public void setupAccount()
		{
			Console.WriteLine("Enter Account Holder username");
			userName = Console.ReadLine();
			Console.WriteLine("Enter Account Holder password");
			password = Console.ReadLine();
			initialBalance = 1000;
			accountId = userName.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			User users = new User() { UserName = userName, Password = password, Type = "Account Holder", Id = accountId };
			this.bank.Users.Add(users);

			Console.WriteLine("Username: {0} ,AccountID:{1}, Balance: {2}", userName, accountId, initialBalance);
			NextMenu();
		}

		public void NextMenu()
		{
			double balance = initialBalance;
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Deposit History\n4.Withdraw History\n5.Logout");
			string MenuList = Console.ReadLine();
			int NextChoice = Convert.ToInt32(MenuList);

			switch (NextChoice)
			{
				case 1:
					Withdraw();
					break;
				case 2:
					Deposit();
					break;

				case 3:
					DepositHistory();
					break;
				case 4:
					WithdrawHistory();
					break;
				case 5:
					Logout();
					break;
			}
		}

		public double Withdraw()
		{
			Console.WriteLine("Available Balance: {0}", initialBalance);
			Console.WriteLine("Withdraw Amount: ");
			double WithdrawAmt = Convert.ToDouble(Console.ReadLine());
			WithdrawList.Add(WithdrawAmt);
			initialBalance = initialBalance -= WithdrawAmt;
			Console.WriteLine(initialBalance);
			NextMenu();
			return WithdrawAmt;
		}

		public double Deposit()
		{
			Console.WriteLine("Available Balance: {0}", initialBalance);
			Console.WriteLine("Deposit Amount: ");
			double DepositAmt = Convert.ToDouble(Console.ReadLine());


			DepositList.Add(DepositAmt);

			initialBalance = initialBalance + DepositAmt;
			Console.WriteLine(initialBalance);
			NextMenu();
			return DepositAmt;
		}

		public void DepositHistory()
		{
			foreach (double i in DepositList)
			{
				Console.WriteLine("Deposit History: " + i);
			}
			NextMenu();
		}

		public void WithdrawHistory()
		{
			foreach (double i in WithdrawList)
			{
				Console.WriteLine("Withdraw History: " + i);
			}
			NextMenu();
		}

		public void Logout()
		{
			Console.WriteLine("Goodbye: " + userName);
		}

	}
}
