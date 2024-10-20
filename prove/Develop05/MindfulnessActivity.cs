using System;
using System.Collections.Generic;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected int duration;

    public MindfulnessActivity(int duration)
    {
        this.duration = duration;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {GetType().Name} activity.");
        Console.WriteLine(GetDescription());
        Console.Write("Set duration (seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        Pause(3);
        PerformActivity();
        EndActivity();
    }

    protected abstract string GetDescription();
    protected abstract void PerformActivity();

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    private void EndActivity()
    {
        Console.WriteLine("Good job! You've completed the activity.");
        Console.WriteLine($"You spent {duration} seconds on this activity.");
        Pause(2);
    }
}
