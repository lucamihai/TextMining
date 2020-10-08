using System;
using System.Collections.Generic;
using TextMining.BusinessLogic.Interfaces;
using TextMining.Services.Interfaces;

namespace TextMining.BusinessLogic
{
    public class TextMiningBusinessLogic : ITextMiningBusinessLogic
    {
        private readonly IFileService fileService;
        private readonly IXmlService xmlService;
        private readonly ITextAnalyzer textAnalyzer;

        public TextMiningBusinessLogic(IFileService fileService, IXmlService xmlService, ITextAnalyzer textAnalyzer)
        {
            this.fileService = fileService;
            this.xmlService = xmlService;
            this.textAnalyzer = textAnalyzer;
        }

        public Dictionary<string, int> GetWordFrequenciesFromXmlFile(string filepath)
        {
            ValidateString(filepath);

            var fileText = fileService.GetAllTextFromFile(filepath);
            var xDocument = xmlService.GetXDocumentFromText(fileText);
            var text = xmlService.GetTextFromAllElements(xDocument, "text");
            var wordFrequencies = textAnalyzer.GetWordFrequenciesFromText(text);

            return wordFrequencies;
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