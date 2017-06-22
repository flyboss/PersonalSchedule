using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using ModelLib;
using ModelLib.Tools;
using SeviceLib;
using PersonalSchedule.Models;
using EncryptLib;
using ToolsLib;
using LIWeiYINYifanLib;

namespace PersonalSchedule.Controllers
{
    public class PersonalScheduleController : Controller
    {
        private Model1 db = new Model1();

        // GET: PersonalSchedule
        // WebPage: Index
        public ActionResult Index()
        {
            WeatherTools.Address address = GetCurrentAddress();
            string json = GetWeatherJson(address);
            Session["weather_json"] = json;
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            string error_message = null;
            myMD5 m1 = new myMD5();
            switch (Request["operate"])
            {
                case "login":
                    error_message = Login(Request["login_id"], m1.getMD5(Request["login_pwd"]));
                    System.Diagnostics.Debug.WriteLine(error_message);
                    Response.Write(error_message);
                    return View();
                case "register":
                    error_message = Register(Request["register_id"], m1.getMD5(Request["register_pwd"]));
                    System.Diagnostics.Debug.WriteLine(error_message);
                    Response.Write(error_message);
                    return View();
                case "logout":
                    error_message = Logout();
                    System.Diagnostics.Debug.WriteLine(error_message);
                    Response.Write(error_message);
                    return View();
                default:
                    break;
            }
            return View();
        }
        private string Login(string login_id, string login_pwd)
        {
            user user = null;
            string output = PersonalScheduleService.Login(db, login_id, login_pwd, out user);
            if (output.Equals("登陆成功"))
            {
                Session["login_id"] = login_id;
                Session["login_name"] = user.u_name;
            }
            return output;
        }
        private string Register(string register_id, string register_pwd, string register_name = null, string register_auth = "usr")
        {
            string message = PersonalScheduleService.Register(db, register_id, register_pwd, register_name, register_auth);
            if (message.Equals("注册成功"))
            {
                try
                {
                    System.IO.File.Copy(Server.MapPath(HeaderInit.FormFilePath("~/img/default/default-user-", 2, ".png")), Server.MapPath("~/img/user/" + register_id + "_.png"), true);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }
            return message;
        }
        private string Logout()
        {
            Session["login_id"] = null;
            Session["login_name"] = null;
            return "退出成功";
        }
        private WeatherTools.Address GetCurrentAddress()
        {
            try
            {
                WeatherTools.Address currentAddress = null;
                if (Session["current_address"] != null)
                {
                    currentAddress = (WeatherTools.Address)Session["current_address"];
                }
                else
                {
                    currentAddress = WeatherTools.GetIpLookUp();
                    Session["current_address"] = currentAddress;
                }
                return currentAddress;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
                return null;
            }
        }
        private string GetWeatherJson(WeatherTools.Address address)
        {
            try
            {
                if (address == null)
                {
                    return "NetworkError";
                }
                WeatherTools.GetIdForPCC(address);
                //return WeatherTools.GetFullWeather(address);
                return address.city.name + " " + WeatherTools.SpideWeather();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return "error";
            }
        }

        // WebPage: Cal

        public ActionResult Cal()
        {
            if (Session["login_id"] == null)
            {
                return Redirect("/PersonalSchedule/Index");
            }
            string login_id = Session["login_id"].ToString();
            ViewBag.cal_output_json = PersonalScheduleService.GetCalActionsByUId(db, login_id);//"[{\"date\":\"2017-05-14\",\"title\":\"SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSingle Day Event\",\"time\":\"10:00-12:00\",\"itemid\":\"000\"},{\"date\":\"2017-05-23\",\"title\":\"Single Day Event\",\"time\":\"2\",\"itemid\":\"000\"},{\"date\":\"2017-05-17\",\"title\":\"Single Day Event\",\"time\":\"3\",\"itemid\":\"000\"},{\"date\":\"2017-05-26\",\"title\":\"Single Day Event1\",\"time\":\"4\",\"itemid\":\"000\"},{\"date\":\"2017-05-28\",\"title\":\"Single Day Event3\",\"time\":\"6\",\"itemid\":\"000\"},{\"date\":\"2017-05-26\",\"title\":\"Single Day Event2\",\"time\":\"5\",\"itemid\":\"000\"},{\"date\":\"2017-06-06\",\"title\":\"Single Day Event2\",\"time\":\"7\",\"itemid\":\"000\"},{\"date\":\"2017-06-08\",\"title\":\"Single Day Event2\",\"time\":\"8\",\"itemid\":\"000\"}]";
            return View();
        }
    }
}