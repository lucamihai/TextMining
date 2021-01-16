using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.FeatureSelectionLogic.Interfaces
{
    public interface IFeatureSelector
    {
        List<string> GetMostImportantWords(DatasetRepresentation datasetRepresentation);
    }
}