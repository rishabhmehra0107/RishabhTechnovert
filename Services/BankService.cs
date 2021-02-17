using System;
using System.Linq;
using System.Xml.Linq;
using BankApp.Model;
using BankApp.Services.Utilities;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
	public class BankService
	{
		Bank Bank;
		User User;
		private Utility Utility { get; set; }
		private StaffService StaffService { get; set; }

		public BankService(Bank bank, Utility utility, User user)
		{
			this.Bank = bank;
			this.User = user; 
			this.Utility = utility;
		}

		public void AddBranch(Branch branch)
        {
			branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
			this.Bank.Branches.Add(branch);
		}

		public void AddAdmin(Staff admin)
        {
			admin.Type = EmployeeTypes.Admin.ToString();
			admin.Id = "ID_" + this.Bank.Staffs.Count + 1;
			this.Bank.Staffs.Add(admin);
		}

		public double Withdraw(double amount, AccountHolder accountHolder)
		{
			Transaction transaction = new Transaction();
			transaction.Type = TransactionTypes.Withdraw.ToString();
			transaction.CreateDate = DateTime.UtcNow;
			transaction.DoneBy = accountHolder.Type;
			transaction.ID = "TXN" + this.Bank.Id + this.User.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = amount;
			accountHolder.InitialBalance = accountHolder.InitialBalance - amount;
			this.User.Transactions.Add(transaction);

			return accountHolder.InitialBalance;
		}

		public double Deposit(double amount, AccountHolder accountHolder)
		{
			Transaction transaction = new Transaction();
			transaction.Type = TransactionTypes.Deposit.ToString();
			transaction.CreateDate = DateTime.UtcNow;
			transaction.DoneBy = accountHolder.Type;
			transaction.ID = "TXN" + this.Bank.Id + this.User.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = amount;
			accountHolder.InitialBalance = accountHolder.InitialBalance + amount;
			this.User.Transactions.Add(transaction);

			return accountHolder.InitialBalance;
		}

		public User LogIn(string username, string password)
        {
			var user = new User();
			if(this.Bank.Staffs.Any(element => element.UserName == username && element.Password == password && element.Type.Equals("Admin")))
            {
				user = this.Bank.Staffs.Find(element => element.UserName.Equals(username) && element.Password.Equals(password) && element.Type.Equals("Admin"));
			}
			else if(this.Bank.Staffs.Any(element => element.UserName == username && element.Password == password && element.Type.Equals("Staff")))
            {
				user = this.Bank.Staffs.Find(element => element.UserName.Equals(username) && element.Password.Equals(password) && element.Type.Equals("Staff"));
			}
			else if(this.Bank.AccountHolders.Any(element => element.UserName == username && element.Password == password))
            {
				user = this.Bank.AccountHolders.Find(element => element.UserName.Equals(username) && element.Password.Equals(password));
			}
			return user;
		}
	}
}
