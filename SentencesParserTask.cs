using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var punctuation = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            var sentences = text.ToLower().Split(punctuation);
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
            var currentWord = new List<char>();
            foreach (var symbol in sentence)
            {
                if (char.IsLetter(symbol) || symbol == '\'')
                {
                    currentWord.Add(symbol);
                }
                else if (currentWord.Count > 0)
                {
                    words.Add(new string(currentWord.ToArray()));
                    currentWord.Clear();
                }
            }
            return words;
        }
    }
}