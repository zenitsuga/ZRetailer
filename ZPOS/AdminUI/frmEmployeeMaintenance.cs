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

namespace ZPOS.AdminUI
{
    public partial class frmEmployeeMaintenance : Form
    {
        clsFunction cf = new clsFunction();
        public string ConnType;
        public SqlConnection SQLCON;
        public bool isClose;

        public frmEmployeeMaintenance()
        {
            InitializeComponent();
            
        }

        private void ObjectControls(bool state,bool isClear)
        {
            textBox1.Enabled = state;
            textBox2.Enabled = state;
            comboBox1.Enabled = state;
            textBox3.Enabled = state;

            if (isClear)
            {
                textBox1.Text = textBox2.Text = textBox3.Text = string.Empty;
            }
        }

        private void Initialize()
        {
            cf.sqlconn_client = SQLCON;
            cf.sqlconn_server = SQLCON;
            //Load UserRole
            comboBox1.DataSource = cf.dtGetEmployeeRole(ConnType);
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "RoleName";
            //Load All Users
            dataGridView1.DataSource = cf.dtGetEmployee(ConnType);
            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                dgr.Cells["Username"].Value = cf.DecryptWords(dgr.Cells["Username"].Value.ToString()).ToString();
            }
            ObjectControls(false, true);
        }

        private void frmEmployeeMaintenance_Load(object sender, EventArgs e)
        {
            if (SQLCON == null)
            {
                MessageBox.Show("Error: Please check your Database connection.", "Database connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isClose = true;
                this.Close();
            }

            if (SQLCON.State == ConnectionState.Closed)
            {
                SQLCON.Open();
            }

            Initialize();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmEmployeeMaintenance_FormClosing(object sender, FormClosingEventArgs e)
        {  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dres = MessageBox.Show("Are you sure you want to create a New Record", "Create new record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dres == DialogResult.Yes)
            {
                ObjectControls(true, true);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Error: Cannot save entry. Please check if there is blank or invalid entry","Invalid Entry",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            DialogResult dres = MessageBox.Show("Are you sure you save this Record", "Save record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dres == DialogResult.Yes)
            {
                ObjectControls(true, true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dres = MessageBox.Show("Are you sure you delete this Record", "Delete record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dres == DialogResult.Yes)
            {
                ObjectControls(true, true);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dres = MessageBox.Show("Are you sure you update this Record", "Update record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dres == DialogResult.Yes)
            {
                ObjectControls(true, true);
            }
        }
    }
}
