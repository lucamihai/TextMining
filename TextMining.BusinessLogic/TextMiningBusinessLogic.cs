using System;
using TextMining.BusinessLogic.Interfaces;
using TextMining.Entities;
using TextMining.Providers.Interfaces;

namespace TextMining.BusinessLogic
{
    public class TextMiningBusinessLogic : ITextMiningBusinessLogic
    {
        private readonly IDocumentDataProvider documentDataProvider;

        public TextMiningBusinessLogic(IDocumentDataProvider documentDataProvider)
        {
            this.documentDataProvider = documentDataProvider;
        }

        public DocumentData GetDocumentDataFromXmlFile(string filepath)
        {
            ValidateString(filepath);

            return documentDataProvider.GetDocumentDataFromXmlFile(filepath);
        }

        private void ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }
        }
    }
}