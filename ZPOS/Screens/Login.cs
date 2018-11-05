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

namespace ZPOS.Screens
{
    public partial class Login : Form
    {
        clsFunction cf = new clsFunction();
        public string BranchCode;
        public SqlConnection SQLClient;
        public SqlConnection SQLServer;
        public string ActiveUser;
        public string ActiveRole;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Error: Invalid User. Please check your user credentials", "User not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cf.sqlconn_client = SQLClient;
            cf.sqlconn_server = SQLServer;
            if (!cf.isUserValid(textBox1.Text, textBox2.Text, BranchCode,ref ActiveRole))
            {
                MessageBox.Show("Error: Invalid User. Please check your user credentials", "User not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ActiveUser = textBox1.Text;
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
