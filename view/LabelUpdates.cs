using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wp_uptime_alert.controller;

namespace wp_uptime_alert.view
{
    class LabelUpdates
    {

        private Actions action; // Create a private instance of the Actions class

        public LabelUpdates()
        {
            action = new Actions(); // Initialize the Actions instance in the constructor
        }


        public void updateWebsiteLabels(Label total_websites_label, Label label5, Label label7, Label label9, Label label16, DataTable dt, DataTable dtBlacklist)
        {
            total_websites_label.Text = action.calculateTotalWebsites(dt, dtBlacklist).ToString();
            
            label5.Text = action.calculateActiveWebsites(dt).ToString();

            UpdateErrorLabels(label7, label9, label16, dt);



        }

        public void UpdateErrorLabels(Label websiteErrors, Label firstTimeError, Label errorWebsite, DataTable dt)
        {
            
            // Filter the DataTable rows based on the "Error" condition
            var errorRows = dt.AsEnumerable().Where(row => row.Field<string>("domainstatus") == "Error" || row.Field<string>("wordpressstatus") == "Error");

            // Count the number of filtered rows
            int errorCount = errorRows.Count();

            if (errorCount > 0)
            {
                // Update the text of the labels with the count of rows that match the error conditions
                websiteErrors.Text = errorCount.ToString();
            }
            else if(errorCount == 0)
            {
                websiteErrors.Text = "0";
            }
        }


    }
}
