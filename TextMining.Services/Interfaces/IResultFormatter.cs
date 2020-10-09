using System.Collections.Generic;

namespace TextMining.Services.Interfaces
{
    public interface IResultFormatter
    {
        string GetStringForWordFrequencies(Dictionary<string, int> wordFrequencies);
    }
}