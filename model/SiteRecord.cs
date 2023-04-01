using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wp_uptime_alert.model
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
        public string LastCheckedTime
        {
            get => lastcheckedtime.ToString("HH:mm:ss");
            set => lastcheckedtime = DateTime.ParseExact(value, "HH:mm:ss", CultureInfo.InvariantCulture);
        }





        public SiteRecord()
        {
            // Create a new DataTable with the desired schema
            //DataTable newDt = new DataTable();




           





        }

      

        //This extra constructor is required to allow "SiteRecord siteRecord = new SiteRecord();" to work in form.cs
       

        public SiteRecord(DataTable list)
        {
            this.list = list;
        }




        public void InitializeDataGridViewColumns(DataGridView dataGridView1)
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

            // Add the columns to the DataGridView
            //dataGridView1.Columns.Add(siteColumn);
            //dataGridView1.Columns.Add(domainStatusColumn);
            //dataGridView1.Columns.Add(wordpressstatusColumn);
            //dataGridView1.Columns.Add(checkedtimeColumn);
        }





    }





}
