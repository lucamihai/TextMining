using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.BusinessLogic.Interfaces
{
    public interface IDocumentDataBusinessLogic
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
        DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths);
        GlobalDocumentData GetGlobalDocumentDataForMultipleXmlFiles(List<string> filepaths);
    }
}