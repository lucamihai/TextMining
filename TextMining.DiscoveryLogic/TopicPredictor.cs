using System.Collections.Generic;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.Entities;
using TextMining.Helpers;

namespace TextMining.DiscoveryLogic
{
    public class TopicPredictor : ITopicPredictor
    {
        public void Train(DatasetRepresentation datasetRepresentation)
        {
            ArgumentValidator.ValidateObject(datasetRepresentation);

            throw new System.NotImplementedException();
        }

        public string PredictTopic(List<string> words)
        {
            ArgumentValidator.ValidateNotEmptyList(words);

            throw new System.NotImplementedException();
        }
    }
}