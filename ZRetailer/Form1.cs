using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRetailer.Screens;
using ZRetailer.Classes;

namespace ZRetailer
{
    public partial class Form1 : Form
    {    
        clsFunctions cf = new clsFunctions();

        bool isSystemOnline = false;
        bool isRegistered = false;
        string ProviderEMAIL = string.Empty;
        string IniPath = Environment.CurrentDirectory + "\\Settings.ini";
 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cf.inif = new IniFile(IniPath);
            
            string IsOnline = cf.GetIniValue("ONLINE", "SYSTEM");
            ProviderEMAIL = cf.GetIniValue("EMAILADDRESS", "SYSTEM");
            isSystemOnline = string.IsNullOrEmpty(IsOnline) ? false: (cf.ParseBool(IsOnline) ? cf.ParseBool(IsOnline):false);

            if (!isSystemOnline)
            {
                MessageBox.Show("Error: Unable to connect to Internet.\nPlease check your Network or,\nEmail at " + ProviderEMAIL, "No Internet Connection : [ErrorCode:002]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            FrmRegistrationNotice frmNotice = new FrmRegistrationNotice();
            isRegistered = frmNotice.isRegistered;
            frmNotice.ShowDialog();
            if (!isRegistered)
            {
                MessageBox.Show("Error: System is unregistered.\nPlease check your provider to fix this.\nEmail at " + ProviderEMAIL, "Unregistered Version : [ErrorCode:001]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tssDateTodayStatus.Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:SS");
        }
    }
}
