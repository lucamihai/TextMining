using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TextMining.DocumentDataLogic.Interfaces;
using TextMining.DocumentDataLogic.Interfaces.Services;
using TextMining.Entities;
using TextMining.Helpers;

namespace TextMining.DocumentDataLogic
{
    public class DocumentDataProvider : IDocumentDataProvider
    {
        private readonly IStopWordProvider stopWordProvider;
        private readonly IXmlService xmlService;
        private readonly ITextAnalyzer textAnalyzer;

        public DocumentDataProvider(IStopWordProvider stopWordProvider, IXmlService xmlService, ITextAnalyzer textAnalyzer)
        {
            this.stopWordProvider = stopWordProvider;
            this.xmlService = xmlService;
            this.textAnalyzer = textAnalyzer;
        }

        public DocumentData GetDocumentDataFromXmlFile(string filepath)
        {
            ArgumentValidator.ValidateString(filepath);

            var xDocument = xmlService.GetXDocumentFromFile(filepath);

            return new DocumentData
            {
                Title = GetTitleFromXDocument(xDocument),
                TextData = GetTextDataFromXDocument(xDocument),
                Topics = xmlService.GetTopicsFromXDocument(xDocument)
            };
        }

        public DocumentData GetDocumentDataForMultipleXmlFiles(List<string> filepaths)
        {
            ArgumentValidator.ValidateNotEmptyList(filepaths);

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
            var stopWords = stopWordProvider.GetStopWords();

            return textAnalyzer.GetTextDataFromText(text, stopWords);
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
    }
}