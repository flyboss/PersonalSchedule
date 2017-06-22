using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ModelLib;
using ModelLib.Tools;
using Newtonsoft.Json.Linq;
using CRUDLib;
using System.Runtime.InteropServices;

namespace SeviceLib
{
    public class EventCentreService
    {

        [DllImport("myTools.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int ToolsIsInnerLink(StringBuilder str);


        [DllImport("myTools.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ToolsFormLinkSrc2(StringBuilder str, int flagIsInnerLink);

        public static string AddNewEventByInputJson(Model1 db, string login_id, string json_input)
        {
            string new_e_id = null;
            try
            {
                JObject jo_main = JObject.Parse(json_input);
                string max_e_id = db._event.Max(e => e.e_id);
                if (max_e_id == null) max_e_id = "0";
                new_e_id = MyTools.getNewEId(max_e_id);
                _event _event = new _event();
                _event.e_id = new_e_id;
                _event.u_id = login_id;
                _event.e_title = jo_main["title"].ToString();
                _event.e_content = jo_main["content"].ToString();
                _event.e_auth = jo_main["auth"].ToString();
                _event.e_type = "其他";
                _event.e_time = DateTime.Now;
                int message = 1;
                if (message > 0) message = EventCRUD.AddEvent(db, _event);
                if (message > 0) message = AddEnclosureForEvent(db, jo_main, _event);
                if (message < 0)
                {
                    return "exception";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return "exception";
            }
            return new_e_id;
        }
        public static string EditEventByInputJson(Model1 db, string login_id, string json_input)
        {
            string e_id = null;
            try
            {
                JObject jo_main = JObject.Parse(json_input);
                e_id = jo_main["id"].ToString();
                _event _event = db._event.Find(e_id);
                _event.e_title = jo_main["title"].ToString();
                _event.e_content = jo_main["content"].ToString();
                _event.e_auth = jo_main["auth"].ToString();
                _event.e_type = "其他";
                _event.e_time = DateTime.Now;
                db.Entry<_event>(_event).State = EntityState.Modified;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;
                int message = 1;
                if (message > 0) message = MarkCRUD.deleteMarksByEId(db, e_id);
                if (message > 0) message = TagCRUD.deleteTagsByEId(db, e_id);
                if (message > 0) message = LinkCRUD.deleteLinksByEId(db, e_id);
                if (message > 0) message = ActionCRUD.deleteActionsByEId(db, e_id);
                if (message > 0) message = AddEnclosureForEvent(db, jo_main, _event);
                if (message < 0)
                {
                    return "exception";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return "exception";
            }
            return e_id;

        }
        public static int AddEnclosureForEvent(Model1 db, JObject jo_main, _event _event)
        {
            try
            {
                JArray ja_actions = (JArray)jo_main["actions"];
                JArray ja_links = (JArray)jo_main["links"];
                JArray ja_tags = (JArray)jo_main["tags"];
                List<action> actionList = new List<action>();
                List<link> linkList = new List<link>();
                List<tag> tagList = new List<tag>();
                for (int i = 0; i != ja_actions.Count; i++)
                {
                    JObject jo_action = (JObject)ja_actions[i];
                    string action_date = jo_action["date"].ToString();
                    string start_time = jo_action["starttime"].ToString();
                    string end_time = jo_action["endtime"].ToString();
                    action action = new action();
                    action.e_id = _event.e_id;
                    action.a_starttime = DateTime.Parse(action_date + " " + start_time);
                    action.a_endtime = DateTime.Parse(action_date + " " + end_time);
                    action.a_content = "";
                    action.a_title = jo_action["title"].ToString();
                    actionList.Add(action);
                }
                for (int i = 0; i != ja_links.Count; i++)
                {
                    JObject jo_link = (JObject)ja_links[i];
                    string l_src = jo_link["src"].ToString();
                    StringBuilder sb = new StringBuilder(l_src);
                    int flagIsInnerLink = ToolsIsInnerLink(sb);
                    link link = new link();
                    link.e_id = _event.e_id;
                    link.l_title = jo_link["title"].ToString();
                    IntPtr intPtr = ToolsFormLinkSrc2(sb, flagIsInnerLink);
                    link.l_src = Marshal.PtrToStringAnsi(intPtr);
                    if (flagIsInnerLink > 0) link.l_type = "inn";
                    else link.l_type = "out";
                    link.l_content = "";
                    linkList.Add(link);
                }
                for (int i = 0; i != ja_tags.Count; i++)
                {
                    JObject jo_tag = (JObject)ja_tags[i];
                    string t_title = jo_tag["title"].ToString();
                    tag tag = new tag();
                    tag.e_id = _event.e_id;
                    tag.t_title = t_title;
                    tag.t_time = DateTime.Now;
                    tagList.Add(tag);
                }
                int message = 1;
                if (message > 0) message = ActionCRUD.AddActionList(db, actionList);
                if (message > 0) message = LinkCRUD.AddLinkList(db, linkList);
                if (message > 0) message = TagCRUD.AddTagList(db, tagList);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return -1;
            }
            return 1;
        }
        public static string DeleteEventByEId(Model1 db, string login_id, string e_id)
        {
            try
            {
                _event _event = db._event.Find(e_id);
                if (_event.u_id != login_id)
                {
                    return "wrongAuth";
                }
                int message = 1;
                if (message > 0) message = MarkCRUD.deleteMarksByEId(db, e_id);
                if (message > 0) message = TagCRUD.deleteTagsByEId(db, e_id);
                if (message > 0) message = LinkCRUD.deleteLinksByEId(db, e_id);
                if (message > 0) message = ActionCRUD.deleteActionsByEId(db, e_id);
                if (message > 0)
                {
                    db._event.Remove(_event);
                    db.SaveChanges();
                }
                if (message < 0)
                {
                    return "exception";
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return "exception";
            }
            return e_id;
        }
        public static int GetEventByEId(Model1 db, string login_id, _event _event, 
            out string owner_id, out string owner_name,
            out string marked, out string link_list, out string action_list, out string tag_list)
        {
            owner_id = null;
            owner_name = null;
            marked = null;
            link_list = null;
            action_list = null;
            tag_list = null;
            try
            {
                user user = db.user.Find(_event.u_id);
                mark mark = db.mark.Find(login_id, _event.e_id);
                owner_id = user.u_id;
                owner_name = user.u_name;
                if (user.u_id == login_id)
                {
                    marked = "self";
                }
                else if (mark == null)
                {
                    marked = "false";
                }
                else
                {
                    marked = "true";
                }
                link_list = LinkCRUD.GetLinkListJsonForEvent(db, _event.e_id);
                action_list = ActionCRUD.GetActionListJsonForEvent(db, _event.e_id);
                tag_list = TagCRUD.GetTagListJsonForEvent(db, _event.e_id);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return -1;
            }
            return 1;
        }

        public static bool DoMark(Model1 db, string u_id, string e_id)
        {
            try
            {
                mark mark = new mark();
                mark.u_id = u_id;
                mark.e_id = e_id;
                mark.m_time = DateTime.Now;
                db.mark.Add(mark);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
        public static bool DoUnmark(Model1 db, string u_id, string e_id)
        {
            try
            {
                mark mark = db.mark.Find(u_id, e_id);
                db.mark.Remove(mark);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
}

/*
 * string new_e_id = null;
            try
            {
                JObject jo_main = JObject.Parse(json_input);
                JArray ja_actions = (JArray)jo_main["actions"];
                JArray ja_links = (JArray)jo_main["links"];
                JArray ja_tags = (JArray)jo_main["tags"];
                string max_e_id = db._event.Max(e => e.e_id);
                if (max_e_id == null) max_e_id = "0";
                new_e_id = MyTools.getNewEId(max_e_id);
                _event _event = new _event();
                List<action> actionList = new List<action>();
                List<link> linkList = new List<link>();
                List<tag> tagList = new List<tag>();
                _event.e_id = new_e_id;
                _event.u_id = login_id;
                _event.e_title = jo_main["title"].ToString();
                _event.e_content = jo_main["content"].ToString();
                _event.e_auth = jo_main["auth"].ToString();
                _event.e_type = "其他";
                for (int i = 0; i != ja_actions.Count; i++)
                {
                    JObject jo_action = (JObject)ja_actions[i];
                    string action_date = jo_action["date"].ToString();
                    string start_time = jo_action["starttime"].ToString();
                    string end_time = jo_action["endtime"].ToString();
                    action action = new action();
                    action.e_id = _event.e_id;
                    action.a_starttime = DateTime.Parse(action_date + " " + start_time);
                    action.a_endtime = DateTime.Parse(action_date + " " + end_time);
                    action.a_content = "";
                    action.a_title = jo_action["title"].ToString();
                    actionList.Add(action);
                }
                for (int i = 0; i != ja_links.Count; i++)
                {
                    JObject jo_link = (JObject)ja_links[i];
                    string l_src = jo_link["src"].ToString();
                    bool flagIsInnerLink = MyTools.isInnerLink(l_src);
                    link link = new link();
                    link.e_id = _event.e_id;
                    link.l_title = jo_link["title"].ToString();
                    link.l_src = MyTools.formLinkSrc(l_src, flagIsInnerLink);
                    if (flagIsInnerLink) link.l_type = "inn";
                    else link.l_type = "out";
                    link.l_content = "";
                    linkList.Add(link);
                }
                for (int i = 0; i != ja_tags.Count; i++)
                {
                    JObject jo_tag = (JObject)ja_tags[i];
                    string t_title = jo_tag["title"].ToString();
                    tag tag = new tag();
                    tag.e_id = _event.e_id;
                    tag.t_title = t_title;
                    tag.t_time = DateTime.Now;
                    tagList.Add(tag);
                }
                int message = 1;
                if (message > 0) message = EventCRUD.AddEvent(db, _event);
                if (message > 0) message = ActionCRUD.AddActionList(db, actionList);
                if (message > 0) message = LinkCRUD.AddLinkList(db, linkList);
                if (message > 0) message = TagCRUD.AddTagList(db, tagList);
                if (message < 0)
                {
                    return "exception";
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return "exception";
            }
            return new_e_id;
 */
