using System;
using System.Collections.Generic;
using Bank.Model;
using static Bank.Model.Constants;

namespace Bank.Contracts
{
    public interface IStaffService
    {
        public bool NewCurrency(Currency currency, string bankName);

        public List<string> BankEmployees(string bankName);

        public List<string> BankAccountHolders(string bankName);

        public bool UpdateCharges(int rtgs, int imps, BankType type, string bankName);

        public void XmlData(string bankName);
    }
}
