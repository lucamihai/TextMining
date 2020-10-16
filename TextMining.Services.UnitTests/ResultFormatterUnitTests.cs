using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextMining.Services.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ResultFormatterUnitTests
    {
        private ResultFormatter resultFormatter;

        [TestInitialize]
        public void Setup()
        {
            resultFormatter = new ResultFormatter();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenWordFrequenciesIsNullGetStringForWordFrequenciesThrowsArgumentNullException()
        {
            resultFormatter.GetStringForWordFrequencies(null);
        }

        [TestMethod]
        public void TestThatGetStringForWordFrequenciesReturnsExpectedValue()
        {
            var wordFrequencies = new Dictionary<string, int>
            {
                { "word1", 5 },
                { "word2", 10 }
            };
            var expectedValue = $"'word2': 10{Environment.NewLine}'word1': 5{Environment.NewLine}";

            var returnedValue = resultFormatter.GetStringForWordFrequencies(wordFrequencies);

            Assert.AreEqual(expectedValue, returnedValue);
        }
    }
}