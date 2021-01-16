using System.Collections.Generic;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DocumentDataLogic.Interfaces;
using TextMining.Entities;
using TextMining.Helpers;

namespace TextMining.BusinessLogic
{
    public class DocumentDataBusinessLogic : IDocumentDataBusinessLogic
    {
        private readonly IDocumentDataProvider documentDataProvider;

        public DocumentDataBusinessLogic(IDocumentDataProvider documentDataProvider)
        {

            this.documentDataProvider = documentDataProvider;
        }

        public DocumentData GetDocumentDataFromXmlFile(string filepath)
        {
            ArgumentValidator.ValidateString(filepath);

            return documentDataProvider.GetDocumentDataFromXmlFile(filepath);
        }

        public List<DocumentData> GetDocumentDataForMultipleXmlFiles(List<string> filePaths)
        {
            ArgumentValidator.ValidateNotEmptyList(filePaths);

            return documentDataProvider.GetDocumentDataForMultipleXmlFiles(filePaths);
        }
    }
}