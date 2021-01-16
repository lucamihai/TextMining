using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.DocumentDataLogic.Interfaces
{
    public interface IDocumentDataProvider
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
        List<DocumentData> GetDocumentDataForMultipleXmlFiles(List<string> filePaths);
    }
}