using System;
using System.Collections.Generic;
using Bank.Model;
using static Bank.Model.Constants;

namespace Bank.Contracts
{
    public interface IStaffService
    {
        public bool NewCurrency(Currency currency, string bankId);

        public List<Employee> BankEmployees();

        public List<AccountHolder> BankAccountHolders();

        public bool UpdateCharges(int rtgs, int imps, TransferTo type, string bankId);
    }
}
