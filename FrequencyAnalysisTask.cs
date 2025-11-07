using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = CalculateAllNGrams(text);
            return GetMostFrequentContinuations(result);
        }

        private static Dictionary<string, Dictionary<string, int>> CalculateAllNGrams(List<List<string>> text)
        {
            var dict = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                for (int n = 2; n <= 3; n++)
                {
                    if (sentence.Count < n) continue;
                    for (var i = 0; i <= sentence.Count - n; i++)
                    {
                        var key = string.Join(" ", sentence.GetRange(i, n - 1));
                        var nextWord = sentence[i + n - 1];

                        if (!dict.ContainsKey(key))
                            dict[key] = new Dictionary<string, int>();
                        if (!dict[key].ContainsKey(nextWord))
                            dict[key][nextWord] = 0;
                        dict[key][nextWord]++;
                    }
                }
            }
            return dict;
        }

        private static Dictionary<string, string> GetMostFrequentContinuations(
            Dictionary<string, Dictionary<string, int>> dict)
        {
            var result = new Dictionary<string, string>();
            foreach (var keyValue in dict)
            {
                string key = keyValue.Key;
                var continuations = keyValue.Value;
                int maxFrequency = continuations.Values.Max();
                result[key] = continuations
                    .Where(pair => pair.Value == maxFrequency)
                    .Select(pair => pair.Key)
                    .OrderBy(word => word, StringComparer.Ordinal)
                    .First();
            }
            return result;
        }
    }
}