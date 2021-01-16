using System.Collections.Generic;

namespace TextMining.DocumentDataLogic.Interfaces
{
    public interface IStopWordProvider
    {
        List<string> GetStopWords();
    }
}