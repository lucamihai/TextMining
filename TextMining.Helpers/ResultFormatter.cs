using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextMining.Helpers.Interfaces;

namespace TextMining.Helpers
{
    public class ResultFormatter : IResultFormatter
    {
        public string GetStringRepresentationForWordFrequencies(Dictionary<string, int> wordFrequencies)
        {
            ArgumentValidator.ValidateObject(wordFrequencies);

            var stringBuilder = new StringBuilder();

            foreach (var wordFrequency in wordFrequencies.OrderByDescending(x => x.Value))
            {
                stringBuilder.AppendLine($"'{wordFrequency.Key}': {wordFrequency.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}