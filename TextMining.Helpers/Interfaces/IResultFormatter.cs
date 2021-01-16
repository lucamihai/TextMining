using System.Collections.Generic;

namespace TextMining.Helpers.Interfaces
{
    public interface IResultFormatter
    {
        string GetStringRepresentationForWordFrequencies(Dictionary<string, int> wordFrequencies);
    }
}