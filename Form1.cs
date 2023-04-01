using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System;
using System.Data;
using wp_uptime_alert.controller;
using static System.Windows.Forms.Design.AxImporter;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Text;
using static System.Windows.Forms.LinkLabel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Net.Sockets;
using System.Net;
using wp_uptime_alert.model;
using System.Collections.Concurrent;
using System.ComponentModel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace wp_uptime_alert
{
    public delegate void DrawListViewItemEventHandler(object sender, DrawListViewItemEventArgs e);

    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        DataTable dtBlacklist = new DataTable();
        Actions action = new Actions();
        [NotNull]
        //SiteRecord siterecord = new SiteRecord();
        private BindingSource _bindingSource = new BindingSource();
        private HtmlStatusIcon _myClassInstance;


        public SiteRecord SiteRecord { get; set; } // Property for SiteRecord

        private BindingSource bindingSource = new BindingSource();


        public Form1()
        {
            InitializeComponent();

            // Create a new SiteRecord object with the dt DataTable
            dt.Columns.Add("site");
            dt.Columns.Add("domainstatus");
            dt.Columns.Add("wordpressstatus");
            dt.Columns.Add("lastcheckedtime");

            SiteRecord = new SiteRecord(); // Create an instance of SiteRecord here

            // Set AutoGenerateColumns to false before setting up the column headers
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = bindingSource;


            // Set the BindingSource to the DataTable
            bindingSource.DataSource = dt;


            dataGridView1.Columns[0].Name = "siteColumn";
            dataGridView1.Columns[0].DataPropertyName = "site";

            dataGridView1.Columns[1].Name = "domainStatusColumn";
            dataGridView1.Columns[1].DataPropertyName = "domainstatus";

            dataGridView1.Columns[2].Name = "wordpressStatusColumn";
            dataGridView1.Columns[2].DataPropertyName = "wordpressstatus";

            dataGridView1.Columns[3].Name = "lastCheckedTimeColumn";
            dataGridView1.Columns[3].DataPropertyName = "lastcheckedtime";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "t";



            // Set up the column headers in dataGridView1
            //InitializeDataGridViewColumns(dataGridView1);

            // Bind the DataTable to the BindingSource
            //_bindingSource.DataSource = dt;

            // Set the DataGridView's DataSource to the BindingSource
            //dataGridView1.DataSource = _bindingSource;

            _myClassInstance = new HtmlStatusIcon(dataGridView1);
            _myClassInstance.MyDataGridView.CellFormatting += action.dataGridView_CellFormatting;

            //dataGridView1.DataSource = bindingSource;

            // Call the cleanInputRefreshDataTableAsInput method and bind the DataGridView to the updated DataTable
            var updatedDataTable = action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);
            bindingSource.DataSource = updatedDataTable;



           

        }

        /*

        public void InitializeDataGridViewColumns(DataGridView dataGridView1)
        {
            // Create and configure columns
            DataGridViewColumn siteColumn = new DataGridViewTextBoxColumn
            {
                Name = "site", // Make sure the Name property matches the DataTable column name
                HeaderText = "Site",
                Width = 270, // Set the width of the 'Site' column
            };

            DataGridViewColumn domainStatusColumn = new DataGridViewTextBoxColumn
            {
                Name = "domainstatus", // Make sure the Name property matches the DataTable column name
                HeaderText = "Domain Status",
                Width = 80, // Set the width of the 'Domain Status' column
            };

            DataGridViewColumn wordpressstatusColumn = new DataGridViewTextBoxColumn
            {
                Name = "wordpressstatus", // Make sure the Name property matches the DataTable column name
                HeaderText = "WordPress Status",
                Width = 80, // Set the width of the 'Domain Status' column
            };

            DataGridViewColumn checkedtimeColumn = new DataGridViewTextBoxColumn
            {
                Name = "lastcheckedtime", // Make sure the Name property matches the DataTable column name
                HeaderText = "Last Checked Time",
                Width = 95, // Set the width of the 'Domain Status' column
            };

            // Add the columns to the DataGridView
            dataGridView1.Columns.Add(siteColumn);
            dataGridView1.Columns.Add(domainStatusColumn);
            dataGridView1.Columns.Add(wordpressstatusColumn);
            dataGridView1.Columns.Add(checkedtimeColumn);
        }

        */

        /*
        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = bindingSource;
        }
        */





        public void SetListViewBoxText(string item)
        {
            //listView1.Items.Add(item);
        }
        public void SetListViewItem(string item)
        {
            //listView1.Items.Add(item);
        }





        static bool StringIsNewLine(string s)
        {
            return (!string.IsNullOrEmpty(s)) &&
                (!string.IsNullOrWhiteSpace(s)) &&
                (((s.Length == 1) && (s[0] == 8203)) ||
                ((s.Length == 2) && (s[0] == 8203) && (s[1] == 8203)));
        }



        //input text box for urls
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private BackgroundWorker _worker = new BackgroundWorker();

        private BlockingCollection<string> _dataQueue = new BlockingCollection<string>();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private CancellationTokenSource _cts;



        private string site;


        //main start testing button
        private async void button3_Click(object sender, EventArgs e)
        {
            var site = "";
            string[] removeSpacesFirst = richTextBox1.Lines;

            for (int i = 0; i < removeSpacesFirst.Length; i++)
            {
                if (removeSpacesFirst[i] == "\r\n" || removeSpacesFirst[i] == " " || removeSpacesFirst[i] == null || removeSpacesFirst[i].Length == 0)
                {
                    continue;
                }
                else
                {
                    site = removeSpacesFirst[i];

                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(site);
                        if (hostEntry.AddressList.Length > 0)
                        {
                            // Domain exists and is valid
                        }
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode == SocketError.HostNotFound)
                        {
                            MessageBox.Show("Error with domain : " + site + " check the spelling and try again", "Error");
                            richTextBox1.Clear();
                            return;
                        }
                    }

                    if (site == null)
                    {
                        MessageBox.Show("site entered is null ", "Error");
                    }
                    else
                    {
                        site = action.FirstCleanRssUrl(site);

                        DataRow[] filteredRows = dt.Select(string.Format("{0} LIKE '%{1}%'", "site", site));
                        if (filteredRows.Length == 0)
                        {
                            site = action.cleanUrlFinal(site);

                            DataRow row = dt.NewRow();
                            row["site"] = site;
                            dt.Rows.Add(row);

                            label5.Text = dt.Rows.Count.ToString();
                            richTextBox1.Clear();

                            // Refresh the DataGridView using the updated DataTable
                            var updatedDataTable = await action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);
                            //bindingSource.DataSource = updatedDataTable;

                            // Initialize the CancellationTokenSource
                            _cts = new CancellationTokenSource();

                            // Pass the CancellationToken to the PerformSiteCheckAsync method
                            await PerformSiteCheckAsync(site, _cts.Token);


                        


                        }
                        else if (filteredRows.Length == 1)
                        {
                            MessageBox.Show("Site already exists", "Error");
                            var rsswait = await action.GetRssfeedAndCheckAsync(site, dt, SiteRecord);

                            if (rsswait)
                            {
                                if (action.UrlValid == true)
                                {
                                    label5.Text = dt.Rows.Count.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }




        private async Task PerformSiteCheckAsync(string site, CancellationToken ct)
        {






            if (!true)
            {
                while (!ct.IsCancellationRequested)
                {
                    // Your code to run continuously goes here
                    await action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);

                    await action.startTestingEachEntryInDataTableAsync(dt, lastCheckedActive_label, activeTestingSite_label, SiteRecord);

                    await action.GetRssfeedAndCheckAsync(site, dt, SiteRecord);

                    // Update the lastcheckedtime column for this particular website
                    //SiteRecord.LastCheckedTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");


                    DataRow row = dt.Select(string.Format("{0} LIKE '%{1}%'", "site", site)).FirstOrDefault();
                    if (row != null)
                    {
                        row["domainstatus"] = SiteRecord.DomainStatus; // Get the value from the siterecord
                        row["wordpressstatus"] = SiteRecord.WpStatus; // Get the value from the siterecord
                        row["lastcheckedtime"] = SiteRecord.LastCheckedTime; // Update the lastcheckedtime column for this particular website

                        //row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm:ss"); // Update the lastcheckedtime column for this particular website
                        // Use Invoke to update the DataGridView on the UI thread
                        Invoke((MethodInvoker)delegate
                        {
                            action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);
                        });
                    }



                    action.updateWebsiteLabels(total_websites_label, label5, label7, dt, dtBlacklist);

                    // This line will delay the loop for a certain period (e.g., 5000 milliseconds or 5 seconds)
                    await Task.Delay(TimeSpan.FromMinutes(5), ct);
                }
            }
        }





        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }






        private async Task ProcessDataLoopAsync(CancellationToken cancellationToken, Action updateUI)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(async () =>
                {
                    await action.startTestingEachEntryInDataTableAsync(dt, lastCheckedActive_label, activeTestingSite_label, SiteRecord);

                    // Call updateUI action here
                    Invoke((MethodInvoker)delegate
                    {
                        updateUI();
                    });

                    action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);
                    action.updateWebsiteLabels(total_websites_label, label5, label7, dt, dtBlacklist);
                });
            }
        }













        //private void cancelButton_Click(object sender, EventArgs e)
        //{
        //    if (_cancellationTokenSource != null)
        //    {
        //        _cancellationTokenSource.Cancel();
        //        _cancellationTokenSource = null;
        //    }
        //}






        private void blacklistRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            var url = e.LinkText;
            OpenUrl(url);
        }



        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            var url = e.LinkText;
            OpenUrl(url);
        }

        private void clearInput_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to clear the list?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                richTextBox1.Clear();
                dt.Rows.Clear();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


            action.updateWebsiteLabels(total_websites_label, label5, label7, dt, dtBlacklist);


        }


        //retest error sites second main button
        private void button2_Click(object sender, EventArgs e)
        {
            action.updateWebsiteLabels(total_websites_label, label5, label7, dt, dtBlacklist);

        }

        private void clearBlacklist_button_Click(object sender, EventArgs e)
        {
            action.updateWebsiteLabels(total_websites_label, label5, label7, dt, dtBlacklist);


        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void siteRecordBindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
