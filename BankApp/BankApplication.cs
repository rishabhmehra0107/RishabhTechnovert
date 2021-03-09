using System;
using Bank.Model;
using Bank.Services;
using System.Linq;
using System.Collections.Generic;
using static Bank.Model.Constants;
using Bank.Services.Utilities;
using Bank.Services.BankStore;
using Bank.Contracts;
using SimpleInjector;

namespace BankApp
{
    public class BankApplication
	{
		static Container container;
		Banks Banks { get; set; }
		public AccountHolder AccountHolder;
		public User LoggedInUser;
		public Bank.Model.Bank CurrentBank;
		private IDatabaseService DatabaseService;
		private IBankService BankService;
		private IStaffService StaffService;
		private ITransactionService TransactionService;
		private IUserService UserService;

		public BankApplication(IDatabaseService databaseService, IBankService bankService, IStaffService staffService, ITransactionService transactionService, IUserService userService)
		{
			DatabaseService = databaseService;

			BankService = bankService;

			StaffService = staffService;

			TransactionService = transactionService;

			UserService = userService;

			this.Banks = new Banks();

			this.AccountHolder = new AccountHolder();

			this.InitializeDependencies();
		}

		public void InitializeDependencies()
        {
			container = new Container();

			container.Register<IBankService>(() => new BankService(this.Banks));

			container.Register<IStaffService>(() => new StaffService(this.Banks, this.AccountHolder));

			container.Register<ITransactionService>(() => new TransactionService(this.Banks));

			container.Register<IUserService>(() => new UserService(this.Banks));

			container.Verify();
		}

		public void MainMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User Login\n3. Exit\n4. Access Database");

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
					case MenuOption.Database:
						this.Database();
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

		public void Database()
        {
			Console.WriteLine("Welcome to Bank Application\n1. Access Banks \n2. Access AccountHolders\n3. Access Employees\n4. Access Branches\n5. Access Currencies\n6. Exit");

			try
			{
				DatabaseOption databaseOption = (DatabaseOption)Convert.ToInt32(Console.ReadLine());
				switch (databaseOption)
				{
					case DatabaseOption.BanksTable:
						this.AccessBanks();
						break;
					case DatabaseOption.AccountHoldersTable:
						this.AccessAccountHolders();
						break;
					case DatabaseOption.EmployeesTable:
						this.AccessEmployees();
						break;
					case DatabaseOption.BranchesTable:
						this.AccessBranches();
						break;
					case DatabaseOption.CurrenciesTable:
						this.AccessCurrencies();
						break;
					case DatabaseOption.Exit:
						this.MainMenu();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.Database();
						break;
				}
				this.Database();
			}
			catch (Exception)
			{
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.Database();
			}
        }

		public void AccessAccountHolders()
        {
			Console.WriteLine("1. GetDetails \n2. UpdateAccount\n3. DeleteAccount\n4. Exit");

			try
			{
				AccessOptions accessOptions = (AccessOptions)Convert.ToInt32(Console.ReadLine());
				switch (accessOptions)
				{
					case AccessOptions.GetDetails:
						Console.WriteLine("Account Holder Details\n");
						List<AccountHolder> accountHolders = DatabaseService.GetAccountHolders();
						foreach (AccountHolder account in accountHolders)
						{
							System.Console.WriteLine("Username:{0} , Password:{1} , AccountNumber:{2} , AvailableBalance:{3} , Name:{4}", account.UserName, account.Password, account.AccountNumber, account.AvailableBalance, account.Name);
						}
						break;
					case AccessOptions.Update:
						Console.WriteLine("Update Account Holder");
						string username = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Username");
						string password = Utility.GetStringInput("^[a-zA-Z0-9 ]{3,}$", "Enter Password");
						string newName = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter New Name");
						DatabaseService.UpdateAccountHolder(username, password, newName);
						break;
					case AccessOptions.Delete:
						Console.WriteLine("Delete Account Holder");
						string accountUsername = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Username");
						string accountPassword = Utility.GetStringInput("^[a-zA-Z0-9 ]{3,}$", "Enter Password");
						DatabaseService.DeleteAccountHolder(accountUsername,accountPassword);
						break;
					case AccessOptions.Exit:
						this.Database();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.AccessAccountHolders();
						break;
				}
				this.AccessAccountHolders();
			}
			catch (Exception)
			{
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.AccessAccountHolders();
			}
		}

		public void AccessBanks()
		{
			Console.WriteLine("1. GetDetails \n2. UpdateBank\n3. DeleteBank\n4. Exit");

			try
			{
				AccessOptions accessOptions = (AccessOptions)Convert.ToInt32(Console.ReadLine());
				switch (accessOptions)
				{
					case AccessOptions.GetDetails:
						Console.WriteLine("Bank Details\n");
						List<Bank.Model.Bank> banks = DatabaseService.GetBanks();
						foreach (Bank.Model.Bank bank in banks)
						{
							System.Console.WriteLine("BankId:{0} , Name:{1} , Location:{2} , SameBankRTGS:{3} , SameBankIMPS:{4} , DifferentBankRTGS:{5} , DifferentBankIMPS:{6}", bank.BankId, bank.Name, bank.Location, bank.SameBankRTGS, bank.SameBankIMPS, bank.DiffBankRTGS, bank.DiffBankIMPS);
						}
						break;
					case AccessOptions.Update:
						Console.WriteLine("Update Bank");
						string bankId = Utility.GetStringInput("^[a-zA-Z0-9 ]{3,}$", "Enter Bank ID");
						string newName = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter New Name Of Bank");
						DatabaseService.UpdateBank(bankId, newName);
						break;
					case AccessOptions.Delete:
						Console.WriteLine("Delete Account Holder");
						string bankID = Utility.GetStringInput("^[a-zA-Z0-9 ]{3,}$", "Enter Bank ID");
						DatabaseService.DeleteBank(bankID);
						break;
					case AccessOptions.Exit:
						this.Database();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.AccessBanks();
						break;
				}
				this.AccessBanks();
			}
			catch (Exception)
			{
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.AccessBanks();
			}
		}

		public void AccessEmployees()
		{
			Console.WriteLine("1. GetDetails \n2. UpdateEmployee\n3. DeleteEmployee\n4. Exit");

			try
			{
				AccessOptions accessOptions = (AccessOptions)Convert.ToInt32(Console.ReadLine());
				switch (accessOptions)
				{
					case AccessOptions.GetDetails:
						Console.WriteLine("Employee Details\n");
						List<Employee> employees = DatabaseService.GetEmployees();
						foreach (Employee employee in employees)
						{
							System.Console.WriteLine("Username:{0} , Password:{1} , EmployeeID:{2} , Name:{3}", employee.UserName, employee.Password, employee.EmployeeId, employee.Name);
						}
						break;
					case AccessOptions.Update:
						Console.WriteLine("Update Employee");
						string username = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Username");
						string password = Utility.GetStringInput("^[a-zA-Z0-9 ]{3,}$", "Enter Password");
						string newName = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter New Name");
						DatabaseService.UpdateEmployee(username, password, newName);
						break;
					case AccessOptions.Delete:
						Console.WriteLine("Delete Employee");
						string accountUsername = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Username");
						string accountPassword = Utility.GetStringInput("^[a-zA-Z0-9 ]{3,}$", "Enter Password");
						DatabaseService.DeleteEmployee(accountUsername, accountPassword);
						break;
					case AccessOptions.Exit:
						this.Database();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.AccessEmployees();
						break;
				}
				this.AccessEmployees();
			}
			catch (Exception)
			{
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.AccessEmployees();
			}
		}

		public void AccessBranches()
		{
			Console.WriteLine("1. GetDetails \n2. UpdateBranch\n3. DeleteBranch\n4. Exit");

			try
			{
				AccessOptions accessOptions = (AccessOptions)Convert.ToInt32(Console.ReadLine());
				switch (accessOptions)
				{
					case AccessOptions.GetDetails:
						Console.WriteLine("Branch Details\n");
						List<Branch> branches = DatabaseService.GetBranches();
						foreach (Branch branch in branches)
						{
							System.Console.WriteLine("BranchID:{0} , BankID:{1} , Location:{2}", branch.BranchId, branch.BankId, branch.Location);
						}
						break;
					case AccessOptions.Update:
						Console.WriteLine("Update Branch");
						string branchId = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Branch ID");
						string newLocation = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter New Location");
						DatabaseService.UpdateBranch(branchId, newLocation);
						break;
					case AccessOptions.Delete:
						Console.WriteLine("Delete Branch");
						string branchID = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Branch ID");
						DatabaseService.DeleteBranch(branchID);
						break;
					case AccessOptions.Exit:
						this.Database();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.AccessBranches();
						break;
				}
				this.AccessBranches();
			}
			catch (Exception)
			{
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.AccessBranches();
			}
		}

		public void AccessCurrencies()
		{
			Console.WriteLine("1. GetDetails \n2. UpdateCurrency\n3. DeleteCurrency\n4. Exit");

			try
			{
				AccessOptions accessOptions = (AccessOptions)Convert.ToInt32(Console.ReadLine());
				switch (accessOptions)
				{
					case AccessOptions.GetDetails:
						Console.WriteLine("Currency Details\n");
						List<Currency> currencies = DatabaseService.GetCurrencies();
						foreach (Currency currency in currencies)
						{
							System.Console.WriteLine("Code:{0} , Name:{1} , Rate:{2}", currency.Code, currency.Name, currency.Rate);
						}
						break;
					case AccessOptions.Update:
						Console.WriteLine("Update Currency");
						string code = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Currency Code");
						string newName = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter New Name Of Currency");
						DatabaseService.UpdateBranch(code, newName);
						break;
					case AccessOptions.Delete:
						Console.WriteLine("Delete Currency");
						string currencyCode = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Currency Code");
						DatabaseService.DeleteBranch(currencyCode);
						break;
					case AccessOptions.Exit:
						this.Database();
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.AccessCurrencies();
						break;
				}
				this.AccessCurrencies();
			}
			catch (Exception)
			{
				Console.Clear();
				Console.WriteLine("Please enter valid number to choose your option");
				this.AccessCurrencies();
			}
		}

		public void SetupBank()
		{
			Bank.Model.Bank bank = new Bank.Model.Bank()
			{
				Name = Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Bank Name").ToUpper(),
				Location = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Bank Location")
			};
			if (BankService.AddBank(bank))
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
			if (BankService.AddBranch(branch,bank.Name))
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

			if (UserService.AddEmployee(admin,bank.Name))
				Console.WriteLine("Admin added successfuly");
			else
				Console.WriteLine("Error. Admin addition failed");

			Console.WriteLine("Bank Name: {0}, User Name: {1}", bank.Name, admin.UserName);
			this.CurrentBank = bank;
		}

		public void Login()
		{
			string bankName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter your Bank Name");
			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter your Username");
			string password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Password");

			var bank = this.Banks.Bank.Find(bank => bank.Name.ToUpper().Equals(bankName.ToUpper()));
			this.LoggedInUser = UserService.LogIn(bankName, userName, password);
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
					this.AccountHolder = bank.AccountHolders.Find(account => account.UserName.Equals(this.LoggedInUser.UserName));
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
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Transaction History\n4.View Balance\n5.Transfer Funds\n6.Revert Transaction\n7.Logout");
            try
            {
				AccountHolderOption accountHolderOption = (AccountHolderOption)Convert.ToInt32(Console.ReadLine()); ;
				switch (accountHolderOption)
				{
					case AccountHolderOption.Withdraw:
						double withdrawAmount = Utility.GetDoubleInput("Enter Withdraw Amount");
						double newBalance = BankService.Withdraw(withdrawAmount, this.AccountHolder.AccountNumber, this.CurrentBank.Name);
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
							withdrawTransaction.CreatedBy = this.LoggedInUser.UserId;
							withdrawTransaction.Amount = withdrawAmount;
							if (TransactionService.AddTransaction(withdrawTransaction, this.AccountHolder.AccountNumber, this.CurrentBank.Name))
								Console.WriteLine("New Balance: {0}", this.AccountHolder.AvailableBalance);
							else
								Console.WriteLine("Unable to add transaction");
						}

						break;

					case AccountHolderOption.Deposit:
						double depositAmount = Utility.GetDoubleInput("Enter Deposit Amount");
						double balance = BankService.Deposit(depositAmount, this.AccountHolder.AccountNumber, this.CurrentBank.Name);
						if (balance != (double)TransactionStatus.Null)
                        {
							this.AccountHolder.AvailableBalance = balance;

							Transaction depositTransaction = new Transaction();
							depositTransaction.Type = TransactionType.Deposit;
							depositTransaction.CreatedBy = this.LoggedInUser.UserId;
							depositTransaction.Amount = depositAmount;
							if (TransactionService.AddTransaction(depositTransaction, this.AccountHolder.AccountNumber, this.CurrentBank.Name))
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
						foreach (Transaction transaction2 in TransactionService.GetTransactionsByAccount(this.AccountHolder.AccountNumber, this.CurrentBank.Name))
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
			var user = this.CurrentBank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));

            if (user != null)
            {
				double amount = Utility.GetDoubleInput("Enter Amount To Transfer");
				double balance = this.AccountHolder.AvailableBalance - amount;
				if (balance >= 0)
				{
					if(BankService.TransferAmount(amount, this.AccountHolder.AccountNumber, user.AccountNumber, this.CurrentBank.Name))
                    {
						Transaction transferTransaction = new Transaction();
						transferTransaction.Type = TransactionType.Transfer;
						transferTransaction.CreatedBy = this.LoggedInUser.UserId;
						transferTransaction.Amount = amount;
						transferTransaction.DestinationAccountNumber = user.AccountNumber;

						if (TransactionService.AddTransaction(transferTransaction, this.AccountHolder.AccountNumber, this.CurrentBank.Name))
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

            if (UserService.AddEmployee(staff, this.CurrentBank.Name))
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

			if(UserService.AddAccountHolder(accountHolder, this.CurrentBank.Name))
				Console.WriteLine("Account Holder added successfully");
			else
				Console.WriteLine("Error. Account Holder addition failed");
		}
		
		public void UpdateAccount()
		{
			Console.WriteLine("Select account to update");
			foreach (AccountHolder accountHolder in this.CurrentBank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
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
					Console.WriteLine("User Account updated successfully");
				}
			}
		}

		public void DeleteAccount()
		{
			Console.WriteLine("Select account to delete");
			foreach (AccountHolder accountHolder in this.CurrentBank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}

			string userName = Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			this.CurrentBank.AccountHolders.RemoveAll(account => account.UserName == userName);
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
				if(StaffService.NewCurrency(currency, this.CurrentBank.Name))
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
			List<string> employeeList = StaffService.BankEmployees(this.CurrentBank.Name);
			foreach (string staff in employeeList)
			{
				Console.WriteLine(staff);
			}
		}

		public void DisplayBankAccountHolders()
		{
			Console.WriteLine("Bank Account Holders");
			List<string> accountHolderList = StaffService.BankAccountHolders(this.CurrentBank.Name);
			foreach (string user in accountHolderList)
			{
				Console.WriteLine(user);
			}
		}

		public void RevertTransaction()
		{
			Console.WriteLine("Transactions of users by their ID and date are as follows:-");
			foreach (Transaction transaction in TransactionService.GetTransactionsByAccount(this.AccountHolder.AccountNumber, this.CurrentBank.Name))
			{
				Console.WriteLine("Transaction ID: {0} , Transaction Date: {1}", transaction.TransactionID, transaction.CreatedOn);
			}

			Console.WriteLine("Enter Transaction ID and Transaction Date to revert that transaction");
            try
            {
				string id = Console.ReadLine();
				DateTime date = Convert.ToDateTime(Console.ReadLine());
				if (TransactionService.RevertTransaction(id, date, this.AccountHolder.AccountNumber, this.CurrentBank.Name))
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
						BankType type = BankType.Same;
						if(StaffService.UpdateCharges(sameBankRTGS, sameBankIMPS, type, this.CurrentBank.Name))
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
						if(StaffService.UpdateCharges(differentBankRTGS, differentBankIMPS, type, this.CurrentBank.Name))
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