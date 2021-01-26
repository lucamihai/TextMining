using System;
using System.Collections.Generic;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.Entities;

namespace TextMining.EvaluationLogic.Interfaces
{
    public interface ITopicPredictorEvaluator
    {
        [Obsolete("Needs result accuracy improvements")]
        List<ClassEvaluationResult> EvaluateTopicPredictor(ITopicPredictor topicPredictor, List<DocumentData> documentDataList);
    }
}