using System.Linq;
using Accord.MachineLearning;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.Entities;
using TextMining.Helpers;

namespace TextMining.DiscoveryLogic
{
    public class KNearestNeighborsTopicPredictor : ITopicPredictor
    {
        private KNearestNeighbors kNearestNeighbors;
        private DatasetRepresentation datasetUsedForTraining;

        public void Train(DatasetRepresentation datasetRepresentation)
        {
            ArgumentValidator.ValidateObject(datasetRepresentation);

            datasetUsedForTraining = datasetRepresentation;
            var inputsOutputsPair = GetInputsAndOutputsForDataset(datasetRepresentation);
            var k = 61;
            kNearestNeighbors = new KNearestNeighbors(k);
            kNearestNeighbors.Learn(inputsOutputsPair.Inputs, inputsOutputsPair.Outputs);
        }

        public string PredictTopic(DocumentData documentData)
        {
            ArgumentValidator.ValidateObject(documentData);

            var inputs = new double[datasetUsedForTraining.Words.Count];

            for (int attributeIndex = 0; attributeIndex < datasetUsedForTraining.Words.Count; attributeIndex++)
            {
                var attribute = datasetUsedForTraining.Words[attributeIndex];

                if (documentData.TextData.WordDictionary.TryGetValue(attribute, out var frequency))
                {
                    inputs[attributeIndex] = frequency;
                }
                else
                {
                    inputs[attributeIndex] = 0;
                }
            }

            var predictedTopicIndex = kNearestNeighbors.Decide(inputs);
            var predictedTopic = datasetUsedForTraining.GetAllDistinctTopics()[predictedTopicIndex];

            return predictedTopic;
        }

        private InputsOutputsPair GetInputsAndOutputsForDataset(DatasetRepresentation datasetRepresentation)
        {
            var rowCount = datasetRepresentation
                .DocumentTopicsLists
                .Select(x => x.Count)
                .Sum();

            var columnCount = datasetRepresentation.Words.Count;

            var inputs = new double[rowCount][];
            var outputs = new int[rowCount];
            var currentRowNumber = 0;

            for (int documentIndex = 0; documentIndex < datasetRepresentation.DocumentWordFrequencies.Count; documentIndex++)
            {
                var documentFrequencies = datasetRepresentation.DocumentWordFrequencies[documentIndex];
                var documentTopics = datasetRepresentation.DocumentTopicsLists[documentIndex];

                var inputRow = new double[columnCount];
                for (var attributeIndex = 0; attributeIndex < columnCount; attributeIndex++)
                {
                    inputRow[attributeIndex] = datasetRepresentation.GetDocumentWordFrequency(documentIndex, attributeIndex);
                }

                for (var topicIndex = 0; topicIndex < documentTopics.Count; topicIndex++)
                {
                    inputs[currentRowNumber] = inputRow;
                    outputs[currentRowNumber] = topicIndex;
                    currentRowNumber++;
                }
            }


            return new InputsOutputsPair
            {
                Inputs = inputs,
                Outputs = outputs
            };
        }

        private class InputsOutputsPair
        {
            public double[][] Inputs { get; set; }
            public int[] Outputs { get; set; }
        }
    }
}