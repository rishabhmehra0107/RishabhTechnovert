using System;
using System.Collections.Generic;
using System.Linq;
using Bank.Console.Data;
using Bank.Model;
using Bank.Contracts;

namespace Bank.Services
{
    public class DatabaseService : IDatabaseService
    {
        public List<AccountHolder> GetAccountHolders()
        {
            try
            {
                var db = new DBContext();
                List<AccountHolder> accountHolders = db.AccountHolders.ToList();
                return accountHolders;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateAccountHolder(string username, string password, string newname)
        {
            var db = new DBContext();
            AccountHolder accountHolder = db.AccountHolders.Find(username, password);
            if (accountHolder != null)
            {
                accountHolder.Name = newname;
            }
            db.SaveChanges();
        }

        public void DeleteAccountHolder(string username, string password)
        {
            var db = new DBContext();
            AccountHolder accountHolder = db.AccountHolders.Find(username, password);
            if (accountHolder != null)
            {
                db.AccountHolders.Remove(accountHolder);
            }
            db.SaveChanges();
        }

        public List<Model.Bank> GetBanks()
        {
            try
            {
                var db = new DBContext();
                List<Model.Bank> banks = db.Banks.ToList();
                return banks;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateBank(string bankId, string newname)
        {
            var db = new DBContext();
            Model.Bank bank = db.Banks.Find(bankId);
            if (bank != null)
            {
                bank.Name = newname;
            }
            db.SaveChanges();
        }

        public void DeleteBank(string bankId)
        {
            var db = new DBContext();
            Model.Bank bank = db.Banks.Find(bankId);
            if (bank != null)
            {
                db.Banks.Remove(bank);
            }
            db.SaveChanges();
        }

        public List<Employee> GetEmployees()
        {
            try
            {
                var db = new DBContext();
                List<Employee> employees = db.Employees.ToList();
                return employees;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateEmployee(string username, string password, string newname)
        {
            var db = new DBContext();
            Employee employee = db.Employees.Find(username,password);
            if (employee != null)
            {
                employee.Name = newname;
            }
            db.SaveChanges();
        }

        public void DeleteEmployee(string username, string password)
        {
            var db = new DBContext();
            Employee employee = db.Employees.Find(username,password);
            if (employee != null)
            {
                db.Employees.Remove(employee);
            }
            db.SaveChanges();
        }

        public List<Branch> GetBranches()
        {
            try
            {
                var db = new DBContext();
                List<Branch> branches = db.Branches.ToList();
                return branches;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateBranch(string branchId, string location)
        {
            var db = new DBContext();
            Branch branch = db.Branches.Find(branchId);
            if (branch != null)
            {
                branch.Location = location;
            }
            db.SaveChanges();
        }

        public void DeleteBranch(string branchId)
        {
            var db = new DBContext();
            Branch branch = db.Branches.Find(branchId);
            if (branch != null)
            {
                db.Branches.Remove(branch);
            }
            db.SaveChanges();
        }

        public List<Currency> GetCurrencies()
        {
            try
            {
                var db = new DBContext();
                List<Currency> currencies = db.Currencies.ToList();
                return currencies;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateCurrency(string code, string newName)
        {
            var db = new DBContext();
            Currency currency = db.Currencies.Find(code);
            if (currency != null)
            {
                currency.Name = newName;
            }
            db.SaveChanges();
        }

        public void DeleteCurrency(string code)
        {
            var db = new DBContext();
            Currency currency = db.Currencies.Find(code);
            if (currency != null)
            {
                db.Currencies.Remove(currency);
            }
            db.SaveChanges();
        }
    }
}
