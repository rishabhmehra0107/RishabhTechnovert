using System;
using System.Collections.Generic;
using Bank.Model;
using Bank.Contracts;
using Bank.Services.BankStore;
using static Bank.Model.Constants;
using Bank.Console.Data;

namespace Bank.Services
{
    public class TransactionService : ITransactionService
	{
		public Banks Banks { get; set; }
		public DBContext DB { get; set; }

		public TransactionService()
		{
			this.DB = new DBContext();
		}

		public bool AddTransaction(Transaction transaction, string accountId, string bankId)
        {
			try
            {
				var bank = this.DB.Banks.Find(bankId);
                if (bank != null)
                {
					var accountHolder = this.DB.AccountHolders.Find(accountId);

					if (accountHolder == null)
						return false;
					else if (transaction.Type.Equals(TransactionType.Transfer))
					{
						transaction.SourceAccountNumber = accountId;
					}
					else if (transaction.Type.Equals(TransactionType.Deposit) || transaction.Type.Equals(TransactionType.Withdraw))
					{
						transaction.DestinationAccountNumber = accountId;
					}

					transaction.CreatedOn = DateTime.UtcNow;
					transaction.TransactionID = "TXN" + bank.BankId + accountHolder.Transactions.Count + DateTime.UtcNow.ToString("MMDDYYY");
					transaction.IsReverted = false;
					this.DB.Transactions.Add(transaction);
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

		public bool RevertTransaction(string id, string accountNumber, string bankId)
		{
			try
            {
				var bank = this.DB.Banks.Find(bankId);
				if (bank != null)
                {
					var accountHolder = this.DB.AccountHolders.Find(accountNumber);
					var transaction = this.DB.Transactions.Find(id);
					if (accountHolder != null && transaction != null)
					{
						transaction.IsReverted = true;
						this.DB.SaveChanges();

						return true;
					}
				}

				return false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public List<Transaction> GetTransactionsByAccount(string accountNumber, string bankId)
        {
			try
            {
				var bank = this.DB.Banks.Find(bankId);
                if (bank != null)
                {
					var accountHolder = this.DB.AccountHolders.Find(accountNumber);
					if (accountHolder != null)
					{
						return accountHolder.Transactions;
					}
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