﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.MachineLearning;
using TextMining.Helpers.Interfaces;

namespace TextMining.Helpers
{
    public class FileService : IFileService
    {
        public string GetAllTextFromFile(string filepath)
        {
            ArgumentValidator.ValidateExistingFilepath(filepath);

            var cv = new KNearestNeighbors<string>();


            return File.ReadAllText(filepath);
        }

        public List<string> GetAllLinesFromFile(string filepath)
        {
            ArgumentValidator.ValidateExistingFilepath(filepath);

            return File.ReadAllLines(filepath).ToList();
        }
    }
}