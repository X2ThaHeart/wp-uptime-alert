﻿using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;
using ListView = System.Windows.Forms.ListView;
using System.Drawing;
using static System.Windows.Forms.ListViewItem;
using System.Drawing.Imaging;
using System.Net.Sockets;
using DocumentFormat.OpenXml.Wordprocessing;
using wp_uptime_alert.model;
using DocumentFormat.OpenXml.Presentation;
using System.Globalization;
using System.Diagnostics;
using wp_uptime_alert.view;

namespace wp_uptime_alert.controller
{
    public class HtmlStatusIcon
    {
       


        public DataGridView MyDataGridView { get; }

        public HtmlStatusIcon(DataGridView dataGridView)
        {
            MyDataGridView = dataGridView;
        }




        public SiteRecord SiteRecord { get; set;}

        public HtmlStatusIcon(SiteRecord siteRecord)
        {
            SiteRecord = siteRecord;
        }

    }



    internal class Actions
    {

        public bool urlValid;

        public bool UrlValid
        { get { return urlValid; } set { urlValid = value; } }



        private static readonly object dtLock = new object();

        public string cleanUrlFinal(string site)
        {
            Uri outUri;

            if (Uri.TryCreate(site, UriKind.Absolute, out outUri)
                && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps))
            {
                string path = string.Join("", outUri.Segments.Skip(1));
                return path;
            }

            return site;
        }


        public async Task<int> checkRssFeed(string urlToCheck)
        {
            urlToCheck = urlToCheck + "/feed/";
            DataTable dataTable = new DataTable();
            try
            {
                var feed = await FeedReader.ReadAsync(urlToCheck);
                if (feed.Items.Count > 0)
                {
                    return feed.Items.Count;
                }
                else
                {
                    return 0;
                }

            }

            catch
            {
                return 0;
            }


           
        }


        public string FirstCleanRssUrl(string site)
        {


            Uri outUri;

            var uriWithScheme = "";
            if (Uri.TryCreate(site, UriKind.Absolute, out outUri)
            && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps))
            {
                //uriWithoutScheme = outUri.Host + outUri.AbsolutePath;
                //uriWithScheme = outUri.Scheme + "://" + outUri.Host;



                site = outUri.Host.ToLower();
                if (site.Contains("www."))
                {
                    site = site.Substring(4);

                }

                //site = site.Replace("www.", "");

                if (site.EndsWith("/"))
                {
                    site = site.Substring(0, site.Length - 1);
                }




                return site;
            }
            else
            {
                return site;


            }



        }




        public async Task<DataTable> cleanInputRefreshDataTableAsInput(DataTable datatable, DataGridView dataGridView, BindingSource bindingSource)
        {
            SiteRecord siteRecord = new SiteRecord();

            DataTable dtCopy;
            lock (dtLock)
            {
                dtCopy = datatable.Copy();
            }


            foreach (DataRow row in dtCopy.Rows)
            {
                string? site = row.Field<string?>("site");
                int domainResponseCode = ActivateServerResponse(site);

                string? lastCheckedTimeString = row.Field<string?>("lastcheckedtime");

                DateTime lastCheckedTime;

                if (lastCheckedTimeString != null)
                {
                    lastCheckedTime = DateTime.ParseExact(lastCheckedTimeString, "HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    //change made
                    lastCheckedTime = DateTime.ParseExact(DateTime.Now.ToString("HH:mm:ss"), "HH:mm:ss", CultureInfo.InvariantCulture);
                }

                // Convert the DateTime value to a formatted time string
                string formattedTime = lastCheckedTime.ToString("HH:mm:ss");

                //string lastCheckedTimeText = lastCheckedTime.ToString("HH:mm:ss");

                string domainStatus;
                switch (domainResponseCode)
                {
                    case 200: // Success
                        domainStatus = "Active".ToString();
                        break;
                    case 404: // Not Found
                        domainStatus = "Error : 404".ToString();
                        break;
                    case 503: // Service Unavailable
                        domainStatus = "Error : 503".ToString();
                        break;
                    default: // Other error codes
                        domainStatus = "Error".ToString();
                        break;
                }

                var rssFeedCheck = await GetRssfeedAndCheckAsync(site, datatable, siteRecord);
                var wordpressStatus = "";
                switch (rssFeedCheck)
                {
                    case (DateTime, true):
                        wordpressStatus = "Active";
                        break;

                    case (DateTime, false):
                        wordpressStatus = "Error";
                        break;

                    default:
                        wordpressStatus = "Unknown";
                        break;
                }

                row["domainstatus"] = domainStatus;
                row["wordpressstatus"] = wordpressStatus;
                row["lastcheckedtime"] = formattedTime; // format the datetime as string
            }


            // Update the original DataTable with the modified copy
            lock (dtLock)
            {
                datatable = dtCopy.Copy();
            }


            //siteRecord.InitializeDataGridViewColumns(dataGridView);
            //dataGridView.DataSource = bindingSource;

            //this was used while working ok
            //dataGridView.DataSource = datatable;


            // Create a DataView from the DataTable
            DataView dv = new DataView(datatable);

            // Set the sort expression for the DataView
            dv.Sort = "domainstatus DESC, wordpressstatus DESC, lastcheckedtime ASC";

            // Set the DataGridView's DataSource to the DataView
            dataGridView.DataSource = dv;

            // Rebind the DataGridView to the updated DataView
            bindingSource.DataSource = dv;
            dataGridView.DataSource = bindingSource;



            //this is new from CustomRowComparer class
            //dataGridView.Sort(new CustomRowComparer(dataGridView));

            // Refresh the DataGridView using the updated DataTable
            dataGridView.Refresh();

            return datatable;
        }


        public void InitializeDataGridViewColumns(DataGridView dataGridView)
        {
            dataGridView.Columns.Add("siteColumn", "Site");
            dataGridView.Columns.Add("domainStatusColumn", "Domain Status");
            dataGridView.Columns.Add("wordpressStatusColumn", "WordPress Status");
            dataGridView.Columns.Add("lastCheckedTimeColumn", "Last Checked Time");

            // Set the format of the "lastcheckedtime" column to show only the time part
            dataGridView.Columns["lastCheckedTimeColumn"].DefaultCellStyle.Format = "t";
        }


        private DataRow CreateNewDataRow(DataTable table, string site, string domainStatus, string wordpressStatus, DateTime lastCheckedTime)
        {
            DataRow row = table.NewRow();
            row["site"] = site;
            row["domainstatus"] = domainStatus;
            row["wordpressstatus"] = wordpressStatus;
            row["lastcheckedtime"] = lastCheckedTime; // format the datetime as string
            return row;
        }





        public void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            // Check if it's the first cell (column index 0 and row index 0)
            if (e.ColumnIndex == 0 && e.RowIndex == 0)
            {
                e.CellStyle.BackColor = System.Drawing.Color.White;
            }

            if (gridView != null)
            {
                // Check if the current cell belongs to the "domainstatus" or "wordpressstatus" column
                if (e.ColumnIndex == gridView.Columns[1].Index || e.ColumnIndex == gridView.Columns[2].Index)
                {
                    // Get the cell value
                    string status = e.Value as string;

                    if (status != null)
                    {
                        // Set the cell background color based on the status value
                        if (status == "Active")
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        }
                        else if (status.StartsWith("Error"))
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.Gray;
                        }

                        // Set the SelectionBackColor and SelectionForeColor
                        e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
                        e.CellStyle.SelectionForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }




        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView != null && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1) // Domain status column
                {
                    string? domainStatus = dataGridView.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";

                    if (domainStatus.Contains("Error"))
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                else if (e.ColumnIndex == 2) // WordPress status column
                {
                    string wordpressStatus = dataGridView.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";

                    if (wordpressStatus == "Error")
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
                else if (e.ColumnIndex == 0 || e.ColumnIndex == 3) // Other columns
                {
                    e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }




        private void YourListView_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            // Set the text color
            e.Item.UseItemStyleForSubItems = false;
            e.SubItem.ForeColor = e.Item.ForeColor;

            // Draw the background color
            using (SolidBrush backgroundBrush = new SolidBrush(e.SubItem.BackColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Draw the text
            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, e.Bounds, e.SubItem.ForeColor, flags);
        }

        private void YourListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }











        public void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Get the response code from the subitem at index 1
            int responseCode = int.Parse(e.Item.SubItems[1].Text);

            // Set the background color of the cell based on the response code
            switch (responseCode)
            {
                case 200: // Success
                    e.Item.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case 404: // Not Found
                case 503: // Service Unavailable
                    e.Item.BackColor = System.Drawing.Color.LightCoral;
                    break;
                default: // Other error codes
                    e.Item.BackColor = System.Drawing.Color.LightGray;
                    break;
            }
        }









       

        void outToLog(string output, string? v, RichTextBox richTextBox1_Text )
        {
            //richTextBox1_Text.AppendText(output + "\r\n");
            if (output.Length > 0)
            {
                richTextBox1_Text.AppendText(output + " : " +  v + "\r\n");
                richTextBox1_Text.ScrollToCaret();
            }
           
        }


        public async Task<(DateTime LastCheckedTime, bool IsValid)> GetRssfeedAndCheckAsync(string site, DataTable dt, SiteRecord SiteRecord)
        {
            try
            {
                bool UrlValid = false;

                // Call checkRssFeed asynchronously
                int rssFeedResult = await checkRssFeed(site);

                if (rssFeedResult == 0)
                {
                    // No RSS feed found, mark the URL as invalid
                    SiteRecord.SiteAddress = site;
                    UrlValid = false;
                }
                else if (dt.Columns.Contains("site"))
                {
                    // RSS feed found, mark the URL as valid
                    UrlValid = true;
                }
                else
                {
                    // Invalid URL
                    UrlValid = false;
                }

                // Perform other operations if required (e.g., updating the SiteRecord)

                // Return the tuple with updated lastcheckedtime and the UrlValid flag
                return (DateTime.Now, UrlValid);
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Debug.WriteLine("Error in getRssfeedAndCheckAsync: " + ex.Message);

                // Return the minimum DateTime value and false in case of an error
                return (DateTime.MinValue, false);
            }
        }







        public async Task startTestingEachEntryInDataTableAsync(DataTable dt, Label lastCheckedActive_label, Label activeTestingSite_label, SiteRecord siteRecord)
        {

            foreach (DataRow row in dt.Rows)
            {
                string site = row["site"].ToString();
                DateTime lastModified = new DateTime();
          
                
                var approved_by = (row["lastcheckedtime"].ToString() ?? "");

                if (approved_by != "")
                {
                    string check = (string)row["lastcheckedtime"];
                    lastModified = DateTime.ParseExact(check, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);


                }
                else
                {
                 

                }


           
                //}
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(site);
                    if (hostEntry.AddressList.Length > 0)
                    {


                        activeTestingSite_label.Text = row["site"].ToString();
                        //_ = getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistView);
                        //check server response status
                        var serverResponseCode = ActivateServerResponse(site);


                        if (serverResponseCode == 200)
                        {
                            await GetRssfeedAndCheckAsync(site, dt, siteRecord);

                            siteRecord.LastCheckedTime = DateTime.Now.ToLongTimeString();
                            lastCheckedActive_label.Text = siteRecord.LastCheckedTime.ToString();
                            //row["domainstatus"] = GetServerStatusIcon(serverResponseCode);
                            //row["domainstatus"] = 200;


                        }
                        else
                        {
                            siteRecord.LastCheckedTime = DateTime.Now.ToLongTimeString();
                            lastCheckedActive_label.Text = "Last Checked Time";
                            siteRecord.DomainStatus = "Error : " + serverResponseCode.ToString();
                        }

                    }
                        //dt.AcceptChanges();

                        activeTestingSite_label.Text = row["site"].ToString();

                    
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.HostNotFound)
                    {
                        MessageBox.Show("The domain " + site + " you entered is invalid, please check the spelling and try again", "Error");
                    }
                }


              

                

            }
            

        }




        public int ActivateServerResponse(string serverUrl, int maxRetries = 3)
        {
            UriBuilder uriBuilder = new UriBuilder(serverUrl);
            if (uriBuilder.Scheme == "")
            {
                uriBuilder.Scheme = "http"; // default to http if no scheme is provided
            }

            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
                    request.Timeout = 5000; // 5 seconds timeout
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    return (int)response.StatusCode;
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse errorResponse)
                    {
                        Debug.WriteLine("error caught inside ActivateServerresponse WebException error");
                        return (int)errorResponse.StatusCode;
                    }
                    else if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        // Increment the retry counter
                        retryCount++;

                        // If we have reached the maximum number of retries, return an error code or throw a custom exception
                        if (retryCount == maxRetries)
                        {
                            // You can either return an error code or throw a custom exception
                            // Example: return -1 to indicate a timeout error after retries
                            return -1;
                        }
                    }
                }
            }

            // This should not be reached, but it's included to satisfy the method's return type requirement
            return -1;
        }








        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }



        public int calculateTotalWebsites(DataTable dt, DataTable dtBlacklist)
        {
            int totalWebsites = 0;

            totalWebsites = dt.Rows.Count;
            totalWebsites += dtBlacklist.Rows.Count;

            return totalWebsites;

        }

      


        public int calculateActiveWebsites(DataTable dt) 
        {
            int totalActiveWebsites = 0;

            totalActiveWebsites = dt.Rows.Count;

            return totalActiveWebsites;
        }




       




    }
}
