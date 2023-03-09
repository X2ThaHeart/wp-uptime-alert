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

        

        public string SiteAddress { get => siteaddress; set => siteaddress = value; }
        public string DomainStatus { get => domainStatus; set => domainStatus = value; }

        public string WpStatus { get => wpStatus; set => wpStatus = value; }
        public string LastCheckedTime { get => lastcheckedtime.ToString(); set => lastcheckedtime.ToString(); }


        public SiteRecord(DataTable list)
        {

            // Create a new DataTable with the desired schema
            DataTable newDt = new DataTable();
            newDt.Columns.Add("Site address");
            newDt.Columns.Add("Domain Status");
            newDt.Columns.Add("WordPress Status");
            newDt.Columns.Add("Last Checked Time");

            // Copy the data from the original DataTable into the new one
            foreach (DataRow row in list.Rows)
            {
                DataRow newRow = newDt.NewRow();
                newRow["Site address"] = row["site"];
                newRow["Domain Status"] = row["domainstatus"];
                newRow["WordPress Status"] = row["wordpressstatus"];
                newRow["Last Checked Time"] = row["lastcheckedtime"];
                newDt.Rows.Add(newRow);
            }

            // Set the new DataTable as the list property of the SiteRecord object
            list = newDt;
        }


        //This extra constructor is required to allow "SiteRecord siteRecord = new SiteRecord();" to work in form.cs
        public SiteRecord()
        {
            DataTable dt = new DataTable();

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
