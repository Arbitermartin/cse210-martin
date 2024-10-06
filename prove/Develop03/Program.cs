using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{   
    // Main program class.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Scripture Memorizer!");
            var scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayedText());
                Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");

                var input = Console.ReadLine();
                if (input?.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWord();
                if (scripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine("All words are now hidden! Exiting...");
                    break;
                }
            }
        }
    }
}
