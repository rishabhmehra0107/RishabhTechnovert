using System;
namespace BankApplication
{
    public class BankSetup
    {
        public BankSetup()
        {
        }
		public string bankname { get; set; }
		public string username { get; set; }
		public string password { get; set; }

		public BankSetup(string bankname, string username, string password)
		{
			this.bankname = bankname;
			this.username = username;
			this.password = password;
			Console.WriteLine("Bankname: {0}, Username: {1}, Password {2}", bankname, username, password);

		}
	}
}
