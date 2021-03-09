using System;
using Bank.Model;

namespace Bank.Contracts
{
	public interface IBankService
	{
		public bool AddBank(Bank.Model.Bank bank);

		public bool AddBranch(Branch branch, string bankId);

		public double Withdraw(double amount, string accountNumber, string bankId, string id);

		public double Deposit(double amount, string accountNumber, string bankId, string id);

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2, string bankId);
	}
}
