using System;
using System.Collections.Generic;
using Bank.Model;
namespace Bank.Contracts
{
    public interface ITransactionService
    {
        public bool AddTransaction(Transaction transaction, string accountNumber, string bankId);

        public bool RevertTransaction(string id, string accountNumber, string bankId);

        public List<Transaction> GetTransactionsByAccount(string accountNumber, string bankId);
    }
}
