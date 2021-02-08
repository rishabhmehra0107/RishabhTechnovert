using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace BankApplication
{
	public class InitiateBank
	{

		public List<Admin> getAdmin = new List<Admin>();
		public List<Branch> getBranch = new List<Branch>();
		
		public void BankMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n");
			Console.WriteLine("1. Setup New Bank \n2. User login\n3. Exit");
			string enterchoice = Console.ReadLine();
			int SubmitChoice = Convert.ToInt32(enterchoice);

			switch (SubmitChoice)
			{
				case 1:
					SetupBank();
					break;
				case 2:
					UserLogin();
					break;
				case 3:
					Exit();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					BankMenu();
					break;


			}
		}

		public void SetupBank()
		{
			string bankName = " ";
			string userName = " ";
			string password = " ";
			string role = "Admin";
			string location = " ";
			

			Console.WriteLine("Enter bankname: ");
			bankName = Console.ReadLine();
			if (Regex.IsMatch(bankName, "^[a-zA-Z]+$"))
			{
				Console.WriteLine("Enter location: ");
				location = Console.ReadLine();

				if (Regex.IsMatch(location, "^[a-zA-Z]+$"))
				{
					Console.WriteLine("Enter Admin username: ");
					userName = Console.ReadLine();

					if (Regex.IsMatch(userName, "^[a-zA-Z]+$"))
					{
						Console.WriteLine("Enter Admin password: ");
						password = Console.ReadLine();
					}
					else
					{
						Console.WriteLine("Invalid Username");
					}
				}
				else
				{
					Console.WriteLine("Invalid Location");
				}

			}
			else
			{
				Console.WriteLine("Inavlid Bank");
			}


			var adminDetails = new Admin(userName, password, role);
			getAdmin.Add(adminDetails);

			var branchDetails = new Branch(bankName, location);
			getBranch.Add(branchDetails);

			BankSetup newBank = new BankSetup(bankName, userName, password);
			BankMenu();
		}

		void UserLogin()
		{
			Console.WriteLine("1. Bank Staff \n2. Account Holder");
			string typechoice = Console.ReadLine();
			int enterChoice = Convert.ToInt32(typechoice);

			switch (enterChoice)
			{
				case 1:
					StaffUser();
					break;
				case 2:
					AccountUser();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					UserLogin();
					break;


			}

			void AccountUser()
			{
				string user = " ";
				string pass = " ";

				Console.WriteLine("Enter Admin username: ");
				user = Console.ReadLine();
				Console.WriteLine("Enter Admin password: ");
				pass = Console.ReadLine();

				foreach (Admin ad in getAdmin)
				{
					if (ad.userName == user && ad.password == pass)
					{
						Account newAccount = new Account();
						newAccount.setupAccount();
					}
					else
					{
						Console.WriteLine("Wrong Username or Password for Admin");
					}

				}

				BankMenu();
			}

			void StaffUser()
			{
				string bankk = " ";
				string user = " ";
				string pass = " ";

				Console.WriteLine("Enter bankname: ");
				bankk = Console.ReadLine();
				Console.WriteLine("Enter Admin username: ");
				user = Console.ReadLine();
				Console.WriteLine("Enter Admin password: ");
				pass = Console.ReadLine();

				foreach (Admin ad in getAdmin)
                {
					if(ad.userName==user && ad.password == pass)
                    {
						StaffAccount newStaff = new StaffAccount(bankk, user, pass);
					}
                    else
                    {
						Console.WriteLine("Wrong Username or Password for Admin");
                    }
						
				}
			
				BankMenu();
			}


		}

		void Exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}
