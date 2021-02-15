using System;
using System.Linq;
using System.Xml.Linq;
using BankApp.Utilities;

namespace BankApp.Services
{
	public class BankServices
	{
		Bank Bank;
		private Utility Utility { get; set; }
		private TransactionService Transaction { get; set; }
		private StaffService StaffService { get; set; }

		public BankServices(Bank bank, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
		}

		public void AddBranch(Branch branch)
        {
			branch.BankId = this.Bank.Id;
			branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
			this.Bank.Branches.Add(branch);
		}

		public void AddAdmin(Admin admin)
        {
			admin.Type = "Admin";
			admin.Id = "ID_" + this.Bank.Admins.Count + 1;
			this.Bank.Admins.Add(admin);
		}

		public User LogIn(string username, string password)
        {
			var user = new User();
			if(this.Bank.Admins.Any(element => element.UserName == username && element.Password == password))
            {
				user = this.Bank.Admins.Find(element => element.UserName.Equals(username) && element.Password.Equals(password));
			}
			else if(this.Bank.Staffs.Any(element => element.UserName == username && element.Password == password))
            {
				user = this.Bank.Staffs.Find(element => element.UserName.Equals(username) && element.Password.Equals(password));
			}
			else if(this.Bank.AccountHolders.Any(element => element.UserName == username && element.Password == password))
            {
				user = this.Bank.AccountHolders.Find(element => element.UserName.Equals(username) && element.Password.Equals(password));
			}

			return user;
        }
	}
}
