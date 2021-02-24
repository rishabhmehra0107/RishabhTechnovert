using System;
using System.Text.RegularExpressions;

namespace Bank.Services.Utilities
{
    public class Utility
    {
        public static string GetStringInput(string regex, string helpText)
        {
            Console.WriteLine(helpText);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(regex) && Regex.IsMatch(input, regex))
            {
                return input;
            }
            Console.WriteLine("Invalid input");

            return GetStringInput(regex, helpText);
        }

        public static double GetDoubleInput(string helpText)
        {

            Console.WriteLine(helpText);
            double inputAmt = Convert.ToDouble(Console.ReadLine());
            if (inputAmt > 0 && inputAmt <= 10000)
            {
                return inputAmt;
            }
            Console.Write("Invalid input");

            return GetDoubleInput(helpText);
        }

        public static int GetIntInput(string helpText)
        {

            Console.WriteLine(helpText);
            int integerInput = Convert.ToInt32(Console.ReadLine());
            if (integerInput >= 0)
            {
                return integerInput;
            }
            Console.Write("Invalid input");

            return GetIntInput(helpText);
        }
    }
}