using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.DiscoveryLogic;
using TextMining.EvaluationLogic.Interfaces;
using TextMining.FeatureSelectionLogic.Interfaces;
using TextMining.Helpers.Extensions;

namespace TextMining
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private const string smallSampleDirectoryPath = @"D:\Projects\TextMining\Resources\Reuters_34\Training\";
        private const string bigSampleDirectoryPath = @"D:\Projects\TextMining\Resources\Reuters_7083\Training\";
        private static IDocumentDataBusinessLogic documentDataBusinessLogic;
        private static ITopicPredictorEvaluator topicPredictorEvaluator;

        private static void Main(string[] args)
        {
            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            documentDataBusinessLogic = serviceProvider.GetService<IDocumentDataBusinessLogic>();
            topicPredictorEvaluator = serviceProvider.GetService<ITopicPredictorEvaluator>();

            var selectedDirectory = HandleDirectorySelection();
            var selectedRunCount = HandleRunCountSelection();
            var filePathsToUseForDocumentData = new List<string>();
            filePathsToUseForDocumentData.AddRange(Directory.GetFiles(selectedDirectory));
            var accuracies = new ConcurrentBag<double>();
            var documentDataList = documentDataBusinessLogic.GetDocumentDataForMultipleXmlFiles(filePathsToUseForDocumentData);

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
            ConsoleWriteLineWithColor("Select which set should be used: ");
            ConsoleWriteLineWithColor($"1. {smallSampleDirectoryPath}");
            ConsoleWriteLineWithColor($"2. {bigSampleDirectoryPath}");

            while (true)
            {
                var userInput = Console.ReadLine();

                if (Convert.ToInt32(userInput) == 1)
                {
                    return smallSampleDirectoryPath;
                }
                else if (Convert.ToInt32(userInput) == 2)
                {
                    return bigSampleDirectoryPath;
                }

                ConsoleWriteLineWithColor("Invalid input. Must provide either 1 or 2.", ConsoleColor.DarkRed);
            }
        }

        private static int HandleRunCountSelection()
        {
            ConsoleWriteLineWithColor("How many times should the task run?");

            while (true)
            {
                var userInput = Console.ReadLine();

                if (int.TryParse(userInput, out var count) && count >= 1)
                {
                    return count;
                }

                ConsoleWriteLineWithColor("Invalid input. Must provide a number greater or equal than 1.", ConsoleColor.DarkRed);
            }
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
