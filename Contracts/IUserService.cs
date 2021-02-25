using System;
using Bank.Model;
namespace Bank.Contracts
{
    public interface IUserService
    {
        public bool AddEmployee(Employee employee);

        public bool AddAccountHolder(AccountHolder accountHolder);

        public User LogIn(string username, string password);
    }
}
