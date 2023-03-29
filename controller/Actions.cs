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

namespace wp_uptime_alert.controller
{
    public class HtmlStatusIcon
    {
        //private const int IconSize = 30;

        //public static string GetHtml(int responseCode)
        //{
        //    Color color;
        //    switch (responseCode)
        //    {
        //        case 200: // Success
        //            color = Color.Green;
        //            break;
        //        case 404: // Not Found
        //        case 503: // Service Unavailable
        //            color = Color.Red;
        //            break;
        //        default: // Other error codes
        //            color = Color.Gray;
        //            break;
        //    }

        //    string html = $"<div style=\"display: inline-block; width: {IconSize}px; height: {IconSize}px; border-radius: 50%; background-color: {ColorTranslator.ToHtml(color)};\"></div>";
        //    return html;
        //}
    }



    internal class Actions
    {

        public bool urlValid;

        public bool UrlValid
        { get { return urlValid; } set { urlValid = value; } }


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




        public async void cleanInputRefreshDataTableAsInput(DataTable datatable, ListView listView)
        {
            // Clear the ListView
            listView.Clear();




            // Add column headers
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = System.Windows.Forms.View.Details;

            SiteRecord siteRecord = new SiteRecord();
            // Add columns to the ListView control
            siteRecord.AddColumnNamesInListView(listView);

            if (datatable.Rows.Count == 0)
            {
                // Add a single empty row to the ListView
                ListViewItem item = new ListViewItem(new string[] { "", "", "", "" });
                listView.Items.Add(item);

                // alert for no data has been entered
            }
            else
            {
                // Add the rows from the DataTable to the ListView
                foreach (DataRow row in datatable.Rows)
                {
                    string? site = row.Field<string>("site");
                    int domainResponseCode = ActivateServerResponse(site);
                    //string? wordpressStatus = row.Field<string>("wordpressstatus");

                    DateTime lastCheckedTime = DateTime.Now;
                    if (row["lastcheckedtime"] != DBNull.Value)
                    {
                        lastCheckedTime = Convert.ToDateTime(row["lastcheckedtime"]);
                    }
                    string lastCheckedTimeText = lastCheckedTime.ToString("HH:mm:ss");


                    string domainStatus;

                    // Determine the appropriate background color based on the domain response code
                    System.Drawing.Color backgroundColor;
                    switch (domainResponseCode)
                    {
                        case 200: // Success
                            domainStatus = "Active".ToString();
                            backgroundColor = System.Drawing.Color.LightGreen;
                            break;
                        case 404: // Not Found
                            domainStatus = "Error : 404".ToString();

                            backgroundColor = System.Drawing.Color.Red;
                            break;

                        case 503: // Service Unavailable
                            domainStatus = "Error : 503".ToString();

                            backgroundColor = System.Drawing.Color.Red;
                            break;
                        default: // Other error codes
                            domainStatus = "Error".ToString();

                            backgroundColor = System.Drawing.Color.Gray;
                            
                            break;
                    }

                    var rssFeedCheck = await GetRssfeedAndCheckAsync(site, datatable, siteRecord);
                    var wordpressStatus = "";
                    switch (rssFeedCheck)
                    {
                        case true:
                            wordpressStatus = "Active";
                            backgroundColor = System.Drawing.Color.LightGreen;

                            break;
                        case false:
                            wordpressStatus = "Error";
                            backgroundColor = System.Drawing.Color.Red;

                            break;
                        default: // Other error codes
                            wordpressStatus = "Error".ToString();

                            backgroundColor = System.Drawing.Color.Gray;

                            break;


                    }
                    // Create a new ListViewItem object with the row data and set the background color of each subitem
                    ListViewItem item = new ListViewItem(new string[] { site, domainStatus, wordpressStatus, lastCheckedTimeText });
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        item.SubItems[i].BackColor = backgroundColor;
                    }

                    // Add the new ListViewItem object to the Items collection of the ListView control
                    listView.Items.Add(item);
                }
            }
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






        void outToLog2(string output, string? v, RichTextBox richTextBox1_Text)
        {
            //richTextBox1_Text.AppendText(output + "\r\n");
            if (output.Length > 0)
            {
                richTextBox1_Text.AppendText(output + " : " + v + "\r\n");
                richTextBox1_Text.ScrollToCaret();
            }




            //ListViewItem site = new ListViewItem(row[0].ToString(), row[1].ToString());


            //for (int i = 1; i < dtBlacklist.Columns.Count; i++)
            //{
            //    site.SubItems.Add(row[i].ToString());

            //    // item.Text = row[i].ToString();
            //}
            //blacklistView.Items.Add(site);




        }



        //not required really
        public void cleanBlacklistViewUpdateInput(DataTable dtBlacklist, ListView blacklistView)
        {
            


            




            //for (int i = 0; i < dtBlacklist.Rows.Count; i++)
            //{
                
            //    blacklistView.Items[i] = dtBlacklist.Rows[i][1].ToString();
            //}
                


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


        //runs after entry in url box
        public async Task<bool> GetRssfeedAndCheckAsync(string site, DataTable dt, SiteRecord siteRecord)
        {
            try
            {
                UrlValid = false;

                // Call checkRssFeed asynchronously
                int rssFeedResult = await checkRssFeed(site);

                if (rssFeedResult == 0)
                {
                    // No RSS feed found, mark the URL as invalid
                    siteRecord.SiteAddress = site;
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

                return UrlValid;
            }
            catch (Exception ex)
            {
                // Log or display the error message
                Console.WriteLine("Error in getRssfeedAndCheckAsync: " + ex.Message);
                return false;
            }
        }


        public void updateListViewWithBlackList(DataTable dtBlacklist, RichTextBox blacklistRichTextBox, Label label7)
        {

            blacklistRichTextBox.Clear();

            foreach (DataRow row in dtBlacklist.Rows)
            {
                outToLog(row[0].ToString(), row[1].ToString(), blacklistRichTextBox);

                //foreach (DataColumn column in datatable.Columns)
                //{
                //    outToLog(row[0].ToString(), row[1].ToString(), richTextBox1_Text);
                //}
                blacklistRichTextBox.Rtf = blacklistRichTextBox.Rtf.Replace("@@@", row[0].ToString());

            }


            blacklistRichTextBox.ReadOnly = true;





            //blacklistView.Items.Clear();

            //string[] Str = new string[2];
            //ListViewItem newItm;
            //foreach (DataRow dataRow in dtBlacklist.Rows)
            //{
            //    Str[0] = dataRow["site"].ToString();
            //    Str[1] = dataRow["lastcheckedtime"].ToString();
            //    //Str[1] = dataRow["lastcheckeddate"].ToString();
            //    //Str[2] = dataRow["Mobile"].ToString();
            //    //newItm = new ListViewItem(Str);
            //    blacklistView.Items.Add(Str[0] + " : " + Str[1]);
            //    label7.Text = dtBlacklist.Rows.Count.ToString();


            //    blacklistView.HotTracking = true;



            //}
        }


        public async Task startTestingEachEntryInBlacklistAsync(DataTable dtBlacklist, Label label7, Label label11, DataTable dt, RichTextBox blacklistRichTextBox, SiteRecord siteRecord)
        {
            foreach (DataRow row in dtBlacklist.Rows)
            {
                string site = row["site"].ToString();
                DateTime lastModified = new DateTime();
                //lastModified  = DateTime.UtcNow;
                //bool alreadyChecked = (bool)row["lastcheckedtime"] ? string.Empty : (string)row["lastcheckedtime"]))
                var approved_by = (row["lastcheckedtime"].ToString() ?? row["lastcheckedtime"]);

                if (approved_by != "")
                {
                    string check = (string)row["lastcheckedtime"];
                    lastModified = DateTime.ParseExact(check, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    

                }


                //if (dt.AsEnumerable().Any(row => (DateTime)row["lastcheckedtime"] ?? row.Field<DateTime>("lastcheckedtime"))){
                //    lastModified = (DateTime)row["lastcheckedtime"];
                //}
                //else
                //{

                //}


                if (DateTime.Now > lastModified.AddMinutes(5))
                {
                    //label13.Text = row["site"].ToString();
                    //_ = getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistView);
                    var UrlValid = await GetRssfeedAndCheckAsync(site, dt, siteRecord);


                    if (UrlValid)
                    {

                        row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm:ss");
                        label11.Text = row["lastcheckedtime"].ToString();
                    }
                    else
                    {
                        row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm:ss");

                    }
                }
                else
                {
                    //MessageBox.Show("Waiting for 5 minutes between tests for error sites ", "Checking");
                    
                }

            }
        }
        public async Task startTestingEachEntryInDataTableAsync(DataTable dt,  Label lastCheckedActive_label, Label activeTestingSite_label, RichTextBox richTextBox1, DataTable dtBlacklist, SiteRecord siteRecord) 
        {
            
            foreach (DataRow row in dt.Rows)
            {
                string site = row["site"].ToString();
                DateTime lastModified = new DateTime();
                //lastModified  = DateTime.UtcNow;
                //bool alreadyChecked = (bool)row["lastcheckedtime"] ? string.Empty : (string)row["lastcheckedtime"]))
                
                //in the row check to see if lastcheckedtime var is avaiable, if not
                var approved_by = (row["lastcheckedtime"].ToString() ?? "");

                if (approved_by != "")
                {
                    string check = (string)row["lastcheckedtime"];
                    lastModified = DateTime.ParseExact(check, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    //lastModified = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                    //lastModified = DateTime.ParseExact(check, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                }


                //if (dt.AsEnumerable().Any(row => (DateTime)row["lastcheckedtime"] ?? row.Field<DateTime>("lastcheckedtime"))){
                //    lastModified = (DateTime)row["lastcheckedtime"];
                //}
                //else
                //{

                //}
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(site);
                    if (hostEntry.AddressList.Length > 0)
                    {
                        if (DateTime.Now > lastModified.AddMinutes(5))
                        {
                            activeTestingSite_label.Text = row["site"].ToString();
                            //_ = getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistView);
                            //check server response status
                            var serverResponseCode = ActivateServerResponse(site);


                            if (serverResponseCode == 200)
                            {
                                await GetRssfeedAndCheckAsync(site, dt, siteRecord);

                                row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm:ss");
                                lastCheckedActive_label.Text = row["lastcheckedtime"].ToString();
                                //row["domainstatus"] = GetServerStatusIcon(serverResponseCode);
                                //row["domainstatus"] = 200;


                            }
                            else
                            {
                                row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm:ss");
                                lastCheckedActive_label.Text = row["lastcheckedtime"].ToString();
                                row["serverstatus"] = "Error : " + serverResponseCode.ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Waiting for 5 minutes between tests ", "Checking");
                        }
                        dt.AcceptChanges();

                        activeTestingSite_label.Text = row["site"].ToString();
                    }
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

        public int ActivateServerResponse(string serverUrl)
        {
            UriBuilder uriBuilder = new UriBuilder(serverUrl);
            if (uriBuilder.Scheme == "")
            {
                uriBuilder.Scheme = "http"; // default to http if no scheme is provided
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
            request.Timeout = 5000; // 5 seconds timeout
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return (int)response.StatusCode;
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    return (int)errorResponse.StatusCode;
                }
                else
                {
                    throw;
                }
            }
        }

        private byte[] GetServerStatusIcon(int responseCode)
        {
            // Create circle image
            using (Bitmap image = new Bitmap(30, 30))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(System.Drawing.Color.Transparent);
                    using (SolidBrush brush = new SolidBrush(System.Drawing.Color.Gray))
                    {
                        switch (responseCode)
                        {
                            case 200: // Success
                                brush.Color = System.Drawing.Color.Green;
                                break;
                            case 404: // Not Found
                            case 503: // Service Unavailable
                                brush.Color = System.Drawing.Color.Red;
                                break;
                        }
                        g.FillEllipse(brush, new Rectangle(0, 0, 30, 30));
                    }
                }

                // Convert the Bitmap image to a byte array
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }




        private Bitmap GetCircleImage(int diameter, System.Drawing.Color color)
        {
            Bitmap image = new Bitmap(diameter, diameter);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(System.Drawing.Color.Transparent);
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, new Rectangle(0, 0, diameter, diameter));
                }
            }
            return image;
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

        public int calculateErrorWebsites(DataTable dtBlacklist) 
        {

            int totalBlacklistWebsites = 0;

            totalBlacklistWebsites = dtBlacklist.Rows.Count;

            return totalBlacklistWebsites;
        }


        public int calculateActiveWebsites(DataTable dt) 
        {
            int totalActiveWebsites = 0;

            totalActiveWebsites = dt.Rows.Count;

            return totalActiveWebsites;
        }




        public void updateWebsiteLabels(Label total_websites_label, Label label5, Label label7, DataTable dt, DataTable dtBlacklist)
        {
            total_websites_label.Text = calculateTotalWebsites(dt, dtBlacklist).ToString();
            label7.Text = calculateErrorWebsites(dtBlacklist).ToString();
            label5.Text = calculateActiveWebsites(dt).ToString();


        }
    }
}
