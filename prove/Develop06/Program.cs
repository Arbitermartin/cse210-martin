using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for all goals
    abstract class Goal
    {
        protected string _description;
        protected int _points;
        protected bool _isComplete;

        public Goal(string description, int points)
        {
            _description = description;
            _points = points;
            _isComplete = false;
        }

        public abstract void RecordEvent();
        public abstract string GetStatus();
        public int GetPoints() => _points;
        public string GetDescription() => _description;
        public bool IsComplete() => _isComplete;
    }

    // Simple goal
    class SimpleGoal : Goal
    {
        public SimpleGoal(string description, int points) : base(description, points) { }

        public override void RecordEvent()
        {
            _isComplete = true;
        }

        public override string GetStatus()
        {
            return _isComplete ? "[X] " + _description : "[ ] " + _description;
        }
    }

    // Eternal goal
    class EternalGoal : Goal
    {
        public EternalGoal(string description, int points) : base(description, points) { }

        public override void RecordEvent()
        {
            _points += 100; // Points are gained, but goal is never complete
        }

        public override string GetStatus()
        {
            return "[E] " + _description; // Indicate it's an eternal goal
        }
    }

    // Checklist goal
    class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _requiredTimes;
        private int _bonus;

        public ChecklistGoal(string description, int points, int requiredTimes, int bonus)
            : base(description, points)
        {
            _timesCompleted = 0;
            _requiredTimes = requiredTimes;
            _bonus = bonus;
        }

        public override void RecordEvent()
        {
            if (!_isComplete)
            {
                _timesCompleted++;
                _points += GetPoints();
                if (_timesCompleted >= _requiredTimes)
                {
                    _points += _bonus;
                    _isComplete = true;
                }
            }
        }

        public override string GetStatus()
        {
            return $"{(_isComplete ? "[X]" : "[ ]")} {_description} - Completed {_timesCompleted}/{_requiredTimes}";
        }
    }

    // Main program
    class Program
    {
        private static List<Goal> _goals = new List<Goal>();
        private static int _totalScore = 0;

        static void Main(string[] args)
        {
            LoadGoals();

            while (true)
            {
                Console.WriteLine("\n--- Eternal Quest ---");
                Console.WriteLine("1. Add Simple Goal");
                Console.WriteLine("2. Add Eternal Goal");
                Console.WriteLine("3. Add Checklist Goal");
                Console.WriteLine("4. Record Event");
                Console.WriteLine("5. Show Goals");
                Console.WriteLine("6. Save Goals and Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddSimpleGoal();
                        break;
                    case "2":
                        AddEternalGoal();
                        break;
                    case "3":
                        AddChecklistGoal();
                        break;
                    case "4":
                        RecordGoalEvent();
                        break;
                    case "5":
                        ShowGoals();
                        break;
                    case "6":
                        SaveGoals();
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void AddSimpleGoal()
        {
            Console.Write("Enter goal description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter points for this goal: ");
            int points = int.Parse(Console.ReadLine());
            _goals.Add(new SimpleGoal(desc, points));
            Console.WriteLine("Simple goal added!");
        }

        private static void AddEternalGoal()
        {
            Console.Write("Enter goal description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter points for this goal: ");
            int points = int.Parse(Console.ReadLine());
            _goals.Add(new EternalGoal(desc, points));
            Console.WriteLine("Eternal goal added!");
        }

        private static void AddChecklistGoal()
        {
            Console.Write("Enter goal description: ");
            string desc = Console.ReadLine();
            Console.Write("Enter points for each completion: ");
            int points = int.Parse(Console.ReadLine());
            Console.Write("Enter number of required completions: ");
            int requiredTimes = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points for completion: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(desc, points, requiredTimes, bonus));
            Console.WriteLine("Checklist goal added!");
        }

        private static void RecordGoalEvent()
        {
            ShowGoals();
            Console.Write("Enter the index of the goal to record an event: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < _goals.Count)
            {
                _goals[index].RecordEvent();
                _totalScore += _goals[index].GetPoints();
                Console.WriteLine("Event recorded!");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        private static void ShowGoals()
        {
            Console.WriteLine("\n--- Goals List ---");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} (Points: {_goals[i].GetPoints()})");
            }
            Console.WriteLine($"Total Score: {_totalScore}");
        }

        private static void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter("goals.txt"))
            {
                writer.WriteLine(_totalScore);
                foreach (var goal in _goals)
                {
                    string status = goal.GetType().Name + "," + goal.GetDescription() + "," + goal.GetPoints() + "," + (goal.IsComplete() ? "1" : "0");
                    writer.WriteLine(status);
                }
            }
            Console.WriteLine("Goals saved!");
        }

        private static void LoadGoals()
        {
            if (File.Exists("goals.txt"))
            {
                string[] lines = File.ReadAllLines("goals.txt");
                _totalScore = int.Parse(lines[0]);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    string type = parts[0];
                    string description = parts[1];
                    int points = int.Parse(parts[2]);
                    bool isComplete = parts[3] == "1";

                    if (type == "SimpleGoal")
                    {
                        var goal = new SimpleGoal(description, points);
                        if (isComplete) goal.RecordEvent();
                        _goals.Add(goal);
                    }
                    else if (type == "EternalGoal")
                    {
                        var goal = new EternalGoal(description, points);
                        if (isComplete) goal.RecordEvent();
                        _goals.Add(goal);
                    }
                    else if (type == "ChecklistGoal")
                    {
                        // For checklist goals, you would need additional parameters to reconstruct the state.
                        // This is a simplified version and could be expanded.
                        var goal = new ChecklistGoal(description, points, 5, 500); // Placeholder values
                        if (isComplete) goal.RecordEvent();
                        _goals.Add(goal);
                    }
                }
                Console.WriteLine("Goals loaded!");
            }
        }
    }
}
