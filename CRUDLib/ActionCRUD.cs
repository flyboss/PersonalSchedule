using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using ModelLib.Tools;

namespace CRUDLib
{
    public class ActionCRUD
    {
        public static List<cal_action> GetCalActionsOwnByUId(Model1 db, string login_id)
        {
            var cal_output_list = new List<cal_action>();
            var cal_output_iqueryable = from e in db._event
                                        join a in db.action on e.e_id equals a.e_id
                                        where e.u_id == login_id
                                        select new
                                        {
                                            starttime = a.a_starttime,
                                            endtime = a.a_endtime,
                                            a_title = a.a_title,
                                            e_title = e.e_title,
                                            e_id = e.e_id
                                        };
            foreach (var a in cal_output_iqueryable)
            {
                cal_output_list.Add(new cal_action()
                {
                    date = a.starttime.ToString("yyyy-MM-dd"),
                    title = a.e_title + ": " + a.a_title,
                    time = a.starttime.ToString("HH:mm") + "-" + a.endtime.ToString("HH:mm"),
                    itemid = a.e_id
                });
            }
            return cal_output_list;
        }
        public static List<cal_action> GetCalActionsMarkByUId(Model1 db, string login_id)
        {
            var cal_output_list = new List<cal_action>();
            var cal_output_iqueryable = from m in db.mark
                                        where m.u_id == login_id
                                        join e in db._event on m.e_id equals e.e_id
                                        join a in db.action on e.e_id equals a.e_id
                                        select new
                                        {
                                            starttime = a.a_starttime,
                                            endtime = a.a_endtime,
                                            a_title = a.a_title,
                                            e_title = e.e_title,
                                            e_id = e.e_id
                                        };
            foreach (var a in cal_output_iqueryable)
            {
                cal_output_list.Add(new cal_action()
                {
                    date = a.starttime.ToString("yyyy-MM-dd"),
                    title = a.e_title + ": " + a.a_title,
                    time = a.starttime.ToString("HH:mm") + "-" + a.endtime.ToString("HH:mm"),
                    itemid = a.e_id
                });
            }
            return cal_output_list;
        }
        public static int AddActionList(Model1 db, List<action> actionList)
        {
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                foreach(var action in actionList)
                {
                    db.action.Add(action);
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
        public static string GetActionListJsonForEvent(Model1 db, string e_id)
        {
            var i_action_list = from a in db.action
                                  .Where(a => a.e_id == e_id)
                                  .OrderBy(a => a.a_starttime)
                                select new
                                {
                                    title = a.a_title,
                                    starttime = a.a_starttime,
                                    endtime = a.a_endtime
                                };
            return JsonTool.ToJson(i_action_list).Replace("\\", "\\\\");
        }
        public static int deleteActionsByEId(Model1 db, string e_id)
        {
            try
            {
                foreach (var action in db.action.Where(a => a.e_id == e_id))
                {
                    db.action.Remove(action);
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

        //
        public static List<string> getTitle(Model1 db, string title)
        {
            var answer = new List<string>();
            var titles = from a in db.action
                           where a.a_title.Contains(title)
                           select new
                           {
                               title = a.a_title
                           };
            foreach (var a in titles)
            {
                 answer.Add(a.title);
            }
            return answer;
        }

        //
        public static List<string> getIdByTitle(Model1 db, string title)
        {
            var answer = new List<string>();
            var titles = from a in db.action
                         where a.a_title.Contains(title)
                         select new
                         {
                             id = a.e_id
                         };
            foreach (var a in titles)
            {
                answer.Add(a.id);
            }
            return answer;
        }
    }
}
