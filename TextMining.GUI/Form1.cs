using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TextMining.BusinessLogic.Interfaces;
using TextMining.DI;
using TextMining.Services.Interfaces;

namespace TextMining.GUI
{
    [ExcludeFromCodeCoverage]
    public partial class Form1 : Form
    {
        private readonly ITextMiningBusinessLogic textMiningBusinessLogic;
        private readonly IResultFormatter resultFormatter;

        public Form1()
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
            var wordFrequencies = textMiningBusinessLogic.GetWordFrequenciesFromXmlFile(textBoxFilepath.Text);
            textBoxResult.Text = resultFormatter.GetStringForWordFrequencies(wordFrequencies);
            SetStatusLabel("Done", Color.GreenYellow);
        }

        private void UpdateRunButtonEnabledProperty()
        {
            buttonRun.Enabled = !string.IsNullOrWhiteSpace(textBoxFilepath.Text) && File.Exists(textBoxFilepath.Text);
        }

        private void SetStatusLabel(string text, Color color)
        {
            labelStatus.Text = text;
            labelStatus.ForeColor = color;
        }
    }
}
