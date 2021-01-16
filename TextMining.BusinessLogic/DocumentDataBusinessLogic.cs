using System.Collections.Generic;
using System.Linq;
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

        public DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths)
        {
            ArgumentValidator.ValidateNotEmptyList(filepaths);

            return documentDataProvider.GetDocumentDataForMultipleXmlFiles(filepaths);
        }

        public GlobalDocumentData GetGlobalDocumentDataForMultipleXmlFiles(List<string> filepaths)
        {
            ArgumentValidator.ValidateNotEmptyList(filepaths);

            var documentDataList = filepaths
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => documentDataProvider.GetDocumentDataFromXmlFile(x))
                .ToList();

            return GetGlobalDocumentDataFromDocumentDataList(documentDataList);
        }

        // TODO: Refactor
        private static GlobalDocumentData GetGlobalDocumentDataFromDocumentDataList(List<DocumentData> documentDataList)
        {
            var uniqueWords = documentDataList
                .SelectMany(x => x.TextData.WordDictionary.Select(y => y.Key))
                .Distinct()
                .ToList();

            var frequencies = new int[documentDataList.Count, uniqueWords.Count];
            for (var documentIndex = 0; documentIndex < documentDataList.Count; documentIndex++)
            {
                var documentData = documentDataList[documentIndex];

                for (var wordIndex = 0; wordIndex < uniqueWords.Count; wordIndex++)
                {
                    var word = uniqueWords[wordIndex];

                    if (documentData.TextData.WordDictionary.TryGetValue(word, out var frequency))
                    {
                        frequencies[documentIndex, wordIndex] = frequency;
                    }
                }
            }

            return new GlobalDocumentData
            {
                Words = uniqueWords,
                Frequencies = frequencies
            };
        }
    }
}