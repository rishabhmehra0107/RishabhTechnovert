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

		Admin ad;
		
		public void mainMenu()
		{
			Console.WriteLine("Welcome to Bank Application\n1. Setup New Bank \n2. User login\n3. Exit");
			string enterChoice = Console.ReadLine();
			int submitChoice = Convert.ToInt32(enterChoice);

			switch (submitChoice)
			{
				case 1:
					setupBank();
					break;
				case 2:
					userLogin();
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
			Console.WriteLine("Enter bankname: ");
			this.Bank.Name = getStringInput(Console.ReadLine());
			Console.WriteLine("Enter location: ");
			this.Bank.Location = getStringInput(Console.ReadLine());
			Console.WriteLine("Bank setup is completed. Please provide admin details");
			ad = new Admin() { Type = "Admin" };
			Console.WriteLine("Enter Admin username: ");
			var name= ad.UserName = getStringInput(Console.ReadLine());
			Console.WriteLine("Enter Admin password: ");
			var password=ad.Password = Console.ReadLine();

			this.Bank.Admins.Add(ad);
			

			string getStringInput(string str)
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
			branch.BankLocation = getStringInput(Console.ReadLine());
			branch.Id = getStringInput(Console.ReadLine());

			Console.WriteLine("Bankname: {0}, Username: {1}, Password {2}",this.Bank.Name,name,password);
			mainMenu();
		}

		void userLogin()
		{
			Console.WriteLine("1. Bank Staff \n2. Account Holder");
			string typeChoice = Console.ReadLine();
			int enterChoice = Convert.ToInt32(typeChoice);

			switch (enterChoice)
			{
				case 1:
					staffUser();
					break;
				case 2:
					accountUser();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					userLogin();
					break;


			}

			void accountUser()
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

				

				mainMenu();
			}

			void staffUser()
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

				if (ad.UserName == user && ad.Password == pass)
				{
					StaffAccount newStaff = new StaffAccount(bankk, user, pass);
				}
				else
				{
					Console.WriteLine("Wrong Username or Password for Admin");
				}

				mainMenu();
			}


		}

		void exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}
}
