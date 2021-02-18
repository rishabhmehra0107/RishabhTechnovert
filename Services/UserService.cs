using System;
using System.Linq;
using BankApp.Model;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
	public class UserService
	{
		public Bank Bank { get; set; }

		public UserService(Bank bank)
		{
			this.Bank = bank;
		}

		public void AddAdmin(Staff admin)
		{
			admin.Type = UserType.Admin;
			admin.Id = $"{UserType.Admin} {this.Bank.Staffs.Count + 1}";
			this.Bank.Staffs.Add(admin);
		}

		public void CreateEmployee(Staff staff)
		{
			staff.Type = UserType.Staff;
			staff.Id = $"{UserType.Staff} {this.Bank.Staffs.Count + 1}";
			this.Bank.Staffs.Add(staff);
		}

		public void CreateAccountHolder(AccountHolder accountHolder)
		{
			accountHolder.Type = UserType.AccountHolder;
			accountHolder.InitialBalance = Constants.InitialBalance;
			accountHolder.AccountNumber = accountHolder.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyyhhmmss");
			accountHolder.AccountType = AccountType.Savings;
			accountHolder.Id = $"{UserType.AccountHolder} {this.Bank.AccountHolders.Count + 1}";
			this.Bank.AccountHolders.Add(accountHolder);
		}

		public User LogIn(string username, string password)
		{
			var user = new User();
			if (this.Bank.Staffs.Any(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password && (staff.Type.Equals(UserType.Admin)|| staff.Type.Equals(UserType.Staff))))
			{
				user = this.Bank.Staffs.Find(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password);
			}
			else if (this.Bank.AccountHolders.Any(account => account.UserName.ToLower() == username.ToLower() && account.Password == password && account.Type.Equals(UserType.AccountHolder)))
			{
				user = this.Bank.AccountHolders.Find(account => account.UserName.ToLower() == username.ToLower() && account.Password == password);
			}

			return user;
		}
	}
}