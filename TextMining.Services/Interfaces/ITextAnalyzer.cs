using System.Collections.Generic;

namespace TextMining.Services.Interfaces
{
    public interface ITextAnalyzer
    {
        Dictionary<string, int> GetWordFrequenciesFromText(string text);
    }
}