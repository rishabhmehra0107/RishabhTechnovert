using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Bank.Model;
using static Bank.Model.Constants;

namespace Bank.Services
{
	public class StaffService
	{
		public Banks Bank { get; set; }
		public AccountHolder User { get; set; }

		public StaffService(Banks bank, AccountHolder user)
		{
			this.Bank = bank;
			this.User = user;
		}

		public bool NewCurrency(Currency currency)
		{
            try
            {
				if (currency.Rate >= 0 && currency.Rate <= 250)
				{
					this.Bank.Currency.Add(currency);

					return true;
				}

				return false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public List<string> BankEmployees()
		{
            try
            {
				List<string> employees = new List<string>();
				employees = this.Bank.Employees.Select(staff => staff.UserName).ToList();

				return employees;
			}
            catch (Exception)
            {
				return null;
            }
		}

		public List<string> BankAccountHolders()
		{
			try
			{
				List<string> accountHolders = new List<string>();
				accountHolders = this.Bank.AccountHolders.Select(accountHolder => accountHolder.UserName).ToList();

				return accountHolders;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public bool UpdateCharges(int rtgs, int imps, BankType type)
		{
            if (type.Equals(BankType.Same))
            {
				this.Bank.SameBankIMPS = imps;
				this.Bank.SameBankRTGS = rtgs;

				return true;
			}
			else if (type.Equals(BankType.Different))
            {
				this.Bank.DiffBankIMPS = imps;
				this.Bank.DiffBankRTGS = rtgs;

				return true;
			}

			return false;
		}

		public void XmlData()
        {
			XDocument xDocument = XDocument.Load("BankApp.Console.xml");
			XElement xElement = new XElement("Branches");
			XElement xElement2 = new XElement("Staff");
			XElement xElement3 = new XElement("AccountHolders");
			XElement xElement4 = new XElement("Currencies");

			xDocument = new XDocument(new XElement("Bank",xElement,xElement2,xElement3,xElement4));

			for (int i = 0; i < this.Bank.Branches.Count; i++)
			{
				var branch = this.Bank.Branches[i];
				xElement.Add(new XElement("BankBranch" + (i + 1), new XElement("BankName", this.Bank.Name), new XElement("Location", this.Bank.Location), new XElement("BankID", this.Bank.Id), new XElement("BranchLocation", branch.Location), new XElement("BranchID", branch.Id)));
				xDocument.Save("BankApp.Console.xml");
			}

			for (int i = 0; i < this.Bank.Employees.Count; i++)
			{
				var employee = this.Bank.Employees[i];
				xElement2.Add(new XElement("Employee" + (i + 1), new XElement("Name", employee.Name), new XElement("Type", employee.Type), new XElement("ID", employee.Id), new XElement("Username", employee.UserName), new XElement("Password", employee.Password)));
				xDocument.Save("BankApp.Console.xml");
			}

			for (int i = 0; i < this.Bank.AccountHolders.Count; i++)
			{
				var account = this.Bank.AccountHolders[i];
				xElement3.Add(new XElement("AccountHolders" + (i + 1), new XElement("Name", account.Name), new XElement("Type", account.Type), new XElement("AccountNumber", account.AccountNumber), new XElement("Username", account.UserName), new XElement("Password", account.Password), new XElement("ID", account.Id)));
				xDocument.Save("BankApp.Console.xml");
			}

			for (int i = 0; i < this.Bank.Currency.Count; i++)
			{
				var currency = this.Bank.Currency[i];
				xElement4.Add(new XElement("Currencies" + (i+1), new XElement("Name", currency.Name), new XElement("Code", currency.Code), new XElement("INRValue", currency.Rate)));
				xDocument.Save("BankApp.Console.xml");
			}
		}
	}
}
