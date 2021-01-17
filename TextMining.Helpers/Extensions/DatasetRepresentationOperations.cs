using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        public static DatasetRepresentation ReconstructByKeepingOnlyTheseFrequencies(this DatasetRepresentation datasetRepresentation, List<int> possibleFrequencyValues)
        {
            var indexesOfDocumentsWithGivenFrequencyValues = new List<int>();

            for (int documentIndex = 0; documentIndex < datasetRepresentation.Frequencies.GetLength(0); documentIndex++)
            {
                for (int wordIndex = 0; wordIndex < datasetRepresentation.Words.Count; wordIndex++)
                {
                    if (possibleFrequencyValues.Contains(datasetRepresentation.Frequencies[documentIndex, wordIndex]))
                    {
                        indexesOfDocumentsWithGivenFrequencyValues.Add(documentIndex);
                        break;
                    }
                }
            }

            var frequencies = new int[indexesOfDocumentsWithGivenFrequencyValues.Count, datasetRepresentation.Words.Count];
            for (int newDocumentIndex = 0; newDocumentIndex < indexesOfDocumentsWithGivenFrequencyValues.Count; newDocumentIndex++)
            {
                var oldDocumentIndex = indexesOfDocumentsWithGivenFrequencyValues[newDocumentIndex];
                for (int wordIndex = 0; wordIndex < datasetRepresentation.Words.Count; wordIndex++)
                {
                    var frequency = datasetRepresentation.Frequencies[oldDocumentIndex, wordIndex];
                    frequencies[newDocumentIndex, wordIndex] = frequency;
                }
            }

            var topics = new List<List<string>>();
            for (int topicIndex = 0; topicIndex < datasetRepresentation.Topics.Count; topicIndex++)
            {
                if (indexesOfDocumentsWithGivenFrequencyValues.Contains(topicIndex))
                {
                    topics.Add(datasetRepresentation.Topics[topicIndex]);
                }
            }

            return new DatasetRepresentation
            {
                Words = datasetRepresentation.Words,
                Frequencies = frequencies,
                Topics = topics
            };
        }
    }
}