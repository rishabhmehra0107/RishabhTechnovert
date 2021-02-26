using System;
using Bank.Model;
using Bank.Services.BankStore;
using Bank.Contracts;
using static Bank.Model.Constants;

namespace Bank.Services
{
    public class BankService : IBankService
	{
		public Banks Banks { get; set; }

		public BankService(Banks banks)
		{
			this.Banks = banks;
		}

		public bool AddBank(Model.Bank bank)
        {
			try
			{
				bank.Id = bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
				bank.SameBankIMPS = SameBankIMPS;
				bank.SameBankRTGS = SameBankRTGS;
				bank.DiffBankIMPS = DiffBankIMPS;
				bank.DiffBankRTGS = DiffBankRTGS;
				this.Banks.Bank.Add(bank);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool AddBranch(Branch branch, string bankName)
        {
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
				branch.BankId = bank.Id;
				branch.Id = $"{bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
				bank.Branches.Add(branch);
				return true;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public double Withdraw(double amount, string accountNumber, string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
            if (bank != null)
            {
				var accountHolder = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
				if (accountHolder != null && accountHolder.AvailableBalance >= amount)
				{
					accountHolder.AvailableBalance = accountHolder.AvailableBalance - amount;
					return accountHolder.AvailableBalance;
				}
				else if (accountHolder != null && accountHolder.AvailableBalance < amount)
				{
					return (double)TransactionStatus.InsufficientBalance;
				}
			}
			
			return (double)TransactionStatus.Null;
		}

		public double Deposit(double amount, string accountNumber, string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
            if (bank != null)
            {
				var accountHolder = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
				if (accountHolder != null)
				{
					accountHolder.AvailableBalance = accountHolder.AvailableBalance + amount;
					return accountHolder.AvailableBalance;
				}
			}

			return (double)TransactionStatus.Null;
		}

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2, string bankName)
		{
			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
            if (bank != null)
            {
				var accountHolder1 = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber1));
				var accountHolder2 = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber2));
				if (accountHolder1 != null && accountHolder2 != null)
				{
					accountHolder1.AvailableBalance -= amount;
					accountHolder2.AvailableBalance += amount;
					return true;
				}
			}

			return false;
		}
	}
}
