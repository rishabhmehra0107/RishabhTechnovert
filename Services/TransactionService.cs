using System;
using BankApp.Model;
using BankApp.Services.Utilities;
using static BankApp.Model.Constants;

namespace BankApp.Services
{
    public class TransactionService
	{
		private Bank Bank { get; set; }
		private Utility Utility { get; set; }

		public TransactionService(Bank bank, Utility utility)
		{
			this.Bank = bank;
			this.Utility = utility;
		}
	}
}