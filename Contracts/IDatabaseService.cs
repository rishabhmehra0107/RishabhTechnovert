using System;
using System.Collections.Generic;
using Bank.Model;

namespace Bank.Contracts
{
    public interface IDatabaseService
    {
        public List<AccountHolder> GetAccountHolders();

        public void UpdateAccountHolder(string username, string password, string newname);

        public void DeleteAccountHolder(string username, string password);

        public List<Model.Bank> GetBanks();

        public void UpdateBank(string bankId, string newname);

        public void DeleteBank(string bankId);

        public List<Employee> GetEmployees();

        public void UpdateEmployee(string username, string password, string newname);

        public void DeleteEmployee(string username, string password);

        public List<Branch> GetBranches();

        public void UpdateBranch(string branchId, string location);

        public void DeleteBranch(string branchId);

        public List<Currency> GetCurrencies();

        public void UpdateCurrency(string code, string newName);

        public void DeleteCurrency(string code);

        public List<Transaction> GetTransactions();

        public void UpdateTransaction(string transactionId, int type);

        public void DeleteTransaction(string transactionId);
    }
}
