using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = int.Parse(Console.ReadLine());
        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Check if the user passed
        if (gradePercentage >= 70)
        {
            Console.WriteLine($"Your letter grade is: {letter}. Congratulations, you passed!");
        }
        else
        {
            Console.WriteLine($"Your letter grade is: {letter}. Keep trying, you'll do better next time!");
        }

        // Determine the sign (+ or -)
        if (letter == "A")
        {
            if (gradePercentage % 10 >= 7)
                sign = "+";
            else if (gradePercentage % 10 < 3)
                sign = "-";
        }
        else if (letter == "B")
        {
            if (gradePercentage % 10 >= 7)
                sign = "+";
            else if (gradePercentage % 10 < 3)
                sign = "-";
        }
        else if (letter == "C")
        {
            if (gradePercentage % 10 >= 7)
                sign = "+";
            else if (gradePercentage % 10 < 3)
                sign = "-";
        }
        else if (letter == "D")
        {
            if (gradePercentage % 10 >= 7)
                sign = "+";
            else if (gradePercentage % 10 < 3)
                sign = "-";
        }
        // F does not have + or -
        
        // Final output with sign
        if (letter == "F")
        {
            Console.WriteLine($"Your final grade is: {letter}");
        }
        else
        {
            Console.WriteLine($"Your final grade is: {letter}{sign}");
        }
    }
}
