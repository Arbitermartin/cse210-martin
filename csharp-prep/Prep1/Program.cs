

using System;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("What is your First Name: ");
        string FirstName = Console.ReadLine();

        Console.WriteLine("What is yor LastName: ");
        string LastName = Console.ReadLine();


        Console.Write($"your name is {LastName},  {FirstName} {LastName}" );


    }
}