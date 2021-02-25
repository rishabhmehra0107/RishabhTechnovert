using System;
using System.Collections.Generic;
using Bank.Model;
namespace Bank.Contracts
{
    public interface ITransactionService
    {
        public bool AddTransaction(Transaction transaction, string accountNumber);

        public bool RevertTransaction(string id, DateTime date, string accountNumber);

        public List<Transaction> GetTransactionsByAccount(string accountNumber);
    }
}
