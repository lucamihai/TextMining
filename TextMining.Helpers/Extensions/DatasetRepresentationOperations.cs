using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TextMining.Entities;

namespace TextMining.Helpers.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DatasetRepresentationOperations
    {
        public static DatasetRepresentation ReconstructByKeepingOnlyTheseWords(this DatasetRepresentation datasetRepresentation, List<string> words)
        {
            var documentCount = datasetRepresentation.Frequencies.GetLength(0);
            var frequencies = new int[documentCount, words.Count];

            for (int wordIndex = 0; wordIndex < words.Count; wordIndex++)
            {
                var indexOfWordInOldRepresentation = datasetRepresentation.Words.IndexOf(words[wordIndex]);

                for (int documentIndex = 0; documentIndex < documentCount; documentIndex++)
                {
                    var frequency = datasetRepresentation.Frequencies[documentIndex, indexOfWordInOldRepresentation];
                    frequencies[documentIndex, wordIndex] = frequency;
                }
            }

            return new DatasetRepresentation
            {
                Words = new List<string>(words),
                Frequencies = frequencies,
                Topics = new List<List<string>>(datasetRepresentation.Topics)
            };
        }
    }
}