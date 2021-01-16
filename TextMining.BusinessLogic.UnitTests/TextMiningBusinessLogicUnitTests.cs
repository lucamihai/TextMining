using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.Tests.Common;

namespace TextMining.BusinessLogic.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TextMiningBusinessLogicUnitTests
    {
        private DocumentDataBusinessLogic documentDataBusinessLogic;
        private Mock<IDocumentDataProvider> documentDataProviderMock;

        [TestInitialize]
        public void Setup()
        {
            documentDataProviderMock = new Mock<IDocumentDataProvider>();

            documentDataBusinessLogic = new DocumentDataBusinessLogic(documentDataProviderMock.Object);
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNotValidGetDocumentDataFromXmlFileThrowsArgumentException(string filepath)
        {
            documentDataBusinessLogic.GetDocumentDataFromXmlFile(filepath);
        }

        [TestMethod]
        public void TestThatGetDocumentDataFromXmlFileCallsDocumentDataProviderGetDocumentDataFromXmlFileOnce()
        {
            const string filepath = "abc.txt";

            documentDataBusinessLogic.GetDocumentDataFromXmlFile(filepath);

            documentDataProviderMock.Verify(x => x.GetDocumentDataFromXmlFile(filepath), Times.Once);
        }

    }
}
