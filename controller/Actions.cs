using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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




        public void cleanInputRefreshDataTableAsInput(DataTable datatable, RichTextBox richTextBox1_Text)
        {

            richTextBox1_Text.Clear();

            foreach (DataRow row in datatable.Rows)
            {
                foreach (DataColumn column in datatable.Columns)
                {
                    //Console.WriteLine(row[column]);
                    outToLog(row[column].ToString(), richTextBox1_Text);
                }
            }



        }


        void outToLog(string output, RichTextBox richTextBox1_Text )
        {
            //richTextBox1_Text.AppendText(output + "\r\n");
            if (output.Length > 0)
            {
                richTextBox1_Text.AppendText(output + "\r\n");
                richTextBox1_Text.ScrollToCaret();
            }
           
        }



        public async Task getRssfeedAndCheckAsync(string site, Label label7, DataTable dtBlacklist, ListView blacklistView)
        {


            Task<int> rssFeedActive = checkRssFeed(site);

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
                        MessageBox.Show("Total feed count  " + rssFeedActive.Result.ToString(), "check");

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


        public void updateListViewWithBlackList(DataTable dtBlacklist, ListView blacklistView, Label label7)
        {
            string[] Str = new string[2];
            ListViewItem newItm;
            foreach (DataRow dataRow in dtBlacklist.Rows)
            {
                Str[0] = dataRow["site"].ToString();
                //Str[1] = dataRow["lastcheckeddate"].ToString();
                //Str[2] = dataRow["Mobile"].ToString();
                newItm = new ListViewItem(Str);
                blacklistView.Items.Add(newItm);
                label7.Text = dtBlacklist.Rows.Count.ToString();
            }
        }


    }
}
