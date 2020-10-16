using TextMining.Entities;

namespace TextMining.Providers.Interfaces
{
    public interface IDocumentDataProvider
    {
        DocumentData GetDocumentDataFromXmlFile(string filepath);
    }
}