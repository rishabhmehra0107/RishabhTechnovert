using System;
using System.Collections.Generic;
using Bank.Model;
using Bank.Contracts;
using Bank.Services.BankStore;
using static Bank.Model.Constants;

namespace Bank.Services
{
    public class TransactionService : ITransactionService
	{
		public Banks Banks { get; set; }

		public TransactionService(Banks banks)
		{
			this.Banks = banks;
		}

		public bool AddTransaction(Transaction transaction, string accountNumber, string bankName)
        {
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
                if (bank != null)
                {
					var accountHolder = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));

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
					transaction.ID = "TXN" + bank.Id + accountHolder.Transactions.Count + DateTime.UtcNow.ToString("MMDDYYY");
					transaction.IsReverted = false;
					accountHolder.Transactions.Add(transaction);

					return true;
				}

				return false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public bool RevertTransaction(string id, DateTime date, string accountNumber, string bankName)
		{
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
				if (bank != null)
                {
					var accountHolder = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
					var transaction = accountHolder.Transactions.Find(element => element.ID == id && element.CreatedOn == date);
					if (accountHolder != null && transaction != null)
					{
						return transaction.IsReverted = true;
					}
				}

				return false;
			}
            catch (Exception)
            {
				return false;
            }
		}

		public List<Transaction> GetTransactionsByAccount(string accountNumber, string bankName)
        {
			try
            {
				var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
                if (bank != null)
                {
					var accountHolder = bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));
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