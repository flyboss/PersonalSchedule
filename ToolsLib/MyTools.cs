using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ToolsLib
{
    public class StringTools
    {

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