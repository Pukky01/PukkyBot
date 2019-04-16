namespace PukkyBot
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.botController = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.screenScraper = new System.Windows.Forms.Timer(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.replayButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SaveRecordButton = new System.Windows.Forms.Button();
            this.recordingName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "pos:";
            // 
            // botController
            // 
            this.botController.Enabled = true;
            this.botController.Tick += new System.EventHandler(this.onBotUpdate);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bot behaviour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 166);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mouse record";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Event Log";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 9);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "zapnuto";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(331, 124);
            this.test.Margin = new System.Windows.Forms.Padding(4);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(100, 28);
            this.test.TabIndex = 8;
            this.test.Text = "Test Button";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.TestButtonClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 160);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 11;
            this.button2.Text = "start record";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click_1);
            // 
            // screenScraper
            // 
            this.screenScraper.Interval = 1000;
            this.screenScraper.Tick += new System.EventHandler(this.ScreenScraper_Tick);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 58);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(202, 148);
            this.listBox1.TabIndex = 12;
            // 
            // replayButton
            // 
            this.replayButton.Location = new System.Drawing.Point(331, 196);
            this.replayButton.Margin = new System.Windows.Forms.Padding(4);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(100, 28);
            this.replayButton.TabIndex = 13;
            this.replayButton.Text = "Replay";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.ReplayButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Last Recorded"});
            this.comboBox1.Location = new System.Drawing.Point(310, 231);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "Last Recorded";
            // 
            // SaveRecordButton
            // 
            this.SaveRecordButton.Location = new System.Drawing.Point(203, 227);
            this.SaveRecordButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveRecordButton.Name = "SaveRecordButton";
            this.SaveRecordButton.Size = new System.Drawing.Size(100, 28);
            this.SaveRecordButton.TabIndex = 15;
            this.SaveRecordButton.Text = "Save Record";
            this.SaveRecordButton.UseVisualStyleBackColor = true;
            this.SaveRecordButton.Click += new System.EventHandler(this.SaveRecordButton_Click);
            // 
            // recordingName
            // 
            this.recordingName.Location = new System.Drawing.Point(203, 262);
            this.recordingName.Name = "recordingName";
            this.recordingName.Size = new System.Drawing.Size(100, 22);
            this.recordingName.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 296);
            this.Controls.Add(this.recordingName);
            this.Controls.Add(this.SaveRecordButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.test);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "PukkyBot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer botController;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer screenScraper;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button SaveRecordButton;
        private System.Windows.Forms.TextBox recordingName;
    }
}

