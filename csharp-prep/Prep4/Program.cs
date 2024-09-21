using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect user input
        while (true)
        {
            Console.Write("Enter number: ");
            int input = int.Parse(Console.ReadLine());

            if (input == 0)
            {
                break;
            }

            numbers.Add(input);
        }

        // Calculate sum, average, and maximum
        int sum = 0;
        int max = int.MinValue;

        foreach (var number in numbers)
        {
            sum += number;
            if (number > max)
            {
                max = number;
            }
        }

        double average = numbers.Count > 0 ? (double)sum / numbers.Count : 0;

        // Output results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenge
        // Find the smallest positive number
        int smallestPositive = int.MaxValue;

        foreach (var number in numbers)
        {
            if (number > 0 && number < smallestPositive)
            {
                smallestPositive = number;
            }
        }

        if (smallestPositive != int.MaxValue)
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        // Sort the list and display it
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
