using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bank_Application.Utilities
{
    public class Utility
    {
        public string getStringInput(string regex, string helpText)
        {
            Console.WriteLine(helpText);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(regex) && Regex.IsMatch(input, regex))
            {
                return input;
            }
            Console.WriteLine("Invalid input");
            return this.getStringInput(regex, helpText);
        }
        public double getIntegerInput(string helpText)
        {
            
            Console.WriteLine(helpText);
            double inputAmt = Convert.ToDouble(Console.ReadLine());
            if (inputAmt>0 && inputAmt<=10000)
            {
                return inputAmt;
            }
            Console.Write("Invalid input");
            return this.getIntegerInput(helpText);
        }
    }
}
