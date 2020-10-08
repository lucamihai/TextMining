using System;
using System.Diagnostics.CodeAnalysis;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.Tests.Common;

namespace TextMining.BusinessLogic.IntegrationTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TextMiningBusinessLogicIntegrationTests
    {
        private ITextMiningBusinessLogic textMiningBusinessLogic;
        private string filepath;
        private CompareLogic compareLogic;

        [TestInitialize]
        public void Setup()
        {
            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();

            textMiningBusinessLogic = serviceProvider.GetService<ITextMiningBusinessLogic>();

            filepath = $"{Environment.CurrentDirectory}\\{Constants.TestFileName}";
            Tests.Common.Setup.CreateFileWithText(filepath, Constants.XmlFileText);
            compareLogic = new CompareLogic
            {
                Config = new ComparisonConfig
                {
                    IgnoreCollectionOrder = true
                }
            };
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromXmlFileReturnsExpectedWordFrequencies()
        {
            var wordFrequencies = textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(filepath);

            Assert.IsTrue(compareLogic.Compare(Constants.WordFrequenciesFromText, wordFrequencies).AreEqual);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Tests.Common.Cleanup.DeleteFileIfExists(filepath);
        }
    }
}
