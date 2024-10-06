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