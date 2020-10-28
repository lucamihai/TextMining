using System.Collections.Generic;

namespace TextMining.Providers.Interfaces
{
    public interface IStopWordProvider
    {
        List<string> GetStopWords();
    }
}