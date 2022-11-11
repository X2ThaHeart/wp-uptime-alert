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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.total_websites_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.blacklistView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.lastCheckTime_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 64);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(395, 167);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label1.Size = new System.Drawing.Size(355, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Paste your WordPress website URLs into the box below to monitor";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Clear List";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(440, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total Websites :";
            // 
            // total_websites_label
            // 
            this.total_websites_label.AutoSize = true;
            this.total_websites_label.Location = new System.Drawing.Point(534, 67);
            this.total_websites_label.Name = "total_websites_label";
            this.total_websites_label.Size = new System.Drawing.Size(51, 15);
            this.total_websites_label.TabIndex = 5;
            this.total_websites_label.Text = "Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(441, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Active Websites :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(544, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(441, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Website Errors :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(535, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "Number";
            // 
            // blacklistView
            // 
            this.blacklistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.blacklistView.FullRowSelect = true;
            this.blacklistView.Location = new System.Drawing.Point(414, 239);
            this.blacklistView.Name = "blacklistView";
            this.blacklistView.Size = new System.Drawing.Size(374, 199);
            this.blacklistView.TabIndex = 10;
            this.blacklistView.UseCompatibleStateImageBehavior = false;
            this.blacklistView.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 600;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(577, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "This is the current sites that have errors";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(200, 385);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 15);
            this.label8.TabIndex = 12;
            this.label8.Text = "Time First Error :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(296, 385);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 15);
            this.label9.TabIndex = 13;
            this.label9.Text = "Date and Time";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(201, 417);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "Last Checked :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(296, 417);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 15);
            this.label11.TabIndex = 15;
            this.label11.Text = "Time";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 238);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label12.Size = new System.Drawing.Size(115, 18);
            this.label12.TabIndex = 16;
            this.label12.Text = "Testing Current Site :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 268);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 15);
            this.label13.TabIndex = 17;
            this.label13.Text = "Website address";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(441, 210);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Re-test Error Sites";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(313, 239);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Start Testing";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 298);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 15);
            this.label14.TabIndex = 20;
            this.label14.Text = "Last Check Time :";
            // 
            // lastCheckTime_label
            // 
            this.lastCheckTime_label.AutoSize = true;
            this.lastCheckTime_label.Location = new System.Drawing.Point(117, 298);
            this.lastCheckTime_label.Name = "lastCheckTime_label";
            this.lastCheckTime_label.Size = new System.Drawing.Size(33, 15);
            this.lastCheckTime_label.TabIndex = 21;
            this.lastCheckTime_label.Text = "Time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lastCheckTime_label);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.blacklistView);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.total_websites_label);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Wp Uptime Alert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox richTextBox1;
        private Label label1;
        private Button button1;
        private Label label2;
        private Label total_websites_label;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ListView blacklistView;
        private Label label3;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Button button2;
        private Button button3;
        private ColumnHeader columnHeader1;
        private Label label14;
        private Label lastCheckTime_label;
    }
}