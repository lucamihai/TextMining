using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class ResultFormatter : IResultFormatter
    {
        public string GetStringForWordFrequencies(Dictionary<string, int> wordFrequencies)
        {
            if (wordFrequencies == null)
            {
                throw new ArgumentNullException(nameof(wordFrequencies));
            }

            var stringBuilder = new StringBuilder();

            foreach (var wordFrequency in wordFrequencies.OrderByDescending(x => x.Value))
            {
                stringBuilder.AppendLine($"'{wordFrequency.Key}': {wordFrequency.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}