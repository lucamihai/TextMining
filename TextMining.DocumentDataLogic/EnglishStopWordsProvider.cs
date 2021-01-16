using System.Collections.Generic;
using TextMining.DocumentDataLogic.Interfaces;
using TextMining.Helpers.Interfaces;

namespace TextMining.DocumentDataLogic
{
    public class EnglishStopWordsProvider : IStopWordProvider
    {
        private readonly IFileService fileService;
        private List<string> stopWords;

        public EnglishStopWordsProvider(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public List<string> GetStopWords()
        {
            if (stopWords == null)
            {
                var stopWordLinesFromFile = fileService.GetAllLinesFromFile("StopWords.txt");

                stopWords = new List<string>();

                foreach (var line in stopWordLinesFromFile)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    stopWords.Add(line.Trim());
                }
            }

            return stopWords;
        }
    }
}