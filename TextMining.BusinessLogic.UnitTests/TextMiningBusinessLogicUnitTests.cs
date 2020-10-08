using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.Services.Interfaces;

namespace TextMining.BusinessLogic.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TextMiningBusinessLogicUnitTests
    {
        private TextMiningBusinessLogic textMiningBusinessLogic;
        private Mock<IFileService> fileServiceMock;
        private Mock<IXmlService> xmlServiceMock;
        private Mock<ITextAnalyzer> textAnalyzerMock;

        private string textReturnedByFileService;
        private XDocument xDocumentReturnedByXmlService;
        private string textReturnedByXmlService;

        [TestInitialize]
        public void Setup()
        {
            fileServiceMock = new Mock<IFileService>();
            xmlServiceMock = new Mock<IXmlService>();
            textAnalyzerMock = new Mock<ITextAnalyzer>();

            textMiningBusinessLogic = new TextMiningBusinessLogic(fileServiceMock.Object, xmlServiceMock.Object, textAnalyzerMock.Object);

            SetupFileServiceMock();
            SetupXmlServiceMock();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNullGetWordFrequenciesFromXmlFileThrowsArgumentException()
        {
            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsEmptyGetWordFrequenciesFromXmlFileThrowsArgumentException()
        {
            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathHasOnlyWhitespacesGetWordFrequenciesFromXmlFileThrowsArgumentException()
        {
            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile("  \t ");
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromXmlFileCallsFileServiceGetAllTextFromFileOnce()
        {
            const string filepath = "abc.txt";

            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(filepath);

            fileServiceMock.Verify(x => x.GetAllTextFromFile(filepath), Times.Once);
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromXmlFileCallsXmlServiceGetXDocumentFromTextOnce()
        {
            const string filepath = "abc.txt";

            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(filepath);

            xmlServiceMock.Verify(x => x.GetXDocumentFromText(textReturnedByFileService), Times.Once);
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromXmlFileCallsXmlServiceGetTextFromAllElementsOnce()
        {
            const string filepath = "abc.txt";
            const string tagName = "text";

            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(filepath);

            xmlServiceMock.Verify(x => x.GetTextFromAllElements(xDocumentReturnedByXmlService, tagName, null), Times.Once);
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromXmlFileCallsTextAnalyzerGetWordFrequenciesFromTextOnce()
        {
            const string filepath = "abc.txt";

            textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(filepath);

            textAnalyzerMock.Verify(x => x.GetWordFrequenciesFromText(textReturnedByXmlService), Times.Once);
        }

        private void SetupFileServiceMock()
        {
            textReturnedByFileService = "<p>abc</p>";
            fileServiceMock
                .Setup(x => x.GetAllTextFromFile(It.IsAny<string>()))
                .Returns(textReturnedByFileService);
        }

        private void SetupXmlServiceMock()
        {
            xDocumentReturnedByXmlService = new XDocument();
            xmlServiceMock
                .Setup(x => x.GetXDocumentFromText(It.IsAny<string>()))
                .Returns(xDocumentReturnedByXmlService);

            textReturnedByXmlService = "abc";
            xmlServiceMock
                .Setup(x => x.GetTextFromAllElements(It.IsAny<XDocument>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(textReturnedByXmlService);
        }
    }
}
