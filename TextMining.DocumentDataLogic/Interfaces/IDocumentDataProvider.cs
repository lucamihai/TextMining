using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.DocumentDataLogic.Interfaces
{
    public interface IDocumentDataProvider
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
        DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths);
    }
}