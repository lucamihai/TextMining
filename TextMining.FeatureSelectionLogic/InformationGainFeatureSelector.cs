using System;
using System.Collections.Generic;
using System.Linq;
using TextMining.Entities;
using TextMining.FeatureSelectionLogic.Interfaces;
using TextMining.Helpers;
using TextMining.Helpers.Extensions;

namespace TextMining.FeatureSelectionLogic
{
    public class InformationGainFeatureSelector : IFeatureSelector
    {
        public List<string> GetMostImportantWords(DatasetRepresentation datasetRepresentation)
        {
            ArgumentValidator.ValidateObject(datasetRepresentation);
            ArgumentValidator.ValidateNotEmptyList(datasetRepresentation.Words);

            var datasetEntropy = GetDatasetEntropy(datasetRepresentation);
            var attributeAndInformationGainPairs = new Dictionary<string, double>();
            double datasetDocumentCount = datasetRepresentation.DocumentTopics.Count;

            for (var attributeIndex = 0; attributeIndex < datasetRepresentation.Words.Count; attributeIndex++)
            {
                var possibleValues = GetPossibleValuesOfAttributeInDataset(datasetRepresentation, attributeIndex);
                var sum = 0d;

                foreach (var possibleValue in possibleValues)
                {
                    var subset = datasetRepresentation.ReconstructByKeepingOnlyTheseFrequencies(new List<int> {possibleValue}, attributeIndex);
                    sum += (subset.DocumentTopics.Count / datasetDocumentCount) * GetDatasetEntropy(subset);
                }

                var attribute = datasetRepresentation.Words[attributeIndex];
                var informationGain = datasetEntropy - sum;
                attributeAndInformationGainPairs.Add(attribute, informationGain);
            }

            return GetTopAttributes(attributeAndInformationGainPairs);
        }

        private double GetDatasetEntropy(DatasetRepresentation datasetRepresentation)
        {
            var possibleTopics = datasetRepresentation.GetAllDistinctTopics();
            var sum = 0d;
            double documentCount = datasetRepresentation.DocumentTopics.Count;

            foreach (var topic in possibleTopics)
            {
                var documentsWithGivenTopic = datasetRepresentation.DocumentTopics.Count(x => x.Contains(topic));
                var probability = documentsWithGivenTopic / documentCount;
                sum += probability * Math.Log2(probability);
            }

            return -sum;
        }

        private List<int> GetPossibleValuesOfAttributeInDataset(DatasetRepresentation datasetRepresentation, int attributeIndex)
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

        private List<string> GetTopAttributes(Dictionary<string, double> attributeAndInformationGainPairs, int topPercentage = 10)
        {
            var entryCount = attributeAndInformationGainPairs.Count;
            var topEntryCount = entryCount * topPercentage / 100;

            return attributeAndInformationGainPairs
                .OrderByDescending(x => x.Value)
                .Take(topEntryCount)
                .Select(x => x.Key)
                .ToList();
        }
    }
}