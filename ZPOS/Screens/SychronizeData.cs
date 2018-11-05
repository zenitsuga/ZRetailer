using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ZPOS.Classes;
using System.Diagnostics;
using System.Threading;

namespace ZPOS.Screens
{
    public partial class SychronizeData : Form
    {
        clsFunction cf = new clsFunction();

        public SqlConnection SQLClient;
        public SqlConnection SQLServer;

        public DateTime LastSync;
        public string Table2Sync;
        public string BranchCode;

        public SychronizeData()
        {
            InitializeComponent();
        }

        private void SychronizeData_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BranchCode))
            {
                bgw.RunWorkerAsync();
            }
            else
            {
               
            }
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            if (!string.IsNullOrEmpty(Table2Sync))
            {
                foreach (string strTables in Table2Sync.Split(','))
                {
                    progressBar1.Value = 10;
                    string TableName = strTables.ToLower().Substring(0, 3) == "tbl" ? strTables : "tbl" + strTables;
                    lblStatus.Text = "Copying " + TableName.Replace("tbl","");
                    progressBar1.Value = 50;
                    cf.CopyTable(BranchCode,TableName,SQLClient,SQLServer);
                    progressBar1.Value = 80;
                    Thread.Sleep(500);
                    progressBar1.Value = 100;
                }
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LastSync = DateTime.Now;
            this.Close();
        }
    }
}
