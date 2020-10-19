using TextMining.Entities;

namespace TextMining.Services.Interfaces
{
    public interface ITextAnalyzer
    {
        TextData GetTextDataFromText(string text);
    }
}