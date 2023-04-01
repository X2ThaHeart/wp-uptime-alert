using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wp_uptime_alert.view
{
    public class CustomRowComparer : System.Collections.IComparer
    {
        private DataGridView _dataGridView;
        public CustomRowComparer(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow row1 = (DataGridViewRow)x;
            DataGridViewRow row2 = (DataGridViewRow)y;

            string status1 = row1.Cells["DomainStatus"].Value.ToString(); // Assuming your status column name is "Status"
            string status2 = row2.Cells["WpStatus"].Value.ToString();

            DateTime lastCheckedTime1 = DateTime.ParseExact(row1.Cells["lastcheckedtime"].Value.ToString(), "HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime lastCheckedTime2 = DateTime.ParseExact(row2.Cells["lastcheckedtime"].Value.ToString(), "HH:mm:ss", CultureInfo.InvariantCulture);

            // Prioritize server errors
            if (status1 == "Server Error" && status2 != "Server Error")
            {
                return -1;
            }
            else if (status1 != "Server Error" && status2 == "Server Error")
            {
                return 1;
            }
            // Prioritize WordPress errors
            else if (status1 == "WordPress Error" && status2 != "WordPress Error")
            {
                return -1;
            }
            else if (status1 != "WordPress Error" && status2 == "WordPress Error")
            {
                return 1;
            }
            // If both rows have the same error status, sort by last checked time (oldest at the top)
            else
            {
                return lastCheckedTime1.CompareTo(lastCheckedTime2);
            }
        }
    }


}
