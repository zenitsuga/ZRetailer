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
using System.Data.SqlClient;
using ZPOS.AdminUI;

namespace ZPOS
{
    public partial class Form1 : Form
    {
        public string ActiveUser;
        public string ActiveRole;

        clsFunction cf = new clsFunction();
        Settings FrmSettings;
        IniFile inif;

        SqlConnection SQLCLient;
        SqlConnection SQLServer;

        string[] ConnType = new string []{"IP","WebLink"};

        bool isOnline = false;
        bool isStandAlone = false;
        bool Error2Close = false;
        bool CheckCashRegister = false;
        string IniPath = Environment.CurrentDirectory + "\\Settings.ini";
        string ConnectionType = string.Empty;

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
            Initialization();
            isStandAlone = cf.isBoolean(inif.Read("Stand-Alone", "System"));
            CheckCompanyInfo();
            //Check Sychronization for data handling
            if (!isStandAlone)
            {
                CheckSync();
            }
            else
            {
                if (isOnline)
                {
                    MessageBox.Show("Warning: Your settings was set to \"ONLINE\".\nIt is recommended to Set your Stand-Alone to true.", "Warning: ONLINE Transaction was set", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            Login login = new Login();
            login.SQLClient = SQLCLient;
            login.SQLServer = SQLServer;
            login.BranchCode = inif.Read("BranchCode", "CompanyDetails");
            login.ShowDialog();
            if (string.IsNullOrEmpty(login.ActiveUser))
            {
                return;
            }
            ActiveUser = login.ActiveUser;
            tssActiveUser.Text = string.IsNullOrEmpty(ActiveUser) ? "N/A" : ActiveUser;
            tssLogin.Visible = string.IsNullOrEmpty(ActiveUser) ? true : false;
            tssLogout.Visible = !tssLogin.Visible;
            if (ActiveRole == "staff")
            {
                POS PS = new POS();
                PS.inif = inif;
                PS.MdiParent = this;
                PS.ShowDialog();
                
            }
            UserValidator(login.ActiveRole);
        }

        private void CheckCompanyInfo()
        {
            try
            {
                string BranchCode = inif.Read("BranchCode", "CompanyDetails");

                if (string.IsNullOrEmpty(BranchCode))
                {
                    Error2Close = true;
                    MessageBox.Show("Error: BranchCode not found \n(Please check your settings).", "BranchCode is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    List<CompanyInfo>Ci = null;
                    if (cf.CheckCompanyInfo(BranchCode, tssTransaction.Text, ref Ci))
                    {
                        inif.Write("CompanyName", Ci[0].CompanyName, "CompanyDetails");
                        inif.Write("BranchCode", Ci[0].BranchCode, "CompanyDetails");
                        inif.Write("Address1", Ci[0].Address1, "CompanyDetails");
                        inif.Write("Address2", Ci[0].Address2, "CompanyDetails");
                        inif.Write("Address3", Ci[0].Address3, "CompanyDetails");
                        inif.Write("ContactNumber", Ci[0].ContactNumber, "CompanyDetails");
                        inif.Write("TIN", Ci[0].TIN, "CompanyDetails");
                    }
                    else
                    {
                        if (!isOnline && !isStandAlone)
                        {
                            Error2Close = true;
                            MessageBox.Show("Warning: System is OFFLINE. Please set it to Stand-alone.", "Stand-Alone :" + isStandAlone.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                        else if(!isStandAlone)
                        {
                            Error2Close = true;
                            MessageBox.Show("Error: BranchCode not found \n(Please check your settings).", "BranchCode is Invalid :" + BranchCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }
            }catch
            {
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings = new Settings();
            FrmSettings.IniPath = IniPath;
            FrmSettings.ShowDialog();
        }
        private void tmr_Tick_1(object sender, EventArgs e)
        {
            tss_Today.Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        }
        private void tmr_Tick(object sender, EventArgs e)
        {
            tss_Today.Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Initialization();
        }

        #region Public/Private Functions
              private void Initialization()
                {
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                    string imagepath = inif.Read("ImageBackground", "System");
                    if (!cf.isFileExists(imagepath))
                    {
                        imagepath = Environment.CurrentDirectory + "\\Resources\\BG_SAMPLE.jpg";
                    }
                    Image imageBg = Image.FromFile(imagepath);
                    this.BackgroundImage = imageBg;
                    string Transac = inif.Read("Transactions", "System").ToUpper();
                    if (Transac == "ONLINE")
                    {
                        isOnline = true;
                    }
                    tssTransaction.Text =  Transac == "ONLINE" ? "ONLINE" : "OFFLINE";
                    ConnectionType = inif.Read("ConnectionType", "System").ToUpper();

                    //For Local Connection
                    if (ConnectionType.ToUpper() == "IP")
                    {
                        string _Servername = inif.Read("ServerName", "Local");
                        string _Databasename = inif.Read("Databasename", "Local");
                        string _Username = inif.Read("Username", "Local");
                        string _Password = inif.Read("Password", "Local");

                        if (cf.checkServerConnection("local", _Servername, _Databasename, _Username, _Password, ref SQLCLient))
                        {
                            tssLocalStatus.Text = "LOCAL: Connected"; 
                        }
                        else
                        {
                            MessageBox.Show("Warning: System Connection Type was " + ConnectionType + ".\nThe status is Disconnected. Please check your settings first.", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tssServerStatus.Text = "SERVER: Not Connected";
                            Error2Close = true;
                            Application.Exit();
                        }
                    }
                    
                    //For Online Connection
                    if (Transac == "ONLINE")
                    {
                        if (!ConnType.Contains(ConnectionType))
                        {
                            Error2Close = true;
                            MessageBox.Show("Error: ConnectionType not found \n(Please choose between IP or WebLink).\nConnection Type: " + ConnectionType, "ConnectionType is Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                        else
                        {
                            if (ConnectionType.ToUpper() == "IP")
                            {
                                string Servername = inif.Read("ServerName", "Server");
                                string Databasename = inif.Read("Databasename", "Server");
                                string Username = inif.Read("Username", "Server");
                                string Password = inif.Read("Password", "Server");

                                if (cf.checkServerConnection("server",Servername, Databasename, Username, Password,ref SQLServer))
                                {
                                    tssServerStatus.Text = "SERVER: Connected";
                                }
                                else
                                {
                                    MessageBox.Show("Warning: System Connection Type was " + ConnectionType + ".\nThe status is Disconnected. Please check your settings first.", "Server Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tssServerStatus.Text = "SERVER: Not Connected";
                                    Error2Close = true;
                                    Application.Exit();
                                }
                            }
                        }
                    }
              }

              private void CheckSync()
                {
                    try
                    {
                        bool OpenSync = false;
                        string syncType = inif.Read("SyncEvery", "Sync");
                        string BranchCode = inif.Read("BranchCode", "CompanyDetails");

                        if (string.IsNullOrEmpty(BranchCode))
                        {
                            OpenSync = false;
                            return;
                        }

                        if(syncType.ToLower() == "appstart")
                        {
                                OpenSync = true;
                        }
                        else if (syncType.Contains("Days["))
                        {
                            if (syncType.Contains("["))
                            {
                                string Days = syncType.Substring(syncType.IndexOf('[', 0) + 1, 3).ToString();
                                if (DateTime.Now.ToString("dddd").ToLower().Contains(Days.ToLower()))
                                {
                                    OpenSync = true;
                                }
                            }
                        }
                        else if (syncType.Contains("DayCount["))
                        {
                            if (syncType.Contains("["))
                            {
                                string DayCount = syncType.Substring(syncType.IndexOf('[', 0) + 1, syncType.IndexOf(']', 0) - syncType.IndexOf('[', 0) - 1).ToString();
                                if (DateTime.Now.ToString("dd").ToLower() == DayCount.ToLower())
                                {
                                    OpenSync = true;
                                }
                            }
                        }
                        else{                                
                                DialogResult dr = MessageBox.Show("Warning: Cannot Sync POS. Please check your configuration settings\nYou may ignore this by pressing Yes or No to Close the application", "Synchronization Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.No)
                                {
                                    Error2Close = true;                                   
                                    Application.Exit();
                                }
                        }

                        if (OpenSync)
                        {
                            SychronizeData sd = new SychronizeData();                            
                            sd.BranchCode = BranchCode;
                            sd.Table2Sync = inif.Read("DataSync", "Sync");
                            sd.SQLClient = SQLCLient;
                            sd.SQLServer = SQLServer;
                            sd.ShowDialog();
                            LastSync.Text = sd.LastSync.ToString("MM-dd-yyyy HH:mm:ss");
                            inif.Write("LastDateSync", LastSync.Text, "Sync");
                        }

                        if (LastSync.Text == "N/A")
                        {
                            DialogResult dr = MessageBox.Show("Warning: POS not yet sync. Please check your configuration settings\nYou may ignore this by pressing Yes or No to Close the application", "Synchronization Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dr == DialogResult.No)
                            {
                                Error2Close = true;
                                Application.Exit();
                            }
                        }
                    }
                    catch
                    {

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

                string MachineCode = cf.GetMachineID();
                inif.Write("MachineCode", MachineCode, "Licensing");

                string StartDate = DecryptAccessCode.Split('_')[1].ToString();
                DateTime dtStartvalue = cf.parseDate(StartDate);
                int daysCount = cf.parseDate(StartDate).Subtract(DateTime.Now).Days;
                if (daysCount <= 30 && daysCount != 0)
                {
                    string AppCode = inif.Read("ApprovalCode", "Licensing");
                    MachineCode = inif.Read("MachineCode", "Licensing");
                    string SystemCode = inif.Read("SystemCode", "Licensing");
                    string RegisteredMail = inif.Read("RegisteredMail", "Licensing");

                    if (string.IsNullOrEmpty(MachineCode))
                    {
                        MachineCode = cf.GetMachineID();
                        inif.Write("MachineCode", MachineCode, "Licensing");
                    }

                    LicenseExpireRenewal ler = new LicenseExpireRenewal();
                    ler.daystorenew = daysCount.ToString();
                    ler.APPLICATIONCODE = AppCode;
                    ler.MachineID = MachineCode;
                    ler.SystemID = systemCode;
                    ler.RegisteredEmail = RegisteredMail;

                    ler.ShowDialog();
                    if (ler.AppliedCode)
                    {
                        inif.Write("ApprovalCode", ler.APPLICATIONCODE, "Licensing");
                        inif.Write("MachineCode", ler.MachineID, "Licensing");
                        inif.Write("SystemCode", ler.SystemID, "Licensing");
                        inif.Write("RegisteredMail", ler.RegisteredEmail, "Licensing");
                    }
                    if (ler.Restart)
                    {
                        Error2Close = true;
                        result = true;
                        Application.Exit();
                    }
                }
                else if (daysCount == 0)
                {
                    MessageBox.Show("Error: Application license is expired. Please contact your provider", "License Exipired", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Error2Close = true;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = true;
            }
            return result;
        }
        #endregion

        private void UserValidator(string Role)
        {
            string branchCode = inif.Read("BranchCode", "CompanyDetails");
            POS_Menu.Visible = false;
            ActiveRole = Role;
            if (ActiveRole.ToLower() == "staff")
            {
                ToolAccess.Visible = false;
                POS PS = new POS();
                PS.inif = inif;
                PS.MdiParent = this;
                PS.Show();
                POS_Menu.Visible = true;
                cf.sqlconn_client = SQLCLient;
                string InitialValue = "0.00";
                if (cf.CheckCashDrawerOfDay(branchCode,ref InitialValue))
                {
                    DialogResult dr = MessageBox.Show("Warning: System detects that you have not yet settle the POS Drawer today.\nWould you like to configure it first?", "Configure Drawer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        CashDrawer cd = new CashDrawer();
                        cd.BranchCode = inif.Read("BranchCode", "CompanyDetails");
                        cd.ActiveUser = ActiveUser;
                        cd._sqlClient = SQLCLient;
                        cd._sqlServer = SQLServer;
                        cd.ShowDialog();
                    }
                }
            }
            else
            {
                if (ActiveRole.ToLower() == "admin")
                {
                    ToolAccess.Visible = true;
                    string StandAlone = inif.Read("Stand-Alone", "System");
                    if (StandAlone.ToLower() == "false")
                    {
                        MessageBox.Show("Error: Your POS was not set to \"Stand-Alone\".\n Kindly set it true to use to have Administratve Module.", "POS: Stand-Alone is Off", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ActiveUser = string.Empty;
                        tssActiveUser.Text = string.IsNullOrEmpty(ActiveUser) ? "N/A" : ActiveUser;
                        tssLogout.Visible = false;
                        tssLogin.Visible = !tssLogout.Visible;
                        return;
                    }
                    else
                    {
                        Admin_Menu.Visible = true;
                        POS_Menu.Visible = true;
                    }
                }
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.BranchCode = inif.Read("BranchCode", "CompanyDetails");
            login.SQLClient = SQLCLient;
            login.SQLServer = SQLServer;
            login.ShowDialog();
            if (string.IsNullOrEmpty(login.ActiveUser))
            {
                return;
            }
            ActiveUser = login.ActiveUser;
            ActiveRole = login.ActiveUser;
            tssActiveUser.Text = string.IsNullOrEmpty(ActiveUser) ? "N/A" : ActiveUser;
            tssLogin.Visible = string.IsNullOrEmpty(ActiveUser) ? true : false;
            tssLogout.Visible = !tssLogin.Visible;
            UserValidator(login.ActiveRole);
        }

        private void tssLogout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to log-out?","System log-out",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dr == System.Windows.Forms.DialogResult.Yes)
            {
                ActiveUser = string.Empty;
                tssActiveUser.Text = string.IsNullOrEmpty(ActiveUser) ? "N/A" : ActiveUser;
                tssLogout.Visible = false;
                tssLogin.Visible = !tssLogout.Visible;
                POS_Menu.Visible = tssLogout.Visible;
                Admin_Menu.Visible = tssLogout.Visible;
            }
        }

        private void openPOSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void startPOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashDrawer cd = new CashDrawer();
            cd.BranchCode = inif.Read("BranchCode", "CompanyDetails");
            cd.ActiveUser = ActiveUser;
            cd._sqlClient = SQLCLient;
            cd._sqlServer = SQLServer;
            cd.ShowDialog();
        }

        private void employeeModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ConnType;
            SqlConnection sconn;
            if (!isOnline)
            {
                sconn = SQLCLient;
                ConnType = "client";
            }
            else
            {
                sconn = SQLServer;
                ConnType = "server";
            }

            frmEmployeeMaintenance emp = new frmEmployeeMaintenance();
            emp.SQLCON = sconn;
            emp.MdiParent = this;
            emp.ConnType = ConnType;
            if (!cf.isOpenChildForm(emp.Name))
            {
                emp.Show();
                if (emp.isClose)
                {
                    emp.Close();
                }
            }
        }

              
    }
}
