using System;
using System.Text.RegularExpressions;
using Bank_Application.Utilities;
namespace Bank_Application.Services
{
    public class StaffService
    {
		Bank Bank;
		private Utility Utility { get; set; }
		public StaffService()
		{
			this.Bank = new Bank();
			this.Utility = new Utility();
		}

		public string UserName { get; set; }
		public string Password { get; set; }
		public string AccountId { get; set; }

		public void nextMenu()
		{
			
			Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User details\n4. Update Service Charges\n5. Add new Currency\n6. Logout");
			int nextChoice = Convert.ToInt32(Console.ReadLine());

			switch (nextChoice)
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
					newCurrency();
					break;
				case 6:
					logout();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					nextMenu();
					break;

			}
		}

		public void addStaff()
		{

			string staffUser = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter username: ");
			string staffPass = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Password: ");

			this.AccountId = staffUser.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");

			Staff staff= new Staff() { UserName = staffUser, Password = staffPass, Type = "Employee", Id = AccountId };

			this.Bank.Staffs.Add(staff);
			Console.WriteLine("Username {0} staff account created", staffUser);
			nextMenu();

		}
		public void addAccountHolder()
		{
			string holderUser = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter username: "); ;
			string holderPass = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter password: ");
		
			this.AccountId = holderUser.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
			AccountHolder accountHolder = new AccountHolder() { Id = AccountId, Password = holderPass, Type = "AccountHolder", UserName = holderUser };
			this.Bank.Users.Add(accountHolder);
			Console.WriteLine("Username {0} Account Holder account created", holderUser);
			nextMenu();

		}
		public void bankUsers()
		{
			Console.WriteLine("Bank Staff Users");
			foreach (Staff staff in this.Bank.Staffs)
			{
				Console.WriteLine(staff.UserName);
			}
			Console.WriteLine("Bank Account Holders");
			foreach (AccountHolder accountHolder in this.Bank.Users)
			{
				Console.WriteLine(accountHolder.UserName);
			}
			nextMenu();
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
					string location = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Bank Location: ");
					Branch branch = new Branch();
					branch.BankId = bname.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
					branch.Location = this.Utility.getStringInput("^[a-zA-Z]+$", "Enter Branch Location: ");
					branch.Id = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Branch ID: ");
			
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

			nextMenu();
		}

		public void newCurrency()
		{
			string currencyCode = " ";
			string currencyName = " ";
			int conversionToInr;

			currencyCode = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
			currencyName = this.Utility.getStringInput("^[a-zA-Z0-9]+$", "Enter Currency Name:");
			Console.WriteLine("Enter Currency Value Cnoverted To INR:");
			try
			{
				conversionToInr = Convert.ToInt32(Console.ReadLine());
				if (conversionToInr >= 0 && conversionToInr <= 250)
				{
					Console.WriteLine("New Currency updated Successfully");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Invalid Conversion value" + e.Message);
			}
			nextMenu();
		}

		public void logout()
		{
			Console.WriteLine("Goodbye: " + UserName);
		}
	}
}
