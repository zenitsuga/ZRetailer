using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZPOS.Screens;
using ZPOS.Classes;

namespace ZPOS
{
    public partial class Form1 : Form
    {
        clsFunction cf = new clsFunction();
        Settings FrmSettings;
        IniFile inif;

        bool Error2Close = false;
        string IniPath = Environment.CurrentDirectory + "\\Settings.ini";

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Error2Close)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to close the application?", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //checkIniFile
            if (!File.Exists(IniPath))
            {
                Error2Close = true;
                MessageBox.Show("Error: Please check your configuration file.\nPath:" + IniPath, "Configuration File not Found (.ini)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            inif = new IniFile(IniPath);
            if (CheckLists())
            {
                Error2Close = true;
                Application.Exit();
            }
        }

        private bool CheckLists()
        {
            bool result = false;
            try
            {
                //Check SystemCode
                string systemCode = inif.Read("SystemCode", "Licensing");
                if (cf.DecryptWords(systemCode) != "RPOS")
                {
                    Error2Close = true;
                    MessageBox.Show("Error: Please check your configuration file.\nPath:" + IniPath, "System Code is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = true;
                }
                //Check AccessCode
                string AccessCode = inif.Read("AccessCode", "Licensing");
                if (string.IsNullOrEmpty(cf.DecryptWords(AccessCode)))
                {
                    Error2Close = true;
                    MessageBox.Show("Error: Please check your configuration file.\nPath:" + IniPath, "Access Code is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = true;
                }
                string DecryptAccessCode = cf.DecryptWords(AccessCode);
                if (!DecryptAccessCode.Contains("RPOS_"))
                {
                    Error2Close = true;
                    MessageBox.Show("Error: Please check your configuration file.\nPath:" + IniPath, "Access Code is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = true;
                }
                string StartDate = DecryptAccessCode.Split('_')[1].ToString();
                DateTime dtStartvalue = cf.parseDate(StartDate);
                int daysCount = cf.parseDate(StartDate).Subtract(DateTime.Now).Days;
                if (daysCount <= 30)
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = true;
            }
            return result;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings = new Settings();
            FrmSettings.IniPath = IniPath;
            FrmSettings.ShowDialog();
        }
    }
}
