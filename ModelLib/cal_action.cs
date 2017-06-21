using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ModelLib.Tools;

namespace ModelLib
{
    public class cal_action
    {
        public string date { get; set; }
        public string title { get; set; }
        public string time { get; set; }
        public string itemid { get; set; }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"date\":\"");
            sb.Append(date);
            sb.Append("\",\"title\":\"");
            sb.Append(JsonTool.String2Json(title).Replace("\\","\\\\"));
            sb.Append("\",\"time\":\"");
            sb.Append(time);
            sb.Append("\",\"itemid\":\"");
            sb.Append(itemid);
            sb.Append("\"}");
            return sb.ToString();
        }
    }
}