using System;
using BankApp.Utilities;
using BankApp.Services;
using System.Linq;

namespace BankApp
{
	public class BankApplication
	{
		private Utility Utility { get; set; }
		private AccountService AccountService { get; set; }
		private BankServices BankServices { get; set; }
		private TransactionService TransactionService { get; set; }
		private StaffService StaffService { get; set; }
		public Bank Bank { get; set; }
		public string PresentUser;

		public BankApplication()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.TransactionService = new TransactionService(this.Bank,this.Utility);
			this.StaffService = new StaffService(this.Bank,this.TransactionService,this.Utility);
			this.AccountService = new AccountService(this.Bank, this.TransactionService, this.Utility);
			this.BankServices = new BankServices(this.Bank, this.Utility);
			this.mainMenu();
		}

		public void mainMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User login\n3. Exit");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					setupBank();
					break;
				case 2:
					login();
					break;
				case 3:
					exit();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					mainMenu();
					break;


			}
		}

		public void setupBank()
		{
			this.Bank.Name = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Bank Name");
			this.Bank.Location = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Bank Location");
			this.Bank.Id = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			Console.WriteLine("Bank setup is completed. Please provide branch details");

			Branch branch = new Branch();

			branch.BankId = this.Bank.Id;
			branch.Location = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Branch Location");
			branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
			this.BankServices.AddBranch(branch);

			//this.AccountService.storeBankData(Bank, branch);
			Console.WriteLine("Branch details added. Please provide admin username and password for admin");

			Admin admin = new Admin();
			admin.Type = "Admin";
			admin.Name = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter admin name");
			admin.UserName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter admin username");
			admin.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter admin password");
			admin.Id = "101";
			this.BankServices.AddAdmin(admin);
			Console.WriteLine("Admin created successfuly");

			

			Console.WriteLine("Bank Name: {0}, User Name: {1}, Password {2}", Bank.Name, admin.UserName, admin.Password);
			mainMenu();
		}

		public void login()
		{
			string user = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter your Username");
			string pass = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Password");
			if (this.Bank.Admins.Find(element => element.UserName.Equals(user) && element.Password.Equals(pass)) != null)
			{
				PresentUser = "Admin";
				showAdminMenu(PresentUser);
			}
			else if (this.Bank.Staffs.Find(element => element.UserName == user && element.Password == pass) != null)
			{
				this.AccountService.SetupStaffAccount(user, pass);
				PresentUser = "Staff";
				showStaffMenu(PresentUser);
			}
			else if (this.Bank.AccountHolders.Any(element => element.UserName == user && element.Password == pass))
			{
				this.AccountService.SetupUserAccount(user, pass);
				PresentUser = "AccountHolder";
				showUserMenu(1000,PresentUser);
			}
			else
			{
				Console.WriteLine("Invalid credentials");
			}
			login();

		}



		public void showAdminMenu(string PresentUser)
		{
			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User details\n4. Update Service Charges\n5. Add new Currency\n6. Update Account\n7. Delete Account\n8.Logout");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					addStaff();
					break;
				case 2:
					addAccountHolder();
					break;
				case 3:
					this.StaffService.BankUsers();
					break;
				case 4:
					this.StaffService.UpdateCharges();
					break;
				case 5:
					this.StaffService.NewCurrency();
					break;
				case 6:
					updateAccount();
					break;
				case 7:
					deleteAccount();
					break;
				case 8:
					this.StaffService.Logout();
					this.StaffService.XmlData();
					mainMenu();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					showAdminMenu(PresentUser);
					break;

			}
		}

		public void showStaffMenu(string PresentUser)
		{
			Console.WriteLine("1. Add Account Holder\n2.Display Bank User details\n3. Update Service Charges\n4. Add new Currency\n5. Logout");
			int option = Convert.ToInt32(Console.ReadLine());
			switch (option)
			{
				case 1:
					addAccountHolder();
					break;
				case 2:
					this.StaffService.BankUsers();
					break;
				case 3:
					this.StaffService.UpdateCharges();
					break;
				case 4:
					this.StaffService.NewCurrency();
					break;
				case 5:
					this.StaffService.Logout();
					mainMenu();
					break;

				default:
					Console.WriteLine("Please select option from the list");
					showStaffMenu(PresentUser);
					break;

			}
		}

		public void showUserMenu(double InitialBalance, string PresentUser)
		{
			double balance = InitialBalance;
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Deposit History\n4.Withdraw History\n5.Logout");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					Console.WriteLine("Available Balance: {0}", InitialBalance);
					double withdrawAmt = this.Utility.GetDoubleInput("Enter Withdraw Amount");
					double w= this.TransactionService.Withdraw(withdrawAmt,PresentUser);
					Console.WriteLine("New Balance: {0}", w);
					showUserMenu(w, PresentUser);
					break;
				case 2:
					Console.WriteLine("Available Balance: {0}", InitialBalance);
					double depositAmt = this.Utility.GetDoubleInput("Enter Deposit Amount");
					double d = this.TransactionService.Deposit(depositAmt,PresentUser);
					Console.WriteLine("New Balance: {0}", d);
					showUserMenu(d, PresentUser);
					break;

				case 3:
					Console.WriteLine("Deposit History");
					this.TransactionService.DepositHistory();
					break;
				case 4:
					Console.WriteLine("Withdraw History");
					this.TransactionService.WithdrawHistory();
					break;
				case 5:
					this.TransactionService.Logout();
					break;
			}
		}

		public void addStaff()
		{
			Staff staff = new Staff();
			staff.UserName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Staff username");
			staff.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Staff password");
			this.AccountService.CreateStaffAccount(staff);
			showAdminMenu("Admin");
		}

		public void addAccountHolder()
		{
			AccountHolder accountHolder = new AccountHolder();
			accountHolder.UserName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Account Holder username");
			accountHolder.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password");
			this.AccountService.CreateUserAccount(accountHolder);
		}
		
		public void updateAccount()
		{
			Console.WriteLine("Select account to update");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}
			string strname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Account username: ");


			foreach (var account in this.Bank.AccountHolders.Where(w => w.UserName == strname))
			{
				Console.WriteLine("Username: {0}. This account can now be updated ", strname);
				account.UserName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Update username of user: ");
				account.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Update password of user: ");
				account.Name = this.Utility.GetStringInput("^[a-zA-Z]+$", "Update Account Holder Name");
				account.Type = "AccountHolder";
				account.InitialBalance = 1000;
				account.AccountNumber = account.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
				account.AccountType = "SavingsAccount";
				account.Id = "31";
				this.AccountService.StoreUpdateAccount(account, strname);
			}
			Console.WriteLine("User Account updated successfully");
		}
		public void deleteAccount()
		{
			Console.WriteLine("Select account to delete");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}
			string strname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Account username: ");
			
			this.Bank.AccountHolders.RemoveAll(x => x.UserName == strname);
			this.AccountService.DeleteStoredAccount(strname);
			Console.WriteLine("User Account deleted successfully");
		}
		
		public void logout()
		{
			Console.WriteLine("Goodbye");
			mainMenu();
		}
		void exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}