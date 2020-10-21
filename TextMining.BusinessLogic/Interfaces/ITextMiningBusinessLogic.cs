using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.BusinessLogic.Interfaces
{
    public interface ITextMiningBusinessLogic
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
        DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths);
    }
}