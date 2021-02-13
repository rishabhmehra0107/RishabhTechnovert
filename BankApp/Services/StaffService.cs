using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using BankApp.Utilities;
namespace BankApp.Services
{
	public class StaffService
	{
		Bank Bank;
		private TransactionService Transaction { get; set; }
		private AccountService AccountService { get; set; }
		private Utility Utility { get; set; }

		public StaffService(Bank bank, TransactionService transactionService, Utility utility)
		{
			this.Bank = bank;
			this.Transaction = transactionService;
			this.Utility = utility;
		}

		public void NewCurrency()
		{
			Currency currency = new Currency();
			currency.Code = this.Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Currency Code:");
			currency.Name = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Currency Name:");
			Console.WriteLine("Enter Currency Value Cnoverted To INR:");
			try
			{
				currency.InrValue = Convert.ToInt32(Console.ReadLine());
				if (currency.InrValue >= 0 && currency.InrValue <= 250)
				{
					Console.WriteLine("New Currency updated Successfully");
					this.Bank.Currency.Add(currency);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Invalid Conversion value" + e.Message);
			}
		}

		public void BankUsers()
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

		public void XmlData()
        {
			XDocument xDocument = XDocument.Load("/Users/apple/Projects/BankApp/BankApp/Data.xml");
			XElement xElement = new XElement("Branches");
			XElement xElement1 = new XElement("Admins");
			XElement xElement2 = new XElement("Staffs");
			XElement xElement3 = new XElement("AccountHolders");
			XElement xElement4 = new XElement("Currencies");
			xDocument = new XDocument(new XElement("Bank",xElement,xElement1,xElement2,xElement3,xElement4));
			int i = 1;
			foreach (Branch branch in this.Bank.Branches)
			{
				xElement.Add(new XElement("BankBranch"+i, new XElement("BankName", this.Bank.Name), new XElement("Location", this.Bank.Location), new XElement("ID", this.Bank.Id), new XElement("BranchLocation", branch.Location), new XElement("BranchID", branch.Id)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach(Admin admin in this.Bank.Admins)
            {
				xElement1.Add(new XElement("Admin"+i, new XElement("Name", admin.Name), new XElement("Type", admin.Type), new XElement("ID", admin.Id), new XElement("Username", admin.UserName), new XElement("Password", admin.Password)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				++i;
			}

			i = 1;
			foreach (Staff staff in this.Bank.Staffs)
            {
				xElement2.Add(new XElement("Employee"+i, new XElement("Name", staff.Name), new XElement("Type", staff.Type), new XElement("ID", staff.Id), new XElement("Username", staff.UserName), new XElement("Password", staff.Password)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach (AccountHolder account in this.Bank.AccountHolders)
			{
				xElement3.Add(new XElement("AccountHolders"+i, new XElement("Name", account.Name), new XElement("Type", account.Type), new XElement("AccountNumber", account.AccountNumber), new XElement("Username", account.UserName), new XElement("Password", account.Password), new XElement("ID", account.Id)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

			i = 1;
			foreach (Currency currency in this.Bank.Currency)
			{
				xElement4.Add(new XElement("Currencies" + i, new XElement("Name", currency.Name), new XElement("Code", currency.Code), new XElement("INRValue", currency.InrValue)));
				xDocument.Save("/Users/apple/Projects/BankApp/BankApp/Data.xml");
				i++;
			}

		}

		public void UpdateCharges()
		{
			Console.WriteLine("Select account from the list");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				Console.WriteLine(user.UserName + " " + user.Id);
			}

			string strname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter username: ");
			foreach (AccountHolder user in this.Bank.AccountHolders)
			{
				if (user.UserName == strname)
				{
					Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", strname);

					string bname = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter bankname of user: ");
					Branch branch = new Branch();
					branch.BankId = bname.Substring(0, 3) + DateTime.UtcNow.ToString("MMddyyyy");
					branch.Location = this.Utility.GetStringInput("^[a-zA-Z]+$", "Enter Branch Location: ");
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

		

		public void Logout()
		{
			Console.WriteLine("Goodbye");
			
		}
	}
}
