using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TextMining.Entities;
using TextMining.Providers.Interfaces;
using TextMining.Services.Interfaces;

namespace TextMining.Providers
{
    public class DocumentDataProvider : IDocumentDataProvider
    {
        private readonly IXmlService xmlService;
        private readonly ITextAnalyzer textAnalyzer;

        public DocumentDataProvider(IXmlService xmlService, ITextAnalyzer textAnalyzer)
        {
            this.xmlService = xmlService;
            this.textAnalyzer = textAnalyzer;
        }

        public DocumentData GetDocumentDataFromXmlFile(string filepath)
        {
            ValidateString(filepath);

            var xDocument = xmlService.GetXDocumentFromFile(filepath);

            return new DocumentData
            {
                Title = GetTitleFromXDocument(xDocument),
                TextData = GetTextDataFromXDocument(xDocument),
                Topics = xmlService.GetCodesFromXDocument(xDocument)
            };
        }

        public DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths)
        {
            ValidateList(filepaths);

            var documentDataList = new List<DocumentData>();

            foreach (var filepath in filepaths)
            {
                try
                {
                    var documentData = GetDocumentDataFromXmlFile(filepath);
                    documentDataList.Add(documentData);
                }
                catch (Exception e)
                {
                    // TODO: Log this
                }
            }

            return JoinDocumentDataList(documentDataList);
        }

        private string GetTitleFromXDocument(XDocument xDocument)
        {
            var title = xmlService.GetTextFromAllElements(xDocument, "title");

            return string.IsNullOrWhiteSpace(title)
                ? "No title"
                : title;
        }

        private TextData GetTextDataFromXDocument(XDocument xDocument)
        {
            var text = xmlService.GetTextFromAllElements(xDocument, "text");

            return textAnalyzer.GetTextDataFromText(text);
        }

        private DocumentData JoinDocumentDataList(List<DocumentData> documentDataList)
        {
            var joinedDocumentData = new DocumentData
            {
                Title = "Joined document data",
                TextData = new TextData
                {
                    AcronymDictionary = new Dictionary<string, int>(),
                    WordDictionary = new Dictionary<string, int>()
                },
                Topics = new List<string>()
            };

            foreach (var documentData in documentDataList)
            {
                AddDictionaryKeyValuePairsToDictionary(documentData.TextData.AcronymDictionary, joinedDocumentData.TextData.AcronymDictionary);
                AddDictionaryKeyValuePairsToDictionary(documentData.TextData.WordDictionary, joinedDocumentData.TextData.WordDictionary);
                AddListValuesToList(documentData.Topics, joinedDocumentData.Topics);
            }

            return joinedDocumentData;
        }

        private void AddDictionaryKeyValuePairsToDictionary(Dictionary<string, int> source, Dictionary<string, int> destination)
        {
            foreach (var sourceKeyValuePair in source)
            {
                if (destination.ContainsKey(sourceKeyValuePair.Key))
                {
                    destination[sourceKeyValuePair.Key] += sourceKeyValuePair.Value;
                }
                else
                {
                    destination.Add(sourceKeyValuePair.Key, sourceKeyValuePair.Value);
                }
            }
        }

        private void AddListValuesToList(List<string> source, List<string> destination)
        {
            foreach (var sourceValue in source)
            {
                if (!destination.Contains(sourceValue))
                {
                    destination.Add(sourceValue);
                }
            }
        }

        private static void ValidateString(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filepath));
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