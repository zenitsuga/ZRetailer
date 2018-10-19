using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRetailer.Classes;


namespace ZRetailer.Screens
{
    public partial class FrmRegistrationNotice : Form
    {    
        clsFunctions cf = new clsFunctions();
        
        public bool isRegistered = false;

        string IniPath = Environment.CurrentDirectory + "\\Settings.ini";
        string WebRegistrationLink = string.Empty;
        
        public FrmRegistrationNotice()
        {
            InitializeComponent();
        }

        private void FrmRegistrationNotice_Load(object sender, EventArgs e)
        {   
            cf.inif = new IniFile(IniPath);
            WebRegistrationLink = cf.GetIniValue("Registration", "Weblinks");
            if (string.IsNullOrEmpty(WebRegistrationLink))
            {
                MessageBox.Show("Error: Please check your settings.","Setting not found: [Weblinks:Registration]",MessageBoxButtons.OK  ,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            textBox1.Text = cf.getMotherBoardID();
            textBox4.Text = cf.GetPCode();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!cf.IsValidEmail(textBox2.Text))
            {
                MessageBox.Show("Error: Invalid Email Address. Please check", "Invalid Format : Email [ErrorCode:003]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            cf.tbHighLight(textBox2, true);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            cf.tbHighLight(textBox2, false);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            cf.tbHighLight(textBox3, true);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            cf.tbHighLight(textBox3, false);
        }

        
    }
}
