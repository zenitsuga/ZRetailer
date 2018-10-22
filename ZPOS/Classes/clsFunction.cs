using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ZPOS.Classes
{
    public class clsFunction
    {   
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
    }
}
