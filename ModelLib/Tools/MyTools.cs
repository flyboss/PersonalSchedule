using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ModelLib.Tools
{
    public class MyTools
    {
        static public string getNewEId(long max_e_id)
        {
            string new_e_id = "00000000000000000000000000000000000000000" + (max_e_id + 1);
            return new_e_id.Substring(new_e_id.Length - 40);
        }
        static public string getNewEId(string max_e_id)
        {
            return getNewEId(long.Parse(max_e_id));
        }
    }
}