using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System;
using System.Data;

namespace wp_uptime_alert
{
    public partial class Form1 : Form
    {
        //correct place
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            



        }

        //input text box for urls
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            string[] inputUrls = richTextBox1.Lines;



            //string[] lines = inputUrls.Split(
            //new string[] { Environment.NewLine },
            //StringSplitOptions.None
            //) ;

            //string test = "2345\n564532\n345634\n234 234543\n1324 2435\n";
            //string[] result = inputUrls.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


            //var result = inputUrls.Split(new string[] { "\\n" }, StringSplitOptions.None);
            
            if (dt.Rows.Count > 0)
            {

            }
            
            dt.Columns.Add("site");
            dt.Columns.Add("status");
            dt.Columns.Add("lastcheckeddate");
            DataRow row = dt.NewRow();

            for (int i = 0; i < inputUrls.Length; i++)
            {
                row[i] = inputUrls[i];
                
            }
            dt.Rows.Add(row["site"]);



            MessageBoxWithDetails messageboxwithdetails = new MessageBoxWithDetails();

            messageboxwithdetails.DialogBoxPopup(inputUrls[0].ToString());


            total_websites_label.Text = dt.Rows.Count.ToString();
        }



       

    }
}