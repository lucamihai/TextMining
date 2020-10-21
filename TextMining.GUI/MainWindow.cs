using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using TextMining.GUI.UserControls;

namespace TextMining.GUI
{
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Form
    {
        private UserControl activeUserControl;
        private readonly List<RadioButton> radioButtonsUserControlSelection;

        public MainWindow()
        {
            InitializeComponent();

            activeUserControl = new UserControl();
            panelActiveUserControl.Controls.Add(activeUserControl);

            radioButtonsUserControlSelection = panelUserControlSelection
                .Controls
                .OfType<RadioButton>()
                .ToList();

            foreach (var radioButton in radioButtonsUserControlSelection)
            {
                radioButton.CheckedChanged += RadioButtonOnCheckedChanged;
            }

            radioButtonUserControlWordFrequency.Checked = true;
        }

        private void RadioButtonOnCheckedChanged(object? sender, EventArgs e)
        {
            var selectedRadioButton = radioButtonsUserControlSelection.First(x => x.Checked);

            if (selectedRadioButton == radioButtonUserControlWordFrequency)
            {
                activeUserControl = new DocumentDataExtractionUserControl();
            }

            if (selectedRadioButton == radioButtonUserControlGlobalDocumentDataBuilder)
            {
                activeUserControl = new GlobalDocumentDataBuilderUserControl();
            }

            UpdatePanelActiveUserControl();
        }

        private void UpdatePanelActiveUserControl()
        {
            panelActiveUserControl.Controls.Clear();
            panelActiveUserControl.Controls.Add(activeUserControl);
        }
    }
}
