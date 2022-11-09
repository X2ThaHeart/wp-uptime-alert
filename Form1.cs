using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System;
using System.Data;
using wp_uptime_alert.controller;
using static System.Windows.Forms.Design.AxImporter;
using System.Security.Policy;

namespace wp_uptime_alert
{
    public partial class Form1 : Form
    {
        //correct place
        DataTable dt = new DataTable();
        Actions action = new Actions();

        public Form1()
        {
            InitializeComponent();
            



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

            var site = "";
            string[] removeSpacesFirst = richTextBox1.Lines;

            if (!dt.Columns.Contains("site"))
            {
                //DataTable dt = new DataTable();
                dt.Columns.Add("site");
                dt.Columns.Add("status");
                dt.Columns.Add("lastcheckeddate");

            }
            dt.Clear();




            for (int i = 0; i < removeSpacesFirst.Length; i++)
            {
                //if (!StringIsNewLine(removeSpacesFirst[i]))

                    if (removeSpacesFirst[i] == "\r\n" || removeSpacesFirst[i] == " " || removeSpacesFirst[i] == null || removeSpacesFirst[i].Length == 0)
                {


                }
                else
                {
                    site = removeSpacesFirst[i];
                    //successfully add item to datatable
                    DataRow row = dt.NewRow();

                    row["site"] = site;
                    dt.Rows.Add(row);
                }





            }
            getRssfeedAndCheckAsync(site);

            //    //foreach (DataRow row in (await rssFeedActive).AsEnumerable())
            //    //{
            //    //    if (row[0].GetType() == typeof(int))
            //    //    {
            //    //    }
            //    //}

            //    //formfields.loadProcessText($"debug: {rssFeedActive}", currentFunction_label);
            //    rssFeedCount.Content = rssFeedActive.Result.Rows.Count.ToString();
            //    if (rssFeedActive.Result.Rows.Count == 0)
            //    {


            //        

            //dt.Rows.Add(row);

            //foreach (string check in removeSpacesFirst)
            //{


            //}
            //string[] inputUrls = richTextBox1.Lines;



            //string[] lines = inputUrls.Split(
            //new string[] { Environment.NewLine },
            //StringSplitOptions.None
            //) ;

            //string test = "2345\n564532\n345634\n234 234543\n1324 2435\n";
            //string[] result = inputUrls.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


            //var result = inputUrls.Split(new string[] { "\\n" }, StringSplitOptions.None);





            //for (int i = 0; i < inputUrls.Length; i++)
            //{
            //    row[i] = inputUrls[i];

            //}




            MessageBoxWithDetails messageboxwithdetails = new MessageBoxWithDetails();

            //messageboxwithdetails.DialogBoxPopup(dt.Rows[1].ToString());


            total_websites_label.Text = dt.Rows.Count.ToString();
        }

        public string cleanRssUrl(string site)
        {

            var uriWithoutScheme = "";
            System.Uri uri = new Uri(site);
            uriWithoutScheme = uri.Host + uri.AbsolutePath;


            //site = uriWithoutScheme.Replace("www.", "");
            site = site.ToLower();
            if (site.EndsWith("/"))
            {
                site = site.Substring(0, site.Length - 1);
            }




            return site;
        }

        public async Task getRssfeedAndCheckAsync(string site)
        {


            Task<int> rssFeedActive = action.checkRssFeed(site);

            if (await Task.WhenAny(rssFeedActive, Task.Delay(10000)) == rssFeedActive)
            {
                MessageBox.Show("Value of rss feed is " + rssFeedActive.Result.ToString(), "check");

            }

        }
        
    }
}
