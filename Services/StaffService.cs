using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Bank.Model;
using Bank.Contracts;
using Bank.Services.BankStore;
using Bank.Console.Data;
using static Bank.Model.Constants;

namespace Bank.Services
{
	public class StaffService : IStaffService
	{
		public Banks Banks { get; set; }
		public AccountHolder User { get; set; }
		public DBContext DB { get; set; }

		public StaffService(Banks banks, AccountHolder user)
		{
			this.Banks = banks;
			this.User = user;
			this.DB = new DBContext();
		}

		public bool NewCurrency(Currency currency, string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			try
            {
				if (currency.Rate >= 0 && currency.Rate <= 250)
				{
					bank.Currency.Add(currency);
					DB.Currencies.Add(currency);
					DB.SaveChanges();

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
	}
}
