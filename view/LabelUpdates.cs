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


        public void updateWebsiteLabels(Label total_websites_label, Label label5, Label label7, DataTable dt, DataTable dtBlacklist)
        {
            total_websites_label.Text = action.calculateTotalWebsites(dt, dtBlacklist).ToString();
            label7.Text = action.calculateErrorWebsites(dtBlacklist).ToString();
            label5.Text = action.calculateActiveWebsites(dt).ToString();


        }



    }
}
