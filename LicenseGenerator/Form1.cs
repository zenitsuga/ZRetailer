using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LicenseGenerator
{
    public partial class Form1 : Form
    {
        string Keys = "zbln-3asd-sqoy19"; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Initialization()
        {
            pictureBox1.Image = Image.FromFile(Environment.CurrentDirectory + "\\Logo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Error: Please provide the data to encrypt.", "No Data to encrypt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            textBox3.Text = Licensing.CryptoEngine.Encrypt(textBox1.Text, Keys);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Error: Please provide the data to decrypt.", "No Data to Decrypt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }
            textBox3.Text = Licensing.CryptoEngine.Decrypt(textBox2.Text, Keys);
        }
    }
}
