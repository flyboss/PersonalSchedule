using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLib;
using SeviceLib;
using Newtonsoft.Json;
namespace PersonalSchedule.Controllers
{
    public class SearchController : Controller
    {
        Model1 db = new Model1();
        // GET: Search
        public ActionResult Index()
        {
            if (Session["login_id"] == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            return View();
        }

        [HttpPost]
        [ActionName("FetchTitles")]
        public string FetchTitlesPost()
        {
            if (Request["title"] == null)
            {
                return "[]";
            }
            string title = Request["title"].ToString();
            List<string> titleList = SerachService.serachContent(db, title);
            return JsonConvert.SerializeObject(titleList);
        }

        [HttpPost]
        [ActionName("FetchEvents")]
        public string FetchEventsPost()
        {
            if (Request["title"] == null || Session["login_id"] == null)
            {
                //return "[]";
            }
            string login_id = "guest";//Session["login_id"].ToString();
            string title = Request["title"].ToString();
            List<string> titleList = SerachService.serachRank(db, title);
            return SerachService.FetchEventsByEIdList(db, login_id, titleList);
        }


        public ActionResult Home()
        {
            if (Session["login_id"] == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            string login_id = Session["login_id"].ToString();
            string u_id = null;
            if (Request["u_id"] == null)
            {
                u_id = login_id;
            }
            else
            {
                u_id = Request["u_id"];
            }
            user user = db.user.Find(u_id);
            if(user == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            string followed = null;
            if(u_id == login_id)
            {
                followed = "self";
            }
            else
            {
                follow follow = db.follow.Find(login_id, u_id);
                if(follow == null)
                {
                    followed = "false";
                }
                else
                {
                    followed = "true";
                }
            }
            ViewBag.followed = followed;
            ViewBag.u_id = user.u_id;
            ViewBag.u_name = user.u_name;
            return View();
        }
        [HttpPost]
        [ActionName("Home")]
        public string HomePost()
        {
            if (Session["login_id"] == null || Request["u_id"] == null)
            {
                return "[]";
            }
            string login_id = Session["login_id"].ToString();
            string u_id = Request["u_id"];
            if (u_id == login_id)
            {
                return SerachService.FetchALLEventsByUId(db, u_id);
            }
            else
            {
                return SerachService.FetchPubEventsByUId(db, u_id);
            }
        }

        [HttpPost]
        [ActionName("HomeFollow")]
        public string HomeFollowPost()
        {
            if (Request["operate"] == null || Request["u_id"] == null || Request["f_u_id"] == null)
            {
                return "";
            }
            string operate = Request["operate"];
            string u_id = Request["u_id"];
            string f_u_id = Request["f_u_id"];
            try
            {
                if(operate == "follow")
                {
                    follow follow = new follow();
                    follow.u_id = u_id;
                    follow.f_u_id = f_u_id;
                    follow.f_time = DateTime.Now;
                    db.follow.Add(follow);
                    db.SaveChanges();
                }
                else if (operate == "unfollow")
                {
                    follow follow = db.follow.Find(u_id, f_u_id);
                    db.follow.Remove(follow);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }
            return "";
        }
    }
}