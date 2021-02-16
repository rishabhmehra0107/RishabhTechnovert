using System;
using BankApp.Utilities;

namespace BankApp.Services
{
	public class AccountService
	{
		Bank Bank;
		SystemValues Account;
		private Utility Utility { get; set; }
		private TransactionService Transaction { get; set; }
		private StaffService StaffService { get; set; }

		public AccountService(Bank bank, TransactionService transactionService, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
			this.Transaction = transactionService;
			this.Account = new SystemValues();
		}

		public void CreateStaffAccount(string username, string password, string name)
		{
			Staff staff = new Staff();
			staff.UserName = username;
			staff.Password = password;
			staff.Name = name;
			staff.Type = Account.UserType[1];
			staff.Id = "Staff_" + this.Bank.Staffs.Count + 1;
			this.Bank.Staffs.Add(staff);
		}

		public void CreateUserAccount(string username, string password, string name)
		{
			AccountHolder account = new AccountHolder();
			account.UserName = username;
			account.Password = password;
			account.Name = name;
			account.Type = Account.UserType[2];
			account.InitialBalance = Account.InitialBalance;
			account.AccountNumber = account.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyyhhmmss");
			account.AccountType = Account.AccountType;
			account.Id = "AccountHolder_"+this.Bank.AccountHolders.Count + 1;
			this.Bank.AccountHolders.Add(account);
		}

		public void TransferAmount(double amount, AccountHolder accountHolder, AccountHolder accountHolder1)
        {
			accountHolder1.InitialBalance += amount;
			accountHolder.InitialBalance -= amount;
        }
		
	}
}
