using System;
using Bank.Services;

namespace BankApp
{
    class Program
    {
        static void Main()
        {
            new BankApplication(new BankService());
        }
    }
}