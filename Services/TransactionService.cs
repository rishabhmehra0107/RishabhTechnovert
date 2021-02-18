using System;
using System.Collections.Generic;
using BankApp.Model;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
    public class TransactionService
	{
		private Bank Bank { get; set; }

		public TransactionService(Bank bank)
		{
			this.Bank = bank;
		}

		public void AddTransaction(Transaction transaction, string accountNumber)
        {
			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
			transaction.CreatedOn = DateTime.UtcNow;
			transaction.ID = "TXN" + this.Bank.Id + accountHolder.Transactions.Count + DateTime.UtcNow.ToString("MMDDYYY");
			transaction.IsReverted = false;
			accountHolder.Transactions.Add(transaction);
		}

		public bool RevertTransaction(string id, DateTime date, string accountNumber)
		{

			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
			var transaction = accountHolder.Transactions.Find(element => element.ID == id && element.CreatedOn == date);

			return transaction.IsReverted = true;

		}

		public List<Transaction> GetCurrentUserTransactions(string accountNumber)
        {
			var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));

			return accountHolder.Transactions;
        }
	}
}