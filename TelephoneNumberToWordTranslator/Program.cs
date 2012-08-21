using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TelephoneNumberToWordTranslator
{
    class Program
    {
        private static IList<string> _words = new List<string>(); 

        static void Main(string[] args)
        {
            try
            {
                var filePath = "PhoneNumbers.txt";//args[0]
                using (StreamReader reader = File.OpenText(filePath))
                {
                    var letters = new[]
                                    {
                                        new [] {'0'},
                                        new [] {'1'},
                                        new [] {'a', 'b', 'c'},
                                        new [] {'d', 'e', 'f'},
                                        new [] {'g', 'h', 'i'},
                                        new [] {'j', 'k', 'l'},
                                        new [] {'m', 'n', 'o'},
                                        new [] {'p', 'q', 'r', 's'},
                                        new [] {'t', 'u', 'v'},
                                        new [] {'w', 'x', 'y', 'z'}
                                    };
                    Console.WriteLine(string.Format("Started processing file '{0}'", filePath));
                    Console.WriteLine();
                    while (!reader.EndOfStream)
                    {
                        Console.WriteLine();
                        _words.Clear();
                        string number = reader.ReadLine();
                        if (string.IsNullOrEmpty(number))
                        {
                            Console.WriteLine("Phone number is empty...");
                            continue;
                        }

                        var sel = new char[number.Length][];
                        for (int i = 0; i < number.Length; i++)
                        {
                            int digit = Int32.Parse(number.Substring(i, 1));
                            sel[i] = letters[digit];
                        }

                        PrintAllPossiblePhoneNumberWords(sel, 0, "");

                        Console.WriteLine(string.Format("Phone# {0}",number));
                        Console.WriteLine();
                        var outputString = _words.OrderBy(st => st).Aggregate((a, b) => string.Format("{0},{1}", a, b));
                        Console.WriteLine(outputString);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("An Exception Occured: Message={0} ; StackTrace{1}",exception.Message,exception.StackTrace));
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }

        public static void PrintAllPossiblePhoneNumberWords(char[][] symbols, int n,  String phoneNumberName) 
        {
            if (n == symbols.Length) 
            {
                _words.Add(phoneNumberName);
                return;
            }
            for (int i = 0; i < symbols[n].Length; i++)
            {
                PrintAllPossiblePhoneNumberWords(symbols, n+1, phoneNumberName + symbols[n][i]);
            }
        }
    }
}
