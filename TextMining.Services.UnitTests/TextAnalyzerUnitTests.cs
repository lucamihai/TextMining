using System;
using System.Diagnostics.CodeAnalysis;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextMining.Tests.Common;

namespace TextMining.Services.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TextAnalyzerUnitTests
    {
        private TextAnalyzer textAnalyzer;
        private CompareLogic compareLogic;

        [TestInitialize]
        public void Setup()
        {
            textAnalyzer = new TextAnalyzer();

            compareLogic = new CompareLogic
            {
                Config = new ComparisonConfig
                {
                    IgnoreCollectionOrder = true
                }
            };
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextIsNotValidGetWordFrequenciesFromTextThrowsArgumentException(string filepath)
        {
            textAnalyzer.GetWordFrequenciesFromText(filepath);
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromTextReturnsExpectedWordFrequencies()
        {
            var wordFrequencies = textAnalyzer.GetWordFrequenciesFromText(Constants.TextFromXmlFileFromTextElements);

            var expectedWordFrequencies = Constants.WordFrequenciesFromText;
            Assert.IsTrue(compareLogic.Compare(expectedWordFrequencies, wordFrequencies).AreEqual);
        }
    }
}