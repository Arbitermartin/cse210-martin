using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Make sure to install the Newtonsoft.Json NuGet package

namespace JournalApp
{
    // JournalEntry class with added fields for tags and timestamp
    public class JournalEntry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public List<string> Tags { get; set; }

        public JournalEntry(string prompt, string response, List<string> tags)
        {
            Prompt = prompt;
            Response = response;
            Date = DateTime.Now.ToShortDateString();
            Time = DateTime.Now.ToShortTimeString();
            Tags = tags;
        }

        public override string ToString()
        {
            return $"{Date} {Time} | {Prompt} | {Response} | Tags: {string.Join(", ", Tags)}";
        }

        // Method for proper CSV formatting
        public string ToCsv()
        {
            return $"\"{Date}\",\"{Time}\",\"{Prompt}\",\"{Response}\",\"{string.Join(",", Tags)}\"";
        }
    }

    // Journal class to manage a collection of entries
    public class Journal
    {
        private List<JournalEntry> entries;
        private List<string> prompts;

        public Journal()
        {
            entries = new List<JournalEntry>();
            prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
        }

        public void AddEntry(string response, List<string> tags)
        {
            var random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];
            var entry = new JournalEntry(prompt, response, tags);
            entries.Add(entry);
            Console.WriteLine("Entry added successfully!");
        }

        public void DisplayEntries()
        {
            Console.WriteLine("\nJournal Entries:");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToCsv(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine(entry.ToCsv());
                }
            }
            Console.WriteLine("Journal saved as CSV successfully!");
        }

        public void LoadFromCsv(string filename)
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split(new[] { ',' }, 5); // Split into 5 parts
                if (parts.Length == 5)
                {
                    var entry = new JournalEntry(parts[2].Trim('"'), parts[3].Trim('"'), new List<string>(parts[4].Trim('"').Split(',')));
                    entry.Date = parts[0].Trim('"');
                    entry.Time = parts[1].Trim('"');
                    entries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded from CSV successfully!");
        }

        public void SaveToJson(string filename)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(filename, json);
            Console.WriteLine("Journal saved as JSON successfully!");
        }

        public void LoadFromJson(string filename)
        {
            entries.Clear();
            string json = File.ReadAllText(filename);
            entries = JsonConvert.DeserializeObject<List<JournalEntry>>(json);
            Console.WriteLine("Journal loaded from JSON successfully!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal entries");
                Console.WriteLine("3. Save journal to CSV");
                Console.WriteLine("4. Load journal from CSV");
                Console.WriteLine("5. Save journal to JSON");
                Console.WriteLine("6. Load journal from JSON");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter your response: ");
                        string response = Console.ReadLine();
                        Console.Write("Enter tags (comma-separated): ");
                        List<string> tags = new List<string>(Console.ReadLine().Split(','));
                        journal.AddEntry(response, tags);
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter filename to save as CSV: ");
                        string csvFileName = Console.ReadLine();
                        journal.SaveToCsv(csvFileName);
                        break;
                    case "4":
                        Console.Write("Enter filename to load from CSV: ");
                        string loadCsvFileName = Console.ReadLine();
                        journal.LoadFromCsv(loadCsvFileName);
                        break;
                    case "5":
                        Console.Write("Enter filename to save as JSON: ");
                        string jsonFileName = Console.ReadLine();
                        journal.SaveToJson(jsonFileName);
                        break;
                    case "6":
                        Console.Write("Enter filename to load from JSON: ");
                        string loadJsonFileName = Console.ReadLine();
                        journal.LoadFromJson(loadJsonFileName);
                        break;
                    case "7":
                        running = false;
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}
