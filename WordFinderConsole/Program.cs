using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Welcome message
            Console.WriteLine("Welcome to WordFinder");
            Console.WriteLine();


            //Matrix Creation
            Console.WriteLine("Matrix Example:");
            string[] matrix = new string[5];
            matrix[0] = "abcgc";
            matrix[1] = "fgwio";
            matrix[2] = "chill";
            matrix[3] = "pqnsd";
            matrix[4] = "uvdxy";

            Console.WriteLine("_______________________________________");
            Console.WriteLine(matrix[0]);
            Console.WriteLine(matrix[1]);
            Console.WriteLine(matrix[2]);
            Console.WriteLine(matrix[3]);
            Console.WriteLine(matrix[4]);
            Console.WriteLine("_______________________________________");

            Console.WriteLine();

            string[] findWords = new string[4]
            {
                "chill",
                "cold",
                "wind",
                "snow",
            };

            Console.WriteLine("Words to Find in Matrix:");
            Console.WriteLine($"* {findWords[0]}");
            Console.WriteLine($"* {findWords[1]}");
            Console.WriteLine($"* {findWords[2]}");
            Console.WriteLine($"* {findWords[3]}");

            Console.WriteLine();
            Console.WriteLine("Press any button to find the words in the Matrix: ");
            Console.ReadLine();

            Console.WriteLine("Words finded: ");
            try
            {
                WordFinder wordFinder = new WordFinder(matrix);

                List<string> result = wordFinder.Find(findWords).ToList();

                foreach(string word in result)
                {
                    Console.WriteLine($"* {word}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error has ocurred: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Program ended.");
            Console.ReadLine();
        }
    }
}
