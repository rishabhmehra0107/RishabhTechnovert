﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Bank_Application
{
    public class StaffAccount
    {
		
		public List<Branch> GetBranches = new List<Branch>();
		public List<User> getUser = new List<User>();
	
		public string bankk { get; set; }
		public string user { get; set; }
		public string pass { get; set; }
		public string accountId { get; set; }

		Bank Bank = new Bank();
		Branch Branch = new Branch();
		Staff Staff;
		User User;
		public StaffAccount(string bankk, string user, string pass)
		{
			this.bankk = bankk;
			this.user = user;
			this.pass = pass;
			Branch.BankId= this.bankk.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			Branch.BankLocation= GetStringInput(Console.ReadLine());
			Branch.Id= GetStringInput(Console.ReadLine());

			this.Bank.Branches.Add(Branch);

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

			

			Console.WriteLine("Admin{0} present in the system ", user);
			NextMenu();
		}

		public void NextMenu()
		{

			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User details\n4. Update Service Charges\n5. Add new Currency\n6. Logout");
			int NextChoice = Convert.ToInt32(Console.ReadLine());

			switch (NextChoice)
			{
				case 1:
					AddStaff();
					break;
				case 2:
					AddAccountHolder();
					break;
				case 3:
					BankUsers();
					break;
				case 4:
					updateCharges();
					break;
				case 5:
					newCurrency();
					break;
				case 6:
					Logout();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					NextMenu();
					break;

			}
		}

		public void AddStaff()
		{

			Console.WriteLine("Enter username: ");
			string Staffuser = Console.ReadLine();
			string Staffpass = " ";
			if (Regex.IsMatch(Staffuser, "^[a-zA-Z]*$"))
			{
				Console.WriteLine("Enter password: ");
				Staffpass = Convert.ToString(Console.ReadLine());
			}
			else
			{
				Console.WriteLine("Username must contains alphabets only");
			}
	
			this.accountId = user.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");

			Staff = new Staff() { UserName = Staffuser, Password = Staffpass, Type = "Bank Staff", Id = accountId };

			this.Bank.Staffs.Add(Staff);
			Console.WriteLine("Username {0} staff account created", Staffuser);
			NextMenu();

		}
		public void AddAccountHolder()
		{
			Console.WriteLine("Enter username: ");
			string holderUser = Console.ReadLine();
			string holderPass = " ";
			if (Regex.IsMatch(holderUser, "^[a-zA-Z]*$"))
			{
				Console.WriteLine("Enter password: ");
				holderPass = Convert.ToString(Console.ReadLine());
			}
			else
			{
				Console.WriteLine("Username must contains alphabets only");
			}

			this.accountId = holderUser.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			User = new User() { Id = accountId, Password = holderPass, Type = "Account Holder", UserName = holderUser };
			this.Bank.Users.Add(User);
			Console.WriteLine("Username {0} Account Holder account created", holderUser);
			NextMenu();

		}
		public void BankUsers()
		{
			Console.WriteLine("Bank Staff Users");
			foreach (Staff staff in this.Bank.Staffs)
			{
				Console.WriteLine(staff.UserName);
			}
			Console.WriteLine("Bank Account Holders");
			foreach (User user in this.Bank.Users)
			{
				Console.WriteLine(user.UserName);
			}
			NextMenu();
		}
		public void updateCharges()
		{
			Console.WriteLine("Select account from the list");
			foreach (User user in this.Bank.Users)
			{
				Console.WriteLine(user.UserName+" "+user.Id);
			}

			Console.WriteLine("Enter username: ");
			string strname = Convert.ToString(Console.ReadLine());
			foreach (User user in this.Bank.Users)
			{
				if (user.UserName == strname)
				{
					Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", strname);
					Console.WriteLine("Enter bankname of user: ");
					string bname = Console.ReadLine();
					Console.WriteLine("Enter Bank Location: ");
					string location = Console.ReadLine();
					this.Branch.BankId = bname.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
					this.Branch.BankLocation = GetStringInput(Console.ReadLine());
					this.Branch.Id = GetStringInput(Console.ReadLine());
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
					this.Bank.Branches.Add(Branch);
					int flag = 0;
					foreach (Branch branch1 in this.Bank.Branches)
					{
						if (branch1.BankId== bname.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy"))
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

			NextMenu();
		}

		public void newCurrency()
		{
			string currencyCode = " ";
			string currencyName = " ";
			int conversionToInr;

			Console.WriteLine("Enter Currency Code:");
			currencyCode = Console.ReadLine();
			if (Regex.IsMatch(currencyCode, "^[a-zA-Z]*$"))
			{
				Console.WriteLine("Enter Currency Name:");
				currencyName = Console.ReadLine();
				if (Regex.IsMatch(currencyName, "^[a-zA-Z]*$"))
				{
					Console.WriteLine("Enter Currency Value Cnoverted To INR:");
					
					try
					{
						conversionToInr = Convert.ToInt32(Console.ReadLine());
						if (conversionToInr >= 0 && conversionToInr <= 250)
						{
							Console.WriteLine("New Currency updated Successfully");
						}
					}
					catch(Exception e)
					{
						Console.WriteLine("Invalid Conversion value"+e.Message);
					}
				}
				else
				{
					Console.WriteLine("Invalid currency name");
				}
			}
			else
			{
				Console.WriteLine("Invalid currency code");
			}
			NextMenu();
		}

		public void Logout()
		{
			Console.WriteLine("Goodbye: " + user);
		}
	}
}
