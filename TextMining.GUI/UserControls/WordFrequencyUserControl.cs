using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.Services.Interfaces;

namespace TextMining.GUI.UserControls
{
    [ExcludeFromCodeCoverage]
    public partial class WordFrequencyUserControl : UserControl
    {
        private readonly ITextMiningBusinessLogic textMiningBusinessLogic;
        private readonly IResultFormatter resultFormatter;

        public WordFrequencyUserControl()
        {
            InitializeComponent();

            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            textMiningBusinessLogic = serviceProvider.GetService<ITextMiningBusinessLogic>();
            resultFormatter = serviceProvider.GetService<IResultFormatter>();

            UpdateRunButtonEnabledProperty();
            SetStatusLabel("Waiting for input", Color.DodgerBlue);
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Browse files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xml",
                Filter = "XML files (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFilepath.Text = openFileDialog.FileName;
                SetStatusLabel("File loaded", Color.DodgerBlue);
            }

            UpdateRunButtonEnabledProperty();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                var documentData = textMiningBusinessLogic.GetDocumentDataFromXmlFile(textBoxFilepath.Text);

                SetDataGridViewForDictionary(dataGridViewWordDictionary, documentData.TextData.WordDictionary);
                SetDataGridViewForDictionary(dataGridViewAcronyms, documentData.TextData.AcronymDictionary);

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
            buttonRun.Enabled = !string.IsNullOrWhiteSpace(textBoxFilepath.Text) && File.Exists(textBoxFilepath.Text);
        }

        private static void SetDataGridViewForDictionary(DataGridView dataGridView, Dictionary<string, int> wordDictionary)
        {
            dataGridView.Rows.Clear();

            foreach (var pair in wordDictionary)
            {
                dataGridView.Rows.Add(pair.Key, pair.Value);
            }
        }

        private void SetStatusLabel(string text, Color color)
        {
            labelStatus.Text = text;
            labelStatus.ForeColor = color;
        }
    }
}
