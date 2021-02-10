using System;
using Bank_Application.Utilities;

namespace Bank_Application.Services
{
	public class AccountService
	{
		Bank Bank;
		Branch Branch;
		
		private Utility Utility { get; set; }
		private TransactionService Transaction { get; set; }
		private StaffService StaffService { get; set; }
		public AccountService()
		{
			this.Bank = new Bank();
			this.Branch = new Branch();
			this.Utility = new Utility();
			this.Transaction = new TransactionService();
			this.StaffService = new StaffService();
		}

		public string BankName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public double InitialBalance { get; set; }
		public string AccountId { get; set; }

		public void setupUserAccount()
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

		public void setupStaffAccount(string bankk, string user, string pass)
		{
			this.BankName = bankk;
			this.UserName = user;
			this.Password = pass;
			Branch.BankId = this.BankName.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			Branch.BankLocation = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Branch Location");
			Branch.Id = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Branch Id");

			Bank.Branches.Add(Branch);


			Console.WriteLine("Admin {0} present in the system ", user);
			this.StaffService.nextMenu();
		}
		
	}
}
