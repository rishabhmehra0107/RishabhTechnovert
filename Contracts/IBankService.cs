using System;
using Bank.Model;

namespace Bank.Contracts
{
	public interface IBankService
	{
		public bool AddBank(Bank.Model.Bank bank);

		public bool AddBranch(Branch branch, string bankName);

		public double Withdraw(double amount, string accountNumber, string bankName);

		public double Deposit(double amount, string accountNumber, string bankName);

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2, string bankName);
	}
}
