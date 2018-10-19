using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.Drawing;

namespace ZRetailer.Classes
{
    //ErrorCode
    //001 = Invalid License
    //002 = No internet Connection
    //003 = Format Validation

    public class clsFunctions
    {
        public IniFile inif;
        //Parser
        #region Parser
        public bool ParseBool(string Val)
        {
            bool result = false;
            try
            {
                result = bool.Parse(Val);
            }
            catch
            {
            }
            return result;
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
        //Checker,Validation
        #region Checker
        public bool CheckInternetConnection()
        {
            bool result = false;
            try
            {

            }
            catch
            {
            }
            return result;
        }
        #endregion
        //Ini and Configuration
        #region Iniconfig
        public string GetIniValue(string Module, string Section)
        {
            string result = string.Empty;
            try
            {
                result = inif.Read(Module, Section);
            }
            catch
            {
            }
            return result;
        }
        public string GetPCode()
        {
           return GetIniValue("PCode", "SYSTEM");
        }
        #endregion

        #region Security
        public String getMotherBoardID()
        {
            String serial = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    serial = mo["SerialNumber"].ToString();
                }
                return serial;
            }
            catch (Exception)
            {
                return serial;
            }
        }
        #endregion

        #region UI Settings
        public TextBox tbHighLight(TextBox tb, bool isLight)
        {
            if (isLight)
            {
                tb.BackColor = Color.LightYellow;
            }
            else
            {
                tb.BackColor = Color.White;
            }

            return tb;
        }
        #endregion
    }
}
