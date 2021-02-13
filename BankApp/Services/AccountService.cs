using System;
using System.Linq;
using System.Xml.Linq;
using Bank_Application.Utilities;

namespace Bank_Application.Services
{
	public class AccountService
	{
		Bank Bank;

		private Utility Utility { get; set; }
		private TransactionService Transaction { get; set; }
		private StaffService StaffService { get; set; }
		public AccountService(Bank bank, TransactionService transactionService, StaffService staffService)
		{
			this.Bank = bank;
			this.Utility = new Utility();
			this.Transaction = transactionService;
			this.StaffService = staffService;
		}

		public void setupUserAccount(string user, string pass)
		{
			AccountHolder accountHolder = new AccountHolder();
			accountHolder.UserName = user;
			accountHolder.Password = pass;
			accountHolder.InitialBalance = 1000;
			accountHolder.AccountNumber = accountHolder.UserName.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			//AccountHolder accountHolder1 = new AccountHolder() { UserName = accountHolder.UserName, Password = accountHolder.Password, Type = "AccountHolder", AccountId = accountHolder.AccountId };
			this.Bank.AccountHolders.Add(accountHolder);

			Console.WriteLine("Username: {0} ,AccountID:{1}, Balance: {2}", accountHolder.UserName, accountHolder.AccountNumber, accountHolder.InitialBalance);
		}

		public void setupStaffAccount(string user, string pass)
		{
			Admin admin = new Admin();
			admin.UserName = user;
			admin.Password = pass;
			this.Bank.Admins.Add(admin);

			Console.WriteLine("Employee {0} present in the system ", user);
		}
		public void createStaffAccount(Staff staff)
		{
			staff.Name = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Staff Name");
			staff.Type = "Employee";
			staff.Id = "45";
			this.Bank.Staffs.Add(staff);
		}
		public void createUserAccount(AccountHolder account)
		{
			account.Name = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Account Holder Name");
			account.Type = "AccountHolder";
			account.InitialBalance = 1000;
			account.AccountNumber = account.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			account.AccountType = "Savings account";
			account.Id = "31";
			this.Bank.AccountHolders.Add(account);
		}
		
		public void storeUpdateAccount(AccountHolder accountHolder,string username)
        {
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = xDocument.Root.Descendants("AccountHolders").Where(x => x.Element("Username").Value == username).FirstOrDefault();
			xElement.Element("Username").Value = accountHolder.UserName;
			xElement.Element("Name").Value = accountHolder.Name;
			xElement.Element("Password").Value = accountHolder.Password;
			xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
		}
		public void deleteStoredAccount(string username)
		{
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = xDocument.Root.Descendants("AccountHolders").Where(x => x.Element("Username").Value == username).FirstOrDefault();
			xElement.Remove();
			xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
		}
		/*
		public void storeCurrencyData(Currency currency)
        {
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			xDocument.Root.Add(new XElement("Currency", new XElement("Currency Code", currency.CurrencyCode), new XElement("Currency Name", currency.CurrencyName), new XElement("ConversionValueToINR", currency.ConvertToInr)));
			xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
		}*/
	}
}
