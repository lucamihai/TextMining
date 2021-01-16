using Porter2StemmerStandard;
using TextMining.DocumentDataLogic.Interfaces.Services;
using TextMining.Helpers;

namespace TextMining.DocumentDataLogic.Services
{
    public class StemmingService : IStemmingService
    {
        private readonly IStemmer stemmer = new EnglishPorter2Stemmer();

        public string GetStemmedWord(string word)
        {
            ArgumentValidator.ValidateString(word);

            return stemmer.Stem(word).Value;
        }
    }
}