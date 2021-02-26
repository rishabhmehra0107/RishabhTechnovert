using System;
using System.Collections.Generic;
using Bank.Model;
namespace Bank.Contracts
{
    public interface ITransactionService
    {
        public bool AddTransaction(Transaction transaction, string accountNumber, string bankName);

        public bool RevertTransaction(string id, DateTime date, string accountNumber, string bankName);

        public List<Transaction> GetTransactionsByAccount(string accountNumber, string bankName);
    }
}
