using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using TextMining.Entities;

namespace TextMining.GUI.UserControls
{
    [ExcludeFromCodeCoverage]
    public partial class DocumentDataDisplayUserControl : UserControl
    {
        public DocumentDataDisplayUserControl()
        {
            InitializeComponent();
        }

        public void DisplayDocumentData(DocumentData documentData)
        {
            UpdateWordsAndAcronymsDataLabels(documentData);
            SetDataGridViewForDictionary(dataGridViewWordDictionary, documentData.TextData.WordDictionary);
            SetDataGridViewForDictionary(dataGridViewAcronymDictionary, documentData.TextData.AcronymDictionary);
        }

        private void UpdateWordsAndAcronymsDataLabels(DocumentData documentData)
        {
            var totalWordsCount = documentData.TextData.GetTotalWordCount();
            var uniqueWordsCount = documentData.TextData.GetUniqueWordCount();
            labelWordsResult.Text = $"Total words: {totalWordsCount}, unique words: {uniqueWordsCount}";

            var totalAcronymsCount = documentData.TextData.GetTotalAcronymCount();
            var uniqueAcronymsCount = documentData.TextData.GetUniqueAcronymCount();
            labelAcronymsResult.Text = $"Total acronyms: {totalAcronymsCount}, unique acronyms: {uniqueAcronymsCount}";
        }

        private static void SetDataGridViewForDictionary(DataGridView dataGridView, Dictionary<string, int> wordDictionary)
        {
            dataGridView.Rows.Clear();

            foreach (var pair in wordDictionary)
            {
                dataGridView.Rows.Add(pair.Key, pair.Value);
            }
        }
    }
}
