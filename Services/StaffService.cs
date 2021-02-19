using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using BankApp.Model;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
	public class StaffService
	{
		public Bank Bank { get; set; }
		public AccountHolder User { get; set; }

		public StaffService(Bank bank, AccountHolder user)
		{
			this.Bank = bank;
			this.User = user;
		}

		public void NewCurrency(string code,string name,int value)
		{
			Currency currency = new Currency();
			currency.Code = code;
			currency.Name = name;
			currency.Rate = value;
			if (currency.Rate >= 0 && currency.Rate <= 250)
			{
				this.Bank.Currency.Add(currency);
			}
		}

		public List<string> BankEmployees()
		{
			List<string> newList = new List<string>();
			newList = this.Bank.Employees.Select(staff => staff.UserName).ToList();

			return newList;
		}

		public List<string> BankAccountHolders()
		{
			List<string> newList = new List<string>();
			newList = this.Bank.AccountHolders.Select(accountHolder => accountHolder.UserName).ToList();

			return newList;
		}

		public void UpdateCharges(int rtgs, int imps, BankType type)
		{
            if (type.Equals(BankType.Same))
            {
				this.Bank.SameBankIMPS = imps;
				this.Bank.SameBankRTGS = rtgs;
			}
			else if (type.Equals(BankType.Different))
            {
				this.Bank.DifferentBankIMPS = imps;
				this.Bank.DifferentBankRTGS = rtgs;
			}
		}

		public void XmlData()
        {
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = new XElement("Branches");
			XElement xElement2 = new XElement("Staffs");
			XElement xElement3 = new XElement("AccountHolders");
			XElement xElement4 = new XElement("Currencies");

			xDocument = new XDocument(new XElement("Bank",xElement,xElement2,xElement3,xElement4));

			int i = 1;
			foreach (Branch branch in this.Bank.Branches)
			{
				xElement.Add(new XElement("BankBranch"+i, new XElement("BankName", this.Bank.Name), new XElement("Location", this.Bank.Location), new XElement("BankID", this.Bank.Id), new XElement("BranchLocation", branch.Location), new XElement("BranchID", branch.Id)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach (Employee employee in this.Bank.Employees)
            {
				xElement2.Add(new XElement("Employee"+i, new XElement("Name", employee.Name), new XElement("Type", employee.Type), new XElement("ID", employee.Id), new XElement("Username", employee.UserName), new XElement("Password", employee.Password)));
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
				xElement4.Add(new XElement("Currencies" + i, new XElement("Name", currency.Name), new XElement("Code", currency.Code), new XElement("INRValue", currency.Rate)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}
		}
	}
}