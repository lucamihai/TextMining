using System;
using System.Collections.Generic;
using System.Linq;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.Entities;
using TextMining.EvaluationLogic.Interfaces;
using TextMining.Helpers;
using TextMining.Helpers.Extensions;

namespace TextMining.EvaluationLogic
{
    public class TopicPredictorEvaluator : ITopicPredictorEvaluator
    {
        [Obsolete("Needs result accuracy improvements")]
        public List<ClassEvaluationResult> EvaluateTopicPredictor(ITopicPredictor topicPredictor, List<DocumentData> documentDataList)
        {
            ArgumentValidator.ValidateObject(topicPredictor);
            ArgumentValidator.ValidateNotEmptyList(documentDataList);

            var evaluationResults = new Dictionary<string, ClassEvaluationResult>();
            double total = documentDataList.Count;
            var successfullyPredicted = 0;

            var distinctTopics = documentDataList.ToDatasetRepresentation().GetAllDistinctTopics();
            foreach (var topic in distinctTopics)
            {
                evaluationResults.Add(topic, new ClassEvaluationResult());
            }

            var predicted = new int[documentDataList.Count(x => x.Topics.Count > 0)];
            var expected = new int[documentDataList.Count(x => x.Topics.Count > 0)];

            foreach (var documentData in documentDataList.Where(x => x.Topics.Count > 0))
            {
                var predictedTopic = topicPredictor.PredictTopic(documentData);
                var expectedTopic = documentData.Topics[0];

                evaluationResults.TryGetValue(predictedTopic, out var resultForPredictedTopic);
                evaluationResults.TryGetValue(expectedTopic, out var resultForExpectedTopic);

                if (resultForPredictedTopic == null && resultForExpectedTopic == null)
                {
                    continue;
                }

                if (predictedTopic != expectedTopic)
                {
                    if (resultForPredictedTopic != null)
                    {
                        resultForPredictedTopic.FalsePositives++;
                    }

                    if (resultForExpectedTopic != null)
                    {
                        resultForExpectedTopic.FalseNegatives++;
                    }
                }
                else
                {
                    if (resultForPredictedTopic != null)
                    {
                        resultForPredictedTopic.TruePositives++;
                    }
                }

                var remainingTopics = distinctTopics
                    .Where(x => x != predictedTopic && x != expectedTopic)
                    .ToList();

                foreach (var remainingTopic in remainingTopics)
                {
                    if (evaluationResults.TryGetValue(remainingTopic, out var resultForRemainingTopic))
                    {
                        resultForRemainingTopic.TrueNegatives++;
                    }
                }
            }

            return evaluationResults.Values.ToList();
        }
    }
}