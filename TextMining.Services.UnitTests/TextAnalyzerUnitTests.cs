﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextMining.Tests.Common;

namespace TextMining.Services.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TextAnalyzerUnitTests
    {
        private Mock<IStemmingService> stemmingServiceMock;
        private TextAnalyzer textAnalyzer;

        private CompareLogic compareLogic;

        [TestInitialize]
        public void Setup()
        {
            stemmingServiceMock = new Mock<IStemmingService>();

            textAnalyzer = new TextAnalyzer(stemmingServiceMock.Object);

            SetupStemmingServiceMock();
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
            textAnalyzer.GetTextDataFromText(filepath, new List<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThatWhenStopWordsIsNullGetWordFrequenciesFromTextThrowsArgumentNullException()
        {
            textAnalyzer.GetTextDataFromText(Constants.TestFileName, null);
        }

        [TestMethod]
        public void TestThatGetWordFrequenciesFromTextReturnsExpectedTextData()
        {
            var textData = textAnalyzer.GetTextDataFromText(Constants.TextFromXmlFileFromTextElements, Constants.StopWords);

            Assert.IsTrue(compareLogic.Compare(Constants.WordFrequenciesFromText, textData.WordDictionary).AreEqual);
            Assert.IsTrue(compareLogic.Compare(Constants.AcronymFrequenciesFromText, textData.AcronymDictionary).AreEqual);
        }

        private void SetupStemmingServiceMock()
        {
            stemmingServiceMock
                .Setup(x => x.GetStemmedWord(It.IsAny<string>()))
                .Returns<string>(x => x);
        }
    }
}