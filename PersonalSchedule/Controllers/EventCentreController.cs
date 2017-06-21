using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLib;
using ModelLib.Tools;
using SeviceLib;
using PersonalSchedule.Models;

namespace PersonalSchedule.Controllers
{
    public class EventCentreController : Controller
    {
        private Model1 db = new Model1();
        // GET: EventCentre
        // WebPage: NewEvent
        public ActionResult NewEvent()
        {
            if (Session["login_id"] == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            return View();
        }

        [HttpPost]
        [ActionName("NewEvent")]
        public string NewEventPost()
        {
            string login_id = Session["login_id"].ToString();
            string json_input = Request["json_input"].ToString();
            if (Session["login_id"] == null || json_input == null)
            {
                NewEvent();
                return "wrongInput";
            }
            return EventCentreService.AddNewEventByInputJson(db, login_id, json_input);
        }


        // WebPage: Event
        public ActionResult Event()
        {
            string login_id = "";
            if (Session["login_id"] != null)
            {
                login_id = Session["login_id"].ToString();
            }
            else
            {
                Session["login_name"] = "Visitor";
            }
            string e_id = Request["e_id"];
            if (e_id == null)
            {
                Session["error_page_content"] = "该事件不存在或不可访问";
                return Redirect("/EventCentre/ErrorPage");
            }
            
            _event _event = db._event.Find(e_id);
            if (_event == null || (login_id != _event.u_id && _event.e_auth != "pub"))
            {
                Session["error_page_content"] = "该事件不存在或不可访问";
                return Redirect("/EventCentre/ErrorPage");
            }
            string owner_id, owner_name, marked, link_list, action_list, tag_list;
            int message = EventCentreService.GetEventByEId(db, login_id, _event,
                out owner_id, out owner_name, out marked, out link_list, out action_list, out tag_list);
            ViewBag.event_id = _event.e_id;
            ViewBag.event_title = _event.e_title;
            ViewBag.event_content = _event.e_content;
            ViewBag.owner_id = owner_id;
            ViewBag.owner_name = owner_name;
            ViewBag.marked = marked;
            ViewBag.link_list = link_list;
            ViewBag.action_list = action_list;
            ViewBag.tag_list = tag_list;
            if (login_id.Equals(""))
            {
                ViewBag.marked = "guest";
            }
            return View();
        }

        [HttpPost]
        [ActionName("Event")]
        public ActionResult EventPost()
        {
            switch (Request["operate"])
            {
                case "mark":
                    EventCentreService.DoMark(db, Request["u_id"], Request["e_id"]);
                    break;
                case "unmark":
                    EventCentreService.DoUnmark(db, Request["u_id"], Request["e_id"]);
                    break;
                default:
                    break;
            }
            return Event();
        }


        // WebPage: ErrorPage
        public ActionResult ErrorPage()
        {
            return View();
        }


        // WebPage: SuccessPage
        public ActionResult SuccessPage()
        {
            if (Session["login_id"] == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            ViewBag.newUrl = Request["e_id"];
            return View();
        }


        // WebPage: EditEvent
        public ActionResult EditEvent()
        {

            if (Session["login_id"] == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            string e_id = Request["e_id"];
            if (e_id == null)
            {
                Session["error_page_content"] = "该事件不存在或不可访问";
                return Redirect("/EventCentre/ErrorPage");
            }
            string login_id = Session["login_id"].ToString();
            _event _event = db._event.Find(e_id);
            if (_event == null || login_id != _event.u_id)
            {
                Session["error_page_content"] = "该事件不存在或不可访问";
                return Redirect("/EventCentre/ErrorPage");
            }
            string owner_id, owner_name, marked, link_list, action_list, tag_list;
            int message = EventCentreService.GetEventByEId(db, login_id, _event,
                out owner_id, out owner_name, out marked, out link_list, out action_list, out tag_list);
            ViewBag.event_id = _event.e_id;
            ViewBag.event_title = _event.e_title;
            ViewBag.event_content = _event.e_content;
            ViewBag.owner_id = owner_id;
            ViewBag.owner_name = owner_name;
            ViewBag.marked = marked;
            ViewBag.link_list = link_list;
            ViewBag.action_list = action_list;
            ViewBag.tag_list = tag_list;
            ViewBag.event_auth = _event.e_auth;
            return View();
        }

        [HttpPost]
        [ActionName("EditEvent")]
        public string EditEventPost()
        {
            if (Session["login_id"] == null || Request["json_input"] == null)
            {
                NewEvent();
                return "wrongInput";
            }
            string login_id = Session["login_id"].ToString();
            string json_input = Request["json_input"].ToString();
            return EventCentreService.EditEventByInputJson(db, login_id, json_input);
        }

        [HttpPost]
        [ActionName("EditEventDelete")]
        public string EditEventDeletePost()
        {
            if (Session["login_id"] == null || Request["e_id"] == null)
            {
                return "wrongInput";
            }
            string login_id = Session["login_id"].ToString();
            string e_id = Request["e_id"].ToString();
            return EventCentreService.DeleteEventByEId(db, login_id, e_id);
        }
    }
}