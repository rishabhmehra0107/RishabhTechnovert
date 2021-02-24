using System;
using Bank.Model;
using Bank.Services;
using System.Linq;
using System.Collections.Generic;
using static Bank.Model.Constants;
using Bank.Services.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace BankApp
{
    public class BankApplication
	{
		private IBankService IBankService;
		private UserService UserService { get; set; }
		private TransactionService TransactionService { get; set; }
		private StaffService StaffService { get; set; }
		public Banks Bank { get; set; }
		public AccountHolder AccountHolder { get; set; }
		public User LoggedInUser;

		public BankApplication(IBankService _iBankService)
		{
			this.IBankService = _iBankService;
			this.Bank = new Banks();
			this.AccountHolder = new AccountHolder();
			this.TransactionService = new TransactionService(this.Bank);
			this.StaffService = new StaffService(this.Bank, this.AccountHolder);
			this.UserService = new UserService(this.Bank);
			this.MainMenu();
		}

		public void MainMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User Login\n3. Exit");

			try
            {
				MenuOption menuOption = (MenuOption)Convert.ToInt32(Console.ReadLine());
				switch (menuOption)
				{
					case MenuOption.Setup:
						this.SetupBank();
						break;
					case MenuOption.Login:
						this.Login();
						break;
					case MenuOption.Exit:
						this.Exit();
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.MainMenu();
						break;
				}
				this.StaffService.XmlData();
				this.MainMenu();
			}
            catch (Exception)
            {
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.MainMenu();
            }
		}

		public void SetupBank()
		{
			this.Bank.Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Bank Name").ToUpper();
			this.Bank.Location = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Bank Location");
			this.Bank.Id = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			this.Bank.SameBankIMPS = Constants.SameBankIMPS;
			this.Bank.SameBankRTGS = Constants.SameBankRTGS;
			this.Bank.DiffBankIMPS = Constants.DiffBankIMPS;
			this.Bank.DiffBankRTGS = Constants.DiffBankRTGS;
			Console.WriteLine("Bank setup is completed. Please provide branch details");

			Branch branch = new Branch()
			{
				BankId = this.Bank.Id,
				Location = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Branch Location")
			};

			if(this.IBankService.AddBranch(branch))
				Console.WriteLine("Branch details added. Please provide admin username and password to setup");
            else
            {
				Console.WriteLine("Error!");
				this.SetupBank();
			}

			Employee admin = new Employee()
			{
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter admin name").ToUpper(),
				UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter admin username").ToLower(),
				Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter admin password"),
				Type = UserType.Admin
			};

			if (this.UserService.AddEmployee(admin))
				Console.WriteLine("Admin added successfuly");
			else
				Console.WriteLine("Error. Admin addition failed");

			Console.WriteLine("Bank Name: {0}, User Name: {1}", Bank.Name, admin.UserName);
		}

		public void Login()
		{
			string username = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter your Username");
			string password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Password");

			this.LoggedInUser = this.UserService.LogIn(username, password);
            try
            {
                if (this.LoggedInUser.Id == null)
                {
					Console.WriteLine("Invalid Credentials");
					this.Login();
				}
				if (this.LoggedInUser.Type.Equals(UserType.Admin))
				{
					this.DisplayAdminMenu();
				}
				else if (this.LoggedInUser.Type.Equals(UserType.Staff))
				{
					this.DisplayStaffMenu();
				}
				else if (this.LoggedInUser.Type.Equals(UserType.AccountHolder))
				{
					this.AccountHolder = this.Bank.AccountHolders.Find(account => account.UserName.Equals(this.LoggedInUser.UserName));
					this.DisplayUserMenu();
				}
			}
            catch (Exception)
            {
				this.Login();
            }
		}

		public void DisplayAdminMenu()
		{
			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User Details\n4. Update Service Charges\n5. Add New Currency\n6. Update Account\n7. Delete Account\n8. Logout");
            try
            {
				AdminOption adminOption = (AdminOption)Convert.ToInt32(Console.ReadLine()); ;
				switch (adminOption)
				{
					case AdminOption.AddStaff:
						this.AddStaff();
						break;
					case AdminOption.AddAccountHolder:
						this.AddAccountHolder();
						break;
					case AdminOption.DisplayUsers:
						this.DisplayBankEmployees();
						this.DisplayBankAccountHolders();
						break;
					case AdminOption.UpdateCharges:
						this.UpdateCharges();
						break;
					case AdminOption.NewCurrency:
						this.NewCurrency();
						break;
					case AdminOption.UpdateAccount:
						this.UpdateAccount();
						break;
					case AdminOption.DeleteAccount:
						this.DeleteAccount();
						break;
					case AdminOption.Logout:
						this.MainMenu();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						break;

				}
				this.StaffService.XmlData();
				this.DisplayAdminMenu();
			}
            catch (Exception)
            {
				Console.WriteLine("Please enter valid number to choose your option");
				this.DisplayAdminMenu();
			}
		}

		public void DisplayStaffMenu()
		{
			Console.WriteLine("1. Add Account Holder\n2. Display Bank User Details\n3. Update Service Charges\n4. Add New Currency\n5. Logout");
            try
            {
				StaffOption staffOption = (StaffOption)Convert.ToInt32(Console.ReadLine());
				switch (staffOption)
				{
					case StaffOption.AddAccountHolder:
						this.AddAccountHolder();
						break;
					case StaffOption.DisplayUsers:
						this.DisplayBankAccountHolders();
						break;
					case StaffOption.UpdateCharges:
						this.UpdateCharges();
						break;
					case StaffOption.NewCurrency:
						this.NewCurrency();
						break;
					case StaffOption.Logout:
						this.MainMenu();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						break;
				}
				this.StaffService.XmlData();
				this.DisplayStaffMenu();
			}
            catch (Exception)
            {
				Console.WriteLine("Please enter valid number to choose your option");
				this.DisplayStaffMenu();
			}
		}

		public void DisplayUserMenu()
		{
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Transaction History\n4.View Balance\n5.Transfer Funds\n6.Revert Transaction\n7.Logout");
            try
            {
				AccountHolderOption accountHolderOption = (AccountHolderOption)Convert.ToInt32(Console.ReadLine()); ;
				switch (accountHolderOption)
				{
					case AccountHolderOption.Withdraw:
						double withdrawAmount = Utility.GetDoubleInput("Enter Withdraw Amount");
						double newBalance = this.IBankService.Withdraw(withdrawAmount, this.AccountHolder.AccountNumber);
						if (newBalance == (double)TransactionStatus.InsufficientBalance)
						{
							Console.WriteLine("Insufficient Balance");
						}
						else if (newBalance == (double)TransactionStatus.Null)
						{
							Console.WriteLine("Account Holder Not Found!");
						}
						else
						{
							this.AccountHolder.AvailableBalance = newBalance;

							Transaction withdrawTransaction = new Transaction();
							withdrawTransaction.Type = TransactionType.Withdraw;
							withdrawTransaction.CreatedBy = this.LoggedInUser.Id;
							withdrawTransaction.Amount = withdrawAmount;
							if (this.TransactionService.AddTransaction(withdrawTransaction, this.AccountHolder.AccountNumber))
								Console.WriteLine("New Balance: {0}", this.AccountHolder.AvailableBalance);
							else
								Console.WriteLine("Unable to add transaction");
						}

						break;

					case AccountHolderOption.Deposit:
						double depositAmount = Utility.GetDoubleInput("Enter Deposit Amount");
						double balance = this.IBankService.Deposit(depositAmount, this.AccountHolder.AccountNumber);
						if (balance != (double)TransactionStatus.Null)
                        {
							this.AccountHolder.AvailableBalance = balance;

							Transaction depositTransaction = new Transaction();
							depositTransaction.Type = TransactionType.Deposit;
							depositTransaction.CreatedBy = this.LoggedInUser.Id;
							depositTransaction.Amount = depositAmount;
							if (this.TransactionService.AddTransaction(depositTransaction, this.AccountHolder.AccountNumber))
								Console.WriteLine("New Balance: {0}", this.AccountHolder.AvailableBalance);
							else
								Console.WriteLine("Unable to add transaction");

						}
						else
						{
							Console.WriteLine("Account Holder Not Found!");
							break;
						}

						break;

					case AccountHolderOption.TransactionHistory:
						Console.WriteLine("Transaction History");
						Console.WriteLine("Transaction Date\t\tTransaction Type\t\tTransaction Amount");
						foreach (Transaction transaction2 in this.TransactionService.GetTransactionsByAccount(this.AccountHolder.AccountNumber))
						{
							Console.WriteLine("{0}\t\t{1}\t\t\t{2}", transaction2.CreatedOn, transaction2.Type, transaction2.Amount);
						}

						break;

					case AccountHolderOption.Balance:
						Console.WriteLine("Current Balance: {0}", this.AccountHolder.AvailableBalance);
						break;

					case AccountHolderOption.TransferFund:
						this.TransferFunds();
						break;

					case AccountHolderOption.RevertTransaction:
						this.RevertTransaction();
						break;

					case AccountHolderOption.Logout:
						this.Logout(this.AccountHolder.Name);
						this.MainMenu();

						break;

					default:
						Console.WriteLine("Please select option from the list");
						break;
				}
				this.StaffService.XmlData();
				this.DisplayUserMenu();
			}
            catch (Exception)
            {
				Console.WriteLine("Please enter valid number to choose your option");
				this.DisplayUserMenu();
			}
		}

		public void TransferFunds()
        {
			string accountNumber = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Please enter vendor's account number to transfer funds.");
			var user = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));

            if (user != null)
            {
				double amount = Utility.GetDoubleInput("Enter Amount To Transfer");
				double balance = this.AccountHolder.AvailableBalance - amount;
				if (balance >= 0)
				{
					if(this.IBankService.TransferAmount(amount, this.AccountHolder.AccountNumber, user.AccountNumber))
                    {
						Transaction transferTransaction = new Transaction();
						transferTransaction.Type = TransactionType.Transfer;
						transferTransaction.CreatedBy = this.LoggedInUser.Id;
						transferTransaction.Amount = amount;
						transferTransaction.DestinationAccountNumber = user.AccountNumber;

						if (this.TransactionService.AddTransaction(transferTransaction, this.AccountHolder.AccountNumber))
							Console.WriteLine("Funds transferred successfully");
						else
							Console.WriteLine("Funds transferred failed");
                    }
                    else
                    {
						Console.WriteLine("Funds transferred failed");
                    }
				}
				else
				{
					Console.WriteLine("Insufficient Balance");
				}
			}
            else
            {
				Console.WriteLine("Vendor account not found");
            }
			
		}

		public void AddStaff()
		{
			Employee staff = new Employee()
			{
				UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Staff username"),
				Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Staff password"),
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Staff Name"),
				Type = UserType.Staff
			};

            if (this.UserService.AddEmployee(staff))
            {
				Console.WriteLine("Staff added successfully");
            }
            else
            {
				Console.WriteLine("Error. Staff addition failed");
            }
		}

		public void AddAccountHolder()
		{
			AccountHolder accountHolder = new AccountHolder()
			{
				UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account Holder username"),
				Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password"),
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Account Holder Name"),
				Type = UserType.AccountHolder
			};

			if(this.UserService.AddAccountHolder(accountHolder))
				Console.WriteLine("Account Holder added successfully");
			else
				Console.WriteLine("Error. Account Holder addition failed");
		}
		
		public void UpdateAccount()
		{
			Console.WriteLine("Select account to update");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}
			
			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			foreach (var account in this.Bank.AccountHolders.Where(account => account.UserName.ToLower() == userName.ToLower()))
			{
                if (account != null)
                {
					Console.WriteLine("Username: {0}. This account can now be updated ", userName);
					account.UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Update username of user: ");
					account.Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Update password of user: ");
					account.Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Update Account Holder Name");
					Console.WriteLine("User Account updated successfully");
				}
			}
		}

		public void DeleteAccount()
		{
			Console.WriteLine("Select account to delete");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}

			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			this.Bank.AccountHolders.RemoveAll(account => account.UserName == userName);
			Console.WriteLine("User Account deleted successfully");
		}

		public void NewCurrency()
		{
			try
			{
				Currency currency = new Currency();
				currency.Code = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
				currency.Name = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Currency Name:");
				currency.Rate = Utility.GetIntInput("Enter Currency Value Cnoverted To INR: ");
				if(this.StaffService.NewCurrency(currency))
					Console.WriteLine("New Currency updated Successfully");
				else
					Console.WriteLine("New Currency Rejected!");
			}
            catch (Exception)
            {
				Console.WriteLine("Invalid input. Please enter valid credentials");
				this.NewCurrency();
            }
		}

		public void DisplayBankEmployees()
		{
			Console.WriteLine("Bank Staff Users");
			List<string> employeeList = this.StaffService.BankEmployees();
			foreach (string staff in employeeList)
			{
				Console.WriteLine(staff);
			}
		}

		public void DisplayBankAccountHolders()
		{
			Console.WriteLine("Bank Account Holders");
			List<string> accountHolderList = this.StaffService.BankAccountHolders();
			foreach (string user in accountHolderList)
			{
				Console.WriteLine(user);
			}
		}

		public void RevertTransaction()
		{
			Console.WriteLine("Transactions of users by their ID and date are as follows:-");
			foreach (Transaction transaction in this.TransactionService.GetTransactionsByAccount(this.AccountHolder.AccountNumber))
			{
				Console.WriteLine("Transaction ID: {0} , Transaction Date: {1}", transaction.ID, transaction.CreatedOn);
			}

			Console.WriteLine("Enter Transaction ID and Transaction Date to revert that transaction");
            try
            {
				string id = Console.ReadLine();
				DateTime date = Convert.ToDateTime(Console.ReadLine());
				if (this.TransactionService.RevertTransaction(id, date, this.AccountHolder.AccountNumber))
                {
					Console.WriteLine("Transaction reverted successfully");
                }
                else
                {
					Console.WriteLine("Revert Transaction Failed!");
                }
			}
            catch (Exception)
            {
				Console.WriteLine("Please enter valid credentials");
				this.RevertTransaction();
            }
		}

		public void UpdateCharges()
		{
			Console.WriteLine("Select account from the list");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				Console.WriteLine(user.UserName + " " + user.Id);
			}

			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter username : ");
			if(this.Bank.AccountHolders.Any(account=>account.UserName.ToLower() == userName.ToLower()))
			{
				Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", userName);
				string bankName = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter bankname : ");
				if(this.Bank.Branches.Any(bank=>bank.BankId.Equals(bankName.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"))))
				{
					Console.WriteLine("New charges of RTGS and IMPS are:-");
                    try
                    {
						int sameBankRTGS = Convert.ToInt32(Console.ReadLine());
						int sameBankIMPS = Convert.ToInt32(Console.ReadLine());
						BankType type = BankType.Same;
						if(this.StaffService.UpdateCharges(sameBankRTGS, sameBankIMPS, type))
							Console.WriteLine("New charges updated successfully!");
						else
							Console.WriteLine("Error!");
					}
                    catch (Exception)
                    {
						Console.WriteLine("Error. Enter credentials again");
						this.UpdateCharges();
                    }
				}
				else
				{
					Console.WriteLine("New charges of RTGS and IMPS are:-");
                    try
                    {
						int differentBankRTGS = Convert.ToInt32(Console.ReadLine());
						int differentBankIMPS = Convert.ToInt32(Console.ReadLine());
						BankType type = BankType.Different;
						if(this.StaffService.UpdateCharges(differentBankRTGS, differentBankIMPS, type))
							Console.WriteLine("New charges updated successfully!");
						else
							Console.WriteLine("Error!");
					}
                    catch (Exception)
                    {
						Console.WriteLine("Error. Enter credentials again");
						this.UpdateCharges();
					}
				}
			}
		}

		public void Logout(string user)
		{
			this.LoggedInUser = new User();
			Console.WriteLine("Goodbye "+user);
		}

		public void Exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}