using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.calculaterWodsCoefficients
{
    public class QuantitativeWordCoefficient : ISetterWordCoefficient
    {
        public Dictionary<string, int> GetWordsCoefficient(
            IEnumerable<string> words, IEnumerable<string> excludedWords)
        {
            words = words.Select(word => word.ToLower());
            excludedWords = excludedWords.Select(word => word.ToLower());
            var wordToCount = new Dictionary<string, int>();
            foreach (var word in words)
                if (wordToCount.ContainsKey(word))
                    wordToCount[word]++;
                else
                    wordToCount[word] = 1;
            foreach (var boringWord in excludedWords)
                wordToCount.Remove(boringWord.ToLower());
            return wordToCount;
        }
    }
}