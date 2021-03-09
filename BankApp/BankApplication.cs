using System;
using Bank.Model;
using Bank.Services;
using System.Linq;
using System.Collections.Generic;
using static Bank.Model.Constants;
using Bank.Services.Utilities;
using Bank.Contracts;
using SimpleInjector;
using Bank.Console.Data;

namespace BankApp
{
    public class BankApplication
	{
		static Container container;
		public AccountHolder AccountHolder;
		public User LoggedInUser;
		public Bank.Model.Bank CurrentBank;
		public DBContext DB { get; set; }

		public BankApplication()
		{
			this.DB = new DBContext();

			this.AccountHolder = new AccountHolder();

			this.InitializeDependencies();

			this.MainMenu();
		}

		public void InitializeDependencies()
        {
			container = new Container();

			container.Register<IBankService, BankService>();

			container.Register<IStaffService, StaffService>();

			container.Register<ITransactionService, TransactionService>();

			container.Register<IUserService, UserService>();

			container.Verify();
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
			var bankService = container.GetInstance<IBankService>();
			var userService = container.GetInstance<IUserService>();
			Bank.Model.Bank bank = new Bank.Model.Bank()
			{
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Bank Name").ToUpper(),
				Location = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Bank Location")
			};
			if (bankService.AddBank(bank))
				Console.WriteLine("Bank setup is completed. Please provide branch details");
            else
            {
				Console.WriteLine("Error!");
				this.SetupBank();
			}
				
			Branch branch = new Branch()
			{
				Location = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Branch Location")
			};
			if (bankService.AddBranch(branch,bank.BankId))
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

			if (userService.AddEmployee(admin,bank.BankId))
				Console.WriteLine("Admin added successfuly");
			else
				Console.WriteLine("Error. Admin addition failed");

			Console.WriteLine("Bank Name: {0}, User Name: {1}", bank.Name, admin.UserName);
			this.CurrentBank = bank;
		}

		public void Login()
		{
			var userService = container.GetInstance<IUserService>();
			string bankId = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Bank ID");
			string employeeId = Utility.GetStringInput("^[a-zA-Z0-9 ]+$", "Enter your Account ID");

			var bank = this.DB.Banks.Find(bankId);
			this.LoggedInUser = userService.LogIn(employeeId);
			this.CurrentBank = bank;
            try
            {
                if (this.LoggedInUser.UserId == null)
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
					this.AccountHolder = this.DB.AccountHolders.Find(this.LoggedInUser.AccountId);
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
			var bankService = container.GetInstance<IBankService>();
			var transactionService = container.GetInstance<ITransactionService>();
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Transaction History\n4.View Balance\n5.Transfer Funds\n6.Revert Transaction\n7.Logout");
            try
            {
				AccountHolderOption accountHolderOption = (AccountHolderOption)Convert.ToInt32(Console.ReadLine()); ;
				switch (accountHolderOption)
				{
					case AccountHolderOption.Withdraw:
						double withdrawAmount = Utility.GetDoubleInput("Enter Withdraw Amount");
						double newBalance = bankService.Withdraw(withdrawAmount, this.AccountHolder.AccountId, this.CurrentBank.BankId, this.LoggedInUser.AccountId);
						if (newBalance == (double)TransactionStatus.Failure)
						{
							Console.WriteLine("Insufficient Balance");
						}
						else if (newBalance == (double)TransactionStatus.NotFound)
						{
							Console.WriteLine("Account Holder Not Found!");
						}
						else
						{
							Console.WriteLine("Withdraw Successfull");
						}

						break;

					case AccountHolderOption.Deposit:
						double depositAmount = Utility.GetDoubleInput("Enter Deposit Amount");
						double balance = bankService.Deposit(depositAmount, this.AccountHolder.AccountId, this.CurrentBank.BankId, this.LoggedInUser.AccountId);
						if (balance != (double)TransactionStatus.NotFound)
                        {
							Console.WriteLine("Deposit Successfull");

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
						foreach (Transaction transaction2 in transactionService.GetTransactionsByAccount(this.AccountHolder.AccountId, this.CurrentBank.BankId))
						{
							Console.WriteLine("{0}\t\t{1}\t\t\t{2}", transaction2.CreatedOn, transaction2.Type, transaction2.Amount);
						}

						break;

					case AccountHolderOption.Balance:
						double getBalance = bankService.GetBalance(this.AccountHolder.AccountId);
						this.AccountHolder.AvailableBalance = getBalance;
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
			var bankService = container.GetInstance<IBankService>();
			var transactionService = container.GetInstance<ITransactionService>();
			string accountNumber = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Please enter vendor's account number to transfer funds.");
			var user = this.DB.AccountHolders.Find(accountNumber);

            if (user != null)
            {
				double amount = Utility.GetDoubleInput("Enter Amount To Transfer");
				double balance = this.AccountHolder.AvailableBalance - amount;
				if (balance >= 0)
				{
					if(bankService.TransferAmount(amount, this.AccountHolder.AccountId, user.AccountId, this.CurrentBank.BankId))
                    {
						Transaction transferTransaction = new Transaction();
						transferTransaction.Type = TransactionType.Transfer;
						transferTransaction.CreatedBy = this.LoggedInUser.UserId;
						transferTransaction.Amount = amount;
						transferTransaction.DestinationAccountNumber = user.AccountId;

						if (transactionService.AddTransaction(transferTransaction, this.AccountHolder.AccountId, this.CurrentBank.BankId))
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
			var userService = container.GetInstance<IUserService>();
			Employee staff = new Employee()
			{
				UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Staff username"),
				Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Staff password"),
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Staff Name"),
				Type = UserType.Staff
			};

            if (userService.AddEmployee(staff, this.CurrentBank.BankId))
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
			var userService = container.GetInstance<IUserService>();
			AccountHolder accountHolder = new AccountHolder()
			{
				UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account Holder username"),
				Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password"),
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Account Holder Name"),
				Type = UserType.AccountHolder
			};

			if(userService.AddAccountHolder(accountHolder, this.CurrentBank.BankId))
				Console.WriteLine("Account Holder added successfully");
			else
				Console.WriteLine("Error. Account Holder addition failed");
		}
		
		public void UpdateAccount()
		{
			Console.WriteLine("Select account to update");
			foreach (AccountHolder accountHolder in this.DB.AccountHolders)
			{
				Console.WriteLine(accountHolder.Name);
			}
			
			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			foreach (var account in this.CurrentBank.AccountHolders.Where(account => account.UserName.ToLower() == userName.ToLower()))
			{
                if (account != null)
                {
					Console.WriteLine("Username: {0}. This account can now be updated ", userName);
					account.UserName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Update username of user: ");
					account.Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Update password of user: ");
					account.Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Update Account Holder Name");
					this.DB.SaveChanges();
					Console.WriteLine("User Account updated successfully");
				}
			}
		}

		public void DeleteAccount()
		{
			Console.WriteLine("Select account to delete");
			foreach (AccountHolder accountHolder in this.DB.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}

			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			this.CurrentBank.AccountHolders.RemoveAll(account => account.UserName == userName);
			Console.WriteLine("User Account deleted successfully");
		}

		public void NewCurrency()
		{
			var staffService = container.GetInstance<IStaffService>();
			try
			{
				Currency currency = new Currency();
				currency.Code = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
				currency.Name = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Currency Name:");
				currency.Rate = Utility.GetIntInput("Enter Currency Value Cnoverted To INR: ");
				if(staffService.NewCurrency(currency, this.CurrentBank.BankId))
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
			var staffService = container.GetInstance<IStaffService>();
			Console.WriteLine("Bank Staff Users");
			List<Employee> employeeList = staffService.BankEmployees();
			foreach (Employee staff in employeeList)
			{
				Console.WriteLine(staff.Name);
			}
		}

		public void DisplayBankAccountHolders()
		{
			var staffService = container.GetInstance<IStaffService>();
			Console.WriteLine("Bank Account Holders");
			List<AccountHolder> accountHolderList = staffService.BankAccountHolders();
			foreach (AccountHolder user in accountHolderList)
			{
				Console.WriteLine(user.Name);
			}
		}

		public void RevertTransaction()
		{
			var transactionService = container.GetInstance<ITransactionService>();
			Console.WriteLine("Transactions of users by their ID and date are as follows:-");
			foreach (Transaction transaction in transactionService.GetTransactionsByAccount(this.AccountHolder.AccountId, this.CurrentBank.BankId))
			{
				Console.WriteLine("Transaction ID: {0} , Transaction Date: {1}", transaction.TransactionID, transaction.CreatedOn);
			}

			Console.WriteLine("Enter Transaction ID to revert that transaction");
            try
            {
				string id = Console.ReadLine();
				if (transactionService.RevertTransaction(id, this.AccountHolder.AccountId, this.CurrentBank.BankId))
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
			var staffService = container.GetInstance<IStaffService>();
			Console.WriteLine("Select account from the list");
			foreach (AccountHolder user in this.CurrentBank.AccountHolders)
			{
				Console.WriteLine(user.UserName + " " + user.UserId);
			}

			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter username : ");
			if(this.CurrentBank.AccountHolders.Any(account=>account.UserName.ToLower() == userName.ToLower()))
			{
				Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", userName);
				string bankName = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter bankname : ");
				if(this.CurrentBank.Branches.Any(bank=>bank.BankId.Equals(bankName.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"))))
				{
					Console.WriteLine("New charges of RTGS and IMPS are:-");
                    try
                    {
						int sameBankRTGS = Convert.ToInt32(Console.ReadLine());
						int sameBankIMPS = Convert.ToInt32(Console.ReadLine());
						TransferTo type = TransferTo.Same;
						if(staffService.UpdateCharges(sameBankRTGS, sameBankIMPS, type, this.CurrentBank.BankId))
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
						TransferTo type = TransferTo.Different;
						if(staffService.UpdateCharges(differentBankRTGS, differentBankIMPS, type, this.CurrentBank.BankId))
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