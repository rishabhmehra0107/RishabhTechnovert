using System;
using Bank.Model;

namespace Bank.Contracts
{
	public interface IBankService
	{
		public bool AddBranch(Branch branch);

		public double Withdraw(double amount, string accountNumber);

		public double Deposit(double amount, string accountNumber);

		public bool TransferAmount(double amount, string accountNumber1, string accountNumber2);
	}
}
