using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.Services.Interfaces;
using TextMining.Tests.Common;

namespace TextMining.Providers.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DocumentDataProviderUnitTests
    {
        private DocumentDataProvider documentDataProvider;
        private Mock<IXmlService> xmlServiceMock;
        private Mock<ITextAnalyzer> textAnalyzerMock;

        private XDocument xDocumentReturnedByXmlService;
        private string textReturnedByXmlService;

        [TestInitialize]
        public void Setup()
        {
            xmlServiceMock = new Mock<IXmlService>();
            textAnalyzerMock = new Mock<ITextAnalyzer>();

            documentDataProvider = new DocumentDataProvider(xmlServiceMock.Object, textAnalyzerMock.Object);

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

            textAnalyzerMock.Verify(x => x.GetTextDataFromText(textReturnedByXmlService), Times.Once);
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