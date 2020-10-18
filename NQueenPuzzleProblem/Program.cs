using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace NQueenPuzzleProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 0;
            bool print = true;

            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter the value of N to N-Queens problem:");
                    N = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nWould you like to print all solutions?[y/n]");
                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "y":
                            print = true;
                            break;
                        case "n":
                            print = false;
                            break;
                        default:
                            Console.WriteLine("\nIncorrect input!\nPlease enter y or n as the answer whether to print all solutions.\n\nPlease press any key to restart...");
                            Console.ReadLine();
                            continue;
                    }

                    Console.WriteLine("\nResults:");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n\nPlease press any key to restart...");
                    Console.ReadLine();
                    continue;
                }
            }

            SolutionChecker slnChker = new SolutionChecker(N, print);

            DateTime dt_start = DateTime.Now;
            Console.WriteLine($"There are {slnChker.GetNQueensSolutions()} solutions to the {N}-Queens problem.");
            DateTime dt_end = DateTime.Now;
            Console.WriteLine($"It takes {dt_end.Subtract(dt_start).TotalSeconds} seconds in total.");

            Console.WriteLine("\n\nPlease press any key to leave...");
            Console.ReadLine();
        }

    }
}
