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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            button3 = new Button();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewLinkColumn();
            serverstatus = new DataGridViewTextBoxColumn();
            wpstatus = new DataGridViewTextBoxColumn();
            lastmodified = new DataGridViewTextBoxColumn();
            siteRecordBindingSource = new BindingSource(components);
            siteRecordBindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)siteRecordBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)siteRecordBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(13, 114);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(190, 178);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.Location = new Point(12, 12);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 0, 0, 3);
            label1.Size = new Size(191, 36);
            label1.TabIndex = 2;
            label1.Text = "Paste your website URLs into the small box below and click Start";
            label1.Click += label1_Click;
            // 
            // clearInput_button
            // 
            clearInput_button.Location = new Point(711, 80);
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
            label2.Location = new Point(348, 9);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 4;
            label2.Text = "Total Websites :";
            // 
            // total_websites_label
            // 
            total_websites_label.AutoSize = true;
            total_websites_label.Location = new Point(451, 9);
            total_websites_label.Name = "total_websites_label";
            total_websites_label.Size = new Size(51, 15);
            total_websites_label.TabIndex = 5;
            total_websites_label.Text = "Number";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(348, 40);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 6;
            label4.Text = "Active Websites :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(451, 40);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 7;
            label5.Text = "Number";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(346, 70);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 8;
            label6.Text = "Website Errors :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(451, 70);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 9;
            label7.Text = "Number";
            // 
            // button3
            // 
            button3.Location = new Point(214, 33);
            button3.Name = "button3";
            button3.Size = new Size(93, 28);
            button3.TabIndex = 19;
            button3.Text = "Start Testing";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(214, 80);
            button1.Name = "button1";
            button1.Size = new Size(93, 27);
            button1.TabIndex = 31;
            button1.Text = "Stop Testing";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, serverstatus, wpstatus, lastmodified });
            dataGridView1.Location = new Point(214, 113);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(570, 325);
            dataGridView1.TabIndex = 36;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Site";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.True;
            dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewTextBoxColumn1.Width = 225;
            // 
            // serverstatus
            // 
            serverstatus.HeaderText = "Server Status";
            serverstatus.Name = "serverstatus";
            // 
            // wpstatus
            // 
            wpstatus.HeaderText = "WordPress Status";
            wpstatus.Name = "wpstatus";
            // 
            // lastmodified
            // 
            dataGridViewCellStyle1.Format = "T";
            dataGridViewCellStyle1.NullValue = null;
            lastmodified.DefaultCellStyle = dataGridViewCellStyle1;
            lastmodified.HeaderText = "Last Checked";
            lastmodified.Name = "lastmodified";
            // 
            // siteRecordBindingSource
            // 
            siteRecordBindingSource.DataSource = typeof(model.SiteRecord);
            // 
            // siteRecordBindingSource1
            // 
            siteRecordBindingSource1.DataSource = typeof(model.SiteRecord);
            siteRecordBindingSource1.CurrentChanged += siteRecordBindingSource1_CurrentChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(button3);
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
            Text = "WP Uptime Alert";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)siteRecordBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)siteRecordBindingSource1).EndInit();
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
        private Button button3;
        private Button button1;
        private DataGridView dataGridView1;
        private BindingSource siteRecordBindingSource;
        private BindingSource siteRecordBindingSource1;
        private DataGridViewLinkColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn serverstatus;
        private DataGridViewTextBoxColumn wpstatus;
        private DataGridViewTextBoxColumn lastmodified;
    }
}