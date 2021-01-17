using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TextMining.Entities;
using TextMining.FeatureSelectionLogic.Interfaces;
using TextMining.Helpers;

namespace TextMining.FeatureSelectionLogic
{
    public class GiniIndexFeatureSelector : IFeatureSelector
    {
        public List<string> GetMostImportantWords(DatasetRepresentation datasetRepresentation)
        {
            ArgumentValidator.ValidateObject(datasetRepresentation);
            ArgumentValidator.ValidateNotEmptyList(datasetRepresentation.Words);

            var datasetImpurity = GetDatasetImpurity(datasetRepresentation);
            var giniIndexes = new List<double>();

            for (var attributeIndex = 0; attributeIndex < datasetRepresentation.Words.Count; attributeIndex++)
            {
                var giniIndex = GetGiniIndexForAttribute(datasetRepresentation, attributeIndex);
                giniIndexes.Add(giniIndex);
            }

            // TODO: Implement feature selection here
            var features = new List<string>();
            features.Add(datasetRepresentation.Words[0]);

            return features;
        }

        private double GetDatasetImpurity(DatasetRepresentation datasetRepresentation)
        {
            var documentCount = datasetRepresentation.DocumentWordFrequencies.Count;
            var allTopics = datasetRepresentation.GetAllDistinctTopics();
            var sum = 0d;

            foreach (var topic in allTopics)
            {
                var documentsWithTopic = 0d;
                foreach (var datasetRepresentationTopic in datasetRepresentation.DocumentTopics)
                {
                    if (datasetRepresentationTopic.Contains(topic))
                    {
                        documentsWithTopic++;
                    }
                }

                var documentProbability = Math.Pow(documentsWithTopic / documentCount, 2);
                sum += documentProbability;
            }

            return 1 - sum;
        }

        // TODO
        private double GetGiniIndexForAttribute(DatasetRepresentation datasetRepresentation, int attributeIndex)
        {
            var pairs = GetBinarySplitPairs(datasetRepresentation, attributeIndex);

            return 0;
        }

        private List<Tuple<int[], int[]>> GetBinarySplitPairs(DatasetRepresentation datasetRepresentation, int attributeIndex, bool debug = false)
        {
            //var data = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22};
            var possibleValues = new List<int>();

            for (int documentIndex = 0; documentIndex < datasetRepresentation.DocumentWordFrequencies.Count; documentIndex++)
            {
                var value = datasetRepresentation.DocumentWordFrequencies[documentIndex][attributeIndex];
                if (!possibleValues.Contains(value))
                {
                    possibleValues.Add(value);
                }
            }

            possibleValues = possibleValues.OrderBy(x => x).ToList();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var combinations = Enumerable
                .Range(0, 1 << (possibleValues.Count))
                .Select(index => possibleValues
                    .Where((v, i) => (index & (1 << i)) != 0)
                    .ToArray())
                .ToList();

            var pairs = new List<Tuple<int[], int[]>>();
            var maxIndex = combinations.Count / 2 - 1;
            for (int i = 1; i <= maxIndex; i++)
            {
                var otherIndex = combinations.Count - 1 - i;
                var pair = new Tuple<int[], int[]>(combinations[i], combinations[otherIndex]);
                pairs.Add(pair);
            }
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (debug)
            {
                var str = "";
                foreach (var pair in pairs)
                {
                    str += string.Join(", ", pair.Item1);
                    str += Environment.NewLine;
                    str += string.Join(", ", pair.Item2);
                    str += Environment.NewLine;
                    str += Environment.NewLine;
                }
            }

            return pairs;
        }
    }
}