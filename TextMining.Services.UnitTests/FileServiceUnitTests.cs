using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextMining.Helpers;
using TextMining.Tests.Common;

namespace TextMining.Services.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FileServiceUnitTests
    {
        private string filepath;
        private string fileText;
        private FileService fileService;

        [TestInitialize]
        public void Setup()
        {
            filepath = $"{Environment.CurrentDirectory}\\{Constants.TestFileName}";
            fileText = $"abc{Environment.NewLine}def";
            File.WriteAllText(filepath, fileText);

            fileService = new FileService();
        }

        [TestMethod]
        [DataRow(Constants.NullString, DisplayName = "Null string")]
        [DataRow(Constants.EmptyString, DisplayName = "Empty string")]
        [DataRow(Constants.OnlyWhitespacesString, DisplayName = "Only whitespaces string")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNotValidGetAllTextFromFileThrowsArgumentException(string filepathArgument)
        {
            fileService.GetAllTextFromFile(filepathArgument);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestThatWhenFileDoesNotExistGetAllTextFromFileThrowsFileNotFoundException()
        {
            fileService.GetAllTextFromFile("derp derp.404");
        }

        [TestMethod]
        public void TestThatGetAllTextFromFileReturnsExpectedValue()
        {
            var returnedText = fileService.GetAllTextFromFile(filepath);

            Assert.AreEqual(fileText, returnedText);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Tests.Common.Cleanup.DeleteFileIfExists(filepath);
        }
    }
}
