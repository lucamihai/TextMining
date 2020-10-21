namespace TextMining.GUI.UserControls
{
    partial class GlobalDocumentDataBuilderUserControl
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
            this.buttonLoadFiles = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioButtonLoadMultipleFiles = new System.Windows.Forms.RadioButton();
            this.panelDocumentDataDisplayUserControl = new System.Windows.Forms.Panel();
            this.buttonSelectDirectory = new System.Windows.Forms.Button();
            this.textBoxSelectedDirectory = new System.Windows.Forms.TextBox();
            this.radioButtonLoadFilesFromDirectory = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // buttonLoadFiles
            // 
            this.buttonLoadFiles.Location = new System.Drawing.Point(32, 63);
            this.buttonLoadFiles.Name = "buttonLoadFiles";
            this.buttonLoadFiles.Size = new System.Drawing.Size(97, 23);
            this.buttonLoadFiles.TabIndex = 0;
            this.buttonLoadFiles.Text = "Load files";
            this.buttonLoadFiles.UseVisualStyleBackColor = true;
            this.buttonLoadFiles.Click += new System.EventHandler(this.buttonLoadFiles_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(34, 154);
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
            this.label1.Location = new System.Drawing.Point(115, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Status:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.Location = new System.Drawing.Point(163, 158);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(100, 15);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Status goes here";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(35, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Global Document Data Builder";
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
            // radioButtonLoadMultipleFiles
            // 
            this.radioButtonLoadMultipleFiles.AutoSize = true;
            this.radioButtonLoadMultipleFiles.Checked = true;
            this.radioButtonLoadMultipleFiles.Location = new System.Drawing.Point(32, 38);
            this.radioButtonLoadMultipleFiles.Name = "radioButtonLoadMultipleFiles";
            this.radioButtonLoadMultipleFiles.Size = new System.Drawing.Size(122, 19);
            this.radioButtonLoadMultipleFiles.TabIndex = 8;
            this.radioButtonLoadMultipleFiles.TabStop = true;
            this.radioButtonLoadMultipleFiles.Text = "Load multiple files";
            this.radioButtonLoadMultipleFiles.UseVisualStyleBackColor = true;
            // 
            // panelDocumentDataDisplayUserControl
            // 
            this.panelDocumentDataDisplayUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDocumentDataDisplayUserControl.Location = new System.Drawing.Point(35, 185);
            this.panelDocumentDataDisplayUserControl.Name = "panelDocumentDataDisplayUserControl";
            this.panelDocumentDataDisplayUserControl.Size = new System.Drawing.Size(765, 400);
            this.panelDocumentDataDisplayUserControl.TabIndex = 7;
            // 
            // buttonSelectDirectory
            // 
            this.buttonSelectDirectory.Location = new System.Drawing.Point(32, 117);
            this.buttonSelectDirectory.Name = "buttonSelectDirectory";
            this.buttonSelectDirectory.Size = new System.Drawing.Size(97, 23);
            this.buttonSelectDirectory.TabIndex = 0;
            this.buttonSelectDirectory.Text = "Select directory";
            this.buttonSelectDirectory.UseVisualStyleBackColor = true;
            this.buttonSelectDirectory.Click += new System.EventHandler(this.buttonSelectDirectory_Click);
            // 
            // textBoxSelectedDirectory
            // 
            this.textBoxSelectedDirectory.Location = new System.Drawing.Point(135, 118);
            this.textBoxSelectedDirectory.Name = "textBoxSelectedDirectory";
            this.textBoxSelectedDirectory.ReadOnly = true;
            this.textBoxSelectedDirectory.Size = new System.Drawing.Size(665, 23);
            this.textBoxSelectedDirectory.TabIndex = 1;
            // 
            // radioButtonLoadFilesFromDirectory
            // 
            this.radioButtonLoadFilesFromDirectory.AutoSize = true;
            this.radioButtonLoadFilesFromDirectory.Location = new System.Drawing.Point(32, 93);
            this.radioButtonLoadFilesFromDirectory.Name = "radioButtonLoadFilesFromDirectory";
            this.radioButtonLoadFilesFromDirectory.Size = new System.Drawing.Size(190, 19);
            this.radioButtonLoadFilesFromDirectory.TabIndex = 8;
            this.radioButtonLoadFilesFromDirectory.Text = "Select directory containing files";
            this.radioButtonLoadFilesFromDirectory.UseVisualStyleBackColor = true;
            // 
            // GlobalDocumentDataBuilderUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.radioButtonLoadFilesFromDirectory);
            this.Controls.Add(this.panelDocumentDataDisplayUserControl);
            this.Controls.Add(this.textBoxSelectedDirectory);
            this.Controls.Add(this.buttonSelectDirectory);
            this.Controls.Add(this.radioButtonLoadMultipleFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonLoadFiles);
            this.Name = "GlobalDocumentDataBuilderUserControl";
            this.Size = new System.Drawing.Size(816, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadFiles;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.RadioButton radioButtonLoadMultipleFiles;
        private System.Windows.Forms.Panel panelDocumentDataDisplayUserControl;
        private System.Windows.Forms.Button buttonSelectDirectory;
        private System.Windows.Forms.TextBox textBoxSelectedDirectory;
        private System.Windows.Forms.RadioButton radioButtonLoadFilesFromDirectory;
    }
}
