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

        static bool StringIsNewLine(string s)
        {
            return (!string.IsNullOrEmpty(s)) &&
                (!string.IsNullOrWhiteSpace(s)) &&
                (((s.Length == 1) && (s[0] == 8203)) ||
                ((s.Length == 2) && (s[0] == 8203) && (s[1] == 8203)));
        }



        //input text box for urls
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


            string[] removeSpacesFirst = richTextBox1.Lines;

            if (!dt.Columns.Contains("site"))
            {
                //DataTable dt = new DataTable();
                dt.Columns.Add("site");
                dt.Columns.Add("status");
                dt.Columns.Add("lastcheckeddate");

            }
            dt.Clear();




            for (int i = 0; i < removeSpacesFirst.Length; i++)
            {
                //if (!StringIsNewLine(removeSpacesFirst[i]))

                    if (removeSpacesFirst[i] == "\r\n" || removeSpacesFirst[i] == " " || removeSpacesFirst[i] == null || removeSpacesFirst[i].Length == 0)
                {


                }
                else
                {
                    string site = removeSpacesFirst[i];
                    //successfully add item to datatable
                    DataRow row = dt.NewRow();

                    row["site"] = site;
                    dt.Rows.Add(row);

                }

            }

            //dt.Rows.Add(row);

            //foreach (string check in removeSpacesFirst)
            //{


            //}
            //string[] inputUrls = richTextBox1.Lines;



            //string[] lines = inputUrls.Split(
            //new string[] { Environment.NewLine },
            //StringSplitOptions.None
            //) ;

            //string test = "2345\n564532\n345634\n234 234543\n1324 2435\n";
            //string[] result = inputUrls.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);


            //var result = inputUrls.Split(new string[] { "\\n" }, StringSplitOptions.None);





            //for (int i = 0; i < inputUrls.Length; i++)
            //{
            //    row[i] = inputUrls[i];

            //}




            MessageBoxWithDetails messageboxwithdetails = new MessageBoxWithDetails();

            //messageboxwithdetails.DialogBoxPopup(dt.Rows[1].ToString());


            total_websites_label.Text = dt.Rows.Count.ToString();
        }



       

    }
}