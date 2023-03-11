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

        public SiteRecord(DataTable list, ListView listView)
        {
            // Create a new DataTable with the desired schema
            DataTable newDt = new DataTable();

            // Add the columns to the new DataTable
            newDt.Columns.Add("Site address");
            newDt.Columns.Add("Domain Status");
            newDt.Columns.Add("WordPress Status");
            newDt.Columns.Add("Last Checked Time");

            // Add columns to the ListView control
            listView.Columns.Add("Site address", 200);
            listView.Columns.Add("Domain", 100);
            listView.Columns.Add("WordPress", 100);
            listView.Columns.Add("Last Checked Time", 150);

            // Add column headers
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;



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
                    newRow["Domain Status"] = row["domainstatus"];
                    newRow["WordPress Status"] = row["wordpressstatus"];
                    newRow["Last Checked Time"] = row["lastcheckedtime"];
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
                item.SubItems.Add(row["Domain Status"].ToString());
                item.SubItems.Add(row["WordPress Status"].ToString());
                item.SubItems.Add(row["Last Checked Time"].ToString());
                listView.Items.Add(item);
            }
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


        public void SetListViewColumns(ListView listView)
        {
            // Add columns to the ListView control
            listView.Columns.Add("Site address", 150);
            listView.Columns.Add("Domain Status", 150);
            listView.Columns.Add("WordPress Status", 150);
            listView.Columns.Add("Last Checked Time", 150);
        }




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
    }

}
