using System;
using BankApp.Model;
using BankApp.Services;
using System.Linq;
using System.Collections.Generic;

namespace BankApp
{
    public class BankApplication
	{
		private BankApp.Services.Utilities.Utility Utility { get; set; }
		private AccountService AccountService { get; set; }
		private BankService BankService { get; set; }
		private TransactionService TransactionService { get; set; }
		private StaffService StaffService { get; set; }
		public Bank Bank { get; set; }
		public User User { get; set; }
		public User LoggedInUser;

		public BankApplication()
		{
			this.Bank = new Bank();
			this.User = new User();
			this.Utility = new BankApp.Services.Utilities.Utility();
			this.TransactionService = new TransactionService(this.Bank,this.Utility);
			this.StaffService = new StaffService(this.Bank, this.Utility, this.User);
			this.AccountService = new AccountService(this.Bank, this.TransactionService, this.Utility);
			this.BankService = new BankService(this.Bank, this.Utility, this.User);
			this.MainMenu();
		}

		public void MainMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User Login\n3. Exit");

			int option = Convert.ToInt32(Console.ReadLine());
			switch (option)
			{
				case 1:
					SetupBank();
					break;
				case 2:
					Login();
					break;
				case 3:
					Exit();
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Please select option from the list");
					MainMenu();
					break;
			}
		}

		public void SetupBank()
		{
			this.Bank.Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Bank Name").ToUpper();
			this.Bank.Location = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Bank Location");
			this.Bank.Id = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			this.Bank.SameBankIMPSCharge = Constants.SameBankIMPS;
			this.Bank.SameBankRTGSCharge = Constants.SameBankRTGS;
			this.Bank.DifferentBankIMPSCharge = Constants.DifferentBankIMPS;
			this.Bank.DifferentBankRTGSCharge = Constants.DifferentBankRTGS;
			Console.WriteLine("Bank setup is completed. Please provide branch details");

			Branch branch = new Branch();
			branch.Location = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Branch Location");
			this.BankService.AddBranch(branch);

			Console.WriteLine("Branch details added. Please provide admin username and password to setup");

			Staff admin = new Staff();
			admin.Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter admin name").ToUpper();
			admin.UserName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter admin username").ToLower();
			admin.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter admin password");
			this.BankService.AddAdmin(admin);
			Console.WriteLine("Admin created successfuly");

			

			Console.WriteLine("Bank Name: {0}, User Name: {1}", Bank.Name, admin.UserName);
			MainMenu();
		}

		public void Login()
		{
			string username = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter your Username").ToLower();
			string password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Password");

			this.LoggedInUser = this.BankService.LogIn(username, password);
            try
            {
				if (this.LoggedInUser.Type.Equals("Admin"))
				{
					DisplayAdminMenu(this.LoggedInUser);
				}
				else if (this.LoggedInUser.Type.Equals("Staff"))
				{
					DisplayStaffMenu(this.LoggedInUser);
				}
				else if (this.LoggedInUser.Type.Equals("AccountHolder"))
				{
					foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
					{
						if (this.LoggedInUser.UserName.Equals(accountHolder.UserName))
						{
							DisplayUserMenu(accountHolder);
						}
					}
				}
			}
            
            catch (Exception)
            {
				Console.WriteLine("Invalid Credentials");
            }
			Login();
		}



		public void DisplayAdminMenu(User LoggedInUser)
		{
			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User Details\n4. Update Service Charges\n5. Add New Currency\n6. Update Account\n7. Delete Account\n8.Edit Transactions\n9.Logout");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					AddStaff();
					break;
				case 2:
					AddAccountHolder();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 3:
					BankUsers();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 4:
					UpdateCharges();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 5:
					NewCurrency();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 6:
					UpdateAccount();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 7:
					DeleteAccount();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 8:
					RevertTransaction();
					DisplayAdminMenu(LoggedInUser);
					break;
				case 9:
					this.StaffService.XmlData();
					Logout(this.LoggedInUser.Name);
					MainMenu();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					DisplayAdminMenu(LoggedInUser);
					break;

			}
		}

		public void DisplayStaffMenu(User LoggedInUser)
		{
			Console.WriteLine("1. Add Account Holder\n2. Display Bank User Details\n3. Update Service Charges\n4. Add New Currency\n5. Edit Transactions\n6. Logout");
			int option = Convert.ToInt32(Console.ReadLine());
			switch (option)
			{
				case 1:
					AddAccountHolder();
					DisplayStaffMenu(LoggedInUser);
					break;
				case 2:
					BankUsers();
					DisplayStaffMenu(LoggedInUser);
					break;
				case 3:
					UpdateCharges();
					DisplayStaffMenu(LoggedInUser);
					break;
				case 4:
					NewCurrency();
					DisplayStaffMenu(LoggedInUser);
					break;
				case 5:
					RevertTransaction();
					DisplayStaffMenu(LoggedInUser);
					break;
				case 6:
					this.StaffService.XmlData();
					Logout(this.LoggedInUser.Name);
					MainMenu();
					break;

				default:
					Console.WriteLine("Please select option from the list");
					DisplayStaffMenu(LoggedInUser);
					break;

			}
		}

		public void DisplayUserMenu(AccountHolder LoggedInAccountHolder)
		{
			double balance = LoggedInAccountHolder.InitialBalance;
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Transaction History\n4.View Balance\n5.Transfer Funds\n6.Logout");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					Console.WriteLine("Available Balance: {0}", LoggedInAccountHolder.InitialBalance);
					double withdrawAmount = this.Utility.GetDoubleInput("Enter Withdraw Amount");
					LoggedInAccountHolder.InitialBalance = this.BankService.Withdraw(withdrawAmount, LoggedInAccountHolder);
					Console.WriteLine("New Balance: {0}", LoggedInAccountHolder.InitialBalance);
					DisplayUserMenu(LoggedInAccountHolder);
					break;
				case 2:
					Console.WriteLine("Available Balance: {0}", LoggedInAccountHolder.InitialBalance);
					double depositAmount = this.Utility.GetDoubleInput("Enter Deposit Amount");
					LoggedInAccountHolder.InitialBalance = this.BankService.Deposit(depositAmount, LoggedInAccountHolder);
					Console.WriteLine("New Balance: {0}", LoggedInAccountHolder.InitialBalance);
					DisplayUserMenu(LoggedInAccountHolder);
					break;
				case 3:
					Console.WriteLine("Transaction History");
					foreach(Transaction transaction in this.User.Transactions)
                    {
						Console.WriteLine("Transaction Date: {0} , Transaction Type: {1} , Transaction Amount: {2}", transaction.CreateDate, transaction.Type,transaction.Amount);
                    }
					break;

				case 4:
					Console.WriteLine("Current Balance: {0}", LoggedInAccountHolder.InitialBalance);
					DisplayUserMenu(LoggedInAccountHolder);
					break;
				case 5:
					TransferFunds(LoggedInAccountHolder);
					DisplayUserMenu(LoggedInAccountHolder);
					break;
				case 6:
					this.StaffService.XmlData();
					Logout(LoggedInAccountHolder.Name);
					MainMenu();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					DisplayUserMenu(LoggedInAccountHolder);
					break;
			}
		}

		public void TransferFunds(AccountHolder accountHolder)
        {
			string accountNumber = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Please enter vendor's account number to transfer funds.");
			foreach (AccountHolder accountHolder1 in this.Bank.AccountHolders)
			{
				if (accountHolder1.AccountNumber.Equals(accountNumber))
				{
					double amount = this.Utility.GetDoubleInput("Enter Amount To Transfer");
					double balance = accountHolder.InitialBalance - amount;
					if(balance >= 0)
                    {
						this.AccountService.TransferAmount(amount, accountHolder, accountHolder1);
					}
                    else
                    {
						Console.WriteLine("Insufficient Balance");
                    }
				}
			}

		}

		public void AddStaff()
		{
			string userName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Staff username").ToLower();
			string password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Staff password");
			string name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Staff Name").ToUpper();
			this.AccountService.CreateStaffAccount(userName,password,name);
			DisplayAdminMenu(this.LoggedInUser);
		}

		public void AddAccountHolder()
		{
			string userName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account Holder username").ToLower();
			string password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password");
			string name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Enter Account Holder Name").ToUpper();
			this.AccountService.CreateUserAccount(userName,password,name);
		}
		
		public void UpdateAccount()
		{
			Console.WriteLine("Select account to update");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}

			string strname = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ").ToLower();
			foreach (var account in this.Bank.AccountHolders.Where(w => w.UserName == strname))
			{
				Console.WriteLine("Username: {0}. This account can now be updated ", strname);
				account.UserName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Update username of user: ").ToLower();
				account.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Update password of user: ");
				account.Name = this.Utility.GetStringInput("^[a-zA-Z ]{3,}$", "Update Account Holder Name").ToUpper();
			}
			Console.WriteLine("User Account updated successfully");
		}

		public void DeleteAccount()
		{
			Console.WriteLine("Select account to delete");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}

			string strname = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter Account username: ").ToLower();
			this.Bank.AccountHolders.RemoveAll(x => x.UserName == strname);
			Console.WriteLine("User Account deleted successfully");
		}

		public void NewCurrency()
		{
			string code = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
			string name = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Currency Name:");
			Console.WriteLine("Enter Currency Value Cnoverted To INR:");
			int value = Convert.ToInt32(Console.ReadLine());
			this.StaffService.NewCurrency(code, name, value);
			Console.WriteLine("New Currency updated Successfully");
		}

		public void BankUsers()
		{
			Console.WriteLine("Bank Staff Users");
			List<string> x = this.StaffService.BankEmployees();
			foreach (string s in x)
			{
				Console.WriteLine(s);
			}
			Console.WriteLine("Bank Account Holders");
			List<string> y = this.StaffService.BankAccountHolders();
			foreach (string ah in y)
			{
				Console.WriteLine(ah);
			}
		}

		public void RevertTransaction()
		{
			Console.WriteLine("Transactions of users by their ID and date are as follows:-");

			foreach (Transaction transaction in this.User.Transactions)
			{
				Console.WriteLine("Transaction ID: {0} , Transaction Date: {1}", transaction.ID, transaction.CreateDate);
			}
			Console.WriteLine("Enter Transaction ID and Transaction Date to revert that transaction");
			string id = Console.ReadLine();
			DateTime date = Convert.ToDateTime(Console.ReadLine());
			this.StaffService.RevertTransaction(id, date);

		}

		public void UpdateCharges()
		{
			Console.WriteLine("Select account from the list");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				Console.WriteLine(user.UserName + " " + user.Id);
			}

			string userName = this.Utility.GetStringInput("^[a-zA-Z@._]+$", "Enter username: ").ToLower();
			if(this.Bank.AccountHolders.Any(user=>user.UserName==userName))
			{
				Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", userName);
				string bankName = this.Utility.GetStringInput("^[a-zA-Z ]+$", "Enter bankname of user: ");
				if(this.Bank.Branches.Any(s=>s.BankId.Equals(bankName.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"))))
				{
					Console.WriteLine("Since same bank, the new charges of RTGS and IMPS are:-");
					int sameBankRTGS =Convert.ToInt32(Console.ReadLine());
					int sameBankIMPS = Convert.ToInt32(Console.ReadLine());
					this.StaffService.UpdateChargesSameBank(sameBankRTGS, sameBankIMPS);
				}
				else
				{
					Console.WriteLine("Since different bank, the new charges of RTGS and IMPS are:-");
					int differentBankRTGS = Convert.ToInt32(Console.ReadLine());
					int differentBankIMPS = Convert.ToInt32(Console.ReadLine());
					this.StaffService.UpdateChargesDifferentBank(differentBankRTGS, differentBankIMPS);
				}

			}

		}

		public void Logout(string user)
		{
			Console.WriteLine("Goodbye "+user);
		}

		void Exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}

	}
}