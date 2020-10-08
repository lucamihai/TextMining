using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsNullGetAllTextFromFileThrowsArgumentException()
        {
            fileService.GetAllTextFromFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathIsEmptyGetAllTextFromFileThrowsArgumentException()
        {
            fileService.GetAllTextFromFile(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestThatWhenFilepathHasOnlyWhitespacesGetAllTextFromFileThrowsArgumentException()
        {
            fileService.GetAllTextFromFile("  \t ");
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
