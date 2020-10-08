using System;
using System.Diagnostics.CodeAnalysis;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextIsNullGetWordFrequenciesFromTextThrowsArgumentException()
        {
            textAnalyzer.GetWordFrequenciesFromText(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextIsEmptyGetWordFrequenciesFromTextThrowsArgumentException()
        {
            textAnalyzer.GetWordFrequenciesFromText(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenTextHasOnlyWhitespacesGetWordFrequenciesFromTextThrowsArgumentException()
        {
            textAnalyzer.GetWordFrequenciesFromText("  \t ");
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