namespace MusicPlayer
{
    partial class MainForm
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
            this.MMLTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MMLPlayButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MidiFileTextBox = new System.Windows.Forms.TextBox();
            this.MidiFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MidiFileLinkLabel = new System.Windows.Forms.LinkLabel();
            this.MidiPlayButton = new System.Windows.Forms.Button();
            this.MSongFileLinkLabel = new System.Windows.Forms.LinkLabel();
            this.MSongFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MSongFileTextBox = new System.Windows.Forms.TextBox();
            this.MSongPlayButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MMLTextBox
            // 
            this.MMLTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MMLTextBox.Location = new System.Drawing.Point(76, 15);
            this.MMLTextBox.Name = "MMLTextBox";
            this.MMLTextBox.Size = new System.Drawing.Size(477, 20);
            this.MMLTextBox.TabIndex = 0;
            this.MMLTextBox.Text = "C D E F G A B C>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MML";
            // 
            // MMLPlayButton
            // 
            this.MMLPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MMLPlayButton.Location = new System.Drawing.Point(478, 41);
            this.MMLPlayButton.Name = "MMLPlayButton";
            this.MMLPlayButton.Size = new System.Drawing.Size(75, 23);
            this.MMLPlayButton.TabIndex = 2;
            this.MMLPlayButton.Text = "Play";
            this.MMLPlayButton.UseVisualStyleBackColor = true;
            this.MMLPlayButton.Click += new System.EventHandler(this.MMLPlayButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 222);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(565, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(15, 17);
            this.toolStripStatusLabel1.Text = "A";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabel2.Text = "B";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(15, 17);
            this.toolStripStatusLabel3.Text = "C";
            // 
            // MidiFileTextBox
            // 
            this.MidiFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MidiFileTextBox.Location = new System.Drawing.Point(76, 87);
            this.MidiFileTextBox.Name = "MidiFileTextBox";
            this.MidiFileTextBox.Size = new System.Drawing.Size(477, 20);
            this.MidiFileTextBox.TabIndex = 5;
            this.MidiFileTextBox.Text = "Content\\RaisingFight.mid";
            // 
            // MidiFileDialog
            // 
            this.MidiFileDialog.DefaultExt = "mid";
            this.MidiFileDialog.Filter = "Midi files|*.mid";
            this.MidiFileDialog.RestoreDirectory = true;
            // 
            // MidiFileLinkLabel
            // 
            this.MidiFileLinkLabel.AutoSize = true;
            this.MidiFileLinkLabel.Location = new System.Drawing.Point(12, 90);
            this.MidiFileLinkLabel.Name = "MidiFileLinkLabel";
            this.MidiFileLinkLabel.Size = new System.Drawing.Size(44, 13);
            this.MidiFileLinkLabel.TabIndex = 7;
            this.MidiFileLinkLabel.TabStop = true;
            this.MidiFileLinkLabel.Text = "Midi File";
            this.MidiFileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MidiFileLinkLabel_LinkClicked);
            // 
            // MidiPlayButton
            // 
            this.MidiPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MidiPlayButton.Location = new System.Drawing.Point(478, 113);
            this.MidiPlayButton.Name = "MidiPlayButton";
            this.MidiPlayButton.Size = new System.Drawing.Size(75, 23);
            this.MidiPlayButton.TabIndex = 8;
            this.MidiPlayButton.Text = "Play";
            this.MidiPlayButton.UseVisualStyleBackColor = true;
            this.MidiPlayButton.Click += new System.EventHandler(this.MidiPlayButton_Click);
            // 
            // MSongFileLinkLabel
            // 
            this.MSongFileLinkLabel.AutoSize = true;
            this.MSongFileLinkLabel.Location = new System.Drawing.Point(12, 163);
            this.MSongFileLinkLabel.Name = "MSongFileLinkLabel";
            this.MSongFileLinkLabel.Size = new System.Drawing.Size(58, 13);
            this.MSongFileLinkLabel.TabIndex = 9;
            this.MSongFileLinkLabel.TabStop = true;
            this.MSongFileLinkLabel.Text = "MSong File";
            this.MSongFileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MSongFileLinkLabel_LinkClicked);
            // 
            // MSongFileDialog
            // 
            this.MSongFileDialog.DefaultExt = "msong";
            this.MSongFileDialog.Filter = "MSong Files|*.msong";
            this.MSongFileDialog.RestoreDirectory = true;
            // 
            // MSongFileTextBox
            // 
            this.MSongFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MSongFileTextBox.Location = new System.Drawing.Point(76, 160);
            this.MSongFileTextBox.Name = "MSongFileTextBox";
            this.MSongFileTextBox.Size = new System.Drawing.Size(477, 20);
            this.MSongFileTextBox.TabIndex = 10;
            this.MSongFileTextBox.Text = "Content\\Sama3y Bayati (3erian).msong";
            // 
            // MSongPlayButton
            // 
            this.MSongPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MSongPlayButton.Location = new System.Drawing.Point(478, 186);
            this.MSongPlayButton.Name = "MSongPlayButton";
            this.MSongPlayButton.Size = new System.Drawing.Size(75, 23);
            this.MSongPlayButton.TabIndex = 11;
            this.MSongPlayButton.Text = "Play";
            this.MSongPlayButton.UseVisualStyleBackColor = true;
            this.MSongPlayButton.Click += new System.EventHandler(this.MSongPlayButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 244);
            this.Controls.Add(this.MSongPlayButton);
            this.Controls.Add(this.MSongFileTextBox);
            this.Controls.Add(this.MSongFileLinkLabel);
            this.Controls.Add(this.MidiPlayButton);
            this.Controls.Add(this.MidiFileLinkLabel);
            this.Controls.Add(this.MidiFileTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MMLPlayButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MMLTextBox);
            this.Name = "MainForm";
            this.Text = "Music Player";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MMLTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MMLPlayButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.TextBox MidiFileTextBox;
        private System.Windows.Forms.OpenFileDialog MidiFileDialog;
        private System.Windows.Forms.LinkLabel MidiFileLinkLabel;
        private System.Windows.Forms.Button MidiPlayButton;
        private System.Windows.Forms.LinkLabel MSongFileLinkLabel;
        private System.Windows.Forms.OpenFileDialog MSongFileDialog;
        private System.Windows.Forms.TextBox MSongFileTextBox;
        private System.Windows.Forms.Button MSongPlayButton;
    }
}

