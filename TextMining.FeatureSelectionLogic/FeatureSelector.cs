using System.Collections.Generic;
using TextMining.Entities;
using TextMining.FeatureSelectionLogic.Interfaces;
using TextMining.Helpers;

namespace TextMining.FeatureSelectionLogic
{
    public class FeatureSelector : IFeatureSelector
    {
        public List<string> GetMostImportantWords(DatasetRepresentation datasetRepresentation)
        {
            ArgumentValidator.ValidateObject(datasetRepresentation);
            ArgumentValidator.ValidateNotEmptyList(datasetRepresentation.Words);

            // TODO: Implement feature selection here
            var features = new List<string>();
            features.Add(datasetRepresentation.Words[0]);

            return features;
        }
    }
}