namespace TextMining.GUI.UserControls
{
    partial class DocumentDataDisplayUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelWordsResult = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WordFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acronym = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcronymFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAcronymDictionary = new System.Windows.Forms.DataGridView();
            this.dataGridViewWordDictionary = new System.Windows.Forms.DataGridView();
            this.labelAcronymsResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAcronymDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWordDictionary)).BeginInit();
            this.SuspendLayout();
            // 
            // labelWordsResult
            // 
            this.labelWordsResult.AutoSize = true;
            this.labelWordsResult.Location = new System.Drawing.Point(16, 29);
            this.labelWordsResult.Name = "labelWordsResult";
            this.labelWordsResult.Size = new System.Drawing.Size(79, 15);
            this.labelWordsResult.TabIndex = 5;
            this.labelWordsResult.Text = "(words result)";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Word";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Frequency";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Acronym";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Frequency";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Word
            // 
            this.Word.HeaderText = "Word";
            this.Word.Name = "Word";
            this.Word.ReadOnly = true;
            this.Word.Width = 200;
            // 
            // WordFrequency
            // 
            this.WordFrequency.HeaderText = "Frequency";
            this.WordFrequency.Name = "WordFrequency";
            this.WordFrequency.ReadOnly = true;
            // 
            // Acronym
            // 
            this.Acronym.HeaderText = "Acronym";
            this.Acronym.Name = "Acronym";
            this.Acronym.ReadOnly = true;
            this.Acronym.Width = 200;
            // 
            // AcronymFrequency
            // 
            this.AcronymFrequency.HeaderText = "Frequency";
            this.AcronymFrequency.Name = "AcronymFrequency";
            this.AcronymFrequency.ReadOnly = true;
            // 
            // dataGridViewAcronymDictionary
            // 
            this.dataGridViewAcronymDictionary.AllowUserToAddRows = false;
            this.dataGridViewAcronymDictionary.AllowUserToDeleteRows = false;
            this.dataGridViewAcronymDictionary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAcronymDictionary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Acronym,
            this.AcronymFrequency});
            this.dataGridViewAcronymDictionary.Location = new System.Drawing.Point(386, 56);
            this.dataGridViewAcronymDictionary.Name = "dataGridViewAcronymDictionary";
            this.dataGridViewAcronymDictionary.ReadOnly = true;
            this.dataGridViewAcronymDictionary.Size = new System.Drawing.Size(344, 320);
            this.dataGridViewAcronymDictionary.TabIndex = 7;
            this.dataGridViewAcronymDictionary.Text = "dataGridView1";
            // 
            // dataGridViewWordDictionary
            // 
            this.dataGridViewWordDictionary.AllowUserToAddRows = false;
            this.dataGridViewWordDictionary.AllowUserToDeleteRows = false;
            this.dataGridViewWordDictionary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWordDictionary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Word,
            this.WordFrequency});
            this.dataGridViewWordDictionary.Location = new System.Drawing.Point(16, 56);
            this.dataGridViewWordDictionary.Name = "dataGridViewWordDictionary";
            this.dataGridViewWordDictionary.ReadOnly = true;
            this.dataGridViewWordDictionary.Size = new System.Drawing.Size(344, 320);
            this.dataGridViewWordDictionary.TabIndex = 7;
            this.dataGridViewWordDictionary.Text = "dataGridView1";
            // 
            // labelAcronymsResult
            // 
            this.labelAcronymsResult.AutoSize = true;
            this.labelAcronymsResult.Location = new System.Drawing.Point(386, 29);
            this.labelAcronymsResult.Name = "labelAcronymsResult";
            this.labelAcronymsResult.Size = new System.Drawing.Size(99, 15);
            this.labelAcronymsResult.TabIndex = 5;
            this.labelAcronymsResult.Text = "(acronyms result)";
            // 
            // DocumentDataDisplayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.labelAcronymsResult);
            this.Controls.Add(this.dataGridViewAcronymDictionary);
            this.Controls.Add(this.dataGridViewWordDictionary);
            this.Controls.Add(this.labelWordsResult);
            this.Name = "DocumentDataDisplayUserControl";
            this.Size = new System.Drawing.Size(765, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAcronymDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWordDictionary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWordsResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dataGridViewWordDictionary;
        private System.Windows.Forms.DataGridView dataGridViewAcronymDictionary;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word;
        private System.Windows.Forms.DataGridViewTextBoxColumn WordFrequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acronym;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcronymFrequency;
        private System.Windows.Forms.Label labelAcronymsResult;
    }
}
