using System;
using System.Linq;
using Bank.Model;
using static Bank.Model.Constants;

namespace Bank.Services
{
	public class UserService
	{
		public Banks Bank { get; set; }

		public UserService(Banks bank)
		{
			this.Bank = bank;
		}

		public bool AddEmployee(Employee employee)
		{
            try
            {
				employee.Id = $"{employee.Type} {this.Bank.Employees.Count + 1}";
				employee.EmployeeId = $"{this.Bank.Name} {employee.Id}";

				if (employee.Type.Equals(UserType.Admin))
				{
					this.Bank.Employees.Add(employee);
				}
				else if (employee.Type.Equals(UserType.Staff))
				{
					this.Bank.Employees.Add(employee);
				}

				return true;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public bool AddAccountHolder(AccountHolder accountHolder)
		{
            try
            {
				accountHolder.AvailableBalance = Constants.InitialBalance;
				accountHolder.AccountNumber = accountHolder.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyyhhmmss");
				accountHolder.AccountType = AccountType.Savings;
				accountHolder.Id = $"{UserType.AccountHolder} {this.Bank.AccountHolders.Count + 1}";
				this.Bank.AccountHolders.Add(accountHolder);

				return true;
			}
            catch (Exception)
            {
				return false;
			}
		}

		public User LogIn(string username, string password)
		{
            try
            {
				var user = new User();
				if (this.Bank.Employees.Any(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password && (staff.Type.Equals(UserType.Admin) || staff.Type.Equals(UserType.Staff))))
				{
					user = this.Bank.Employees.Find(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password);
				}
				else if (this.Bank.AccountHolders.Any(account => account.UserName.ToLower() == username.ToLower() && account.Password == password && account.Type.Equals(UserType.AccountHolder)))
				{
					user = this.Bank.AccountHolders.Find(account => account.UserName.ToLower() == username.ToLower() && account.Password == password);
				}

				return user;
			}
            catch (Exception)
            {
				return null;
            }
		}
	}
}