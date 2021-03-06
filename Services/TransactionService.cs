﻿using System;
using System.Collections.Generic;
using Bank.Model;
using Bank.Contracts;
using static Bank.Model.Constants;

namespace Bank.Services
{
    public class TransactionService : ITransactionService
	{
		private Banks Bank { get; set; }

		public TransactionService(Banks bank)
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
				else if (transaction.Type.Equals(TransactionType.Transfer))
                {
					transaction.SourceAccountNumber = accountNumber;
				}
				else if (transaction.Type.Equals(TransactionType.Deposit) || transaction.Type.Equals(TransactionType.Withdraw))
                {
					transaction.DestinationAccountNumber = accountNumber;
                }

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