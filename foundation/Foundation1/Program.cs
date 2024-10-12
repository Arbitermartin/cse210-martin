using System;
using System.Collections.Generic;

public class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // in seconds
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Create videos
        Video video1 = new Video("Learning C# Basics", "John Doe", 360);
        video1.AddComment(new Comment("Alice", "Great video! Very helpful."));
        video1.AddComment(new Comment("Bob", "I learned a lot, thanks!"));
        video1.AddComment(new Comment("Charlie", "Could you cover more examples?"));

        Video video2 = new Video("Understanding OOP", "Jane Smith", 480);
        video2.AddComment(new Comment("David", "Excellent explanation of classes."));
        video2.AddComment(new Comment("Eve", "I love this series!"));
        video2.AddComment(new Comment("Frank", "More videos like this please!"));

        Video video3 = new Video("C# Advanced Topics", "Sarah Lee", 540);
        video3.AddComment(new Comment("Grace", "Very detailed, thank you!"));
        video3.AddComment(new Comment("Heidi", "This helped me so much."));
        video3.AddComment(new Comment("Ivan", "Can't wait for the next part!"));

        // Add videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Display video details
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}



