namespace TextMining.DocumentDataLogic.Interfaces.Services
{
    public interface IStemmingService
    {
        string GetStemmedWord(string word);
    }
}