using System;
using System.Linq;
using BankApp.Model;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
	public class UserService
	{
		Bank Bank;

		public UserService(Bank bank)
		{
			this.Bank = bank;
		}

		public void AddAdmin(Staff admin)
		{
			admin.Type = EmployeeTypes.Admin.ToString();
			admin.Id = "ID_" + this.Bank.Staffs.Count + 1;
			this.Bank.Staffs.Add(admin);
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

		public User LogIn(string username, string password)
		{
			var user = new User();
			if (this.Bank.Staffs.Any(element => element.UserName == username && element.Password == password && element.Type.Equals("Admin")))
			{
				user = this.Bank.Staffs.Find(element => element.UserName.Equals(username) && element.Password.Equals(password) && element.Type.Equals("Admin"));
			}
			else if (this.Bank.Staffs.Any(element => element.UserName == username && element.Password == password && element.Type.Equals("Staff")))
			{
				user = this.Bank.Staffs.Find(element => element.UserName.Equals(username) && element.Password.Equals(password) && element.Type.Equals("Staff"));
			}
			else if (this.Bank.AccountHolders.Any(element => element.UserName == username && element.Password == password))
			{
				user = this.Bank.AccountHolders.Find(element => element.UserName.Equals(username) && element.Password.Equals(password));
			}
			return user;
		}

	}
}
