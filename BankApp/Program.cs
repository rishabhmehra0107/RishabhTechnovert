using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Bank.Model;
using Bank.Console.Data;
using System.Linq;
using SimpleInjector;
using Bank.Contracts;
using Bank.Services;

namespace BankApp
{
    class Program
    {
        static void Main()
        {
            new BankApplication();
        }
    }
}