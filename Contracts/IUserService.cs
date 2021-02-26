using System;
using Bank.Model;
namespace Bank.Contracts
{
    public interface IUserService
    {
        public bool AddEmployee(Employee employee, string bankName);

        public bool AddAccountHolder(AccountHolder accountHolder, string bankName);

        public User LogIn(string bankName, string username, string password);
    }
}
