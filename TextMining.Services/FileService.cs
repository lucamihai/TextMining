using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class FileService : IFileService
    {
        public string GetAllTextFromFile(string filepath)
        {
            ValidateExistingFilepath(filepath);

            return File.ReadAllText(filepath);
        }

        public List<string> GetAllLinesFromFile(string filepath)
        {
            ValidateExistingFilepath(filepath);

            return File.ReadAllLines(filepath).ToList();
        }

        private void ValidateExistingFilepath(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filepath));
            }

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }
        }
    }
}