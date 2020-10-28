using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.Services.Interfaces
{
    public interface ITextAnalyzer
    {
        TextData GetTextDataFromText(string text, List<string> stopWords);
    }
}