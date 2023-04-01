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
using System.Globalization;

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

            var lastCheckedTimeColumn = dataGridView1.Columns["lastCheckedTimeColumn"];
            lastCheckedTimeColumn.DefaultCellStyle.Format = "t";


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


            dataGridView1.AllowUserToAddRows = false;



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
            string[] removeSpacesFirst = richTextBox1.Lines;

            // Check if the rich text box is empty and there are no existing table entries
            if (removeSpacesFirst.Length == 0 && dt.Rows.Count == 0)
            {
                MessageBox.Show("Please enter a site to check.", "Error");
                return;
            }

            // Check for new entries in the rich text box
            for (int i = 0; i < removeSpacesFirst.Length; i++)
            {
                string site = removeSpacesFirst[i];

                if (string.IsNullOrWhiteSpace(site))
                {
                    continue;
                }

                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(site);
                    if (hostEntry.AddressList.Length > 0)
                    {
                        // Domain exists and is valid
                        Debug.WriteLine("Domain is valid.");
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

                site = action.FirstCleanRssUrl(site);

                DataRow[] filteredRows = dt.Select(string.Format("{0} LIKE '%{1}%'", "site", site));
                if (filteredRows.Length == 0)
                {
                    // Add the new site to the DataTable
                    site = action.cleanUrlFinal(site);

                    DataRow row = dt.NewRow();
                    row["site"] = site;
                    dt.Rows.Add(row);

                    label5.Text = dt.Rows.Count.ToString();
                    richTextBox1.Clear();

                    // Refresh the DataGridView using the updated DataTable
                    var updatedDataTable = await action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);
                }
            }

            // Initialize the CancellationTokenSource
            _cts = new CancellationTokenSource();

            // Check for existing table entries and start the background worker
            foreach (DataRow row in dt.Rows)
            {
                if (!row.IsNull("site") && !string.IsNullOrWhiteSpace(row["site"].ToString()))
                {
                    await PerformSiteCheckAsync(_cts.Token);
                }
            }
        }








        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {

                // Create a new CancellationTokenSource if it doesn't exist
                _cts ??= new CancellationTokenSource();


                // Cancel the current operation if it's running
                _cts?.Cancel();

                // Create a new CancellationTokenSource if it doesn't exist
                // Wait for a short delay to allow cancellation to propagate (optional)
                await Task.Delay(100);

                // Dispose the current CancellationTokenSource and create a new one


                // Perform the site check with the CancellationToken


                // Handle cancellation here (optional)
                if (_cts.IsCancellationRequested)
                {
                    Debug.WriteLine("Task cancelled.");
                    await PerformSiteCheckAsync(_cts.Token);
                    _cts?.Dispose();
                    _cts = new CancellationTokenSource();
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation here (optional)
                Debug.WriteLine("Task cancelled.");
            }
        }








        private async Task PerformSiteCheckAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();

                    // Iterate through all the sites in the DataTable
                    foreach (DataRow row in dt.Rows)
                    {
                        ct.ThrowIfCancellationRequested();

                        string site = row.Field<string>("site");

                        string lastCheckedTimeString = row.Field<string>("lastcheckedtime");
                        DateTime lastChecked = string.IsNullOrEmpty(lastCheckedTimeString)
                            ? DateTime.MinValue
                            : DateTime.ParseExact(lastCheckedTimeString, "HH:mm:ss", CultureInfo.InvariantCulture);

                        // Check if 5 minutes have passed since the last update
                        if (DateTime.Now.Subtract(lastChecked).TotalMinutes >= 5)
                        {
                            // Call the GetRssfeedAndCheckAsync method for each site
                            (DateTime updatedLastCheckedTime, bool isValid) = await action.GetRssfeedAndCheckAsync(site, dt, SiteRecord);

                            // Update the lastcheckedtime field with the new value
                            row["lastcheckedtime"] = updatedLastCheckedTime.ToString("HH:mm:ss");

                            // Refresh the DataGridView using the updated DataTable
                            await action.cleanInputRefreshDataTableAsInput(dt, dataGridView1, bindingSource);
                        }
                    }

                    // This line will delay the loop for a certain period (e.g., 5000 milliseconds or 5 seconds)
                    await Task.Delay(TimeSpan.FromSeconds(5), ct);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Task cancelled from inside async.");
                // Cleanup code here
            }
            catch (Exception ex)
            {
                // Handle other exceptions here
                Debug.WriteLine("Exception thrown: " + ex.Message);
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
