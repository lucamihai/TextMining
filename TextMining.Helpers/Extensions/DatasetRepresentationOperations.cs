using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using TextMining.Entities;

namespace TextMining.Helpers.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DatasetRepresentationOperations
    {
        public static List<int> GetPossibleValuesOfAttributeInDataset(this DatasetRepresentation datasetRepresentation, int attributeIndex)
        {
            var possibleValues = new List<int>();

            for (int documentIndex = 0; documentIndex < datasetRepresentation.DocumentWordFrequencies.Count; documentIndex++)
            {
                var value = datasetRepresentation.GetDocumentWordFrequency(documentIndex, attributeIndex);
                if (!possibleValues.Contains(value))
                {
                    possibleValues.Add(value);
                }
            }

            return possibleValues
                .OrderBy(x => x)
                .ToList();
        }

        public static DatasetRepresentation ReconstructByEliminatingWordsBelowAndAboveThresholds(
            this DatasetRepresentation datasetRepresentation,
            int lowerThresholdPercentage,
            int upperThresholdPercentage)
        {
            var allWords = new List<string>(datasetRepresentation.Words);
            var wordsToKeep = new List<string>();
            double documentCount = datasetRepresentation.DocumentTopicsLists.Count;

            for (var wordIndex = 0; wordIndex < allWords.Count; wordIndex++)
            {
                var word = allWords[wordIndex];
                var documentsInWhichWordAppears = 0;

                for (int documentIndex = 0; documentIndex < documentCount; documentIndex++)
                {
                    if (datasetRepresentation.GetDocumentWordFrequency(documentIndex, wordIndex) > 0)
                    {
                        documentsInWhichWordAppears++;
                    }
                }

                var apparitionPercentage = documentsInWhichWordAppears * 100 / documentCount;

                if (apparitionPercentage > lowerThresholdPercentage && apparitionPercentage < upperThresholdPercentage)
                {
                    wordsToKeep.Add(word);
                }
            }

            return datasetRepresentation.ReconstructByKeepingOnlyTheseWords(wordsToKeep);
        }

        public static DatasetRepresentation ReconstructByKeepingOnlyTheseWords(this DatasetRepresentation datasetRepresentation, List<string> wordsToKeep)
        {
            var documentCount = datasetRepresentation.DocumentWordFrequencies.Count;
            var documentWordFrequencies = new List<Dictionary<int, int>>();

            for (int documentIndex = 0; documentIndex < documentCount; documentIndex++)
            {
                var frequency = new Dictionary<int, int>();

                for (int wordIndex = 0; wordIndex < wordsToKeep.Count; wordIndex++)
                {
                    var indexOfWordInOldRepresentation = datasetRepresentation.Words.IndexOf(wordsToKeep[wordIndex]);
                    var oldWordFrequency = datasetRepresentation.GetDocumentWordFrequency(documentIndex, indexOfWordInOldRepresentation);

                    if (oldWordFrequency > 0)
                    {
                        frequency.Add(wordIndex, oldWordFrequency);
                    }
                }

                documentWordFrequencies.Add(frequency);
            }

            return new DatasetRepresentation
            {
                Words = new List<string>(wordsToKeep),
                DocumentWordFrequencies = documentWordFrequencies,
                DocumentTopicsLists = new List<List<string>>(datasetRepresentation.DocumentTopicsLists)
            };
        }

        public static DatasetRepresentation ReconstructByKeepingOnlyTheseFrequencies(
            this DatasetRepresentation datasetRepresentation,
            List<int> possibleFrequencyValues,
            int wordIndex)
        {
            var indexesOfDocumentsWithGivenFrequencyValues = new List<int>();

            for (int documentIndex = 0; documentIndex < datasetRepresentation.DocumentWordFrequencies.Count; documentIndex++)
            {
                if (possibleFrequencyValues.Contains(datasetRepresentation.GetDocumentWordFrequency(documentIndex, wordIndex)))
                {
                    indexesOfDocumentsWithGivenFrequencyValues.Add(documentIndex);
                    break;
                }
            }

            var newDocumentWordFrequencies = new List<Dictionary<int, int>>();
            foreach (var oldDocumentIndex in indexesOfDocumentsWithGivenFrequencyValues)
            {
                newDocumentWordFrequencies.Add(datasetRepresentation.DocumentWordFrequencies[oldDocumentIndex]);
            }

            var topics = new List<List<string>>();
            for (var oldDocumentIndex = 0; oldDocumentIndex < datasetRepresentation.DocumentTopicsLists.Count; oldDocumentIndex++)
            {
                if (indexesOfDocumentsWithGivenFrequencyValues.Contains(oldDocumentIndex))
                {
                    topics.Add(datasetRepresentation.DocumentTopicsLists[oldDocumentIndex]);
                }
            }

            return new DatasetRepresentation
            {
                Words = datasetRepresentation.Words,
                DocumentWordFrequencies = newDocumentWordFrequencies,
                DocumentTopicsLists = topics
            };
        }

        public static string ToArffFileFormat(this DatasetRepresentation datasetRepresentation)
        {
            var stringBuilder = new StringBuilder();
            var topics = datasetRepresentation.GetAllDistinctTopics();

            foreach (var attribute in datasetRepresentation.Words)
            {
                stringBuilder.AppendLine($"@attribute {attribute} NUMERIC");
            }

            stringBuilder.AppendLine();
            var formattedTopics = topics
                .Select(x => $"'{x}'")
                .ToList();
            stringBuilder.AppendLine(string.Join(", ", formattedTopics));
            stringBuilder.AppendLine();

            foreach (var topic in topics)
            {
                stringBuilder.AppendLine($"@topics {topic}");
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("@data");

            for (var documentIndex = 0; documentIndex < datasetRepresentation.DocumentWordFrequencies.Count; documentIndex++)
            {
                if (datasetRepresentation.DocumentTopicsLists[documentIndex].Count == 0)
                {
                    continue;
                }

                var datasetRepresentationDocumentWordFrequency = datasetRepresentation.DocumentWordFrequencies[documentIndex];
                var formattedPairs = datasetRepresentationDocumentWordFrequency
                    .Select(x => $"{x.Key}:{x.Value}")
                    .ToList();
                var pairsString = string.Join(',', formattedPairs);

                for (int topicIndex = 0; topicIndex < datasetRepresentation.DocumentTopicsLists[documentIndex].Count; topicIndex++)
                {
                    stringBuilder.AppendLine($"{pairsString} # {datasetRepresentation.DocumentTopicsLists[documentIndex][topicIndex]}");
                }
            }

            return stringBuilder.ToString();
        }
    }
}