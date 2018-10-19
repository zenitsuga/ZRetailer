using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using WS_ZRetailer.Classes;
namespace WS_ZRetailer
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {   
        //Sample webservice for testing purposes only.
        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        public string HelloWorldJSON()
        {
            pubclass[] pc = new pubclass[] 
            {
                new pubclass()
                {
                    MessageString = "Hello World"
                }
            };
            return new JavaScriptSerializer().Serialize(pc);
        }
        [WebMethod]
        public string HelloWorldXML()
        {
            return "Hello World ";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HelloWorldwithArguementJSON(string Name)
        {
            pubclass[] pc = new pubclass[] 
            {
                new pubclass()
                {
                    MessageString = "Hello World " + Name
                }
            };
            return new JavaScriptSerializer().Serialize(pc);
        }
        [WebMethod]
        public string HelloWorldWithArguementXML(string Name)
        {
            return "Hello World " + Name;
        }
        [WebMethod]
        public string CheckSettingsValue(string SettingName)
        {
            string SettingValue = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            return SettingName + " :" + SettingValue;
        }
        [WebMethod]
        public int CheckLicense(string ProjectCode,string MachineID)
        {
            Functions func = new Functions();
            int result = 0;
            try
            {   
                DateTime dtExpiry = new DateTime();
                result = func.CheckLicenseExpiry(ProjectCode, MachineID, ref dtExpiry);
            }
            catch
            {
            }
            return result;
        }
    }
}