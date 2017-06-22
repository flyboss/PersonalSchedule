using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLib;
using SeviceLib;
using EncryptLib;
using System.Runtime.InteropServices;
using System.Text;
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

        //[DllImport("myTools.dll", CallingConvention = CallingConvention.Cdecl)]
        //public extern static int ToolsIsInnerLink(StringBuilder str);

        //[DllImport("myTools.dll", CallingConvention = CallingConvention.Cdecl)]
        //public extern static int ToolsIsOuterLink(StringBuilder str);


        //[DllImport("myTools.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr ToolsFormLinkSrc2(StringBuilder str,int flagIsInnerLink);


        //[DllImport("myTools.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr ToolsFormLinkSrc(StringBuilder str);

        //public JsonResult SendData()
        //{
        //    StringBuilder str = new StringBuilder(255);
        //    str.Append("1111111111111111111111");
        //    int f1 = ToolsIsInnerLink(str);//1
        //    str.Append(".");
        //    int f2 = ToolsIsInnerLink(str);//-1



        //    StringBuilder url = new StringBuilder(255);
        //    url.Append("www.baidu.com");
        //    IntPtr intPtr = ToolsFormLinkSrc(url);
        //    string realUrl = Marshal.PtrToStringAnsi(intPtr);
        //    StringBuilder url2 = new StringBuilder(255);
        //    url2.Append("http://www.taobao.com");
        //    IntPtr intPtr2 = ToolsFormLinkSrc(url2);
        //    string realUrl2 = Marshal.PtrToStringAnsi(intPtr);

        //    return Json(new { str1 = f1, str2 = f2, url1=realUrl,url2=realUrl2 });
        //}
    }
}