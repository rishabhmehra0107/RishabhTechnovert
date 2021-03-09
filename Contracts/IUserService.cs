using System;
using Bank.Model;
namespace Bank.Contracts
{
    public interface IUserService
    {
        public bool AddEmployee(Employee employee, string bankId);

        public bool AddAccountHolder(AccountHolder accountHolder, string bankId);

        public User LogIn(string employeeId);
    }
}
