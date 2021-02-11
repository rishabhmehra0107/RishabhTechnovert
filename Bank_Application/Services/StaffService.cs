using System;
using Bank_Application.Utilities;
namespace Bank_Application.Services
{
    public class StaffService
    {
		private Utility Utility { get; set; }
		public StaffService()
		{
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

	}
}
