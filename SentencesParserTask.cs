using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var separators = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            var sentences = text.Split(separators);
            foreach (var sentence in sentences)
            {
                var words = GetWords(sentence);
                if (words.Count > 0)
                    sentencesList.Add(words);
            }
            return sentencesList;
        }

        public static List<string> GetWords(string sentence)
        {
            var words = new List<string>();
            var currentWord = new StringBuilder();
            foreach (var symbol in sentence)
            {
                if (char.IsLetter(symbol) || symbol == '\'')
                {
                    currentWord.Append(char.ToLower(symbol));
                }
                else if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
            if (currentWord.Length > 0)
                words.Add(currentWord.ToString());
            return words;
        }
    }
}