using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic;
using TextMining.BusinessLogic.Interfaces;
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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IResultFormatter, ResultFormatter>();
            services.AddScoped<ITextAnalyzer, TextAnalyzer>();
            services.AddScoped<IXmlService, XmlService>();

            return services;
        }
    }
}
