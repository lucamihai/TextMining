using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.DiscoveryLogic.Interfaces;
using TextMining.FeatureSelectionLogic.Interfaces;
using TextMining.Helpers.Extensions;

namespace TextMining.GUI.UserControls
{
    [ExcludeFromCodeCoverage]
    public partial class GlobalDocumentDataBuilderUserControl : UserControl
    {
        private readonly IDocumentDataBusinessLogic documentDataBusinessLogic;
        private readonly IFeatureSelector featureSelector;
        private readonly ITopicPredictor topicPredictor;
        private readonly DocumentDataDisplayUserControl documentDataDisplayUserControl;

        private readonly List<string> filepathsToUseForDocumentData;

        public GlobalDocumentDataBuilderUserControl()
        {
            InitializeComponent();

            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            documentDataBusinessLogic = serviceProvider.GetService<IDocumentDataBusinessLogic>();
            featureSelector = serviceProvider.GetService<IFeatureSelector>();
            topicPredictor = serviceProvider.GetService<ITopicPredictor>();

            documentDataDisplayUserControl = new DocumentDataDisplayUserControl();
            panelDocumentDataDisplayUserControl.Controls.Add(documentDataDisplayUserControl);

            UpdateRunButtonEnabledProperty();
            SetStatusLabel("Waiting for input", Color.DodgerBlue);

            filepathsToUseForDocumentData = new List<string>();
        }

        private void buttonLoadFiles_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Browse files",

                CheckFileExists = true,
                CheckPathExists = true,

                Multiselect = true,
                DefaultExt = "xml",
                Filter = "XML files (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepathsToUseForDocumentData.Clear();
                filepathsToUseForDocumentData.AddRange(openFileDialog.FileNames);

                SetStatusLabel("Files loaded", Color.DodgerBlue);
            }

            UpdateRunButtonEnabledProperty();
        }

        private void buttonSelectDirectory_Click(object sender, EventArgs e)
        {
            using var folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSelectedDirectory.Text = folderBrowserDialog.SelectedPath;

                filepathsToUseForDocumentData.Clear();
                filepathsToUseForDocumentData.AddRange(Directory.GetFiles(folderBrowserDialog.SelectedPath));

                SetStatusLabel("Files from directory loaded", Color.DodgerBlue);
            }

            UpdateRunButtonEnabledProperty();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                var documentDataList = documentDataBusinessLogic.GetDocumentDataForMultipleXmlFiles(filepathsToUseForDocumentData);
                var lists = SplitListIntoTwoSeparateLists(documentDataList, 70);
                var listForTraining = lists.Item1;
                var listForValidation = lists.Item2;

                var datasetRepresentationTraining = documentDataList.ToDatasetRepresentation();
                datasetRepresentationTraining = datasetRepresentationTraining.ReconstructByEliminatingWordsBelowAndAboveThresholds(5, 95);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var features = featureSelector.GetMostImportantWords(datasetRepresentationTraining);
                stopwatch.Stop();

                var featuresJson = JsonConvert.SerializeObject(features);
                File.WriteAllText("features.json", featuresJson);
                
                datasetRepresentationTraining = datasetRepresentationTraining.ReconstructByKeepingOnlyTheseWords(features);
                var datasetJson = JsonConvert.SerializeObject(datasetRepresentationTraining);
                var datasetArff = datasetRepresentationTraining.ToArffFileFormat();
                File.WriteAllText("dataset.json", datasetJson);
                File.WriteAllText("dataset.arff", datasetArff);

                topicPredictor.Train(datasetRepresentationTraining);

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

                //documentDataDisplayUserControl.DisplayDocumentData(documentData);
                SetStatusLabel("Done", Color.GreenYellow);
            }
            catch (Exception exception)
            {
                SetStatusLabel("Error", Color.Red);
                //textBoxResult.Text = exception.ToString();
            }
        }

        private void UpdateRunButtonEnabledProperty()
        {
            buttonRun.Enabled = filepathsToUseForDocumentData != null && filepathsToUseForDocumentData.Count > 0;
        }

        private void SetStatusLabel(string text, Color color)
        {
            labelStatus.Text = text;
            labelStatus.ForeColor = color;
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
