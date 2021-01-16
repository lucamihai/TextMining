using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DocumentDataLogic;
using TextMining.DocumentDataLogic.Interfaces;
using TextMining.DocumentDataLogic.Interfaces.Services;
using TextMining.DocumentDataLogic.Services;
using TextMining.Helpers;
using TextMining.Helpers.Interfaces;

namespace TextMining.DI
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static ServiceCollection GetServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<IDocumentDataBusinessLogic, DocumentDataBusinessLogic>();

            services.AddScoped<IStopWordProvider, EnglishStopWordsProvider>();
            services.AddScoped<IDocumentDataProvider, DocumentDataProvider>();

            services.AddScoped<IStemmingService, StemmingService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IResultFormatter, ResultFormatter>();
            services.AddScoped<ITextAnalyzer, TextAnalyzer>();
            services.AddScoped<IXmlService, XmlService>();

            return services;
        }
    }
}
