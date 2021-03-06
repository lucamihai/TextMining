﻿using System;
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
            double datasetDocumentCount = datasetRepresentation.DocumentTopicsLists.Count;

            for (var attributeIndex = 0; attributeIndex < datasetRepresentation.Words.Count; attributeIndex++)
            {
                var possibleValues = datasetRepresentation.GetPossibleValuesOfAttributeInDataset(attributeIndex);
                var sum = 0d;

                foreach (var possibleValue in possibleValues)
                {
                    var subset = datasetRepresentation.ReconstructByKeepingOnlyTheseFrequencies(new List<int> {possibleValue}, attributeIndex);
                    sum += (subset.DocumentTopicsLists.Count / datasetDocumentCount) * GetDatasetEntropy(subset);
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
            double documentCount = datasetRepresentation.DocumentTopicsLists.Count;

            foreach (var topic in possibleTopics)
            {
                var documentsWithGivenTopic = datasetRepresentation.DocumentTopicsLists.Count(x => x.Contains(topic));
                var probability = documentsWithGivenTopic / documentCount;
                sum += probability * Math.Log2(probability);
            }

            return -sum;
        }

        private List<string> GetTopAttributes(Dictionary<string, double> attributeAndInformationGainPairs, int topPercentage = 10)
        {
            var entryCount = attributeAndInformationGainPairs.Count;
            var topEntryCount = entryCount * topPercentage / 100;
            var sorted = attributeAndInformationGainPairs
                .OrderByDescending(x => x.Value)
                .Select(x => new {Key = x.Key, Value = x.Value})
                .ToList();

            return attributeAndInformationGainPairs
                .OrderByDescending(x => x.Value)
                .Take(topEntryCount)
                .Select(x => x.Key)
                .ToList();
        }
    }
}