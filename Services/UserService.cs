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

		public void AddEmployee(Employee employee)
		{
            if (employee.Type.Equals(UserType.Admin))
            {
				employee.Id = $"{UserType.Admin} {this.Bank.Employees.Count + 1}";
				employee.EmployeeId = $"{this.Bank.Name} {employee.Id}";
				this.Bank.Employees.Add(employee);
			}
            else if (employee.Type.Equals(UserType.Staff))
            {
				employee.Id = $"{UserType.Staff} {this.Bank.Employees.Count + 1}";
				employee.EmployeeId = $"{this.Bank.Name} {employee.Id}";
				this.Bank.Employees.Add(employee);
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
			if (this.Bank.Employees.Any(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password && (staff.Type.Equals(UserType.Admin)|| staff.Type.Equals(UserType.Staff))))
			{
				user = this.Bank.Employees.Find(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password);
			}
			else if (this.Bank.AccountHolders.Any(account => account.UserName.ToLower() == username.ToLower() && account.Password == password && account.Type.Equals(UserType.AccountHolder)))
			{
				user = this.Bank.AccountHolders.Find(account => account.UserName.ToLower() == username.ToLower() && account.Password == password);
			}

			return user;
		}
	}
}