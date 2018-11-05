using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZPOS.Classes;
using System.Data.SqlClient;
namespace ZPOS.Screens
{
    public partial class CashDrawer : Form
    {
        public SqlConnection _sqlServer;
        public SqlConnection _sqlClient;

        public string ActiveUser;
        public string BranchCode;

        clsFunction cf = new clsFunction();
        public CashDrawer()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CashDrawer_Load(object sender, EventArgs e)
        {
            LoadCashRegister();
            dataGridView1.Columns["Count"].ReadOnly = false;
            dataGridView1.Columns["Amount"].ReadOnly = true;
            dataGridView1.Columns["Total"].ReadOnly = true;
            CheckAndLoadCashRegister();
        }

        private void CheckAndLoadCashRegister()
        {
            try
            {
                string dateToday = DateTime.Now.ToString("yyyy-MM-dd");
                string Query = "select * from tblMoneyEntry where branchCode = '" + BranchCode + "' and datecreated between '"+ dateToday +" 00:00:00' and '"+ dateToday +" 23:59:59' and isActive = 1";
                DataTable dtRecords = cf.GetRecords(Query, "client");
                if (dtRecords.Rows.Count > 0)
                {
                    textBox1.Text = dtRecords.Rows[0]["OnRegisterMoney"].ToString();
                    string Detail_ID = dtRecords.Rows[0]["sysID"].ToString();
                    string QueryDetails = "select * from tblMoneyEntryDetails where moneyEntryID =" + Detail_ID + " order by sysid"; 
                    DataTable dtRecordDetails = cf.GetRecords(QueryDetails,"client");
                    if (dtRecordDetails.Rows.Count > 0)
                    {
                        foreach (DataRow drow in dtRecordDetails.Rows)
                        {
                            string MoneyValue = drow["MoneyValue"].ToString();
                            string MoneyQuantity = drow["Quantity"].ToString();
                            int rowIndex = 0;
                            DataGridViewRow row = dataGridView1.Rows
                                                   .Cast<DataGridViewRow>()
                                                   .Where(r => r.Cells["Amount"].Value.ToString().Equals(MoneyValue))
                                                   .First();
                            rowIndex = row.Index;
                            dataGridView1["Amount", rowIndex].Value = MoneyValue;
                            dataGridView1["Count", rowIndex].Value = MoneyQuantity;
                        }
                    }
                }

            }
            catch
            {
            }
        }
        private void LoadCashRegister()
        {
            try
            {
                string Query = "select CurrencyAmount as 'Amount','0' as [Count],'0.00' as Total " +  
                               " from tblCurrency " +
                               " where isActive = 1 " +
                               " order by CurrencyAmount desc ";
                cf.sqlconn_client = _sqlClient;
                cf.sqlconn_server = _sqlServer;
                DataTable dtRecords = cf.GetRecords(Query, "client");
                if (dtRecords.Rows.Count > 0)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = dtRecords;
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Recount(DataGridViewCellEventArgs e)
        {
            double Amount = double.Parse(dataGridView1["Amount", e.RowIndex].Value.ToString());
            double Count = double.Parse(dataGridView1["Count", e.RowIndex].Value.ToString());

            dataGridView1["Total", e.RowIndex].Value = string.Format("{0:0.00}", (Amount * Count).ToString());

            double GrandTotal = 0.00;

            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                GrandTotal += double.Parse(dgr.Cells["Total"].Value.ToString());
            }
            lblTotalAmount.Text = string.Format("{0:0.00}", GrandTotal.ToString());
            textBox1.Text = lblTotalAmount.Text;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Recount(e);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int TotalCount = 0;
            string CashRegisterInitialValue = "0.00";
            if(!cf.CheckCashDrawerOfDay(BranchCode,ref CashRegisterInitialValue))
            {
                MessageBox.Show("Error: You have already set your cash drawer today.\nTo change it use administrative account.","Cash Drawer Amount : " + CashRegisterInitialValue , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Are you sure you want to set this value to your cash drawer?", "Cash Drawer Value:" + lblTotalAmount.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if ((lblTotalAmount.Text == "0.00") || (lblTotalAmount.Text == "0"))
                {
                    DialogResult dr2 = MessageBox.Show("Warning: Are you sure you want to save 0.00 amount?", "Zero Amount Defined", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr2 == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    bool ErrorSaved = false;
                    foreach (DataGridViewRow dgr in dataGridView1.Rows)
                    {
                        if (dgr.Cells["Count"].Value != "0")
                        { 
                            TotalCount += dgr.Cells["Total"].Value.ToString() != "0.00" ? int.Parse(dgr.Cells["Total"].Value.ToString()):0;
                        }
                    }

                    if ((decimal.Parse(TotalCount.ToString()) == decimal.Parse(lblTotalAmount.Text)).ToString() 
                            == ((decimal.Parse(textBox1.Text).ToString() == (decimal.Parse(lblTotalAmount.Text).ToString())).ToString()))
                    {
                        string PK = string.Empty;

                        if (cf.InsertCashDrawer(BranchCode, lblTotalAmount.Text, "In", ActiveUser, ref PK))
                        {
                            if (!string.IsNullOrEmpty(PK))
                            {
                                foreach (DataGridViewRow dgr in dataGridView1.Rows)
                                {
                                    if (dgr.Cells["Count"].Value.ToString() != "0")
                                    {
                                        int PK_ID = int.Parse(cf.GetFieldsbyValues("tblMoneyEntry", "TOP 1 sysiD", " where POS_PK='" + PK + "'"));
                                        int MoneyValue = int.Parse(dgr.Cells["Amount"].Value.ToString());
                                        int Quantity = int.Parse(dgr.Cells["Count"].Value.ToString());
                                        if (!cf.InsertDetailCashDrawer(BranchCode, PK_ID, MoneyValue, Quantity))
                                        {
                                            MessageBox.Show("Error: Cannot save cash drawer information. Please check your entry", "Cash Drawer Save Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            if (cf.DeleteCashDrawer(PK))
                                            {
                                                cf.DeleteDetailCashDrawer(PK_ID.ToString());
                                            }
                                            ErrorSaved = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (!ErrorSaved)
                        {
                            MessageBox.Show("Cash Drawer for today was successfully saved", "Cash Drawer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: Details amount is not tally with Total Amount.", "Please check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            Recount(e);
        }
    }
}
