using System;
using System.Collections.Generic;
using TextMining.Providers.Interfaces;
using TextMining.Services.Interfaces;

namespace TextMining.Providers
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
                var stopWordsFileText = fileService.GetAllTextFromFile("StopWords.txt");
                var lines = stopWordsFileText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

                stopWords = new List<string>();

                foreach (var line in lines)
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