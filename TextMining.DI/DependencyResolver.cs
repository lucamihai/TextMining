using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Porter2StemmerStandard;
using TextMining.BusinessLogic;
using TextMining.BusinessLogic.Interfaces;
using TextMining.Providers;
using TextMining.Providers.Interfaces;
using TextMining.Services;
using TextMining.Services.Interfaces;

namespace TextMining.DI
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static ServiceCollection GetServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<ITextMiningBusinessLogic, TextMiningBusinessLogic>();

            services.AddScoped<IStopWordProvider, EnglishStopWordsProvider>();
            services.AddScoped<IDocumentDataProvider, DocumentDataProvider>();

            services.AddScoped<IStemmer, EnglishPorter2Stemmer>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IResultFormatter, ResultFormatter>();
            services.AddScoped<ITextAnalyzer, TextAnalyzer>();
            services.AddScoped<IXmlService, XmlService>();

            return services;
        }
    }
}
