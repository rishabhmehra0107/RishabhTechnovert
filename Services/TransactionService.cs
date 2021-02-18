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
			transaction.Type = TransactionTypes.Withdraw.ToString();
			transaction.CreateDate = DateTime.UtcNow;
			transaction.DoneBy = accountHolder.Type;
			transaction.ID = "TXN" + this.Bank.Id + accountHolder.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = amount;
			accountHolder.Transactions.Add(transaction);
		}

		public void AddDepositTransaction(double amount, AccountHolder accountHolder)
        {
			Transaction transaction = new Transaction();
			transaction.Type = TransactionTypes.Deposit.ToString();
			transaction.CreateDate = DateTime.UtcNow;
			transaction.DoneBy = accountHolder.Type;
			transaction.ID = "TXN" + this.Bank.Id + accountHolder.Transactions.Count;
			transaction.isReverted = false;
			transaction.Amount = amount;
			accountHolder.Transactions.Add(transaction);
		}

		public List<Transaction> GetCurrentUserTransactions(AccountHolder accountHolder)
        {
			return accountHolder.Transactions;
        }
	}
}