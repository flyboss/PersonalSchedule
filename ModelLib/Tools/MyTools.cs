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
        static public bool isInnerLink(string l_src)
        {
            if (l_src.Contains('.'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static public bool isOuterLink(String l_src)
        {
            return !isInnerLink(l_src);
        }
        static public string formLinkSrc(String l_src)
        {
            return formLinkSrc(l_src, isInnerLink(l_src));
        }
        static public string formLinkSrc(String l_src, bool flagIsInnerLink)
        {
            if (flagIsInnerLink)
            {
                return l_src;
            }
            int index = -1;
            try
            {
                index = l_src.IndexOf("http");
            }
            catch(Exception e)
            {
                if (e != null)
                {
                    index = -1;
                }
            }
            if(index == 0)
            {
                return l_src;
            }
            return "http://" + l_src;
        }
        static public string transDomStrToValidate (string str)
        {
            StringBuilder result = new StringBuilder();
            for (var i = 0; i != str.Length; i++)
            {
                char chara = str.ElementAt(i);
                if (chara == '!')
                {
                    result.Append("!!");
                }
                else if (chara == '<')
                {
                    result.Append("!1");
                }
                else if (chara == '>')
                {
                    result.Append("!2");
                }
                else
                {
                    result.Append(chara);
                }
            }
            return result.ToString();
        }
        
        static public string transValidateToDomStr(string str)
        {
            StringBuilder result = new StringBuilder();
            for (var i = 0; i != str.Length; i++)
            {
                char chara = str.ElementAt(i);
                if (chara != '!')
                {
                    result.Append(chara);
                }
                else
                {
                    i++;
                    chara = str.ElementAt(i);
                    if (chara == '!')
                    {
                        result.Append("!");
                    }
                    else if (chara == '1')
                    {
                        result.Append("<");
                    }
                    else if (chara == '2')
                    {
                        result.Append(">");
                    }
                }
            }
            return result.ToString();
        }
    }
}