using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System;
using System.Data;
using wp_uptime_alert.controller;
using static System.Windows.Forms.Design.AxImporter;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace wp_uptime_alert
{
    public partial class Form1 : Form
    {
        //correct place
        DataTable dt = new DataTable();
        DataTable dtBlacklist = new DataTable();
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


        }

       

        
        //main start testing button
        private async void button3_Click(object sender, EventArgs e)
        {
            var site = "";
            string[] removeSpacesFirst = richTextBox1.Lines;

            if (!dt.Columns.Contains("site"))
            {
                dt.Clear();
                //DataTable dt = new DataTable();
                dt.Columns.Add("site");
                dt.Columns.Add("lastcheckedtime");

            }

            if (!dtBlacklist.Columns.Contains("site"))
            {
                dtBlacklist.Clear();
                //DataTable dt = new DataTable();
                dtBlacklist.Columns.Add("site");
                dtBlacklist.Columns.Add("lastcheckedtime");

            }



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
                    DataRow blrow = dtBlacklist.NewRow();
                    if (site == null)
                    {
                        //site = action.FirstCleanRssUrl(site);

                        MessageBox.Show("site entered is null ", "Error");

                    }
                    else
                    {
                        site = action.FirstCleanRssUrl(site);

                        DataRow[] filteredRows =
                        dt.Select(string.Format("{0} LIKE '%{1}%'", "site", site));

                        if (filteredRows.Length == 0)
                        {

                            var rsswait = action.getRssfeedAndCheckAsync(site, label7, dtBlacklist, blacklistView);

                            if (await Task.WhenAny(rsswait, Task.Delay(10000)) == rsswait)
                            {
                                if (rsswait.IsCompleted)
                                {
                                    if (action.UrlValid == true)
                                    {

                                        site = action.cleanUrlFinal(site);
                                        row["site"] = site;
                                        dt.Rows.Add(row);
                                        label5.Text = dt.Rows.Count.ToString();


                                    }

                                    if (action.urlValid == false)
                                    {
                                        site = action.cleanUrlFinal(site);
                                        blrow["site"] = site;
                                        dtBlacklist.Rows.Add(blrow);
                                        label7.Text = dtBlacklist.Rows.Count.ToString();
                                    }
                                }

                            }

                            else
                            {
                                MessageBox.Show("Invalid Site Entered - last button click window ", "Error");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Site already exists in table " + (site), "Error");

                        }

                    }                 
                    
                }

            }

            action.updateListViewWithBlackList(dtBlacklist, blacklistView, label7);


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

            //action.cleanInputRefreshDataTableAsInput(dt, richTextBox1);

            action.startTestingEachEntryInDataTable(dt, label7, dtBlacklist, blacklistView, label13, lastCheckTime_label);
            action.cleanInputRefreshDataTableAsInput(dt, richTextBox1);


        }
    }
}
