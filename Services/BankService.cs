using System;
using Bank.Model;
using Bank.Services.BankStore;
using Bank.Contracts;
using static Bank.Model.Constants;
using Bank.Console.Data;

namespace Bank.Services
{
    public class BankService : IBankService
	{
		public DBContext DB { get; set; }
		public ITransactionService TransactionService { get; set; }

		public BankService(ITransactionService transactionService)
		{
			this.DB = new DBContext();
			this.TransactionService = transactionService;
		}

		public bool AddBank(Model.Bank bank)
        {
			try
			{
				bank.BankId = bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
				bank.SameBankIMPS = SameBankIMPS;
				bank.SameBankRTGS = SameBankRTGS;
				bank.DiffBankIMPS = DiffBankIMPS;
				bank.DiffBankRTGS = DiffBankRTGS;
				this.DB.Banks.Add(bank);
				this.DB.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool AddBranch(Branch branch, string bankId)
        {
			try
            {
				var bank = this.DB.Banks.Find(bankId);
				branch.BankId = bank.BankId;
				branch.BranchId = $"{bank.BankId} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
				this.DB.Branches.Add(branch);
				this.DB.SaveChanges();
				return true;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public double Withdraw(double amount, string accountNumber, string bankId, string id)
		{
			var bank = this.DB.Banks.Find(bankId);
            if (bank != null)
            {
				var accountHolder = this.DB.AccountHolders.Find(accountNumber);
				if (accountHolder != null && accountHolder.AvailableBalance >= amount)
				{
					accountHolder.AvailableBalance = accountHolder.AvailableBalance - amount;
					Transaction withdrawTransaction = new Transaction();
					withdrawTransaction.Type = TransactionType.Withdraw;
					withdrawTransaction.CreatedBy = id;
					withdrawTransaction.Amount = amount;
					this.TransactionService.AddTransaction(withdrawTransaction, accountNumber, bankId);
					this.DB.SaveChanges();
					return accountHolder.AvailableBalance;
				}
				else if (accountHolder != null && accountHolder.AvailableBalance < amount)
				{
					return (double)TransactionStatus.InsufficientBalance;
				}
			}
			
			return (double)TransactionStatus.Null;
		}

		public double Deposit(double amount, string accountNumber, string bankId, string id)
		{
			var bank = this.DB.Banks.Find(bankId);
            if (bank != null)
            {
				var accountHolder = this.DB.AccountHolders.Find(accountNumber);
				if (accountHolder != null)
				{
					accountHolder.AvailableBalance = accountHolder.AvailableBalance + amount;
					Transaction depositTransaction = new Transaction();
					depositTransaction.Type = TransactionType.Deposit;
					depositTransaction.CreatedBy = id;
					depositTransaction.Amount = amount;
					this.TransactionService.AddTransaction(depositTransaction, accountNumber, bankId);
					this.DB.SaveChanges();
					return accountHolder.AvailableBalance;
				}
			}

			return (double)TransactionStatus.Null;
		}

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2, string bankId)
		{
			var bank = this.DB.Banks.Find(bankId);
            if (bank != null)
            {
				var accountHolder1 = this.DB.AccountHolders.Find(accountNumber1);
				var accountHolder2 = this.DB.AccountHolders.Find(accountNumber2);
				if (accountHolder1 != null && accountHolder2 != null)
				{
					accountHolder1.AvailableBalance -= amount;
					accountHolder2.AvailableBalance += amount;
					this.DB.SaveChanges();
					return true;
				}
			}

			return false;
		}
	}
}
