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

		public void AddWithdrawTransaction(double amount, AccountHolder accountHolder)
        {
			Transaction transaction = new Transaction();
			transaction.Type = TransactionType.Withdraw;
			transaction.CreatedOn = DateTime.UtcNow;
			transaction.CreatedBy = accountHolder.Id;
			transaction.ID = "TXN" + this.Bank.Id + accountHolder.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = amount;
			accountHolder.Transactions.Add(transaction);
		}

		public void AddDepositTransaction(double amount, AccountHolder accountHolder)
        {
			Transaction transaction = new Transaction();
			transaction.Type = TransactionType.Deposit;
			transaction.CreatedOn = DateTime.UtcNow;
			transaction.CreatedBy = accountHolder.Id;
			transaction.ID = "TXN" + this.Bank.Id + accountHolder.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = amount;
			accountHolder.Transactions.Add(transaction);
		}

		public bool RevertTransaction(string id, DateTime date, AccountHolder accountHolder)
		{
			
			var transaction = accountHolder.Transactions.Find(element => element.ID == id && element.CreatedOn == date);

			return transaction.isReverted = true;

		}

		public List<Transaction> GetCurrentUserTransactions(AccountHolder accountHolder)
        {
			return accountHolder.Transactions;
        }
	}
}