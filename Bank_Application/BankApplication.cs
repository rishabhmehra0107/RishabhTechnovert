using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bank_Application.Utilities;
using Bank_Application.Services;

namespace Bank_Application
{
	public class BankApplication
	{

		Bank Bank;
		private Utility Utility { get; set; }
		private AccountService AccountService { get; set; }

		public BankApplication()
        {
			this.Bank = new Bank();
			this.Utility = new Utility();
			this.AccountService = new AccountService();
			this.mainMenu();
        }
		Admin Admin = new Admin();

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
			Bank.Name = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Bank Name");
			Bank.Location = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Bank Location");
			Console.WriteLine("Bank setup is completed. Please provide admin username and password for admin");

			
			Admin.UserName = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter admin username");
			Admin.Password = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter admin password");
			Bank.Admins.Add(Admin);
			
			Branch branch = new Branch();

			branch.BankId = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			branch.BankLocation = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Branch Location");
			branch.Id = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Branch Id");
			Bank.Branches.Add(branch);
			Console.WriteLine("Bankname: {0}, Username: {1}, Password {2}",Bank.Name,Admin.UserName,Admin.Password);
			mainMenu();
		}

		void login()
		{
			User user = new User() { Type = "AccountHolder" };
			Staff staff = new Staff() { Type = "BankStaff" };
			
			string userType = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter User Type");
			if (user.Type.Equals(userType))
            {
				accountUser();
            }
			else if (staff.Type.Equals(userType))
            {
				staffUser();
            }
            else
            {
				login();
            }
			
		}

		void staffUser()
		{
			string bankk = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Bank Name");
			string user = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Admin Username");
			string pass = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Admin Password");

			
			if (Admin.UserName.Equals(user) && Admin.Password.Equals(pass))
			{
				StaffAccount newStaff = new StaffAccount(bankk, user, pass);
			}
			else
			{
				Console.WriteLine("Wrong Username or Password for Admin");
			}

			mainMenu();
		}

		void accountUser()
		{
			string user = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Admin Username");
			string pass = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Admin Password");

			if (Admin.UserName.Equals(user) && Admin.Password.Equals(pass))
			{
				
				Account account = new Account();
				account.setUpAccount();
				
			}
			else
			{
				Console.WriteLine("Wrong Username or Password for Admin");
			}



			mainMenu();
		}

		void exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}
