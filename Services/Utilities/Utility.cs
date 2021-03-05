using System;
using System.Text.RegularExpressions;

namespace Bank.Services.Utilities
{
    public class Utility
    {
        public static string GetStringInput(string regex, string helpText)
        {
            System.Console.WriteLine(helpText);
            var input = System.Console.ReadLine();
            if (Regex.IsMatch(input, regex))
            {
                return input;
            }
            System.Console.WriteLine("Invalid input");

            return GetStringInput(regex, helpText);
        }

        public static double GetDoubleInput(string helpText)
        {

            System.Console.WriteLine(helpText);
            double inputAmt = Convert.ToDouble(System.Console.ReadLine());
            if (inputAmt > 0 && inputAmt <= 10000)
            {
                return inputAmt;
            }
            System.Console.Write("Invalid input");

            return GetDoubleInput(helpText);
        }

        public static int GetIntInput(string helpText)
        {

            System.Console.WriteLine(helpText);
            int integerInput = Convert.ToInt32(System.Console.ReadLine());
            if (integerInput >= 0)
            {
                return integerInput;
            }
            System.Console.Write("Invalid input");

            return GetIntInput(helpText);
        }
    }
}