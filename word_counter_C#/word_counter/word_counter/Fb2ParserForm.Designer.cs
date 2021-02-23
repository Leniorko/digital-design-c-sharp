
namespace word_counter
{
    partial class Fb2ParserForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileSelectButton = new System.Windows.Forms.Button();
            this.ProceedParsingButton = new System.Windows.Forms.Button();
            this.Fb2FilePath = new System.Windows.Forms.TextBox();
            this.OutputToTextBox = new System.Windows.Forms.TextBox();
            this.ChooseOutputFolder = new System.Windows.Forms.Button();
            this.OpenResultFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FileSelectButton
            // 
            this.FileSelectButton.Location = new System.Drawing.Point(12, 11);
            this.FileSelectButton.Name = "FileSelectButton";
            this.FileSelectButton.Size = new System.Drawing.Size(75, 23);
            this.FileSelectButton.TabIndex = 0;
            this.FileSelectButton.Text = "Chose File";
            this.FileSelectButton.UseVisualStyleBackColor = true;
            this.FileSelectButton.Click += new System.EventHandler(this.FileSelectButton_Click);
            // 
            // ProceedParsingButton
            // 
            this.ProceedParsingButton.Location = new System.Drawing.Point(12, 69);
            this.ProceedParsingButton.Name = "ProceedParsingButton";
            this.ProceedParsingButton.Size = new System.Drawing.Size(584, 23);
            this.ProceedParsingButton.TabIndex = 0;
            this.ProceedParsingButton.Text = "Proceed Parsing";
            this.ProceedParsingButton.UseVisualStyleBackColor = true;
            this.ProceedParsingButton.Click += new System.EventHandler(this.ParsieFile);
            // 
            // Fb2FilePath
            // 
            this.Fb2FilePath.Location = new System.Drawing.Point(93, 12);
            this.Fb2FilePath.Name = "Fb2FilePath";
            this.Fb2FilePath.Size = new System.Drawing.Size(503, 23);
            this.Fb2FilePath.TabIndex = 3;
            // 
            // OutputToTextBox
            // 
            this.OutputToTextBox.Location = new System.Drawing.Point(93, 41);
            this.OutputToTextBox.Name = "OutputToTextBox";
            this.OutputToTextBox.Size = new System.Drawing.Size(503, 23);
            this.OutputToTextBox.TabIndex = 3;
            this.OutputToTextBox.Text = "D:\\DefaultWindowsDirs\\Documents";
            // 
            // ChooseOutputFolder
            // 
            this.ChooseOutputFolder.Location = new System.Drawing.Point(12, 40);
            this.ChooseOutputFolder.Name = "ChooseOutputFolder";
            this.ChooseOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.ChooseOutputFolder.TabIndex = 0;
            this.ChooseOutputFolder.Text = "Output To";
            this.ChooseOutputFolder.UseVisualStyleBackColor = true;
            this.ChooseOutputFolder.Click += new System.EventHandler(this.ChooseOutputFolderButton_Click);
            // 
            // OpenResultFile
            // 
            this.OpenResultFile.Location = new System.Drawing.Point(12, 98);
            this.OpenResultFile.Name = "OpenResultFile";
            this.OpenResultFile.Size = new System.Drawing.Size(584, 29);
            this.OpenResultFile.TabIndex = 4;
            this.OpenResultFile.Text = "Open Last Result File";
            this.OpenResultFile.UseVisualStyleBackColor = true;
            this.OpenResultFile.Click += new System.EventHandler(this.OpenResultFile_Click);
            // 
            // Fb2ParserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 137);
            this.Controls.Add(this.OpenResultFile);
            this.Controls.Add(this.OutputToTextBox);
            this.Controls.Add(this.Fb2FilePath);
            this.Controls.Add(this.ProceedParsingButton);
            this.Controls.Add(this.ChooseOutputFolder);
            this.Controls.Add(this.FileSelectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Fb2ParserForm";
            this.Text = "FB2 parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FileSelectButton;
        private System.Windows.Forms.Button ProceedParsingButton;
        private System.Windows.Forms.TextBox Fb2FilePath;
        private System.Windows.Forms.TextBox OutputToTextBox;
        private System.Windows.Forms.Button ChooseOutputFolder;
        private System.Windows.Forms.Button OpenResultFile;
    }
}