using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLib;
using SeviceLib;
namespace PersonalSchedule.Controllers
{
    public class TestController : Controller
    {
        private Model1 db = new Model1();
        // GET: Test
        public ActionResult Index()
        {
            var eContentList=SerachService.serachContent(db, "123");
            var mylist = SerachService.serachRank(db, "123");
            return View();
        }
    }
}