namespace ZPOS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Employee_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tssLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tssLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.POS_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.openPOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dayendPOSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helplToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Admin_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.employeeModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssActiveUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssSYNC_STATUS = new System.Windows.Forms.ToolStripStatusLabel();
            this.LastSync = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTransaction = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel13 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssLocalStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel12 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tss_Today = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolAccess = new System.Windows.Forms.ToolStrip();
            this.tssEmployee = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ToolAccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Employee_Menu,
            this.POS_Menu,
            this.helpToolStripMenuItem,
            this.Admin_Menu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(821, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Employee_Menu
            // 
            this.Employee_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLogin,
            this.tssLogout,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.Employee_Menu.Name = "Employee_Menu";
            this.Employee_Menu.Size = new System.Drawing.Size(71, 20);
            this.Employee_Menu.Text = "Employee";
            // 
            // tssLogin
            // 
            this.tssLogin.Name = "tssLogin";
            this.tssLogin.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tssLogin.Size = new System.Drawing.Size(134, 22);
            this.tssLogin.Text = "Log&in";
            this.tssLogin.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // tssLogout
            // 
            this.tssLogout.Name = "tssLogout";
            this.tssLogout.Size = new System.Drawing.Size(134, 22);
            this.tssLogout.Text = "Logout";
            this.tssLogout.Visible = false;
            this.tssLogout.Click += new System.EventHandler(this.tssLogout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // POS_Menu
            // 
            this.POS_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPOSToolStripMenuItem,
            this.setRegisterToolStripMenuItem,
            this.toolStripSeparator3,
            this.reportsToolStripMenuItem});
            this.POS_Menu.Name = "POS_Menu";
            this.POS_Menu.Size = new System.Drawing.Size(41, 20);
            this.POS_Menu.Text = "POS";
            this.POS_Menu.Visible = false;
            // 
            // openPOSToolStripMenuItem
            // 
            this.openPOSToolStripMenuItem.Name = "openPOSToolStripMenuItem";
            this.openPOSToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.openPOSToolStripMenuItem.Text = "&Open POS";
            this.openPOSToolStripMenuItem.Click += new System.EventHandler(this.openPOSToolStripMenuItem_Click);
            // 
            // setRegisterToolStripMenuItem
            // 
            this.setRegisterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startPOSToolStripMenuItem,
            this.dayendPOSToolStripMenuItem});
            this.setRegisterToolStripMenuItem.Name = "setRegisterToolStripMenuItem";
            this.setRegisterToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.setRegisterToolStripMenuItem.Text = "&Set Cash Drawer";
            // 
            // startPOSToolStripMenuItem
            // 
            this.startPOSToolStripMenuItem.Name = "startPOSToolStripMenuItem";
            this.startPOSToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.startPOSToolStripMenuItem.Text = "Start POS";
            this.startPOSToolStripMenuItem.Click += new System.EventHandler(this.startPOSToolStripMenuItem_Click);
            // 
            // dayendPOSToolStripMenuItem
            // 
            this.dayendPOSToolStripMenuItem.Name = "dayendPOSToolStripMenuItem";
            this.dayendPOSToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dayendPOSToolStripMenuItem.Text = "Day-end POS";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(156, 6);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.helplToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helplToolStripMenuItem
            // 
            this.helplToolStripMenuItem.Name = "helplToolStripMenuItem";
            this.helplToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.helplToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // Admin_Menu
            // 
            this.Admin_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.employeeModuleToolStripMenuItem});
            this.Admin_Menu.Name = "Admin_Menu";
            this.Admin_Menu.Size = new System.Drawing.Size(96, 20);
            this.Admin_Menu.Text = "Administrative";
            this.Admin_Menu.Visible = false;
            // 
            // employeeModuleToolStripMenuItem
            // 
            this.employeeModuleToolStripMenuItem.Name = "employeeModuleToolStripMenuItem";
            this.employeeModuleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.employeeModuleToolStripMenuItem.Text = "Employee Module";
            this.employeeModuleToolStripMenuItem.Click += new System.EventHandler(this.employeeModuleToolStripMenuItem_Click);
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tss_Status,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.tssActiveUser,
            this.toolStripStatusLabel8,
            this.tssSYNC_STATUS,
            this.LastSync,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel9,
            this.tssTransaction,
            this.toolStripStatusLabel10,
            this.toolStripStatusLabel5,
            this.tssServerStatus,
            this.toolStripStatusLabel13,
            this.tssLocalStatus,
            this.toolStripStatusLabel11,
            this.toolStripStatusLabel12,
            this.toolStripStatusLabel6,
            this.tss_Today});
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(821, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // tss_Status
            // 
            this.tss_Status.Name = "tss_Status";
            this.tss_Status.Size = new System.Drawing.Size(43, 17);
            this.tss_Status.Text = "READY";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(33, 17);
            this.toolStripStatusLabel3.Text = "User:";
            // 
            // tssActiveUser
            // 
            this.tssActiveUser.Name = "tssActiveUser";
            this.tssActiveUser.Size = new System.Drawing.Size(29, 17);
            this.tssActiveUser.Text = "N/A";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel8.Spring = true;
            // 
            // tssSYNC_STATUS
            // 
            this.tssSYNC_STATUS.Name = "tssSYNC_STATUS";
            this.tssSYNC_STATUS.Size = new System.Drawing.Size(40, 17);
            this.tssSYNC_STATUS.Text = "SYNC:";
            // 
            // LastSync
            // 
            this.LastSync.Name = "LastSync";
            this.LastSync.Size = new System.Drawing.Size(29, 17);
            this.LastSync.Text = "N/A";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel7.Spring = true;
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(72, 17);
            this.toolStripStatusLabel9.Text = "Transaction:";
            // 
            // tssTransaction
            // 
            this.tssTransaction.Name = "tssTransaction";
            this.tssTransaction.Size = new System.Drawing.Size(29, 17);
            this.tssTransaction.Text = "N/A";
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // tssServerStatus
            // 
            this.tssServerStatus.Name = "tssServerStatus";
            this.tssServerStatus.Size = new System.Drawing.Size(136, 17);
            this.tssServerStatus.Text = "SERVER: Not Connected ";
            // 
            // toolStripStatusLabel13
            // 
            this.toolStripStatusLabel13.Name = "toolStripStatusLabel13";
            this.toolStripStatusLabel13.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel13.Spring = true;
            // 
            // tssLocalStatus
            // 
            this.tssLocalStatus.Name = "tssLocalStatus";
            this.tssLocalStatus.Size = new System.Drawing.Size(131, 17);
            this.tssLocalStatus.Text = "LOCAL: Not Connected";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel11.Spring = true;
            // 
            // toolStripStatusLabel12
            // 
            this.toolStripStatusLabel12.Name = "toolStripStatusLabel12";
            this.toolStripStatusLabel12.Size = new System.Drawing.Size(3, 17);
            this.toolStripStatusLabel12.Spring = true;
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(54, 17);
            this.toolStripStatusLabel6.Text = "Today is:";
            // 
            // tss_Today
            // 
            this.tss_Today.Name = "tss_Today";
            this.tss_Today.Size = new System.Drawing.Size(144, 17);
            this.tss_Today.Text = "MM-DD-YYYY HH:MM:SS";
            // 
            // ToolAccess
            // 
            this.ToolAccess.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssEmployee});
            this.ToolAccess.Location = new System.Drawing.Point(0, 24);
            this.ToolAccess.Name = "ToolAccess";
            this.ToolAccess.Size = new System.Drawing.Size(821, 25);
            this.ToolAccess.TabIndex = 5;
            this.ToolAccess.Text = "toolStrip1";
            this.ToolAccess.Visible = false;
            // 
            // tssEmployee
            // 
            this.tssEmployee.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tssEmployee.Image = ((System.Drawing.Image)(resources.GetObject("tssEmployee.Image")));
            this.tssEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssEmployee.Name = "tssEmployee";
            this.tssEmployee.Size = new System.Drawing.Size(23, 22);
            this.tssEmployee.Text = "toolStripButton1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(821, 377);
            this.Controls.Add(this.ToolAccess);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZPOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ToolAccess.ResumeLayout(false);
            this.ToolAccess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Employee_Menu;
        private System.Windows.Forms.ToolStripMenuItem tssLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helplToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tss_Status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tssActiveUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel tss_Today;
        private System.Windows.Forms.ToolStripStatusLabel tssSYNC_STATUS;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel LastSync;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel tssTransaction;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel tssServerStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripStatusLabel tssLocalStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel12;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel13;
        private System.Windows.Forms.ToolStripMenuItem tssLogout;
        private System.Windows.Forms.ToolStripMenuItem Admin_Menu;
        private System.Windows.Forms.ToolStripMenuItem POS_Menu;
        private System.Windows.Forms.ToolStripMenuItem openPOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setRegisterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startPOSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dayendPOSToolStripMenuItem;
        private System.Windows.Forms.ToolStrip ToolAccess;
        private System.Windows.Forms.ToolStripButton tssEmployee;
        private System.Windows.Forms.ToolStripMenuItem employeeModuleToolStripMenuItem;
    }
}

