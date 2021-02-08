using System;
namespace BankApplication
{
    public class Branch
    {
        public Branch()
        {
        }
        public string bankName { get; set; }
        public string bankLocation { get; set; }
       
        public Branch(string bankName, string bankLocation)
        {
            this.bankName = bankName;
            this.bankLocation = bankLocation;
        }
    }
}
