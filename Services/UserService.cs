using System;
using System.Linq;
using Bank.Model;
using Bank.Services.BankStore;
using Bank.Contracts;
using static Bank.Model.Constants;
using Bank.Console.Data;

namespace Bank.Services
{
	public class UserService : IUserService
	{
		public DBContext DB { get; set; }

		public UserService()
		{
			this.DB = new DBContext();
		}

		public bool AddEmployee(Employee employee, string bankId)
		{
			try
            {
				var bank = this.DB.Banks.Find(bankId);
				employee.BankId = bank.BankId;
				employee.UserId = $"{employee.Type} {bank.Employees.Count + 1}";
				employee.AccountId = $"{bank.Name} {employee.UserId}";

				if (employee.Type.Equals(UserType.Admin) || employee.Type.Equals(UserType.Staff))
				{
					this.DB.Employees.Add(employee);
					this.DB.SaveChanges();
				}

				return true;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public bool AddAccountHolder(AccountHolder accountHolder, string bankId)
		{
			try
            {
				var bank = this.DB.Banks.Find(bankId);
				accountHolder.BankId = bank.BankId;
				accountHolder.AvailableBalance = Constants.InitialBalance;
				accountHolder.AccountId = accountHolder.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyyhhmmss");
				accountHolder.AccountType = AccountType.Savings;
				accountHolder.UserId = $"{UserType.AccountHolder} {bank.AccountHolders.Count + 1}";
				this.DB.AccountHolders.Add(accountHolder);
				this.DB.SaveChanges();

				return true;
			}
            catch (Exception)
            {
				return false;
			}
		}

		public User LogIn(string userId)
		{
			try
            {
				var employee = this.DB.Employees.Find(userId);
				if (employee != null)
					return employee;
                else
                {
					var accountHolder = this.DB.AccountHolders.Find(userId);
					return accountHolder;
                }
			}
            catch (Exception)
            {
				return null;
            }
		}
	}
}