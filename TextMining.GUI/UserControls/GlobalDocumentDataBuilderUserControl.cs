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
    public partial class GlobalDocumentDataBuilderUserControl : UserControl
    {
        private readonly ITextMiningBusinessLogic textMiningBusinessLogic;
        private readonly IResultFormatter resultFormatter;
        private readonly DocumentDataDisplayUserControl documentDataDisplayUserControl;

        private readonly List<string> filepathsToUseForDocumentData;

        public GlobalDocumentDataBuilderUserControl()
        {
            InitializeComponent();

            var serviceProvider = DependencyResolver.GetServices().BuildServiceProvider();
            textMiningBusinessLogic = serviceProvider.GetService<ITextMiningBusinessLogic>();
            resultFormatter = serviceProvider.GetService<IResultFormatter>();

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
                var documentData = textMiningBusinessLogic.GetDocumentDataForMultipleXmlFiles(filepathsToUseForDocumentData);

                documentDataDisplayUserControl.DisplayDocumentData(documentData);

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
