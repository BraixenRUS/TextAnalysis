using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var bigramFrequencies = GetNGramFrequencies(text, 2);
            var trigramFrequencies = GetNGramFrequencies(text, 3);
            result = IncludeDictionaryInDictionary(bigramFrequencies, result);
            return IncludeDictionaryInDictionary(trigramFrequencies, result);
        }

        public static Dictionary<string, string> GetNGramFrequencies(List<List<string>> text, int n)
        {
            var frequencyDict = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                if (sentence.Count < n) continue;
                for (int i = 0; i <= sentence.Count - n; i++)
                {
                    string key = string.Join(" ", sentence.GetRange(i, n - 1));
                    string nextWord = sentence[i + n - 1];
                    if (!frequencyDict.ContainsKey(key))
                        frequencyDict[key] = new Dictionary<string, int>();
                    if (!frequencyDict[key].ContainsKey(nextWord))
                        frequencyDict[key][nextWord] = 0;
                    frequencyDict[key][nextWord]++;
                }
            }
            return GetMostFrequentContinuations(frequencyDict);
        }

        public static Dictionary<string, string> GetMostFrequentContinuations(Dictionary<string, Dictionary<string, int>> dict)
        {
            var result = new Dictionary<string, string>();
            foreach (var keyValue in dict)
            {
                string key = keyValue.Key;
                var continuations = keyValue.Value;
                int maxFrequency = continuations.Values.Max();
                var mostFrequent = continuations
                    .Where(pair => pair.Value == maxFrequency)
                    .Select(pair => pair.Key);
                string bestContinuation = mostFrequent
                    .OrderBy(word => word, StringComparer.Ordinal)
                    .First();
                result[key] = bestContinuation;
            }
            return result;
        }

        public static Dictionary<string, string> IncludeDictionaryInDictionary(Dictionary<string, string> dict, Dictionary<string, string> result)
        {
            foreach (var pair in dict)
                result[pair.Key] = pair.Value;
            return result;
        }
    }
}