public class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration) { }

    protected override string GetDescription()
    {
        return "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    protected override void PerformActivity()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Pause(3);

        List<string> items = new List<string>();
        int elapsedTime = 0;
        while (elapsedTime < duration)
        {
            Console.Write("List an item: ");
            string item = Console.ReadLine();
            items.Add(item);
            elapsedTime += 3; // Each item takes about 3 seconds
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }
}
