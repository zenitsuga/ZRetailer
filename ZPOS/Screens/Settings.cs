using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ZPOS.Screens
{
    public partial class Settings : Form
    {
        public string IniPath;
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (File.Exists(IniPath))
            {
                richTextBox1.Text = File.ReadAllText(IniPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DialogResult dr = MessageBox.Show("Are you sure you want to save this settings?", "Save Configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           if (dr == DialogResult.Yes)
           {
               SaveIni(IniPath, richTextBox1.Text);
           }
        }
        private bool SaveIni(string IniPath, string Value)
        {
            bool result = false;
            try
            {
                File.WriteAllText(IniPath, string.Empty);
                using (StreamWriter writetext = new StreamWriter(IniPath,true,Encoding.UTF8))
                {
                    foreach (string StrValue in Value.Split('\n'))
                    {
                        writetext.WriteLine(StrValue);
                    }
                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error Reading Configuration File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}
