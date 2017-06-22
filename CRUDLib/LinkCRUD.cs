using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using ModelLib.Tools;
using ToolsLib;

namespace CRUDLib
{
    public class LinkCRUD
    {
        public static int AddLinkList(Model1 db, List<link> linkList)
        {
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                foreach (var link in linkList)
                {
                    db.link.Add(link);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                db.Configuration.ValidateOnSaveEnabled = true;
                return -1;
            }
            finally
            {
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            return 1;
        }
        public static string GetLinkListJsonForEvent(Model1 db, string e_id)
        {
            var i_link_list = from l in db.link
                                .Where(l => l.e_id == e_id)
                              select new
                              {
                                  title = l.l_title,
                                  type = l.l_type,
                                  src = l.l_src
                              };
            return JsonTool.ToJson(i_link_list).Replace("\\", "\\\\");
        }
        public static int deleteLinksByEId(Model1 db, string e_id)
        {
            try
            {
                foreach (var link in db.link.Where(l => l.e_id == e_id))
                {
                    db.link.Remove(link);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return -1;
            }
            return 1;
        }

        //lw
        public static List<string> getTitle(Model1 db, string title)
        {
            var answer = new List<string>();
            var titles = from l in db.link
                         where l.l_title.Contains(title)
                         select new
                         {
                             title = l.l_title
                         };
            foreach (var e in titles)
            {
                    answer.Add(e.title);
            }
            return answer;
        }

        //lw
        public static List<string> getIdByTitle(Model1 db, string title)
        {
            var answer = new List<string>();
            var titles = from l in db.link
                         where l.l_title.Contains(title)
                         select new
                         {
                             id = l.e_id
                         };
            foreach (var e in titles)
            {
                answer.Add(e.id);
            }
            return answer;
        }
    }
}
