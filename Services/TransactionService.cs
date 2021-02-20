using System;
using System.Collections.Generic;
using BankApp.Model;

namespace BankApp.Services
{
    public class TransactionService
	{
		private Bank Bank { get; set; }

		public TransactionService(Bank bank)
		{
			this.Bank = bank;
		}

		public bool AddTransaction(Transaction transaction, string accountNumber)
        {
            try
            {
				var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
				if (accountHolder == null)
					return false;

				transaction.CreatedOn = DateTime.UtcNow;
				transaction.ID = "TXN" + this.Bank.Id + accountHolder.Transactions.Count + DateTime.UtcNow.ToString("MMDDYYY");
				transaction.IsReverted = false;
				accountHolder.Transactions.Add(transaction);

				return true;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public bool RevertTransaction(string id, DateTime date, string accountNumber)
		{
            try
            {
				var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
				var transaction = accountHolder.Transactions.Find(element => element.ID == id && element.CreatedOn == date);
				if (accountHolder != null && transaction != null)
				{
					return transaction.IsReverted = true;
				}

				return transaction.IsReverted = false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public List<Transaction> GetTransactionsByAccount(string accountNumber)
        {
            try
            {
				var accountHolder = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
				if (accountHolder != null)
				{
					return accountHolder.Transactions;
				}

				return null;
			}
            catch (Exception)
            {
				return null;
            }
        }
	}
}