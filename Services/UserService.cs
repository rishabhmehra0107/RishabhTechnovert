using System;
using System.Linq;
using Bank.Model;
using Bank.Services.BankStore;
using Bank.Contracts;
using static Bank.Model.Constants;

namespace Bank.Services
{
	public class UserService : IUserService
	{
		public Banks Banks { get; set; }

		public UserService(Banks banks)
		{
			this.Banks = banks;
		}

		public bool AddEmployee(Employee employee, string bankName)
		{
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
				employee.UserId = $"{employee.Type} {bank.Employees.Count + 1}";
				employee.EmployeeId = $"{bank.Name} {employee.UserId}";

				if (employee.Type.Equals(UserType.Admin) || employee.Type.Equals(UserType.Staff))
				{
					bank.Employees.Add(employee);
				}

				return true;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public bool AddAccountHolder(AccountHolder accountHolder, string bankName)
		{
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
				accountHolder.AvailableBalance = Constants.InitialBalance;
				accountHolder.AccountNumber = accountHolder.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyyhhmmss");
				accountHolder.AccountType = AccountType.Savings;
				accountHolder.UserId = $"{UserType.AccountHolder} {bank.AccountHolders.Count + 1}";
				bank.AccountHolders.Add(accountHolder);

				return true;
			}
            catch (Exception)
            {
				return false;
			}
		}

		public User LogIn(string bankName, string username, string password)
		{
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
				var user = new User();
				if (bank.Employees.Any(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password && (staff.Type.Equals(UserType.Admin) || staff.Type.Equals(UserType.Staff))))
				{
					user = bank.Employees.Find(staff => staff.UserName.ToLower() == username.ToLower() && staff.Password == password);
				}
				else if (bank.AccountHolders.Any(account => account.UserName.ToLower() == username.ToLower() && account.Password == password && account.Type.Equals(UserType.AccountHolder)))
				{
					user = bank.AccountHolders.Find(account => account.UserName.ToLower() == username.ToLower() && account.Password == password);
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