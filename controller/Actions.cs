using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace wp_uptime_alert.controller
{
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

                //site = outUri.Host;
                //site = site.Replace("www.", "");

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
                uriWithScheme = outUri.Scheme + "://" + outUri.Host;



                site = uriWithScheme.ToLower();
                //site = site.Replace("www.", "");

                if (site.EndsWith("/"))
                {
                    site = site.Substring(0, site.Length - 1);
                }




                return site;
            }
            else
            {
                return null;


            }



        }




        public void cleanInputRefreshDataTableAsInput(DataTable datatable, RichTextBox richTextBox1_Text, DataTable dtBlacklist, RichTextBox blacklistRichTextBox)
        {

            richTextBox1_Text.Clear();

            foreach (DataRow row in datatable.Rows)
            {
                outToLog(row[0].ToString(), row[1].ToString(), richTextBox1_Text);

                //foreach (DataColumn column in datatable.Columns)
                //{
                //    outToLog(row[0].ToString(), row[1].ToString(), richTextBox1_Text);
                //}
            }

            //blacklistRichTextBox.Clear();

            ////for (int i = 0; i < dtBlacklist.Rows.Count; i++)
            ////{
            ////    ListViewItem row = new ListViewItem(dtBlacklist.Rows[i][0].ToString());
            ////    for (int j = 1; j < dtBlacklist.Columns.Count; j++)
            ////    {
            ////        row.SubItems.Add(dtBlacklist.Rows[i][j].ToString());

            ////    }
            ////    blacklistView.Items.Add(row);
            ////}


            //foreach (DataRow row in dtBlacklist.Rows)
            //{

            //    blacklistRichTextBox.Add(row[0].ToString());

            //}

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



        public async Task getRssfeedAndCheckAsync(string site, Label label7, DataTable dtBlacklist, RichTextBox blacklistRichTextBox)
        {
            UrlValid = false;

            Task<int> rssFeedActive = checkRssFeed(site);
            wait(3000);
            if (await Task.WhenAny(rssFeedActive, Task.Delay(10000)) == rssFeedActive)
            {
                

                if (rssFeedActive.IsCompleted)
                {

                    if (await rssFeedActive == 0)
                    {
                        //no rss feed has been found so url is invalid add to blacklist
                        if (!dtBlacklist.Columns.Contains("site"))
                        {
                            //DataTable dt = new DataTable();
                            dtBlacklist.Columns.Add("site");
                            //dtBlacklist.Columns.Add("status");
                            dtBlacklist.Columns.Add("lastcheckeddate");

                        }
                        dtBlacklist.Rows.Add(site);
                        //label for inactive sites list

                        
                        UrlValid = false;


                        //foreach (DataRow row in dtBlacklist.Rows)
                        //{
                        //    ListViewItem item = new ListViewItem(row[0].ToString());
                        //    MessageBox.Show("inside row for list view  " + item.ToString(), "check");

                        //    for (int i = 1; i < dtBlacklist.Columns.Count; i++)
                        //    {
                        //        item.SubItems.Add(row["site"].ToString());
                        //        item.SubItems.Add(row["status"].ToString());
                        //        item.SubItems.Add(row["lastcheckeddate"].ToString());

                        //    }

                        //    blacklistView.Items.Add(item);

                        //}






                    }
                    else
                    {
                        //MessageBox.Show("Total feed count  " + rssFeedActive.Result.ToString(), "check");

                        UrlValid = true;
                    }


                }
                else
                {
                    UrlValid = false;

                }

            }
            else
            {
                UrlValid = false;

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


        public void startTestingEachEntryInBlacklist(DataTable dtBlacklist, Label label7, Label label11, RichTextBox blacklistRichTextBox)
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
                    lastModified = DateTime.ParseExact(check, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    string check = DateTime.Now.ToString("HH:mm");
                    row["lastcheckedtime"] = check;
                    lastModified = DateTime.ParseExact(check, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

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
                    _ = getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistRichTextBox);


                    if (UrlValid)
                    {

                        row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm");
                        label11.Text = row["lastcheckedtime"].ToString();
                    }
                    else
                    {
                        row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm");

                    }
                }
                else
                {
                    MessageBox.Show("Waiting for 5 minutes between tests for error sites ", "Checking");
                    
                }

            }
        }
        public void startTestingEachEntryInDataTable(DataTable dt, DataTable dtBlacklist, RichTextBox blacklistRichTextBox, Label label13, Label label7, Label lastCheckTime_label, Label label11) 
        {
            
            foreach (DataRow row in dt.Rows)
            {
                string site = row["site"].ToString();
                DateTime lastModified = new DateTime();
                //lastModified  = DateTime.UtcNow;
                //bool alreadyChecked = (bool)row["lastcheckedtime"] ? string.Empty : (string)row["lastcheckedtime"]))
                var approved_by = (row["lastcheckedtime"].ToString() ?? row["lastcheckedtime"]);

                if (approved_by != "")
                {
                    string check = (string)row["lastcheckedtime"];
                    lastModified = DateTime.ParseExact(check, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
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
                    label13.Text = row["site"].ToString();
                    //_ = getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistView);
                    _ = getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistRichTextBox);


                    if (UrlValid)
                    {

                        row["lastcheckedtime"] = DateTime.Now.ToString("HH:mm");
                        lastCheckTime_label.Text = row["lastcheckedtime"].ToString();
                    }
                    else
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Waiting for 5 minutes between tests ", "Checking");
                }

            }

           
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

    }
}
