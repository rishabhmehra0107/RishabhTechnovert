using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankApp.Utilities
{
    public class Utility
    {
        public string GetStringInput(string regex, string helpText)
        {
            Console.WriteLine(helpText);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(regex) && Regex.IsMatch(input, regex))
            {
                return input;
            }
            Console.WriteLine("Invalid input");

            return this.GetStringInput(regex, helpText);
        }
        public double GetDoubleInput(string helpText)
        {

            Console.WriteLine(helpText);
            double inputAmt = Convert.ToDouble(Console.ReadLine());
            if (inputAmt > 0 && inputAmt <= 10000)
            {
                return inputAmt;
            }
            Console.Write("Invalid input");

            return this.GetDoubleInput(helpText);
        }
    }
}