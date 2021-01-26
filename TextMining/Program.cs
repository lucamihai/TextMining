using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.DiscoveryLogic;
using TextMining.Entities;
using TextMining.EvaluationLogic.Interfaces;
using TextMining.FeatureSelectionLogic.Interfaces;
using TextMining.Helpers;
using TextMining.Helpers.Extensions;

namespace TextMining
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static IDocumentDataBusinessLogic documentDataBusinessLogic;
        private static ITopicPredictorEvaluator topicPredictorEvaluator;

        private static void Main(string[] args)
        {
            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            documentDataBusinessLogic = serviceProvider.GetService<IDocumentDataBusinessLogic>();
            topicPredictorEvaluator = serviceProvider.GetService<ITopicPredictorEvaluator>();

            var selectedDirectory = HandleDirectorySelection();
            var selectedRunCount = HandleRunCountSelection();
            var documentDataList = HandleDocumentDataListProcessing(selectedDirectory);
            var accuracies = new ConcurrentBag<double>();

            Parallel.For(0, selectedRunCount, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (runNumber) =>
            {
                var featureSelector = serviceProvider.GetService<IFeatureSelector>();
                var topicPredictor = new KNearestNeighborsTopicPredictor();

                var lists = SplitListIntoTwoSeparateLists(documentDataList, 70);
                var listForTraining = lists.Item1;
                var listForValidation = lists.Item2;

                var datasetRepresentationTraining = listForTraining.ToDatasetRepresentation();
                datasetRepresentationTraining = datasetRepresentationTraining.ReconstructByEliminatingWordsBelowAndAboveThresholds(5, 95);

                var features = featureSelector.GetMostImportantWords(datasetRepresentationTraining);
                datasetRepresentationTraining = datasetRepresentationTraining.ReconstructByKeepingOnlyTheseWords(features);

                topicPredictor.Train(datasetRepresentationTraining);
                var evaluationResults = topicPredictorEvaluator.EvaluateTopicPredictor(topicPredictor, listForValidation);

                double total = listForValidation.Count;
                var successfullyPredicted = 0;

                foreach (var documentData in listForValidation)
                {
                    var predictedTopic = topicPredictor.PredictTopic(documentData);

                    if (documentData.Topics.Contains(predictedTopic))
                    {
                        successfullyPredicted++;
                    }
                }

                var accuracy = successfullyPredicted / total * 100;
                accuracies.Add(accuracy);
                ConsoleWriteLineWithColor($"Accuracy for run {runNumber}: {accuracy}");
            });

            ConsoleWriteLineWithColor("----------------------------------------------------------");
            ConsoleWriteLineWithColor($"Average accuracy = {accuracies.Sum() / accuracies.Count}");
        }

        private static string HandleDirectorySelection()
        {
            ConsoleWriteLineWithColor("Enter the directory containing the XML articles");

            return UserInputHandler.GetPathInputFromUser();
        }

        private static int HandleRunCountSelection()
        {
            ConsoleWriteLineWithColor("How many times should the task run?");

            return UserInputHandler.GetNumberInputFromUser();
        }

        private static List<DocumentData> HandleDocumentDataListProcessing(string selectedDirectory)
        {
            var processedDocumentDataListJsonPath = Path.Combine(selectedDirectory, "document_data_list.json");
            var processedDocumentDataListJsonFileExists = File.Exists(processedDocumentDataListJsonPath);

            if (processedDocumentDataListJsonFileExists)
            {
                ConsoleWriteLineWithColor($"Found processed documents file at '{processedDocumentDataListJsonPath}'");
                ConsoleWriteLineWithColor("Would you like to use that in order to skip the processing step?");
                ConsoleWriteLineWithColor("1. Yes");
                ConsoleWriteLineWithColor("2. No");

                var selectedOption = UserInputHandler.GetNumberInputFromUser(new List<int> {1, 2});
                if (selectedOption == 1)
                {
                    var processedDocumentDataListJson = File.ReadAllText(processedDocumentDataListJsonPath);
                    var processedDocumentDataList = JsonConvert.DeserializeObject<List<DocumentData>>(processedDocumentDataListJson);

                    return processedDocumentDataList;
                }
            }

            var filePathsToUseForDocumentData = new List<string>();
            filePathsToUseForDocumentData.AddRange(Directory.GetFiles(selectedDirectory));
            var documentDataList = documentDataBusinessLogic.GetDocumentDataForMultipleXmlFiles(filePathsToUseForDocumentData);

            if (processedDocumentDataListJsonFileExists)
            {
                ConsoleWriteLineWithColor("Overwrite existing processed documents file?");
                ConsoleWriteLineWithColor("1. Yes");
                ConsoleWriteLineWithColor("2. No");

                var selectedOption = UserInputHandler.GetNumberInputFromUser(new List<int> { 1, 2 });
                if (selectedOption == 1)
                {
                    var documentDataListJson = JsonConvert.SerializeObject(documentDataList);
                    File.WriteAllText(processedDocumentDataListJsonPath, documentDataListJson);
                }
            }
            else
            {
                var documentDataListJson = JsonConvert.SerializeObject(documentDataList);
                File.WriteAllText(processedDocumentDataListJsonPath, documentDataListJson);
            }

            return documentDataList;
        }

        private static void ConsoleWriteLineWithColor(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            var oldForeGroundColor = Console.ForegroundColor;

            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);

            Console.ForegroundColor = oldForeGroundColor;
        }

        private static Tuple<List<T>, List<T>> SplitListIntoTwoSeparateLists<T>(List<T> list, int firstListPercentage)
        {
            var firstListItemCount = list.Count * firstListPercentage / 100;
            var firstListIndexes = GenerateRandomNumbersInRange(firstListItemCount, 0, list.Count - 1);

            var firstList = new List<T>();
            var secondList = new List<T>();

            for (int currentIndex = 0; currentIndex < list.Count; currentIndex++)
            {
                var currentItem = list[currentIndex];

                if (firstListIndexes.Contains(currentIndex))
                {
                    firstList.Add(currentItem);
                }
                else
                {
                    secondList.Add(currentItem);
                }
            }

            return new Tuple<List<T>, List<T>>(firstList, secondList);
        }

        private static List<int> GenerateRandomNumbersInRange(int desiredCount, int lowestPossibleNumber, int highestPossibleNumber)
        {
            var rng = new Random();
            var numbers = new List<int>();

            while (numbers.Count < desiredCount)
            {
                var generatedNumber = rng.Next(lowestPossibleNumber, highestPossibleNumber);

                if (!numbers.Contains(generatedNumber))
                {
                    numbers.Add(generatedNumber);
                }
            }

            return numbers;
        }
    }
}
