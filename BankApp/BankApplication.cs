﻿using System;
using BankApp.Utilities;
using BankApp.Services;
using System.Linq;
using System.Collections.Generic;

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
		public string LoggedInUser;

		public BankApplication()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.TransactionService = new TransactionService(this.Bank,this.Utility);
			this.StaffService = new StaffService(this.Bank,this.TransactionService,this.Utility);
			this.AccountService = new AccountService(this.Bank, this.TransactionService, this.Utility);
			this.BankServices = new BankServices(this.Bank, this.Utility);
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
					break;
				default:
					Console.WriteLine("Please select option from the list");
					MainMenu();
					break;
			}
		}

		public void SetupBank()
		{
			this.Bank.Name = this.Utility.GetStringInput("^[a-zA-Z]{3,}$", "Enter Bank Name");
			this.Bank.Location = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Bank Location");
			this.Bank.Id = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
			this.Bank.SameBankIMPSCharge =5;
			this.Bank.SameBankRTGSCharge =0;
			this.Bank.DifferentBankIMPSCharge =6;
			this.Bank.DifferentBankRTGSCharge =2;
			Console.WriteLine("Bank setup is completed. Please provide branch details");

			Branch branch = new Branch();
			branch.Location = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Branch Location");
			this.BankServices.AddBranch(branch);

			Console.WriteLine("Branch details added. Please provide admin username and password to setup");

			Admin admin = new Admin();
			admin.Name = this.Utility.GetStringInput("^[a-zA-Z]{3,}$", "Enter admin name");
			admin.UserName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter admin username");
			admin.Password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter admin password");
			this.BankServices.AddAdmin(admin);
			Console.WriteLine("Admin created successfuly");

			

			Console.WriteLine("Bank Name: {0}, User Name: {1}", Bank.Name, admin.UserName);
			MainMenu();
		}

		public void Login()
		{
			string user = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter your Username");
			string pass = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter your Password");
			if (this.Bank.Admins.Find(element => element.UserName.Equals(user) && element.Password.Equals(pass)) != null)
			{
				Console.WriteLine("Admin {0} present in the system ", user);
				LoggedInUser = "Admin";
				ShowAdminMenu(LoggedInUser);
			}
			else if (this.Bank.Staffs.Find(element => element.UserName == user && element.Password == pass) != null)
			{
				Console.WriteLine("Employee {0} present in the system ", user);
				LoggedInUser = "Staff";
				ShowStaffMenu(LoggedInUser);
			}
			else if (this.Bank.AccountHolders.Any(element => element.UserName == user && element.Password == pass))
			{
				Console.WriteLine("Account Holder {0} present in the system ", user);
				LoggedInUser = "AccountHolder";
				ShowUserMenu(1000, LoggedInUser);
			}
			else
			{
				Console.WriteLine("Invalid credentials");
			}
			Login();

		}



		public void ShowAdminMenu(string LoggedInUser)
		{
			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User details\n4. Update Service Charges\n5. Add new Currency\n6. Update Account\n7. Delete Account\n8.Edit Transactions\n9.Logout");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					AddStaff();
					break;
				case 2:
					AddAccountHolder();
					ShowAdminMenu(LoggedInUser);
					break;
				case 3:
					BankUsers();
					ShowAdminMenu(LoggedInUser);
					break;
				case 4:
					UpdateCharges();
					ShowAdminMenu(LoggedInUser);
					break;
				case 5:
					NewCurrency();
					ShowAdminMenu(LoggedInUser);
					break;
				case 6:
					UpdateAccount();
					ShowAdminMenu(LoggedInUser);
					break;
				case 7:
					DeleteAccount();
					ShowAdminMenu(LoggedInUser);
					break;
				case 8:
					RevertTransaction();
					ShowAdminMenu(LoggedInUser);
					break;
				case 9:
					Logout(LoggedInUser);
					this.StaffService.XmlData();
					MainMenu();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					ShowAdminMenu(LoggedInUser);
					break;

			}
		}

		public void ShowStaffMenu(string LoggedInUser)
		{
			Console.WriteLine("1. Add Account Holder\n2.Display Bank User details\n3. Update Service Charges\n4. Add new Currency\n5. Edit Transactions\n6. Logout");
			int option = Convert.ToInt32(Console.ReadLine());
			switch (option)
			{
				case 1:
					AddAccountHolder();
					ShowStaffMenu(LoggedInUser);
					break;
				case 2:
					BankUsers();
					ShowStaffMenu(LoggedInUser);
					break;
				case 3:
					UpdateCharges();
					ShowStaffMenu(LoggedInUser);
					break;
				case 4:
					NewCurrency();
					ShowStaffMenu(LoggedInUser);
					break;
				case 5:
					RevertTransaction();
					ShowStaffMenu(LoggedInUser);
					break;
				case 6:
					Logout(LoggedInUser);
					MainMenu();
					break;

				default:
					Console.WriteLine("Please select option from the list");
					ShowStaffMenu(LoggedInUser);
					break;

			}
		}

		public void ShowUserMenu(double InitialBalance, string LoggedInUser)
		{
			double balance = InitialBalance;
			Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Deposit History\n4.Withdraw History\n5.View Balance\n6.Logout");
			int option = Convert.ToInt32(Console.ReadLine());

			switch (option)
			{
				case 1:
					Console.WriteLine("Available Balance: {0}", balance);
					double withdrawAmt = this.Utility.GetDoubleInput("Enter Withdraw Amount");
					balance= this.TransactionService.Withdraw(withdrawAmt, LoggedInUser);
					Console.WriteLine("New Balance: {0}", balance);
					ShowUserMenu(balance, LoggedInUser);
					break;
				case 2:
					Console.WriteLine("Available Balance: {0}", balance);
					double depositAmt = this.Utility.GetDoubleInput("Enter Deposit Amount");
					balance = this.TransactionService.Deposit(depositAmt, LoggedInUser);
					Console.WriteLine("New Balance: {0}", balance);
					ShowUserMenu(balance, LoggedInUser);
					break;
				case 3:
					Console.WriteLine("Deposit History");
					foreach (Transaction transaction in this.Bank.Transactions)
					{
						if (transaction.Type.Equals("Deposit"))

						{
							Console.WriteLine("Deposit Amount: {0}\nTransaction Date: {1}\nTransaction ID: {2}", transaction.Amount, transaction.CreateDate, transaction.ID);
						}
					}
					ShowUserMenu(balance, LoggedInUser);
					break;
				case 4:
					Console.WriteLine("Withdraw History");
					foreach (Transaction transaction in this.Bank.Transactions)
					{
						if (transaction.Type.Equals("Withdraw"))

						{
							Console.WriteLine("Withdraw Amount: {0}\nTransaction Date: {1}\nTransaction ID: {2}", transaction.Amount, transaction.CreateDate, transaction.ID);
						}
					}
					ShowUserMenu(balance, LoggedInUser);
					break;
				case 5:
					Console.WriteLine("Current Balance: {0}",balance);
					ShowUserMenu(balance, LoggedInUser);
					break;
				case 6:
					Logout(LoggedInUser);
					MainMenu();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					ShowUserMenu(balance, LoggedInUser);
					break;
			}
		}

		public void AddStaff()
		{
			string userName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Staff username");
			string password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Staff password");
			string name = this.Utility.GetStringInput("^[a-zA-Z]{3,}$", "Enter Staff Name");
			this.AccountService.CreateStaffAccount(userName,password,name);
			ShowAdminMenu("Admin");
		}

		public void AddAccountHolder()
		{
			string userName = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Account Holder username");
			string password = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Account Holder password");
			string name = this.Utility.GetStringInput("^[a-zA-Z]{3,}$", "Enter Account Holder Name");
			this.AccountService.CreateUserAccount(userName,password,name);
		}
		
		public void UpdateAccount()
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
				account.Name = this.Utility.GetStringInput("^[a-zA-Z]{3,}$", "Update Account Holder Name");
				account.Type = "AccountHolder";
				account.InitialBalance = 1000;
				account.AccountNumber = account.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
				account.AccountType = "SavingsAccount";
				account.Id = "31";
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
			string strname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Account username: ");
			this.Bank.AccountHolders.RemoveAll(x => x.UserName == strname);
			Console.WriteLine("User Account deleted successfully");
		}

		public void NewCurrency()
		{
			string code = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
			string name = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Currency Name:");
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

			foreach (Transaction transaction in this.Bank.Transactions)
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

			string strname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter username: ");
			if(this.Bank.AccountHolders.Any(user=>user.UserName==strname))
			{
				Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", strname);
				string bname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter bankname of user: ");

				if(this.Bank.Branches.Any(s=>s.BankId.Equals(bname.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy"))))
				{
					Console.WriteLine("Since same bank, the new charges of RTGS and IMPS are:-");
					int srtgs =Convert.ToInt32(Console.ReadLine());
					int simps = Convert.ToInt32(Console.ReadLine());
					this.StaffService.UpdateChargesSameBank(srtgs, simps);
				}
				else
				{
					Console.WriteLine("Since different bank, the new charges of RTGS and IMPS are:-");
					int drtgs = Convert.ToInt32(Console.ReadLine());
					int dimps = Convert.ToInt32(Console.ReadLine());
					this.StaffService.UpdateChargesDifferentBank(drtgs, dimps);
				}

			}

		}

		public void Logout(string user)
		{
			Console.WriteLine("Goodbye "+user);
			MainMenu();
		}

		void Exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}