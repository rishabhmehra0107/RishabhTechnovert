using System;
using System.Xml.Linq;
using System.Linq;
using BankApp.Utilities;
using System.Collections.Generic;

namespace BankApp.Services
{
	public class StaffService
	{
		Bank Bank;
		private TransactionService Transaction { get; set; }
		private AccountService AccountService { get; set; }
		private Utility Utility { get; set; }

		public StaffService(Bank bank, TransactionService transactionService, Utility utility)
		{
			this.Bank = bank;
			this.Transaction = transactionService;
			this.Utility = utility;
		}

		public void NewCurrency(string code,string name,int value)
		{
			Currency currency = new Currency();
			currency.Code = code;
			currency.Name = name;
			currency.InrValue = value;
			if (currency.InrValue >= 0 && currency.InrValue <= 250)
			{
				this.Bank.Currency.Add(currency);
			}
		}

		public List<string> BankEmployees()
		{
			List<string> newList = new List<string>();
			newList = this.Bank.Staffs.Select(s => s.UserName).ToList();
			return newList;
		}

		public List<string> BankAccountHolders()
		{
			List<string> newList = new List<string>();
			newList = this.Bank.AccountHolders.Select(s => s.UserName).ToList();
			return newList;
		}

		public void RevertTransaction(string id, DateTime date)
        {
			Transaction transaction = new Transaction();
			transaction = this.Bank.Transactions.Find(element => element.ID == id && element.CreateDate == date);
            {
				this.Bank.Transactions.Remove(transaction);
            }

		}

		public void UpdateChargesSameBank(int srtgs, int simps)
		{
			this.Bank.SameBankIMPSCharge = simps;
			this.Bank.SameBankRTGSCharge = srtgs;

		}
		public void UpdateChargesDifferentBank(int drtgs,int dimps)
        {
			this.Bank.DifferentBankIMPSCharge = dimps;
			this.Bank.DifferentBankRTGSCharge = drtgs;
        }


		public void XmlData()
        {
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = new XElement("Branches");
			XElement xElement1 = new XElement("Admins");
			XElement xElement2 = new XElement("Staffs");
			XElement xElement3 = new XElement("AccountHolders");
			XElement xElement4 = new XElement("Currencies");
			xDocument = new XDocument(new XElement("Bank",xElement,xElement1,xElement2,xElement3,xElement4));
			int i = 1;
			foreach (Branch branch in this.Bank.Branches)
			{
				xElement.Add(new XElement("BankBranch"+i, new XElement("BankName", this.Bank.Name), new XElement("Location", this.Bank.Location), new XElement("ID", this.Bank.Id), new XElement("BranchLocation", branch.Location), new XElement("BranchID", branch.Id)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach(Admin admin in this.Bank.Admins)
            {
				xElement1.Add(new XElement("Admin"+i, new XElement("Name", admin.Name), new XElement("Type", admin.Type), new XElement("ID", admin.Id), new XElement("Username", admin.UserName), new XElement("Password", admin.Password)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				++i;
			}

			i = 1;
			foreach (Staff staff in this.Bank.Staffs)
            {
				xElement2.Add(new XElement("Employee"+i, new XElement("Name", staff.Name), new XElement("Type", staff.Type), new XElement("ID", staff.Id), new XElement("Username", staff.UserName), new XElement("Password", staff.Password)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach (AccountHolder account in this.Bank.AccountHolders)
			{
				xElement3.Add(new XElement("AccountHolders"+i, new XElement("Name", account.Name), new XElement("Type", account.Type), new XElement("AccountNumber", account.AccountNumber), new XElement("Username", account.UserName), new XElement("Password", account.Password), new XElement("ID", account.Id)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach (Currency currency in this.Bank.Currency)
			{
				xElement4.Add(new XElement("Currencies" + i, new XElement("Name", currency.Name), new XElement("Code", currency.Code), new XElement("INRValue", currency.InrValue)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

		}
	}
}
