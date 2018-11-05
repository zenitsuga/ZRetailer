using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZPOS.Classes;

namespace ZPOS.Screens
{
    public partial class POS : Form
    {
        public IniFile inif;

        bool isBarcode;

        public POS()
        {
            InitializeComponent();
        }

        private void Initialized()
        {
            
        }

        private void POS_Load(object sender, EventArgs e)
        {
            isBarcode = inif.Read("Code", "POS").ToLower() == "barcode" ? true:false;
            radioButton1.Checked = isBarcode;
            radioButton2.Checked = !isBarcode;

            if(radioButton1.Checked)
            {
                int CodeLength =  int.Parse (string.IsNullOrEmpty(inif.Read("CodeLength","POS")) ? "0": inif.Read("CodeLength","POS"));
                tbItemCode.MaxLength = CodeLength > 0 ? CodeLength:0;
            }

            this.Left = 1;
            this.Top = 1;
            Initialized();
        }
    }
}
