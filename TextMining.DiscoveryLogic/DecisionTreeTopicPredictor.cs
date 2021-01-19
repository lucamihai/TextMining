using System.Collections.Generic;
using System.Data;
using System.Linq;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using Accord.Statistics.Filters;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.Entities;
using TextMining.Helpers;
using TextMining.Helpers.Extensions;

namespace TextMining.DiscoveryLogic
{
    public class DecisionTreeTopicPredictor : ITopicPredictor
    {
        private DecisionTree decisionTree;
        private Codification codeBook;
        private string[,] input;

        public void Train(DatasetRepresentation datasetRepresentation)
        {
            ArgumentValidator.ValidateObject(datasetRepresentation);

            var dataTable = GetDataTableForDataset(datasetRepresentation);
            codeBook = new Codification(dataTable);

            var symbols = codeBook.Apply(dataTable);
            var inputs = symbols.ToJagged<int>(datasetRepresentation.Words.Select(x => x).ToArray());
            var outputs = symbols.ToArray<int>("Topic");
            var id3LearningForDataset = GetId3LearningForDataset(datasetRepresentation);

            decisionTree = id3LearningForDataset.Learn(inputs, outputs);

            input = new string[datasetRepresentation.Words.Count, 2];
            for (var index = 0; index < datasetRepresentation.Words.Count; index++)
            {
                var datasetRepresentationWord = datasetRepresentation.Words[index];
                input[index, 0] = datasetRepresentationWord;
                input[index, 1] = "2";
            }
        }

        public string PredictTopic(DocumentData documentData)
        {
            ArgumentValidator.ValidateObject(documentData);

            var query = codeBook.Transform(input);

            decisionTree.Decide(query);
            throw new System.NotImplementedException();
        }

        private DataTable GetDataTableForDataset(DatasetRepresentation datasetRepresentation)
        {
            var dataTable = new DataTable("Text mining");

            var headerColumns = GetHeaderColumns(datasetRepresentation);
            dataTable.Columns.AddRange(headerColumns);

            for (int documentIndex = 0; documentIndex < datasetRepresentation.DocumentWordFrequencies.Count; documentIndex++)
            {
                var documentRows = GetRowsForDocument(datasetRepresentation, documentIndex);

                foreach (var documentRow in documentRows)
                {
                    dataTable.Rows.Add(documentRow);
                }
            }

            return dataTable;
        }

        private DataColumn[] GetHeaderColumns(DatasetRepresentation datasetRepresentation)
        {
            var headerColumns = datasetRepresentation
                .Words
                .Select(x => new DataColumn(x))
                .ToList();

            headerColumns.Add(new DataColumn("Topic"));

            return headerColumns.ToArray();
        }

        private List<string[]> GetRowsForDocument(DatasetRepresentation datasetRepresentation, int documentIndex)
        {
            var documentFrequencies = datasetRepresentation.DocumentWordFrequencies[documentIndex];
            var documentTopics = datasetRepresentation.DocumentTopicsLists[documentIndex];
            var rows = new List<string[]>();

            foreach (var documentTopic in documentTopics)
            {
                var values = new string[datasetRepresentation.Words.Count + 1];

                for (int attributeIndex = 0; attributeIndex < datasetRepresentation.Words.Count; attributeIndex++)
                {
                    values[attributeIndex] = datasetRepresentation.GetDocumentWordFrequency(documentIndex, attributeIndex).ToString();
                }

                values[^1] = documentTopic;
                rows.Add(values);
            }

            return rows;
        }

        private ID3Learning GetId3LearningForDataset(DatasetRepresentation datasetRepresentation)
        {
            var attributeCount = datasetRepresentation.Words.Count;
            var decisionVariables = new DecisionVariable[attributeCount];

            for (int attributeIndex = 0; attributeIndex < datasetRepresentation.Words.Count; attributeIndex++)
            {
                var attribute = datasetRepresentation.Words[attributeIndex];
                var possibleValues = datasetRepresentation.GetPossibleValuesOfAttributeInDataset(attributeIndex);
                var decisionVariable = new DecisionVariable(attribute, possibleValues.Count);

                decisionVariables[attributeIndex] = decisionVariable;
            }

            return new ID3Learning(decisionVariables);
        }
    }
}