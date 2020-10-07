using System.Collections.Generic;

namespace TextMining.Services.Interfaces
{
    public interface IFormatter
    {
        string GetStringForWordFrequencies(Dictionary<string, int> wordFrequencies);
    }
}