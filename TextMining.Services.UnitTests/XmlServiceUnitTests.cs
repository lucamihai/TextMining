using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextMining.Tests.Common;

namespace TextMining.Services.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class XmlServiceUnitTests
    {
        private XmlService xmlService;
        private CompareLogic comparer;

        [TestInitialize]
        public void Setup()
        {
            xmlService = new XmlService();

            comparer = new CompareLogic();
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextIsNotValidGetXDocumentFromTextThrowsArgumentException(string text)
        {
            xmlService.GetXDocumentFromText(text);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void TestThatWhenTextIsNotValidXmlTextGetXDocumentFromTextThrowsXmlException()
        {
            xmlService.GetXDocumentFromText("abc");
        }

        [TestMethod]
        public void TestThatGetXDocumentFromTextReturnsExpectedXDocument()
        {
            var xDocument = xmlService.GetXDocumentFromText(Constants.XmlFileText);

            Assert.IsNotNull(xDocument);
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNotValidGetXDocumentFromFileThrowsArgumentException(string filepath)
        {
            xmlService.GetXDocumentFromFile(filepath);
        }

        [TestMethod]
        public void TestThatGetXDocumentFromFileReturnsExpectedXDocument()
        {
            var filepath = Constants.TestFileName;
            Tests.Common.Setup.CreateFileWithText(filepath, Constants.XmlFileText);

            var xDocument = xmlService.GetXDocumentFromFile(filepath);

            Cleanup.DeleteFileIfExists(filepath);
            Assert.IsNotNull(xDocument);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenXmlDocumentIsNullGetTextFromAllElementsThrowsArgumentNullException()
        {
            xmlService.GetTextFromAllElements(null, Constants.TextTagName);
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenElementNameIsNotValidGetTextFromAllElementsThrowsArgumentException(string elementName)
        {
            xmlService.GetTextFromAllElements(new XDocument(), elementName);
        }

        [TestMethod]
        public void TestThatGetTextFromAllElementsReturnsExpectedValue1()
        {
            var xDocument = XDocument.Parse(Constants.XmlFileText);

            var returnedValue = xmlService.GetTextFromAllElements(xDocument, Constants.TextTagName);
            
            var expectedValue = Constants.TextFromXmlFileFromTextElements.Replace("\r\n", "\n");
            Assert.AreEqual(expectedValue, returnedValue);
        }

        [TestMethod]
        public void TestThatGetTextFromAllElementsReturnsExpectedValue2()
        {
            var xDocument = XDocument.Parse(Constants.XmlFileText);

            var returnedValue = xmlService.GetTextFromAllElements(xDocument, Constants.ParagraphTagName);

            var expectedValue = Constants.ExpectedTextFromXmlFileFromParagraphElements.Replace("\r\n", "\n");
            Assert.AreEqual(expectedValue, returnedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenXDocumentIsNullGetTopicsFromXDocumentThrowsArgumentNullException()
        {
            xmlService.GetTopicsFromXDocument(null);
        }

        [TestMethod]
        public void TestThatGetTopicsFromXDocumentReturnsExpectedValue()
        {
            var xDocument = XDocument.Parse(Constants.XmlFileText);

            var returnedValue = xmlService.GetTopicsFromXDocument(xDocument);

            Assert.IsTrue(comparer.Compare(Constants.CodesFromXml, returnedValue).AreEqual);
        }
    }
}