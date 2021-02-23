using System;
using BankApp.Services;

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