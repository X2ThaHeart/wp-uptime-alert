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

namespace wp_uptime_alert
{
    public delegate void DrawListViewItemEventHandler(object sender, DrawListViewItemEventArgs e);

    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        DataTable dtBlacklist = new DataTable();
        Actions action = new Actions();
        [NotNull]
        SiteRecord siterecord;



        public Form1()
        {
            InitializeComponent();

            //listView1.DrawItem += (sender, e) => action.listView1_DrawItem(sender, e);


            // Create a new SiteRecord object with the dt DataTable
            dt.Columns.Add("site");
            dt.Columns.Add("domainstatus");
            dt.Columns.Add("wordpressstatus");
            dt.Columns.Add("lastcheckedtime");

            siterecord = new SiteRecord(dt, dataGridView1);


        }

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




        //main start testing button
        private async void button3_Click(object sender, EventArgs e)
        {
            var site = "";
            string[] removeSpacesFirst = richTextBox1.Lines;

            if (!dt.Columns.Contains("site"))
            {
                //test if this clears the whole box each time a new site isn't found of already existing ones.
                dt.Clear();

                //DataTable dt = new DataTable();
                //dt.Columns.Add("site");
                //dt.Columns.Add("status");
                //dt.Columns.Add("lastcheckedtime");

            }

            if (!dtBlacklist.Columns.Contains("site"))
            {
                dtBlacklist.Clear();

                //DataTable dt = new DataTable();
                //dtBlacklist.Columns.Add("site");
                //dtBlacklist.Columns.Add("status");
                //dtBlacklist.Columns.Add("lastcheckedtime");

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



                    //DataRow blrow = dtBlacklist.NewRow();
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

                            //var rsswait = await action.GetRssfeedAndCheckAsync(site, dt, siterecord);

                            //if (await Task.WhenAny(rsswait, Task.Delay(10000)) == rsswait)
                            //{


                            //if (rsswait)
                            //{

                            site = action.cleanUrlFinal(site);


                            row["site"] = site;
                            dt.Rows.Add(row);
                            label5.Text = dt.Rows.Count.ToString();

                            // }






                        }

                        else if (filteredRows.Length == 1)
                        {
                            MessageBox.Show("Site already exists", "Error");
                            var rsswait = await action.GetRssfeedAndCheckAsync(site, dt, siterecord);

                            if (rsswait)
                            {
                                if (action.UrlValid == true)
                                {

                                    //site = action.cleanUrlFinal(site);


                                    //row["site"] = site;
                                    //dt.Rows.Add(row);
                                    label5.Text = dt.Rows.Count.ToString();


                                }

                            }
                        }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Site already exists in table " + (site), "Error");

                        //}

                    }

                }

            }








            MessageBoxWithDetails messageboxwithdetails = new MessageBoxWithDetails();

            //messageboxwithdetails.DialogBoxPopup(dt.Rows[1].ToString());


            //total_websites_label.Text = dt.Rows.Count.ToString();

            //action.cleanInputRefreshDataTableAsInput(dt, richTextBox1);

            await action.startTestingEachEntryInDataTableAsync(dt, lastCheckedActive_label, activeTestingSite_label, richTextBox1, dtBlacklist, siterecord);
            //await action.startTestingEachEntryInBlacklistAsync(dtBlacklist, label7, label11, dt, blacklistRichTextBox, siterecord);

            action.cleanInputRefreshDataTableAsInput(dt, dataGridView1);
            //action.cleanBlacklistViewUpdateInput(dtBlacklist, blacklistView);
            //action.updateListViewWithBlackList(dtBlacklist, blacklistRichTextBox, label7);
            //this makes the whole row a link so not suitable
            //blacklistRichTextBox.DetectUrls = true;


            action.updateWebsiteLabels(total_websites_label, label5, label7, dt, dtBlacklist);

            //int totalwebsites = action.calculateTotalWebsites(dt, dtBlacklist);

            //total_websites_label.Text = totalwebsites.ToString();

            richTextBox1.Clear();

        }

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
    }
}
