using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.BusinessLogic.Interfaces
{
    public interface IDocumentDataBusinessLogic
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
        List<DocumentData> GetDocumentDataForMultipleXmlFiles(List<string> filePaths);
    }
}