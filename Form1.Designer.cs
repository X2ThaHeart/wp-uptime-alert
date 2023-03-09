namespace wp_uptime_alert
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            clearInput_button = new Button();
            label2 = new Label();
            total_websites_label = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            button2 = new Button();
            button3 = new Button();
            label14 = new Label();
            blacklistRichTextBox = new RichTextBox();
            clearBlacklist_button = new Button();
            label15 = new Label();
            label16 = new Label();
            activeTestingSite_label = new Label();
            lastCheckedActive_label = new Label();
            autoErrorRetest = new CheckBox();
            button1 = new Button();
            label13 = new Label();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(13, 54);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(536, 154);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 30);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 3);
            label1.Size = new Size(355, 18);
            label1.TabIndex = 2;
            label1.Text = "Paste your WordPress website URLs into the box below to monitor";
            // 
            // clearInput_button
            // 
            clearInput_button.Location = new Point(676, 147);
            clearInput_button.Name = "clearInput_button";
            clearInput_button.Size = new Size(69, 23);
            clearInput_button.TabIndex = 3;
            clearInput_button.Text = "Clear List";
            clearInput_button.UseVisualStyleBackColor = true;
            clearInput_button.Click += clearInput_button_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(452, 9);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 4;
            label2.Text = "Total Websites :";
            // 
            // total_websites_label
            // 
            total_websites_label.AutoSize = true;
            total_websites_label.Location = new Point(556, 9);
            total_websites_label.Name = "total_websites_label";
            total_websites_label.Size = new Size(51, 15);
            total_websites_label.TabIndex = 5;
            total_websites_label.Text = "Number";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(453, 33);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 6;
            label4.Text = "Active Websites :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(556, 33);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 7;
            label5.Text = "Number";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(636, 9);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 8;
            label6.Text = "Website Errors :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(739, 9);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 9;
            label7.Text = "Number";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 240);
            label8.Name = "label8";
            label8.Size = new Size(92, 15);
            label8.TabIndex = 12;
            label8.Text = "Time First Error :";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(111, 240);
            label9.Name = "label9";
            label9.Size = new Size(83, 15);
            label9.TabIndex = 13;
            label9.Text = "Date and Time";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(16, 267);
            label10.Name = "label10";
            label10.Size = new Size(83, 15);
            label10.TabIndex = 14;
            label10.Text = "Last Checked :";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(111, 267);
            label11.Name = "label11";
            label11.Size = new Size(33, 15);
            label11.TabIndex = 15;
            label11.Text = "Time";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(571, 55);
            label12.Name = "label12";
            label12.Padding = new Padding(0, 0, 0, 3);
            label12.Size = new Size(151, 18);
            label12.TabIndex = 16;
            label12.Text = "Testing Current Active Site :";
            // 
            // button2
            // 
            button2.Location = new Point(236, 417);
            button2.Name = "button2";
            button2.Size = new Size(114, 23);
            button2.TabIndex = 18;
            button2.Text = "Re-test Error Sites";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(570, 147);
            button3.Name = "button3";
            button3.Size = new Size(79, 23);
            button3.TabIndex = 19;
            button3.Text = "Start Testing";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(570, 114);
            label14.Name = "label14";
            label14.Size = new Size(99, 15);
            label14.TabIndex = 20;
            label14.Text = "Last Check Time :";
            // 
            // blacklistRichTextBox
            // 
            blacklistRichTextBox.Location = new Point(210, 240);
            blacklistRichTextBox.Name = "blacklistRichTextBox";
            blacklistRichTextBox.Size = new Size(578, 171);
            blacklistRichTextBox.TabIndex = 22;
            blacklistRichTextBox.Text = "";
            blacklistRichTextBox.LinkClicked += blacklistRichTextBox_LinkClicked;
            // 
            // clearBlacklist_button
            // 
            clearBlacklist_button.Location = new Point(147, 417);
            clearBlacklist_button.Name = "clearBlacklist_button";
            clearBlacklist_button.Size = new Size(75, 23);
            clearBlacklist_button.TabIndex = 23;
            clearBlacklist_button.Text = "Clear List";
            clearBlacklist_button.UseVisualStyleBackColor = true;
            clearBlacklist_button.Click += clearBlacklist_button_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(15, 303);
            label15.Name = "label15";
            label15.Size = new Size(143, 15);
            label15.TabIndex = 24;
            label15.Text = "Error Testing Current Site :";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(15, 328);
            label16.Name = "label16";
            label16.Padding = new Padding(0, 0, 0, 3);
            label16.Size = new Size(92, 18);
            label16.TabIndex = 25;
            label16.Text = "Website address";
            // 
            // activeTestingSite_label
            // 
            activeTestingSite_label.AutoSize = true;
            activeTestingSite_label.Location = new Point(570, 82);
            activeTestingSite_label.Name = "activeTestingSite_label";
            activeTestingSite_label.Padding = new Padding(0, 0, 0, 3);
            activeTestingSite_label.Size = new Size(49, 18);
            activeTestingSite_label.TabIndex = 28;
            activeTestingSite_label.Text = "Website";
            // 
            // lastCheckedActive_label
            // 
            lastCheckedActive_label.AutoSize = true;
            lastCheckedActive_label.Location = new Point(676, 114);
            lastCheckedActive_label.Name = "lastCheckedActive_label";
            lastCheckedActive_label.Size = new Size(33, 15);
            lastCheckedActive_label.TabIndex = 29;
            lastCheckedActive_label.Text = "Time";
            // 
            // autoErrorRetest
            // 
            autoErrorRetest.AutoSize = true;
            autoErrorRetest.Checked = true;
            autoErrorRetest.CheckState = CheckState.Checked;
            autoErrorRetest.Location = new Point(13, 419);
            autoErrorRetest.Name = "autoErrorRetest";
            autoErrorRetest.Size = new Size(120, 19);
            autoErrorRetest.TabIndex = 30;
            autoErrorRetest.Text = "Auto Retest Errors";
            autoErrorRetest.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(570, 183);
            button1.Name = "button1";
            button1.Size = new Size(79, 23);
            button1.TabIndex = 31;
            button1.Text = "Stop Testing";
            button1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(210, 222);
            label13.Name = "label13";
            label13.Size = new Size(211, 15);
            label13.TabIndex = 11;
            label13.Text = "This is the current sites that have errors";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(autoErrorRetest);
            Controls.Add(lastCheckedActive_label);
            Controls.Add(activeTestingSite_label);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(clearBlacklist_button);
            Controls.Add(blacklistRichTextBox);
            Controls.Add(label14);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label13);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(total_websites_label);
            Controls.Add(label2);
            Controls.Add(clearInput_button);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Wp Uptime Alert";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Label label1;
        private Button clearInput_button;
        private Label label2;
        private Label total_websites_label;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Button button2;
        private Button button3;
        private Label label14;
        private RichTextBox blacklistRichTextBox;
        private Button clearBlacklist_button;
        private Label label15;
        private Label label16;
        private Label activeTestingSite_label;
        private Label lastCheckedActive_label;
        private CheckBox autoErrorRetest;
        private Button button1;
        private Label label13;
    }
}