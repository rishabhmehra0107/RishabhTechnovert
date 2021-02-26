using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Bank.Model;
using Bank.Contracts;
using Bank.Services.BankStore;
using static Bank.Model.Constants;

namespace Bank.Services
{
	public class StaffService : IStaffService
	{
		public Banks Banks { get; set; }
		public AccountHolder User { get; set; }

		public StaffService(Banks banks, AccountHolder user)
		{
			this.Banks = banks;
			this.User = user;
		}

		public bool NewCurrency(Currency currency, string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			try
            {
				if (currency.Rate >= 0 && currency.Rate <= 250)
				{
					bank.Currency.Add(currency);

					return true;
				}

				return false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public List<string> BankEmployees(string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			try
            {
				List<string> employees = new List<string>();
				employees = bank.Employees.Select(staff => staff.UserName).ToList();

				return employees;
			}
            catch (Exception)
            {
				return null;
            }
		}

		public List<string> BankAccountHolders(string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			try
			{
				List<string> accountHolders = new List<string>();
				accountHolders = bank.AccountHolders.Select(accountHolder => accountHolder.UserName).ToList();

				return accountHolders;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public bool UpdateCharges(int rtgs, int imps, BankType type, string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			if (type.Equals(BankType.Same))
            {
				bank.SameBankIMPS = imps;
				bank.SameBankRTGS = rtgs;

				return true;
			}
			else if (type.Equals(BankType.Different))
            {
				bank.DiffBankIMPS = imps;
				bank.DiffBankRTGS = rtgs;

				return true;
			}

			return false;
		}

		public void XmlData(string bankName)
        {
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			XDocument xDocument = XDocument.Load("BankApp.Console.xml");
			XElement xElement = new XElement("Branches");
			XElement xElement2 = new XElement("Staff");
			XElement xElement3 = new XElement("AccountHolders");
			XElement xElement4 = new XElement("Currencies");

			xDocument = new XDocument(new XElement("Bank",xElement,xElement2,xElement3,xElement4));

			for (int i = 0; i < bank.Branches.Count; i++)
			{
				var branch = bank.Branches[i];
				xElement.Add(new XElement("BankBranch" + (i + 1), new XElement("BankName", bank.Name), new XElement("Location", bank.Location), new XElement("BankID", bank.Id), new XElement("BranchLocation", branch.Location), new XElement("BranchID", branch.Id)));
				xDocument.Save("BankApp.Console.xml");
			}

			for (int i = 0; i < bank.Employees.Count; i++)
			{
				var employee = bank.Employees[i];
				xElement2.Add(new XElement("Employee" + (i + 1), new XElement("Name", employee.Name), new XElement("Type", employee.Type), new XElement("ID", employee.Id), new XElement("Username", employee.UserName), new XElement("Password", employee.Password)));
				xDocument.Save("BankApp.Console.xml");
			}

			for (int i = 0; i < bank.AccountHolders.Count; i++)
			{
				var account = bank.AccountHolders[i];
				xElement3.Add(new XElement("AccountHolders" + (i + 1), new XElement("Name", account.Name), new XElement("Type", account.Type), new XElement("AccountNumber", account.AccountNumber), new XElement("Username", account.UserName), new XElement("Password", account.Password), new XElement("ID", account.Id)));
				xDocument.Save("BankApp.Console.xml");
			}

			for (int i = 0; i < bank.Currency.Count; i++)
			{
				var currency = bank.Currency[i];
				xElement4.Add(new XElement("Currencies" + (i+1), new XElement("Name", currency.Name), new XElement("Code", currency.Code), new XElement("INRValue", currency.Rate)));
				xDocument.Save("BankApp.Console.xml");
			}
		}
	}
}
