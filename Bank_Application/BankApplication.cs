using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bank_Application.Utilities;
using Bank_Application.Services;
using System.Linq;
namespace Bank_Application
{
	public class BankApplication
	{

		
		private Utility Utility { get; set; }
		private AccountService AccountService { get; set; }
		private TransactionService TransactionService { get; set; }
		private StaffService StaffService { get; set; }

		public Bank Bank { get; set; }

		public BankApplication()
        {
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.TransactionService = new TransactionService();
			this.StaffService = new StaffService();
			this.AccountService = new AccountService(this.Bank,this.TransactionService,this.StaffService);
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
			this.Bank.Name = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Bank Name");
			this.Bank.Location = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Bank Location");
			this.Bank.Id = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"); 
			Console.WriteLine("Bank setup is completed. Please provide admin username and password for admin");

			Admin admin = new Admin();
			admin.Type = "Admin";
			admin.UserName = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter admin username");
			admin.Password = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter admin password");
			this.Bank.Admins.Add(admin);

			Console.WriteLine("Admin created successfully. Please provide branch details");

			Branch branch = new Branch();

			branch.BankId = this.Bank.Id;
			branch.Location = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Branch Location");
			branch.Id = $"{this.Bank.Id} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";
			this.Bank.Branches.Add(branch);
			Console.WriteLine("Bank Name: {0}, User Name: {1}, Password {2}",Bank.Name,admin.UserName,admin.Password);
			mainMenu();
		}
		
		public void login()
		{
			string user = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter your Username");
			string pass = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter your Password");
			if (this.Bank.Admins.Find(element => element.UserName.Equals(user) && element.Password.Equals(pass))!=null)
			{
				showAdminMenu();
			}
			else if (this.Bank.Staffs.Find(element => element.UserName == user && element.Password == pass)!=null)
			{
				this.AccountService.setupStaffAccount(user, pass);
				showStaffMenu();
			}
			else if (this.Bank.AccountHolders.Any(element => element.UserName == user && element.Password == pass))
			{
				this.AccountService.setupUserAccount(user, pass);
			}
			else
            {
				Console.WriteLine("Invalid credentials");
            }
			login();
			
		}

		

		public void showAdminMenu()
        {
			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User details\n4. Update Service Charges\n5. Add new Currency\n6. Update Account\n7. Logout");
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
					bankUsers();
					break;
				case 4:
					updateCharges();
					break;
				case 5:
					this.StaffService.newCurrency();
					break;
				case 6:
					updateAccount();
					break;
				case 7:
					logout();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					showAdminMenu();
					break;

			}
		}

		public void showStaffMenu()
		{
			Console.WriteLine("1. Add Account Holder\n2.Display Bank User details\n3. Update Service Charges\n4. Add new Currency\n5. Logout");
			int nextChoice = Convert.ToInt32(Console.ReadLine());
			switch (nextChoice)
			{
				case 1:
					addAccountHolder();
					break;
				case 2:
					bankUsers();
					break;
				case 3:
					updateCharges();
					break;
				case 4:
					this.StaffService.newCurrency();
					break;
				case 5:
					logout();
					break;

				default:
					Console.WriteLine("Please select option from the list");
					showStaffMenu();
					break;

			}
		}

		public void addStaff()
		{
			Staff staff = new Staff();
			staff.UserName = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Staff username");
			staff.Password = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Staff password");
			this.AccountService.createStaffAccount(staff);
			showAdminMenu();
		}

		public void addAccountHolder()
		{
			AccountHolder accountHolder = new AccountHolder();
			accountHolder.UserName = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Account Holder username");
			accountHolder.Password = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Account Holder password");
			this.AccountService.createUserAccount(accountHolder);
		}

		public void bankUsers()
		{
			Console.WriteLine("Bank Staff Users");
			foreach (Staff staff in this.Bank.Staffs)
			{
				Console.WriteLine(staff.UserName);
			}
			Console.WriteLine("Bank Account Holders");
			foreach (AccountHolder accountHolder in this.Bank.AccountHolders)
			{
				Console.WriteLine(accountHolder.UserName);
			}
		}

		public void updateAccount()
        {
			Console.WriteLine("Select account to update");
			foreach(AccountHolder accountHolder in this.Bank.AccountHolders)
            {
				Console.WriteLine(accountHolder.UserName);
            }
			string strname = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Account username: ");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				if (user.UserName == strname)
				{
					Console.WriteLine("Username: {0}. This account can now be updated ", strname);

					string bname = this.Utility.getStringInput("^[a-zA-Z]+$", "Update bankname of user: ");
					Branch branch = new Branch();
					branch.BankId = bname.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
					branch.Location = this.Utility.getStringInput("^[a-zA-Z]+$", "Update Branch Location: ");
					branch.Id = $"{branch.BankId} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";

					this.Bank.Branches.Add(branch);

					user.UserName= this.Utility.getStringInput("^[a-zA-Z]+$", "Update username of user: ");
					user.Password= this.Utility.getStringInput("^[a-zA-Z]+$", "Update password of user: ");
					user.Name = this.Utility.getStringInput("^[a-zA-Z]+$", "Update Account Holder Name");
					user.Type = "AccountHolder";
					user.InitialBalance = 1000;
					user.AccountNumber = user.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
					user.AccountType = "Savings account";
					user.Id = "31";
					Console.WriteLine("User Account updated successfully");
				}

			}
		}

		public void updateCharges()
		{
			Console.WriteLine("Select account from the list");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				Console.WriteLine(user.UserName + " " + user.Id);
			}

			string strname = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter username: ");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				if (user.UserName == strname)
				{
					Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", strname);

					string bname = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter bankname of user: ");
					Branch branch = new Branch();
					branch.BankId = bname.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
					branch.Location = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Branch Location: ");
					branch.Id = $"{branch.BankId} {branch.Location}{DateTime.UtcNow.ToString("MMddyy")}";

					this.Bank.Branches.Add(branch);
					int flag = 0;
					foreach (Branch branch1 in this.Bank.Branches)
					{
						if (branch1.BankId == bname.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"))
						{
							Console.WriteLine("Since same bank, the new charges are:-");
							Console.WriteLine("RTGS: ");
							int srtgs = Convert.ToInt32(Console.ReadLine());
							Console.WriteLine("IMPS: ");
							int simps = Convert.ToInt32(Console.ReadLine());
							flag = 1;
							break;
						}
					}
					if (flag == 0)
					{
						Console.WriteLine("Since different bank, the new charges are:-");
						Console.WriteLine("RTGS: ");
						int drtgs = Convert.ToInt32(Console.ReadLine());
						Console.WriteLine("IMPS: ");
						int dimps = Convert.ToInt32(Console.ReadLine());
					}
				}

			}

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
