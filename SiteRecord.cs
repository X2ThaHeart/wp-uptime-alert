using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace wp_uptime_alert
{
    public class SiteRecord
    {
        private string siteaddress;
        private string domainStatus;
        private string wpStatus;
        private DateTime lastcheckedtime;
        private DataTable list;

        public string SiteAddress { get => siteaddress; set => siteaddress = value; }
        public string DomainStatus { get => domainStatus; set => domainStatus = value; }
        public string WpStatus { get => wpStatus; set => wpStatus = value; }
        public string LastCheckedTime { get => lastcheckedtime.ToString(); set => lastcheckedtime.ToString(); }

        public SiteRecord(DataTable list, DataGridView dataGridView)
        {
            // Create a new DataTable with the desired schema
            DataTable newDt = new DataTable();

            // Add the columns to the new DataTable
            newDt.Columns.Add("Site address");
            newDt.Columns.Add("Server");
            newDt.Columns.Add("WordPress");
            newDt.Columns.Add("Last Checked");

            // Add columns to the ListView control
            //AddColumnNamesInListView(listView);

            // Add column headers
            //listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            //listView.View = View.Details;



            if (list.Rows.Count == 0)
            {
                // Add a single empty row to the data table
                DataRow emptyRow = newDt.NewRow();
                newDt.Rows.Add(emptyRow);
            }
            else
            {
                // Add the data to the new DataTable
                foreach (DataRow row in list.Rows)
                {
                    DataRow newRow = newDt.NewRow();
                    newRow["Site address"] = row["site"];
                    newRow["Server"] = row["domainstatus"];
                    newRow["WordPress"] = row["wordpressstatus"];
                    newRow["Last Checked"] = row["lastcheckedtime"];
                    newDt.Rows.Add(newRow);
                }
            }

            // Add columns to the ListView control
            //listView.Columns.Add("Site address", 150);
            //listView.Columns.Add("Domain Status", 150);
            //listView.Columns.Add("WordPress Status", 150);
            //listView.Columns.Add("Last Checked Time", 150);

            // Add data rows to the ListView control
            foreach (DataRow row in newDt.Rows)
            {
                ListViewItem item = new ListViewItem(row["Site address"].ToString());
                item.SubItems.Add(row["Server"].ToString());
                item.SubItems.Add(row["WordPress"].ToString());
                item.SubItems.Add(row["Last Checked"].ToString());
                //listView.Items.Add(item);
            }
        }

        public void AddColumnNamesInListView(ListView listView)
        {
            listView.Columns.Add("Site address", 300);
            listView.Columns.Add("Server", 80);
            listView.Columns.Add("WordPress", 80);
            listView.Columns.Add("Last Checked", 95).Tag = typeof(DateTime);
        }

        //This extra constructor is required to allow "SiteRecord siteRecord = new SiteRecord();" to work in form.cs
        public SiteRecord()
        {
            DataTable dt = new DataTable();
        }

        public SiteRecord(DataTable list)
        {
            this.list = list;
        }


        //public void SetListViewColumns(ListView listView)
        //{
        //    // Add columns to the ListView control
        //    listView.Columns.Add("Site address", 150);
        //    listView.Columns.Add("Domain Status", 150);
        //    listView.Columns.Add("WordPress Status", 150);
        //    listView.Columns.Add("Last Checked", 150);
        //}




        public static DataTable assignAllRowsToReturnDatatableList()
        {
            DataTable list = new DataTable();

            SiteRecord siteRecord = new SiteRecord(list);

            var row = list.NewRow();

            row["Site address"] = siteRecord.SiteAddress;
            row["Domain Status"] = siteRecord.DomainStatus;
            row["WordPress Status"] = siteRecord.WpStatus;
            row["Last Checked Time"] = siteRecord.LastCheckedTime;

            list.Rows.Add(row);

            return list;
        }




        //public void AddColumnNamesInDataGridView(DataGridView dataGridView)
        //{
        //    dataGridView.Columns.Add("site", "Site");
        //    dataGridView.Columns.Add("domainstatus", "Domain Status");
        //    dataGridView.Columns.Add("wordpressstatus", "WordPress Status");
        //    dataGridView.Columns.Add("lastcheckedtime", "Last Checked Time");
        //}


        public void AddColumnNamesInDataGridView(DataGridView dataGridView)
        {
            // Create and configure columns
            DataGridViewColumn siteColumn = new DataGridViewTextBoxColumn
            {
                Name = "site",
                HeaderText = "Site",
                Width = 270, // Set the width of the 'Site' column
            };

            DataGridViewColumn domainStatusColumn = new DataGridViewTextBoxColumn
            {
                Name = "domainstatus",
                HeaderText = "Domain Status",
                Width = 80, // Set the width of the 'Domain Status' column
            };

            DataGridViewColumn wordpressstatusColumn = new DataGridViewTextBoxColumn
            {
                Name = "wordpressstatus",
                HeaderText = "WordPress Status",
                Width = 80, // Set the width of the 'Domain Status' column
            };

            DataGridViewColumn checkedtimeColumn = new DataGridViewTextBoxColumn
            {
                Name = "lastcheckedtime",
                HeaderText = "Last Checked Time",
                Width = 95, // Set the width of the 'Domain Status' column
            };


            // Add more columns similarly

            // Add the columns to the DataGridView
            dataGridView.Columns.Add(siteColumn);
            dataGridView.Columns.Add(domainStatusColumn);
            dataGridView.Columns.Add(wordpressstatusColumn);

            dataGridView.Columns.Add(checkedtimeColumn);

            // Add more columns to the DataGridView
        }




    }





}
