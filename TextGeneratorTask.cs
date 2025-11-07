using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var result = phraseBeginning.Split(' ').ToList();
            for (var i = 0; i < wordsCount; i++)
            {
                var nextWord = GetNextWord(nextWords, result);
                if (nextWord == null || nextWord == "")
                    break;
                result.Add(nextWord);
            }
            return string.Join(" ", result);
        }

        public static string GetNextWord(Dictionary<string, string> nextWords, List<string> result)
        {
            if (result.Count >= 2)
            {
                var lastTwoWords = string.Format("{0} {1}",
                                                    result[result.Count - 2],
                                                    result[result.Count - 1]);
                if (nextWords.ContainsKey(lastTwoWords))
                    return nextWords[lastTwoWords];
            }
            if (result.Count >= 1)
            {
                var lastWord = result[result.Count - 1];
                if (nextWords.ContainsKey(lastWord))
                    return nextWords[lastWord];
            }
            return null;
        }
    }
}