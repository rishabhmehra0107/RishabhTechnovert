using System;
using System.Linq;
using System.Xml.Linq;
using BankApp.Utilities;

namespace BankApp.Services
{
	public class AccountService
	{
		Bank Bank;
		private Utility Utility { get; set; }
		private TransactionService Transaction { get; set; }
		private StaffService StaffService { get; set; }

		public AccountService(Bank bank, TransactionService transactionService, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
			this.Transaction = transactionService;
		}

		public void CreateStaffAccount(string username, string password, string name)
		{
			Staff staff = new Staff();
			staff.UserName = username;
			staff.Password = password;
			staff.Name = name;
			staff.Type = "Employee";
			staff.Id = "Staff_"+this.Bank.Staffs.Count + 1;
			this.Bank.Staffs.Add(staff);
		}
		public void CreateUserAccount(string username, string password, string name)
		{
			AccountHolder account = new AccountHolder();
			account.UserName = username;
			account.Password = password;
			account.Name = name;
			account.Type = "AccountHolder";
			account.InitialBalance = 1000;
			account.AccountNumber = account.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			account.AccountType = "Savings account";
			account.Id = "AccountHolder_"+this.Bank.AccountHolders.Count + 1;
			this.Bank.AccountHolders.Add(account);
		}
		
		public void StoreUpdateAccount(AccountHolder accountHolder,string username)
        {
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = xDocument.Root.Descendants("AccountHolders").Where(x => x.Element("Username").Value == username).FirstOrDefault();
			xElement.Element("Username").Value = accountHolder.UserName;
			xElement.Element("Name").Value = accountHolder.Name;
			xElement.Element("Password").Value = accountHolder.Password;
			xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
		}
		public void DeleteStoredAccount(string username)
		{
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = xDocument.Root.Descendants("AccountHolders").Where(x => x.Element("Username").Value == username).FirstOrDefault();
			xElement.Remove();
			xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
		}
	}
}
