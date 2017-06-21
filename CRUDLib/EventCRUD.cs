using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;

namespace CRUDLib
{
    public class EventCRUD
    {
        public static int AddEvent(Model1 db, _event _event)
        {
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db._event.Add(_event);
                db.SaveChanges();
            }
            catch(Exception e)
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

        //lw
        public static List<string> getTitle(Model1 db, string title)
        {
            var answer = new List<string>();
            var titles = from e in db._event
                           where e.e_title.Contains(title)
                           select new
                           {
                               title = e.e_title
                           };
            foreach(var e in titles)
            {
                answer.Add(e.title);
            }
            return answer;
        }

        //lw
        public static List<string> getIdByTitle(Model1 db, string title)
        {
            var answer = new List<string>();
            var titles = from e in db._event
                         where e.e_title.Contains(title)
                         select new
                         {
                             id = e.e_id
                         };
            foreach (var e in titles)
            {
                answer.Add(e.id);
            }
            return answer;
        }
        //lw
        public static List<string> getIdByContent(Model1 db, string searchContent)
        {
            var answer = new List<string>();
            var contents = from e in db._event
                         where e.e_content.Contains(searchContent)
                         select new
                         {
                             id = e.e_id
                         };
            foreach (var e in contents)
            {
                answer.Add(e.id);
            }
            return answer;
        }
    }
}
