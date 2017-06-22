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
    public class TagCRUD
    {
        public static int AddTagList(Model1 db, List<tag> tagList)
        {
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                foreach (var tag in tagList)
                {
                    db.tag.Add(tag);
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
        public static string GetTagListJsonForEvent(Model1 db, string e_id)
        {
            var i_tag_list = from t in db.tag
                                  .Where(t => t.e_id == e_id)
                                select new
                                {
                                    title = t.t_title
                                };
            return JsonTool.ToJson(i_tag_list).Replace("\\", "\\\\");
        }
        public static int deleteTagsByEId(Model1 db, string e_id)
        {
            try
            {
                foreach (var tag in db.tag.Where(t => t.e_id == e_id))
                {
                    db.tag.Remove(tag);
                }
                db.SaveChanges();
            }
            catch(Exception e)
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
            var titles = from t in db.tag
                         where t.t_title.Contains(title)
                         select new
                         {
                             title = t.t_title
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
            var titles = from t in db.tag
                         where t.t_title.Contains(title)
                         select new
                         {
                             id = t.e_id
                         };
            foreach (var e in titles)
            {
                answer.Add(e.id);
            }
            return answer;
        }
    }
}
