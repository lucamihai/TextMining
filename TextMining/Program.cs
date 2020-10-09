using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.Services.Interfaces;

namespace TextMining
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            var textMiningBusinessLogic = serviceProvider.GetService<ITextMiningBusinessLogic>();
            var formatter = serviceProvider.GetService<IResultFormatter>();

            // TODO: Get filepath from command line OR make filepath relative
            const string filepath = @"D:\Projects\TextMining\Resources\Reuters_34\Training\2504NEWS.XML";

            var wordFrequencies = textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(filepath);
            var resultAsString = formatter.GetStringForWordFrequencies(wordFrequencies);

            Console.WriteLine(resultAsString);
            Console.Read();
        }
    }
}
