class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Mindfulness Program!");
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine();

            if (choice == "4") break;

            Console.Write("Enter duration in seconds: ");
            int duration = int.Parse(Console.ReadLine());

            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(duration),
                "2" => new ReflectionActivity(duration),
                "3" => new ListingActivity(duration),
                _ => null
            };

            activity?.StartActivity();
        }
    }
}