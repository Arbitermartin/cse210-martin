  // Represents the scripture, including the reference and the text.
  public class Scripture
    {
        private List<Word> words;
        public Reference ScriptureReference { get; }

        public Scripture(string reference, string text)
        {
            ScriptureReference = new Reference(reference);
            words = text.Split(' ').Select(w => new Word(w)).ToList();
        }
        // Hides a random word in the scripture.
        public void HideRandomWord()
        {
            var unhiddenWords = words.Where(w => !w.IsHidden).ToList();
            if (unhiddenWords.Count == 0) return;

            var random = new Random();
            int index = random.Next(unhiddenWords.Count);
            unhiddenWords[index].IsHidden = true;
        }

        // Returns the current state of the scripture as a string.
        public string GetDisplayedText()
        {
            return ScriptureReference.Text + "\n" +
                   string.Join(" ", words.Select(w => w.IsHidden ? "___" : w.Text));
        }

        // Check if all words are hidden.
        public bool AllWordsHidden() => words.All(w => w.IsHidden);
    }