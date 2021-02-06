using System;
using System.Collections.Generic;

public class Bank
{

	public List<string> BankList = new List<string>();
	public List<string> AdminUserList = new List<string>();
	public List<string> StaffUserList = new List<string>();
	public List<string> AccountHolderList = new List<string>();
	public List<double> DepositList = new List<double>();
	public List<double> WithdrawList = new List<double>();
	public List<string> UserList = new List<string>();
	public List<string> AccountID = new List<string>();
	public List<string> TransactionID = new List<string>();

	public static void Main()
	{
		Bank_operations();

		void Bank_operations()
		{
			Console.WriteLine("1. Setup New Bank \n2. User login as account holder/bank staff \n3. Save and Exit");
			string enterchoice = Console.ReadLine();
			int SubmitChoice = Convert.ToInt32(enterchoice);

			switch (SubmitChoice)
			{
				case 1:
					Setup_new_bank();
					break;
				case 2:
					User_login();
					break;
				case 3:
					Save_Exit();
					break;
				default:
					Console.WriteLine("Please select option from the list");
					Bank_operations();
					break;


			}
		}

		void Setup_new_bank()
		{
			string bankname = " ";
			string username = " ";
			string password = " ";

			Console.WriteLine("Enter bankname: ");
			bankname = Console.ReadLine();
			Console.WriteLine("Enter username: ");
			username = Console.ReadLine();
			Console.WriteLine("Enter password: ");
			password = Console.ReadLine();

			BankSetup newBank = new BankSetup(bankname, username, password);

			Bank_operations();
		}

		void User_login()
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
					User_login();
					break;


			}

			void AccountUser()
			{
				string user = " ";
				string pass = " ";

				Console.WriteLine("Enter username: ");
				user = Console.ReadLine();
				Console.WriteLine("Enter password: ");
				pass = Console.ReadLine();

				AccountHolder newAccount = new AccountHolder(user, pass, 1000);

				Bank_operations();
			}

			void StaffUser()
			{
				string bankk = " ";
				string user = " ";
				string pass = " ";

				Console.WriteLine("Enter bankname: ");
				bankk = Console.ReadLine();
				Console.WriteLine("Enter username: ");
				user = Console.ReadLine();
				Console.WriteLine("Enter password: ");
				pass = Console.ReadLine();

				StaffAccount newStaff = new StaffAccount(bankk, user, pass);

				Bank_operations();
			}


		}

		void Save_Exit()
		{
			Console.WriteLine("Date saved!!! You can exit!");
		}
	}

}

public class BankSetup : Bank
{

	public string bankname { get; set; }
	public string username { get; set; }
	public string password { get; set; }

	public BankSetup(string bankname, string username, string password)
	{
		this.bankname = bankname;
		this.username = username;
		this.password = password;

		BankList.Add(bankname);
		AdminUserList.Add(username);

		Console.WriteLine("Bankname: {0}, Username: {1}, Password {2}", bankname, username, password);

	}

}

public class StaffAccount : Bank
{
	public string bankk { get; set; }
	public string user { get; set; }
	public string pass { get; set; }



	public StaffAccount(string bankk, string user, string pass)
	{
		this.bankk = bankk;
		this.user = user;
		this.pass = pass;
		BankList.Add(bankk);
		AdminUserList.Add(user);

		Console.WriteLine("Username: {0} ", user);
		NextMenu();
	}

	public void NextMenu()
	{

		Console.WriteLine("1. Add Staff\n2. Add Account Holder\n3.Display Bank User details\n4. Update Service Charges\n5. Add new Currency\n6. Logout");
		string StringMenu = Console.ReadLine();
		int NextChoice = Convert.ToInt32(StringMenu);

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
				new_currency();
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
		string Staffuser = Convert.ToString(Console.ReadLine());
		Console.WriteLine("Enter password: ");
		string Staffpass = Convert.ToString(Console.ReadLine());
		Console.WriteLine("Username {0} staff account created", Staffuser);
		StaffUserList.Add(Staffuser);
		NextMenu();

	}
	public void AddAccountHolder()
	{

		Console.WriteLine("Enter username: ");
		string Holderuser = Convert.ToString(Console.ReadLine());
		Console.WriteLine("Enter password: ");
		string Holderpass = Convert.ToString(Console.ReadLine());
		Console.WriteLine("Username {0} Account Holder account created", Holderuser);
		AccountHolderList.Add(Holderuser);
		NextMenu();

	}
	public void BankUsers()
	{
		Console.WriteLine("Bank Staff Users");
		foreach (String u in StaffUserList)
		{
			Console.WriteLine(u);
		}
		Console.WriteLine("Bank Account Holders");
		foreach (String a in AccountHolderList)
		{
			Console.WriteLine(a);
		}
		NextMenu();
	}
	public void updateCharges()
	{
		Console.WriteLine("Select account from the list");
		foreach (String ele in AccountHolderList)
		{
			Console.WriteLine(ele);
		}

		Console.WriteLine("Enter username: ");
		string strname = Convert.ToString(Console.ReadLine());

		if (AccountHolderList.Contains(strname))
		{
			Console.WriteLine("Username: {0} \nDefault RTGS for same bank: 0%, Default RTGS for different bank: 2%, Default IMPS for same bank: 5%, Default IMPS for different bank: 6%, ", strname);
			Console.WriteLine("Enter bankname of user: ");
			string bname = Console.ReadLine();

			if (BankList.Contains(bname))
			{
				Console.WriteLine("Since same bank, the new charges are:-");
				Console.WriteLine("RTGS: ");
				int srtgs = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("IMPS: ");
				int simps = Convert.ToInt32(Console.ReadLine());
			}

			else
			{
				Console.WriteLine("Since different bank, the new charges are:-");
				Console.WriteLine("RTGS: ");
				int drtgs = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("IMPS: ");
				int dimps = Convert.ToInt32(Console.ReadLine());
			}
		}

		else
		{
			Console.WriteLine("Select valid account holder from the list");
			updateCharges();
		}

		NextMenu();
	}

	public void new_currency()
	{
		Console.WriteLine("Enter Currency Code:");
		string cc = Console.ReadLine();
		Console.WriteLine("Enter Currency Name:");
		string cn = Console.ReadLine();
		Console.WriteLine("Enter Currency Value Cnoverted To INR:");
		int cti = Convert.ToInt32(Console.ReadLine());
		NextMenu();
	}

	public void Logout()
	{
		Console.WriteLine("Goodbye: " + user);
	}
}

public class AccountHolder : Bank
{

	public string user { get; set; }
	public string pass { get; set; }
	public double Balance { get; set; }
	public string accountid { get; set; }

	public AccountHolder(string user, string pass, double Balance)
	{
		this.user = user;
		this.pass = pass;
		this.Balance = Balance;
		this.accountid = user.Substring(0, 3) + DateTime.UtcNow.ToString("MM-dd-yyyy");
		AccountID.Add(accountid);
		UserList.Add(user);

		Console.WriteLine("Username: {0} ,AccountID:{1}, Balance: {2}", user, accountid, Balance);
		NextMenu();
	}

	public void NextMenu()
	{
		double balance = Balance;
		Console.WriteLine("1.Withdrawl \n2.Deposit\n3.Deposit History\n4.Withdraw History\n5.Logout");
		string MenuList = Console.ReadLine();
		int NextChoice = Convert.ToInt32(MenuList);

		switch (NextChoice)
		{
			case 1:
				Withdraw();
				break;
			case 2:
				Deposit();
				break;

			case 3:
				DepositHistory();
				break;
			case 4:
				WithdrawHistory();
				break;
			case 5:
				Logout();
				break;
		}
	}

	public double Withdraw()
	{
		Console.WriteLine("Available Balance: {0}", Balance);
		Console.WriteLine("Withdraw Amount: ");
		double WithdrawAmt = Convert.ToDouble(Console.ReadLine());
		WithdrawList.Add(WithdrawAmt);
		Balance = Balance -= WithdrawAmt;
		Console.WriteLine(Balance);
		NextMenu();
		return WithdrawAmt;
	}

	public double Deposit()
	{
		Console.WriteLine("Available Balance: {0}", Balance);
		Console.WriteLine("Deposit Amount: ");
		double DepositAmt = Convert.ToDouble(Console.ReadLine());


		DepositList.Add(DepositAmt);

		Balance = Balance + DepositAmt;
		Console.WriteLine(Balance);
		NextMenu();
		return DepositAmt;
	}

	public void DepositHistory()
	{
		foreach (double i in DepositList)
		{
			Console.WriteLine("Deposit History: " + i);
		}
		NextMenu();
	}

	public void WithdrawHistory()
	{
		foreach (double i in WithdrawList)
		{
			Console.WriteLine("Withdraw History: " + i);
		}
		NextMenu();
	}

	public void Logout()
	{
		Console.WriteLine("Goodbye: " + user);
	}

}