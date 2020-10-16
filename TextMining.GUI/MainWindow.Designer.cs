﻿namespace TextMining.GUI
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelUserControlSelection = new System.Windows.Forms.Panel();
            this.panelActiveUserControl = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonUserControlWordFrequency = new System.Windows.Forms.RadioButton();
            this.panelUserControlSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelUserControlSelection
            // 
            this.panelUserControlSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserControlSelection.Controls.Add(this.radioButtonUserControlWordFrequency);
            this.panelUserControlSelection.Controls.Add(this.label1);
            this.panelUserControlSelection.Location = new System.Drawing.Point(14, 13);
            this.panelUserControlSelection.Name = "panelUserControlSelection";
            this.panelUserControlSelection.Size = new System.Drawing.Size(200, 100);
            this.panelUserControlSelection.TabIndex = 0;
            // 
            // panelActiveUserControl
            // 
            this.panelActiveUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActiveUserControl.Location = new System.Drawing.Point(234, 12);
            this.panelActiveUserControl.Name = "panelActiveUserControl";
            this.panelActiveUserControl.Size = new System.Drawing.Size(816, 489);
            this.panelActiveUserControl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menu";
            // 
            // radioButtonUserControlWordFrequency
            // 
            this.radioButtonUserControlWordFrequency.AutoSize = true;
            this.radioButtonUserControlWordFrequency.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonUserControlWordFrequency.Location = new System.Drawing.Point(9, 43);
            this.radioButtonUserControlWordFrequency.Name = "radioButtonUserControlWordFrequency";
            this.radioButtonUserControlWordFrequency.Size = new System.Drawing.Size(125, 23);
            this.radioButtonUserControlWordFrequency.TabIndex = 1;
            this.radioButtonUserControlWordFrequency.TabStop = true;
            this.radioButtonUserControlWordFrequency.Text = "Word frequency";
            this.radioButtonUserControlWordFrequency.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panelActiveUserControl);
            this.Controls.Add(this.panelUserControlSelection);
            this.Name = "MainWindow";
            this.Text = "Text Mining";
            this.panelUserControlSelection.ResumeLayout(false);
            this.panelUserControlSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUserControlSelection;
        private System.Windows.Forms.Panel panelActiveUserControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonUserControlWordFrequency;
    }
}

