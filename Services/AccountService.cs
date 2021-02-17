using System;
using BankApp.Model;
using BankApp.Services.Utilities;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
	public class AccountService
	{
		Bank Bank;
		private Utility Utility { get; set; }
		private TransactionService Transaction { get; set; }
		private StaffService StaffService { get; set; }

		public AccountService(Bank bank, TransactionService transactionService, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
			this.Transaction = transactionService;
		}

		public void CreateStaffAccount(string username, string password, string name)
		{
			Staff staff = new Staff();
			staff.UserName = username;
			staff.Password = password;
			staff.Name = name;
			staff.Type = EmployeeTypes.Staff.ToString();
			staff.Id = "Staff_" + this.Bank.Staffs.Count + 1;
			this.Bank.Staffs.Add(staff);
		}

		public void CreateUserAccount(string username, string password, string name)
		{
			AccountHolder account = new AccountHolder();
			account.UserName = username;
			account.Password = password;
			account.Name = name;
			account.Type = UserTypes.AccountHolder.ToString();
			account.InitialBalance = Constants.InitialBalance;
			account.AccountNumber = account.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyyhhmmss");
			account.AccountType = AccountTypes.Savings.ToString();
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
