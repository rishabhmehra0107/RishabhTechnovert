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
		public DBContext DB { get; set; }

		public StaffService()
		{
			this.DB = new DBContext();
		}

		public bool NewCurrency(Currency currency, string bankId)
		{
			var bank = this.DB.Banks.Find(bankId);
			try
            {
				if (currency.Rate >= 0 && currency.Rate <= 250)
				{
					this.DB.Currencies.Add(currency);
					this.DB.SaveChanges();

					return true;
				}

				return false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public List<Employee> BankEmployees()
		{
			try
            {
				List<Employee> employees = this.DB.Employees.ToList();
				return employees;
			}
            catch (Exception)
            {
				return null;
            }
		}

		public List<AccountHolder> BankAccountHolders()
		{
			try
			{
				List<AccountHolder> accountHolders = this.DB.AccountHolders.ToList();
				return accountHolders;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public bool UpdateCharges(int rtgs, int imps, TransferTo type, string bankId)
		{
			var bank = this.DB.Banks.Find(bankId);
			if (type.Equals(TransferTo.Same))
            {
				bank.SameBankIMPS = imps;
				bank.SameBankRTGS = rtgs;
				this.DB.SaveChanges();

				return true;
			}
			else if (type.Equals(TransferTo.Different))
            {
				bank.DiffBankIMPS = imps;
				bank.DiffBankRTGS = rtgs;
				this.DB.SaveChanges();

				return true;
			}

			return false;
		}
	}
}
