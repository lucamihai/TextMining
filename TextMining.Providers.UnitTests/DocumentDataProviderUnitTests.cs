using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.Tests.Common;

namespace TextMining.Providers.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DocumentDataProviderUnitTests
    {
        private DocumentDataProvider documentDataProvider;
        private Mock<IStopWordProvider> stopWordsProviderMock;
        private Mock<IXmlService> xmlServiceMock;
        private Mock<ITextAnalyzer> textAnalyzerMock;

        private List<string> stopWordsReturnedByStopWordsProvider;
        private XDocument xDocumentReturnedByXmlService;
        private string textReturnedByXmlService;

        [TestInitialize]
        public void Setup()
        {
            stopWordsProviderMock = new Mock<IStopWordProvider>();
            xmlServiceMock = new Mock<IXmlService>();
            textAnalyzerMock = new Mock<ITextAnalyzer>();

            documentDataProvider = new DocumentDataProvider(stopWordsProviderMock.Object, xmlServiceMock.Object, textAnalyzerMock.Object);

            SetupStopWordsProviderMock();
            SetupXmlServiceMock();
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNotValidGetDocumentDataFromXmlFileThrowsArgumentException(string filepath)
        {
            documentDataProvider.GetDocumentDataFromXmlFile(filepath);
        }

        [TestMethod]
        public void TestThatGetDocumentDataFromXmlFileCallsXmlServiceGetXDocumentFromFileOnce()
        {
            const string filepath = "abc.txt";

            documentDataProvider.GetDocumentDataFromXmlFile(filepath);

            xmlServiceMock.Verify(x => x.GetXDocumentFromFile(filepath), Times.Once);
        }

        [TestMethod]
        public void TestThatGetDocumentDataFromXmlFileCallsXmlServiceGetTextFromAllElementsOnce()
        {
            const string filepath = "abc.txt";
            const string tagName = "text";

            documentDataProvider.GetDocumentDataFromXmlFile(filepath);

            xmlServiceMock.Verify(x => x.GetTextFromAllElements(xDocumentReturnedByXmlService, tagName, null), Times.Once);
        }

        [TestMethod]
        public void TestThatGetDocumentDataFromXmlFileCallsTextAnalyzerGetWordFrequenciesFromTextOnce()
        {
            const string filepath = "abc.txt";

            documentDataProvider.GetDocumentDataFromXmlFile(filepath);

            textAnalyzerMock.Verify(x => x.GetTextDataFromText(textReturnedByXmlService, stopWordsReturnedByStopWordsProvider), Times.Once);
        }

        private void SetupStopWordsProviderMock()
        {
            stopWordsReturnedByStopWordsProvider = new List<string>();
            stopWordsProviderMock
                .Setup(x => x.GetStopWords())
                .Returns(stopWordsReturnedByStopWordsProvider);
        }

        private void SetupXmlServiceMock()
        {
            xDocumentReturnedByXmlService = new XDocument();
            xmlServiceMock
                .Setup(x => x.GetXDocumentFromFile(It.IsAny<string>()))
                .Returns(xDocumentReturnedByXmlService);

            textReturnedByXmlService = "abc";
            xmlServiceMock
                .Setup(x => x.GetTextFromAllElements(It.IsAny<XDocument>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(textReturnedByXmlService);
        }
    }
}