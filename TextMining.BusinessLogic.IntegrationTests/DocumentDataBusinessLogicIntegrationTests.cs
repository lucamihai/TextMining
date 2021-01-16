using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.Tests.Common;

namespace TextMining.BusinessLogic.IntegrationTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DocumentDataBusinessLogicIntegrationTests
    {
        private Mock<IStopWordProvider> stopWordsProviderMock;
        private IDocumentDataBusinessLogic documentDataBusinessLogic;
        private string filepath;
        private CompareLogic compareLogic;

        [TestInitialize]
        public void Setup()
        {
            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();

            stopWordsProviderMock = new Mock<IStopWordProvider>();
            documentDataBusinessLogic = new DocumentDataBusinessLogic(new DocumentDataProvider(stopWordsProviderMock.Object, serviceProvider.GetService<IXmlService>(), serviceProvider.GetService<ITextAnalyzer>()));

            filepath = $"{Environment.CurrentDirectory}\\{Constants.TestFileName}";
            Tests.Common.Setup.CreateFileWithText(filepath, Constants.XmlFileText);
            compareLogic = new CompareLogic
            {
                Config = new ComparisonConfig
                {
                    IgnoreCollectionOrder = true
                }
            };

            stopWordsProviderMock
                .Setup(x => x.GetStopWords())
                .Returns(new List<string>());
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromXmlFileReturnsExpectedDocumentData()
        {
            var documentData = documentDataBusinessLogic.GetDocumentDataFromXmlFile(filepath);

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
