using System;
using System.Collections.Generic;
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

        public DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths)
        {
            ValidateList(filepaths);

            return documentDataProvider.GetDocumentDataForMultipleXmlFiles(filepaths);
        }

        private void ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }
        }

        private void ValidateList<T>(List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (list.Count == 0)
            {
                throw new ArgumentException(nameof(list));
            }
        }
    }
}