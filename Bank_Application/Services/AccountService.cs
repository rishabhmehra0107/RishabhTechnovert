using System;
using System.Collections.Generic;
using Bank_Application.Utilities;
using Bank_Application.Services;
namespace Bank_Application.Services
{
	public class AccountService
	{
		Bank Bank;
		private Utility Utility { get; set; }
		private Transaction Transaction { get; set; }
		public AccountService()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.Transaction = new Transaction();
		}
		public List<double> DepositList = new List<double>();
		public List<double> WithdrawList = new List<double>();

		public string UserName { get; set; }
		public string Password { get; set; }
		public string Role = "Account Holder";
		public double InitialBalance { get; set; }
		public string AccountId { get; set; }
		public void SetUpAccount()
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
			this.Transaction.nextMenu();
		}

	}
}
