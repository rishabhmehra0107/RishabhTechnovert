using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bank_Application.Services;
using Bank_Application.Utilities;

namespace Bank_Application
{
    public class Account
    {
		Bank Bank;
		private AccountService AccountService { get; set; }
		private Utility Utility { get; set; }
		public Account()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.AccountService = new AccountService();
		}
		public void setUpAccount()
        {
			this.AccountService.SetUpAccount();
        }
		
		
	}
}
