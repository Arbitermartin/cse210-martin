using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Represents a reference to a scripture.
    public class Reference
    {
        public string Text { get; }

        public Reference(string text)
        {
            Text = text;
        }
    }

    // Represents a word in the scripture.
    public class Word
    {
        public string Text { get; }
        public bool IsHidden { get; set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }
    }

    // Represents the scripture, including the reference and the text.
    public class Scripture
    {
        private List<Word> words;
        public Reference ScriptureReference { get; }

        public Scripture(string reference, string text)
        {
            ScriptureReference = new Reference(reference);
            words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        // Hides a random word in the scripture.
        public void HideRandomWord()
        {
            var unhiddenWords = words.Where(w => !w.IsHidden).ToList();
            if (unhiddenWords.Count == 0) return;

            var random = new Random();
            int index = random.Next(unhiddenWords.Count);
            unhiddenWords[index].IsHidden = true;
        }

        // Returns the current state of the scripture as a string.
        public string GetDisplayedText()
        {
            return ScriptureReference.Text + "\n" +
                   string.Join(" ", words.Select(w => w.IsHidden ? "___" : w.Text));
        }

        // Check if all words are hidden.
        public bool AllWordsHidden() => words.All(w => w.IsHidden);
    }

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
