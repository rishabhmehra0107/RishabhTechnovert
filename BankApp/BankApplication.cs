using System;
using BankApp.Model;
using BankApp.Services;
using System.Linq;
using System.Collections.Generic;
using static BankApp.Model.Constants;

namespace BankApp
{
    public class BankApplication
	{
		private Services.Utilities.Utility Utility { get; set; }
		private UserService UserService { get; set; }
		private BankService BankService { get; set; }
		private TransactionService TransactionService { get; set; }
		private StaffService StaffService { get; set; }
		public Bank Bank { get; set; }
		public AccountHolder AccountHolder { get; set; }
		public User LoggedInUser;

		public BankApplication()
		{
			this.Bank = new Bank();
			this.AccountHolder = new AccountHolder();
			this.Utility = new BankApp.Services.Utilities.Utility();
			this.TransactionService = new TransactionService(this.Bank);
			this.StaffService = new StaffService(this.Bank, this.AccountHolder);
			this.UserService = new UserService(this.Bank);
			this.BankService = new BankService(this.Bank);
			this.MainMenu();
		}

		public void MainMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User Login\n3. Exit");

			try
            {
				int option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 1:
						this.SetupBank();
						break;
					case 2:
						this.Login();
						break;
					case 3:
						this.Exit();
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Please select option from the list");
						this.MainMenu();
						break;
				}
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
			this.Bank.Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Bank Name").ToUpper();
			this.Bank.Location = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Bank Location");
			this.Bank.Id = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			this.Bank.SameBankIMPS = Constants.SameBankIMPS;
			this.Bank.SameBankRTGS = Constants.SameBankRTGS;
			this.Bank.DifferentBankIMPS = Constants.DifferentBankIMPS;
			this.Bank.DifferentBankRTGS = Constants.DifferentBankRTGS;
			Console.WriteLine("Bank setup is completed. Please provide branch details");

			Branch branch = new Branch()
			{
				BankId = this.Bank.Id,
				Location = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Branch Location")
			};
			this.BankService.AddBranch(branch);
			Console.WriteLine("Branch details added. Please provide admin username and password to setup");

			Employee admin = new Employee()
			{
				Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter admin name").ToUpper(),
				UserName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter admin username").ToLower(),
				Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter admin password"),
				Type = UserType.Admin
			};
			this.UserService.AddEmployee(admin);
			Console.WriteLine("Admin created successfuly");

			Console.WriteLine("Bank Name: {0}, User Name: {1}", Bank.Name, admin.UserName);
			MainMenu();
		}

		public void Login()
		{
			string username = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter your Username");
			string password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Password");

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
					DisplayAdminMenu();
				}
				else if (this.LoggedInUser.Type.Equals(UserType.Staff))
				{
					DisplayStaffMenu();
				}
				else if (this.LoggedInUser.Type.Equals(UserType.AccountHolder))
				{
					this.AccountHolder = this.Bank.AccountHolders.Find(account => account.UserName.Equals(this.LoggedInUser.UserName));
					DisplayUserMenu();
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
				int option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 1:
						this.AddStaff();
						break;
					case 2:
						this.AddAccountHolder();
						break;
					case 3:
						this.DisplayBankEmployees();
						this.DisplayBankAccountHolders();
						break;
					case 4:
						this.UpdateCharges();
						break;
					case 5:
						this.NewCurrency();
						break;
					case 6:
						this.UpdateAccount();
						break;
					case 7:
						this.DeleteAccount();
						break;
					case 8:
						this.StaffService.XmlData();
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
				int option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 1:
						this.AddAccountHolder();
						break;
					case 2:
						this.DisplayBankAccountHolders();
						break;
					case 3:
						this.UpdateCharges();
						break;
					case 4:
						this.NewCurrency();
						break;
					case 5:
						this.StaffService.XmlData();
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
			double balance = this.AccountHolder.AvailableBalance;
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Transaction History\n4.View Balance\n5.Transfer Funds\n6.Revert Transaction\n7.Logout");
            try
            {
				int option = Convert.ToInt32(Console.ReadLine());
				switch (option)
				{
					case 1:
						double withdrawAmount = this.Utility.GetDoubleInput("Enter Withdraw Amount");
						double newBalanceAfterWithdraw = this.BankService.Withdraw(withdrawAmount, this.AccountHolder.AccountNumber);
						if (newBalanceAfterWithdraw == -1)
						{
							Console.WriteLine("Insufficient Balance");
						}
						else if (newBalanceAfterWithdraw == -2)
						{
							Console.WriteLine("Account Holder Not Found!");
						}
						else
						{
							this.AccountHolder.AvailableBalance = newBalanceAfterWithdraw;

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

					case 2:
						double depositAmount = this.Utility.GetDoubleInput("Enter Deposit Amount");
						double newBalanceAfterDeposit = this.BankService.Deposit(depositAmount, this.AccountHolder.AccountNumber);
						if (newBalanceAfterDeposit != -1)
							this.AccountHolder.AvailableBalance = newBalanceAfterDeposit;
						else
						{
							Console.WriteLine("Account Holder Not Found!");
							break;
						}

						Transaction depositTransaction = new Transaction();
						depositTransaction.Type = TransactionType.Deposit;
						depositTransaction.CreatedBy = this.LoggedInUser.Id;
						depositTransaction.Amount = depositAmount;
						if (this.TransactionService.AddTransaction(depositTransaction, this.AccountHolder.AccountNumber))
							Console.WriteLine("New Balance: {0}", this.AccountHolder.AvailableBalance);
						else
							Console.WriteLine("Unable to add transaction");

						break;

					case 3:
						Console.WriteLine("Transaction History");
						Console.WriteLine("Transaction Date\t\tTransaction Type\t\tTransaction Amount");
						foreach (Transaction transaction2 in this.TransactionService.GetTransactionsByAccount(this.AccountHolder.AccountNumber))
						{
							Console.WriteLine("{0}\t\t{1}\t\t\t{2}", transaction2.CreatedOn, transaction2.Type, transaction2.Amount);
						}

						break;

					case 4:
						Console.WriteLine("Current Balance: {0}", this.AccountHolder.AvailableBalance);
						break;

					case 5:
						this.TransferFunds();
						break;

					case 6:
						this.RevertTransaction();
						break;

					case 7:
						this.StaffService.XmlData();
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
			string accountNumber = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Please enter vendor's account number to transfer funds.");
			var user = this.Bank.AccountHolders.Find(account => account.AccountNumber.Equals(accountNumber));

            if (user != null)
            {
				double amount = this.Utility.GetDoubleInput("Enter Amount To Transfer");
				double balance = this.AccountHolder.AvailableBalance - amount;
				if (balance >= 0)
				{
					if(this.BankService.TransferAmount(amount, this.AccountHolder.AccountNumber, user.AccountNumber))
                    {
						Console.WriteLine("Funds transferred successfully");
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
				UserName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Staff username"),
				Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Staff password"),
				Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Staff Name"),
				Type = UserType.Staff
			};
			this.UserService.AddEmployee(staff);
		}

		public void AddAccountHolder()
		{
			AccountHolder accountHolder = new AccountHolder()
			{
				UserName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account Holder username"),
				Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password"),
				Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Account Holder Name")
			};
			this.UserService.AddAccountHolder(accountHolder);
		}
		
		public void UpdateAccount()
		{
			Console.WriteLine("Select account to update");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}
			
			string userName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			foreach (var account in this.Bank.AccountHolders.Where(account => account.UserName.ToLower() == userName.ToLower()))
			{
                if (account != null)
                {
					Console.WriteLine("Username: {0}. This account can now be updated ", userName);
					account.UserName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Update username of user: ");
					account.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Update password of user: ");
					account.Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Update Account Holder Name");
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

			string userName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ");
			this.Bank.AccountHolders.RemoveAll(account => account.UserName == userName);
			Console.WriteLine("User Account deleted successfully");
		}

		public void NewCurrency()
		{
			string code = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
			string name = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Currency Name:");
			Console.WriteLine("Enter Currency Value Cnoverted To INR:");
            try
            {
				int value = Convert.ToInt32(Console.ReadLine());
				this.StaffService.NewCurrency(code, name, value);
				Console.WriteLine("New Currency updated Successfully");
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
				this.TransactionService.RevertTransaction(id, date, this.AccountHolder.AccountNumber);
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

			string userName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter username : ");
			if(this.Bank.AccountHolders.Any(account=>account.UserName.ToLower() == userName.ToLower()))
			{
				Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", userName);
				string bankName = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter bankname : ");
				if(this.Bank.Branches.Any(bank=>bank.BankId.Equals(bankName.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"))))
				{
					Console.WriteLine("New charges of RTGS and IMPS are:-");
                    try
                    {
						int sameBankRTGS = Convert.ToInt32(Console.ReadLine());
						int sameBankIMPS = Convert.ToInt32(Console.ReadLine());
						BankType type = BankType.Same;
						this.StaffService.UpdateCharges(sameBankRTGS, sameBankIMPS, type);
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
						this.StaffService.UpdateCharges(differentBankRTGS, differentBankIMPS, type);
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