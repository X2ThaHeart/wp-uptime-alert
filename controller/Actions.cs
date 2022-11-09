using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wp_uptime_alert.controller
{
    internal class Actions
    {

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
    }
}
