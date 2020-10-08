using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextMining.Services;

namespace TextMining.UnitTests
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
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextIsNullGetXDocumentFromTextThrowsArgumentException()
        {
            xmlService.GetXDocumentFromText(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextIsEmptyGetXDocumentFromTextThrowsArgumentException()
        {
            xmlService.GetXDocumentFromText(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextHasOnlyWhitespacesGetXDocumentFromTextThrowsArgumentException()
        {
            xmlService.GetXDocumentFromText("  \t ");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void TestThatWhenTextIsInvalidGetXDocumentFromTextThrowsXmlException()
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenXmlDocumentIsNullGetTextFromAllElementsThrowsArgumentNullException()
        {
            xmlService.GetTextFromAllElements(null, Constants.TextTagName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenElementNameIsNullGetTextFromAllElementsThrowsArgumentException()
        {
            xmlService.GetTextFromAllElements(new XDocument(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenElementNameIsEmptyGetTextFromAllElementsThrowsArgumentException()
        {
            xmlService.GetTextFromAllElements(new XDocument(), string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenElementNameHasOnlyWhitespacesGetTextFromAllElementsThrowsArgumentException()
        {
            xmlService.GetTextFromAllElements(new XDocument(), "  \t ");
        }

        [TestMethod]
        public void TestThatGetTextFromAllElementsReturnsExpectedValue1()
        {
            var xDocument = XDocument.Parse(Constants.XmlFileText);

            var returnedValue = xmlService.GetTextFromAllElements(xDocument, Constants.TextTagName);
            
            var expectedValue = Constants.TextFromXmlFileFromTextElements.Replace("\r\n", "\n");
            Assert.AreEqual(expectedValue, returnedValue);
        }
    }
}