using System;
using System.Xml.Linq;

namespace SecureCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void clickNumber(object sender, EventArgs e)
        {
            var button = sender as Button;
            txtSecurityCode.Text += button.Text;
        }

        private bool checkAndAdd(DateTime dateTime, int accessCode, int[] codes, string name)
        {
            foreach (int code in codes)
            {
                if (accessCode == code)
                {
                    lbxAccessLog.Items.Add(dateTime + " " + name);
                    File.AppendAllText("log.txt", dateTime + " " + name);
                    return true;
                }
            }
            return false;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            int[] technicianCodes = { 1689, 1689 };
            int[] custodianCodes = { 8345 };
            int[] scientistCodes = { 9998, 1006, 1007, 1008 };
            int accessCode = Int32.Parse(txtSecurityCode.Text);
            DateTime dateTime = DateTime.Today;

            if (accessCode >= 0 && accessCode < 10)
            {
                lbxAccessLog.Items.Add(dateTime + " " + "Restricted Access");
                File.AppendAllText("log.txt", dateTime + " " + "Restricted Access");
                txtSecurityCode.Text = "";
                return;
            }

            bool isCorrect1 = checkAndAdd(dateTime, accessCode, technicianCodes, "Technicians");
            bool isCorrect2 = checkAndAdd(dateTime, accessCode, custodianCodes, "Custodians");
            bool isCorrect3 = checkAndAdd(dateTime, accessCode, scientistCodes, "Scientist");

            if (!(isCorrect1 || isCorrect2 || isCorrect3))
            {
                lbxAccessLog.Items.Add(dateTime + " " + "Access denied");
                File.AppendAllText("log.txt", dateTime + " " + "Access denied");
            }

            txtSecurityCode.Text = "";
        }

        private void resetSecurityCode(object sender, EventArgs e)
        {
            txtSecurityCode.Text = "";
        }
    }
}