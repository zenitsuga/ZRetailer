using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace ZPOS.Classes
{
    public class clsFunction
    {
        Variables vars = new Variables();
        Database db = new Database();
        public SqlConnection sqlconn_client;
        public SqlConnection sqlconn_server;

        #region General Functions

        public void HighlightEntry(Color clr, Object obj)
        {
            try
            {
                switch (obj.GetType().ToString())
                {

                }
            }
            catch
            {
            }
        }

        public string EncryptWords(string Value)
        {
            string value = string.Empty;
            try
            {
                string keys = Licensing.CryptoEngine.Key().ToString();
                value = Licensing.CryptoEngine.Encrypt(Value, keys);
            }
            catch
            {
            }
            return value;
        }
        public string DecryptWords(string Value)
        {
            string value = string.Empty;
            try
            {
              string keys = Licensing.CryptoEngine.Key().ToString();
              value = Licensing.CryptoEngine.Decrypt(Value, keys);  
            }
            catch
            {
            }
            return value;
        }
        public DataTable GetRecords(string Query,string TypeDB)
        {
            DataTable dtResult = new DataTable();
            try
            {
                if (TypeDB.ToLower() == "client")
                {
                    db.sqlConn_Client = sqlconn_client;
                    db.sqlConn_Server = sqlconn_server;
                    dtResult = db._ExecuteQuery(Query);
                }
                else if (TypeDB.ToLower() == "server")
                {
                    dtResult = db.ExecuteQuery(Query);
                }
            }
            catch
            {
            }
            return dtResult;
        }
        public DateTime parseDate(string Value)
        {
            DateTime DefaultValue = DateTime.Now;
            try
            {
                DefaultValue = DateTime.Parse(Value);
            }
            catch
            { 
            }
            return DefaultValue;
        }
        public string GetMachineID()
        {
            string result = string.Empty;
            result = GenerateMachineID();
            return result;
        }

        public string GetFieldsbyValues(string Table,string FieldName, string Criteria)
        {
            string result = string.Empty;
            try
            {
                string query = "Select " + FieldName + " from " + Table + " " + Criteria;
                DataTable dtResult = GetRecords(query, "client");
                if (dtResult.Rows.Count > 0)
                {
                    result = dtResult.Rows[0][0].ToString();
                }
            }
            catch
            {
            }
            return result;
        }

        public string GenerateMachineID()
        {
            string result = string.Empty;
            try
            {
                string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
                string drive = "C";
                ManagementObject dsk = new ManagementObject(
                    @"win32_logicaldisk.deviceid=""" + drive + @":""");
                dsk.Get();
                string volumeSerial = dsk["VolumeSerialNumber"].ToString();

                result = cpuInfo + volumeSerial;
                if(result != string.Empty)
                result = EncryptWords(result);
            }
            catch
            {
            }
            return result;
        }
        public bool checkServerConnection(string ConnType,string ServerName,string DatabaseName,string Username,string Password, ref SqlConnection SQLConn)
        {
            bool result = false;
            try
            {
                string Server = DecryptWords(ServerName);
                string Database = DecryptWords(DatabaseName);
                string UserName = DecryptWords(Username);
                string PassWord = DecryptWords(Password);
                
                result = InitializeServerConnection(ConnType, Server, Database, UserName, PassWord);

                if (ConnType.ToLower() == "local")
                {
                    SQLConn = sqlconn_client;
                }
                else
                {
                    SQLConn = sqlconn_server;
                }
            }
            catch
            {
            }
            return result;
        }

        public bool InitializeServerConnection(string ConnectionType,string Servername, string Database, string Username, string Password)
        {
            bool result = false;
            try
            {
                string ConnectionString = "Server = " + Servername + "; Database =" + Database + "; User Id =" + Username + ";Password = " + Password + ";";
                if (ConnectionType.ToLower() == "server")
                {
                    sqlconn_server = new SqlConnection(ConnectionString);
                    sqlconn_server.Open();
                    result = sqlconn_server.State == System.Data.ConnectionState.Open ? true : false;
                }
                else
                {
                    sqlconn_client = new SqlConnection(ConnectionString);
                    sqlconn_client.Open();
                    result = sqlconn_client.State == System.Data.ConnectionState.Open ? true : false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Connection to SQL was Failed. Please check your connection\n" + ex.Message.ToString(), "Cannot connect to Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        #endregion

        #region Start-up Function/Validation
        public bool CheckCompanyInfo(string BranchCode,string Transaction,ref List<CompanyInfo>Ci)
        {
            bool result = false;
            try
            {
                if (Transaction == "ONLINE")
                {
                    Ci = new List<CompanyInfo>();
                    db.sqlConn_Server = sqlconn_server;
                    DataTable dtRecords = db.ExecuteQuery("Select * from tblBranch where isActive = 1 and BranchCode ='" + BranchCode + "'");
                    if (dtRecords.Rows.Count > 0)
                    {
                        CompanyInfo ci = new CompanyInfo();
                        foreach (DataRow dr in dtRecords.Rows)
                        {   
                            ci.BranchCode = dr["BranchCode"].ToString();
                            ci.CompanyName = dr["CompanyName"].ToString();
                            ci.Address1 = dr["Address1"].ToString();
                            ci.Address2 = dr["Address2"].ToString();
                            ci.Address3 = dr["Address3"].ToString();
                            ci.ContactNumber = dr["ContactNumber"].ToString();
                            ci.TIN = dr["TIN"].ToString();
                        }
                        Ci.Add(ci);
                        result = true;
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        public bool isOpenChildForm(string FormName)
        {
            bool result = false;
            try
            {
                Form Frm = Application.OpenForms[FormName];
                if (Frm != null)
                {
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }

        public bool isFileExists(string FilePath)
        {
            bool result = false;
            try
            {
                result = File.Exists(FilePath);
            }
            catch
            {
            }
            return result;
        }
        public bool isBoolean(string StrValue)
        {
            bool result = false;
            try
            {
                result = bool.Parse(StrValue);
            }
            catch
            {
            }
            return result;
        }
        
        public bool CopyTable(string BranchCode,string TableName,SqlConnection SClient,SqlConnection SServer)
        {
            bool result = false;
            try
            {
                //Truncate Local Table
                string TruncateQuery = "Truncate table " + TableName.Replace("_","");

                string QueryTableServer = "Select * from " + TableName.Replace("_", "") + " where isActive = 1 order by sysid asc";
                db.sqlConn_Client = SClient;
                db.sqlConn_Server = SServer;
                DataTable dtServer = db.ExecuteQuery(QueryTableServer);

                if (dtServer.Rows.Count > 0)
                {
                    db.CopyBulkTableForSync(TableName,BranchCode);
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion

        #region LoginFunction
        public bool isUserValid(string Username, string Password, string BranchCode,ref string ActiveRole)
        {
            bool result = false;

            Username = EncryptWords(Username);
            Password = EncryptWords(Password);

            try
            {
                string Query = "Select u.*,r.RoleName from tblusers u " + 
                               "left join tblUserRole r on u.Roleid = r.sysid " +
                               "where branchCode='" + BranchCode + "' and tblUsername='" + Username + "' and tblPassword='" + Password + "'";
                DataTable dtRecords = new DataTable();
                db.sqlConn_Client = sqlconn_client;
                dtRecords = db._ExecuteQuery(Query);
                if (dtRecords.Rows.Count > 0)
                {
                    ActiveRole = dtRecords.Rows[0]["RoleName"].ToString();
                    result = true;
                }
            }
            catch
            {
            }
            return result;
        }
        #endregion

        #region CashDrawer
        public bool CheckCashDrawerOfDay(string BranchCode,ref string InitialValue)
        {
            bool result = false;
            try
            {
                string Query = "select * from tblMoneyEntry " +
                               " where [datetime] between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59' " +
                               " and isActive = 1 and BranchCode='" + BranchCode + "' and status = 'In'";
                db.sqlConn_Client = sqlconn_client;
                DataTable dtRecords = db._ExecuteQuery(Query);
                if (dtRecords.Rows.Count == 0)
                {
                    result = true;
                }
                else
                {
                    InitialValue = dtRecords.Rows[0]["OnRegisterMoney"].ToString();
                }
            }
            catch
            {
            }
            return result;
        }
        public bool DeleteCashDrawer(string PK)
        {
            bool result = false;
            try
            {
                string Query = "Delete from tblMoneyEntry where POS_PK ='" + PK + "'";
                result = db._ExecuteNonQuery(Query);
            }
            catch
            {
            }
            return result;
        }
        public bool InsertCashDrawer(string BranchCode,string MoneyValue,string status,string ActiveUser, ref string PK)
        {
            bool result = false;
            try
            {
                string UserID = GetFieldsbyValues("tblUsers", "TOP 1 sysid", " where tblUsername='" + EncryptWords(ActiveUser) + "'").ToString();
                PK = BranchCode + "_" + status + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                string Query = "Insert into tblMoneyEntry(DateTime,POS_PK,BranchCode,OnRegisterMoney,Status,CreatedBy) values (" + 
                               "'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + PK + "','" + BranchCode + "'," +
                               MoneyValue + ",'" + status + "'," + UserID + ")";
               result = db._ExecuteNonQuery(Query);
            }
            catch
            {
            }
            return result;
        }
        public bool DeleteDetailCashDrawer(string MoneyID)
        {
            bool result = false;
            try
            {
                string Query = "Delete from tblMoneyEntryDetails where MoneyEntryID =" + MoneyID;
                result = db._ExecuteNonQuery(Query);
            }
            catch
            {
            }
            return result;
        }
        public bool InsertDetailCashDrawer(string BranchCode,int MoneyID,int Value,int Quantity)
        {
            bool result = false;
            try
            {
                string Query = "Insert into tblMoneyEntryDetails(MoneyEntryID, MoneyValue,Quantity)values" +
                               "(" + MoneyID + "," + Value + "," + Quantity + ")";
                result = db._ExecuteNonQuery(Query);
            }
            catch
            {
            }
            return result;
        }
        #endregion

        #region EmployeeManagement
        public DataTable dtGetEmployeeRole(string Role)
        {
            DataTable dtResult = new DataTable();
            try
            {
                string Query = "Select RoleName,Description from tblUserRole where isActive = 1 order by Description asc";
                dtResult = GetRecords(Query, Role);
            }
            catch
            {
            }
            return dtResult;
        }
        public DataTable dtGetEmployee(string Role)
        {
            DataTable dtResult = new DataTable();
            try
            {
                string Query = "Select u.SysID as ID, u.tblUsername as 'Username',u.tblPassword as 'Password',r.RoleName from tblUsers u Left Join tblUserRole r on u.RoleID = r.sysID where u.isActive = 1 order by Description asc";
                dtResult = GetRecords(Query, Role);
            }
            catch
            {
            }
            return dtResult;
        }
        #endregion
    }
}
