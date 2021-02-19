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

		public void AddEmployee(Staff staff)
		{
            if (staff.Type.Equals(UserType.Admin))
            {
				staff.Id = $"{UserType.Admin} {this.Bank.Staffs.Count + 1}";
				this.Bank.Staffs.Add(staff);
			}
            else if (staff.Type.Equals(UserType.Staff))
            {
				staff.Id = $"{UserType.Staff} {this.Bank.Staffs.Count + 1}";
				this.Bank.Staffs.Add(staff);
			}
		}

		public void AddAccountHolder(AccountHolder accountHolder)
		{
			accountHolder.Type = UserType.AccountHolder;
			accountHolder.AvailableBalance = Constants.InitialBalance;
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