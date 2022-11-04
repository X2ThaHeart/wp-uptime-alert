using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace wp_uptime_alert
{
    partial class MessageBoxWithDetails
    {
        public bool DialogBoxPopup(string message)
        {

        
        // Initializes the variables to pass to the MessageBox.Show method.
        //string message = "You did not enter a server name. Cancel this operation?";
        string caption = "Error Detected in Input";
        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        DialogResult result;

        // Displays the MessageBox.
        result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Closes the parent form.
                return true;
            }
            return false;
        }






    }
}
