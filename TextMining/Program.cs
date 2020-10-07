using System;
using TextMining.Services;

namespace TextMining
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileService = new FileService();
            var xmlService = new XmlService();
            var textAnalyzer = new TextAnalyzer();
            var formatter = new Formatter();

            // TODO: Get filepath from command line OR make filepath relative
            const string filepath = @"D:\Projects\TextMining\Resources\Reuters_34\Training\2504NEWS.XML";

            var fileText = fileService.GetAllTextFromFile(filepath);
            var xDocument = xmlService.GetXDocumentFromText(fileText);
            var text = xmlService.GetTextFromAllElements(xDocument, "text");
            var wordFrequencies = textAnalyzer.GetWordFrequenciesFromText(text);
            var resultAsString = formatter.GetStringForWordFrequencies(wordFrequencies);

            Console.WriteLine(resultAsString);
            Console.Read();
        }
    }
}
