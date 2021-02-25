using System;
using System.Collections.Generic;
using Bank.Model;
using static Bank.Model.Constants;

namespace Bank.Contracts
{
    public interface IStaffService
    {
        public bool NewCurrency(Currency currency);

        public List<string> BankEmployees();

        public List<string> BankAccountHolders();

        public bool UpdateCharges(int rtgs, int imps, BankType type);

        public void XmlData();
    }
}
