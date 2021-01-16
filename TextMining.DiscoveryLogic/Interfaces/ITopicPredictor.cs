﻿using System.Collections.Generic;
using TextMining.Entities;

namespace TextMining.DiscoveryLogic.Interfaces
{
    public interface ITopicPredictor
    {
        void Train(DatasetRepresentation datasetRepresentation);
        string PredictTopic(List<string> words);
    }
}