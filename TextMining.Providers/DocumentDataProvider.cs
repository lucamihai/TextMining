using System;
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

        private static void ValidateString(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filepath));
            }
        }
    }
}