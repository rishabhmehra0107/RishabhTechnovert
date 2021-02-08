using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Bank_Application
{
	public class BankApplication
	{
		
		Bank Bank;

		public BankApplication()
        {
			this.Bank = new Bank();
        }

		Admin ad = new Admin();
		
		public void BankMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User login\n3. Exit");
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
			Console.WriteLine("Enter bankname: ");
			this.Bank.Name = GetStringInput(Console.ReadLine());
			Console.WriteLine("Enter location: ");
			this.Bank.Location = GetStringInput(Console.ReadLine());
			Console.WriteLine("Bank setup is completed. Please provide admin details");
			Admin admin = new Admin() { Type = "Admin" };
			Console.WriteLine("Enter Admin username: ");
			var name= admin.UserName = GetStringInput(Console.ReadLine());
			Console.WriteLine("Enter Admin password: ");
			var password=admin.Password = Console.ReadLine();

			this.Bank.admin.Add(ad);
			

			string GetStringInput(string str)
			{
				if (Regex.IsMatch(str, "^[a-zA-Z]+$"))
				{
					return str;
				}
                else
                {
					return "Invalid Input";
                }
			}

			Branch branch = new Branch();

			branch.BankId = this.Bank.Name.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			branch.BankLocation = GetStringInput(Console.ReadLine());
			branch.Id = GetStringInput(Console.ReadLine());

			BankSetup newBank = new BankSetup(this.Bank.Name, name, password);
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

				
					if (ad.UserName == user && ad.Password == pass)
					{
						Account newAccount = new Account();
						newAccount.setupAccount();
					}
					else
					{
						Console.WriteLine("Wrong Username or Password for Admin");
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

				
					
						StaffAccount newStaff = new StaffAccount(bankk, user, pass);
					
						
				
			
				BankMenu();
			}


		}

		void Exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}
