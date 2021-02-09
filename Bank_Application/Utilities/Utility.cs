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
            return this.getStringInput(regex, helpText);
        }
        public double getIntegerInput(string helpText)
        {
            double input;
            Console.WriteLine(helpText);
            var amount = Console.ReadLine();
            bool parseSuccess = double.TryParse(amount, out input);

            if (parseSuccess==true)
            {
                return input;
            }
            return this.getIntegerInput(helpText);
        }
    }
}
