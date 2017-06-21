using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using CRUDLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeviceLib
{
    public class SerachService
    {
        private static int eContent = 5;
        private static int eTitle = 5;
        private static int aTitle = 5;
        private static int tTitle = 5;
        private static int lTitle = 5;
        public static List<string> serachContent(Model1 db,string title)
        {
            var eventTitles = EventCRUD.getTitle(db, title);
            var actionTitles = ActionCRUD.getTitle(db, title);
            var tagTitles = TagCRUD.getTitle(db, title);
            var linkTitles = LinkCRUD.getTitle(db, title);
            List<string> answer = new List<string>();
            answer.AddRange(eventTitles);
            answer.AddRange(actionTitles);
            answer.AddRange(tagTitles);
            answer.AddRange(linkTitles);
            return answer.Distinct().ToList();
        }

        public static List<string> serachRank(Model1 db,string title)
        {
            var eventTitles = EventCRUD.getIdByTitle(db, title);
            var eventContents = EventCRUD.getIdByContent(db, title);
            var actionTitles = ActionCRUD.getIdByTitle(db, title);
            var tagTitles = TagCRUD.getIdByTitle(db, title);
            var linkTitles = LinkCRUD.getIdByTitle(db, title);
            List<searchWeight> answer = new List<searchWeight>();
            addAnswer(answer, eventTitles, eTitle);
            addAnswer(answer, eventContents, eContent);
            addAnswer(answer, actionTitles, aTitle);
            addAnswer(answer, tagTitles, tTitle);
            addAnswer(answer, linkTitles, lTitle);
            answer.Sort(mySort);
            List<string> finalAnswer = new List<string>();
            foreach(var s in answer)
            {
                finalAnswer.Add(s.id);
            }
            return finalAnswer;
        }

        private static void addAnswer(List<searchWeight> answer,List<string> intList,int weight)
        {
            foreach (var i in intList)
            {
                bool change = false;
                foreach (var sw in answer)
                {
                    if (sw.id.Equals(i))
                    {
                        sw.count += weight;
                        change = true;
                    }
                }
                if (change == false)
                {
                    answer.Add(new searchWeight(i, weight));
                }
            }
        }

        private static int mySort(searchWeight s1, searchWeight s2)
        {
            if (s1.count > s2.count)
                return -1;
            else
                return 1;  
        }

        public static string FetchEventsByEIdList(Model1 db, string login_id, List<string> e_id_list)
        {
            JArray json = new JArray();
            foreach(var e_id in e_id_list)
            {
                _event _event = db._event.Find(e_id);
                if(_event != null && (_event.e_auth == "pub" || _event.e_auth == login_id))
                {
                    JObject jo = new JObject();
                    jo.Add("e_id", _event.e_id);
                    jo.Add("e_title", _event.e_title);
                    jo.Add("u_id", _event.u_id);
                    jo.Add("e_time", _event.e_time.ToString("yyyy-MM-dd HH:mm"));
                    //user user = db.user.Find(_event.u_id);
                    jo.Add("u_name", _event.user.u_name);
                    JArray tags = new JArray();
                    foreach(var tag in _event.tag)
                    {
                        tags.Add(tag.t_title);
                    }
                    jo.Add("tags", tags);
                    json.Add(jo);
                }
            }
            return json.ToString();
        }
        public static string FetchALLEventsByUId(Model1 db, string u_id)
        {
            var e_id_i = from e in db._event
                         where e.u_id == u_id
                         orderby e.e_time descending
                         select new
                         {
                             e_id = e.e_id
                         };
            List<string> e_id_list = new List<string>();
            foreach (var e in e_id_i)
            {
                e_id_list.Add(e.e_id);
            }
            return FetchAllEventsByEIdList(db, e_id_list);
        }
        public static string FetchPubEventsByUId(Model1 db, string u_id)
        {
            var e_id_i = from e in db._event
                         where e.u_id == u_id && e.e_auth == "pub"
                         orderby e.e_time descending
                         select new
                         {
                             e_id = e.e_id
                         };
            List<string> e_id_list = new List<string>();
            foreach (var e in e_id_i)
            {
                e_id_list.Add(e.e_id);
            }
            return FetchAllEventsByEIdList(db, e_id_list);
        }
        public static string FetchAllEventsByEIdList(Model1 db, List<string> e_id_list)
        {
            JArray json = new JArray();
            foreach (var e_id in e_id_list)
            {
                _event _event = db._event.Find(e_id);
                if (_event != null)
                {
                    JObject jo = new JObject();
                    jo.Add("e_id", _event.e_id);
                    jo.Add("e_title", _event.e_title);
                    jo.Add("u_id", _event.u_id);
                    jo.Add("e_time", _event.e_time.ToString("yyyy-MM-dd HH:mm"));
                    //user user = db.user.Find(_event.u_id);
                    jo.Add("u_name", _event.user.u_name);
                    JArray tags = new JArray();
                    foreach (var tag in _event.tag)
                    {
                        tags.Add(tag.t_title);
                    }
                    jo.Add("tags", tags);
                    json.Add(jo);
                }
            }
            return json.ToString();
        }
    }
}
