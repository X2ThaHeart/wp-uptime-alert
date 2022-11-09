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


        public string cleanRssUrl(string site)
        {
            Uri outUri;

            var uriWithoutScheme = "";
            if (Uri.TryCreate(site, UriKind.Absolute, out outUri)
            && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps))
            {
                uriWithoutScheme = outUri.Host + outUri.AbsolutePath;


                //site = uriWithoutScheme.Replace("www.", "");
                site = site.ToLower();
                if (site.EndsWith("/"))
                {
                    site = site.Substring(0, site.Length - 1);
                }




                return site;
            }
            else
            {
                return null;


            }



        }



    }
}
