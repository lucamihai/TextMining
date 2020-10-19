namespace TextMining.GUI.UserControls
{
    partial class WordFrequencyUserControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxFilepath = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WordFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acronym = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AcronymFrequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewAcronyms = new System.Windows.Forms.DataGridView();
            this.dataGridViewWordDictionary = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAcronyms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWordDictionary)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // textBoxFilepath
            // 
            this.textBoxFilepath.Location = new System.Drawing.Point(35, 92);
            this.textBoxFilepath.Name = "textBoxFilepath";
            this.textBoxFilepath.ReadOnly = true;
            this.textBoxFilepath.Size = new System.Drawing.Size(746, 23);
            this.textBoxFilepath.TabIndex = 1;
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(35, 141);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Status:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.Location = new System.Drawing.Point(164, 145);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(100, 15);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Status goes here";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Result";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(35, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Word frequency";
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
            // Word
            // 
            this.Word.HeaderText = "Word";
            this.Word.Name = "Word";
            this.Word.ReadOnly = true;
            this.Word.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Word.Width = 200;
            // 
            // WordFrequency
            // 
            this.WordFrequency.HeaderText = "Frequency";
            this.WordFrequency.Name = "WordFrequency";
            this.WordFrequency.ReadOnly = true;
            this.WordFrequency.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Acronym
            // 
            this.Acronym.HeaderText = "Acronym";
            this.Acronym.Name = "Acronym";
            this.Acronym.ReadOnly = true;
            this.Acronym.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Acronym.Width = 200;
            // 
            // AcronymFrequency
            // 
            this.AcronymFrequency.HeaderText = "Frequency";
            this.AcronymFrequency.Name = "AcronymFrequency";
            this.AcronymFrequency.ReadOnly = true;
            this.AcronymFrequency.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // WordFrequencyUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            // 
            // dataGridViewAcronyms
            // 
            this.dataGridViewAcronyms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAcronyms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Acronym,
            this.AcronymFrequency});
            this.dataGridViewAcronyms.Location = new System.Drawing.Point(405, 209);
            this.dataGridViewAcronyms.Name = "dataGridViewAcronyms";
            this.dataGridViewAcronyms.Size = new System.Drawing.Size(344, 320);
            this.dataGridViewAcronyms.TabIndex = 7;
            this.dataGridViewAcronyms.Text = "dataGridView1";
            // 
            // dataGridViewWordDictionary
            // 
            this.dataGridViewWordDictionary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWordDictionary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Word,
            this.WordFrequency});
            this.dataGridViewWordDictionary.Location = new System.Drawing.Point(35, 209);
            this.dataGridViewWordDictionary.Name = "dataGridViewWordDictionary";
            this.dataGridViewWordDictionary.Size = new System.Drawing.Size(344, 320);
            this.dataGridViewWordDictionary.TabIndex = 7;
            this.dataGridViewWordDictionary.Text = "dataGridViewWordDictionary";
            this.Controls.Add(this.dataGridViewAcronyms);
            this.Controls.Add(this.dataGridViewWordDictionary);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.textBoxFilepath);
            this.Controls.Add(this.button1);
            this.Name = "WordFrequencyUserControl";
            this.Size = new System.Drawing.Size(816, 600);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAcronyms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWordDictionary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxFilepath;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewWordDictionary;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word;
        private System.Windows.Forms.DataGridViewTextBoxColumn WordFrequency;
        private System.Windows.Forms.DataGridView dataGridViewAcronyms;
        private System.Windows.Forms.DataGridViewTextBoxColumn Acronym;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcronymFrequency;
    }
}
