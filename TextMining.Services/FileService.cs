using System;
using System.IO;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class FileService : IFileService
    {
        public string GetAllTextFromFile(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filepath));
            }

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }

            return File.ReadAllText(filepath);
        }
    }
}