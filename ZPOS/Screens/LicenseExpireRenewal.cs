using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZPOS.Screens
{
    public partial class LicenseExpireRenewal : Form
    {
        public string daystorenew;
        public string APPLICATIONCODE;
        public bool Restart;
        public bool AppliedCode;

        public string MachineID;
        public string SystemID;
        public string RegisteredEmail;

        public LicenseExpireRenewal()
        {
            InitializeComponent();
        }

        private void LicenseExpireRenewal_Load(object sender, EventArgs e)
        {   
            lblWarning.Text = string.Format("WARNING: YOU ONLY HAVE {0} DAYS TO \nRENEW YOUR LICENSE.\nPlease fill-up the fields \nbelow then press Submit button \nand wait your approval code on your registered email.", daystorenew);
            textBox1.Text = APPLICATIONCODE;
            textBox4.Text = MachineID;
            textBox2.Text = SystemID;
            textBox3.Text = RegisteredEmail;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your new license will be applied. Please restart your application to take effect.", "Apply License", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Restart = true;
            this.Close();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "[Your Email HERE]")
            {
                textBox3.Text = string.Empty;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == string.Empty)
            {
                textBox3.Text = "[Your Email HERE]";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("All entries will be applied on your current license details. \nPress Yes to accept or Close this to ignore.", "License Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                AppliedCode = true;
            }
            this.Close();
        }
    }
}
