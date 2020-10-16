using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.Providers.Interfaces;
using TextMining.Tests.Common;

namespace TextMining.BusinessLogic.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TextMiningBusinessLogicUnitTests
    {
        private TextMiningBusinessLogic textMiningBusinessLogic;
        private Mock<IDocumentDataProvider> documentDataProviderMock;

        [TestInitialize]
        public void Setup()
        {
            documentDataProviderMock = new Mock<IDocumentDataProvider>();

            textMiningBusinessLogic = new TextMiningBusinessLogic(documentDataProviderMock.Object);
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNotValidGetDocumentDataFromXmlFileThrowsArgumentException(string filepath)
        {
            textMiningBusinessLogic.GetDocumentDataFromXmlFile(filepath);
        }

        [TestMethod]
        public void TestThatGetDocumentDataFromXmlFileCallsDocumentDataProviderGetDocumentDataFromXmlFileOnce()
        {
            const string filepath = "abc.txt";

            textMiningBusinessLogic.GetDocumentDataFromXmlFile(filepath);

            documentDataProviderMock.Verify(x => x.GetDocumentDataFromXmlFile(filepath), Times.Once);
        }

    }
}
