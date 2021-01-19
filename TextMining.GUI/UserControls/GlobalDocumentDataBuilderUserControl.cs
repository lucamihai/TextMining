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
                var lastDocument = documentDataList[2];
                documentDataList.Remove(lastDocument);

                var datasetRepresentation = documentDataList.ToDatasetRepresentation();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var features = featureSelector.GetMostImportantWords(datasetRepresentation);
                stopwatch.Stop();
                var featuresJson = JsonConvert.SerializeObject(features);
                File.WriteAllText("features.json", featuresJson);
                
                datasetRepresentation = datasetRepresentation.ReconstructByKeepingOnlyTheseWords(features);
                var datasetJson = JsonConvert.SerializeObject(datasetRepresentation);
                var datasetArff = datasetRepresentation.ToArffFileFormat();
                File.WriteAllText("dataset.json", datasetJson);
                File.WriteAllText("dataset.arff", datasetArff);
                topicPredictor.Train(datasetRepresentation);
                topicPredictor.PredictTopic(lastDocument);

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
    }
}
