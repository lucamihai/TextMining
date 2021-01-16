using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.DocumentDataLogic.Interfaces.Services
{
    public interface ITextAnalyzer
    {
        TextData GetTextDataFromText(string text, List<string> stopWords);
    }
}