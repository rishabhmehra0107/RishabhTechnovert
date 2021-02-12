using System;
using System.Linq;
using Bank_Application.Utilities;
namespace Bank_Application.Services
{
	public class StaffService
	{
		Bank Bank;
		private TransactionService Transaction { get; set; }
		private AccountService AccountService { get; set; }
		private Utility Utility { get; set; }
		public StaffService(Bank bank, TransactionService transactionService, AccountService accountService)
		{
			this.Bank = bank;
			this.AccountService = accountService;
			this.Transaction = transactionService;
			this.Utility = new Utility();
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
			//nextMenu();
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
			
		}
	}
}
