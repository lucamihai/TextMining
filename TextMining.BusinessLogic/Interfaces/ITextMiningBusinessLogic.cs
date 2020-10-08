using System.Collections.Generic;

namespace TextMining.BusinessLogic.Interfaces
{
    public interface ITextMiningBusinessLogic
    {
        Dictionary<string, int> GetWordFrequenciesFromXmlFile(string filepath);
    }
}