using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.Helpers.Interfaces;

namespace TextMining
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            var textMiningBusinessLogic = serviceProvider.GetService<IDocumentDataBusinessLogic>();
            var formatter = serviceProvider.GetService<IResultFormatter>();

            // TODO: Get filepath from command line OR make filepath relative
            const string filepath = @"D:\Projects\TextMining\Resources\Reuters_34\Training\2504NEWS.XML";

            var documentData = textMiningBusinessLogic.GetDocumentDataFromXmlFile(filepath);
            var wordFrequencies = documentData.TextData.WordDictionary;
            var resultAsString = formatter.GetStringRepresentationForWordFrequencies(wordFrequencies);

            Console.WriteLine(resultAsString);
            Console.Read();
        }
    }
}
