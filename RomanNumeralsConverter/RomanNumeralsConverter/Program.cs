using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsConverter
{
    class Program
    {
        private static readonly Dictionary<char, int> RomanNumberDict = new Dictionary<char, int>()
        {
            { 'I', 1},
            { 'V', 5},
            { 'X', 10},
            { 'L', 50},
            { 'C', 100},
            { 'D', 500},
            { 'M', 1000}
        };
        private static readonly Dictionary<int, string> NumberRomanDict = new Dictionary<int, string>(){
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" },
        };

        static void Main(string[] args)
        {
            string exitOrContinue = "y";
            int selection;
            string result = string.Empty;

            while (exitOrContinue.ToLower().Trim() == "y")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t**********************************\n\n" +
                                  "\t\t\t*   Roman Numerals Converter   *\n\n" +
                                  "\t\t\t***********************************\n\n");
                Console.WriteLine("Please select a conversion type;\n");
                Console.WriteLine("1. Roman numerals to decimal (Press 1)");
                Console.WriteLine("2. Decimal to roman numerals (Press 2)\n");

                selection = ReadToInt32();
                result = ConvertInput(selection);
                Console.Write("Result = {0}\t", result);

                Console.WriteLine("Continue(Y/N)");
                exitOrContinue = Console.ReadLine();
            }
        }

        private static string ConvertInput(int selection)
        {
            string result = string.Empty;
            string input = string.Empty;

            switch (selection)
            {
                case 1:
                    Console.WriteLine("Conversion type: Roman numerals -> Decimal numerals");
                    input = ReadInput(selection);
                    result = RomanToInteger(input);
                    break;
                case 2:
                    Console.WriteLine("Conversion type: Decimal numerals -> Roman numerals");
                    input = ReadInput(selection);
                    result = IntegerToRoman(int.Parse(input));
                    break;
            }
            return result;
        }
        private static bool FormatChecking(string input, int selection)
        {
            char[] inputArr = input.ToCharArray();
            int forSelection2;
            char temp = ' ';
            int tempCounter = 0;
            if (selection == 1 && input != string.Empty)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (temp == inputArr[i])
                    {
                        if (++tempCounter == 3)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        temp = inputArr[i];
                        tempCounter = 0;
                    }
                    if (!RomanNumberDict.ContainsKey(inputArr[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return int.TryParse(input, out forSelection2) && forSelection2 < 3889;
            }
        }

        private static string IntegerToRoman(int number)
        {
            var result = new StringBuilder();

            foreach (var item in NumberRomanDict)
            {
                while (number >= item.Key)
                {
                    result.Append(item.Value);
                    number -= item.Key;
                }
            }

            return result.ToString();
        }

        public static string RomanToInteger(string input)
        {
            int number = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (i + 1 < input.Length && RomanNumberDict[input[i]] < RomanNumberDict[input[i + 1]])
                {
                    number -= RomanNumberDict[input[i]];
                }
                else
                {
                    number += RomanNumberDict[input[i]];
                }
            }
            return number.ToString();
        }

        private static string ReadInput(int selection)
        {
            string input = string.Empty;
            bool formatChecking = true;

            do
            {
                input = Console.ReadLine();
                formatChecking = FormatChecking(input, selection);
                if (!formatChecking)
                    Console.WriteLine("Not in the valid format or range. Try again.");
            } while (!formatChecking);

            return input;
        }

        private static int ReadToInt32()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            } while (key.Key != ConsoleKey.D1 && key.Key != ConsoleKey.NumPad1 &&
                     key.Key != ConsoleKey.D2 && key.Key != ConsoleKey.NumPad2);
            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}