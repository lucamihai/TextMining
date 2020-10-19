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
        public void TestThatGetWordFrequenciesFromXmlFileReturnsExpectedDocumentData()
        {
            var documentData = textMiningBusinessLogic.GetDocumentDataFromXmlFile(filepath);

            Assert.IsTrue(compareLogic.Compare(Constants.DocumentTitle, documentData.Title).AreEqual);
            Assert.IsTrue(compareLogic.Compare(Constants.CodesFromXml, documentData.Topics).AreEqual);
            Assert.IsTrue(compareLogic.Compare(Constants.WordFrequenciesFromText, documentData.TextData.WordDictionary).AreEqual);
            Assert.IsTrue(compareLogic.Compare(Constants.AcronymFrequenciesFromText, documentData.TextData.AcronymDictionary).AreEqual);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Tests.Common.Cleanup.DeleteFileIfExists(filepath);
        }
    }
}
