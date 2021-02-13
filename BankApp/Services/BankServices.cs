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
			this.Bank.Branches.Add(branch);
		}

		public void AddAdmin(Admin admin)
        {
			this.Bank.Admins.Add(admin);
		}
	}
}
