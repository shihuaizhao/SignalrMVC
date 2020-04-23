using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalMvc.Controllers
{
    public class HomeController : Controller
    {
        //集线器简单实例1
        public ActionResult Index()
        {
            return View();
        }
        //集线器简单实例2
        public ActionResult Index2()
        {
            return View();
        }
        //永久连接类简单实例
        public ActionResult Index3()
        {
            return View();
        }
    }
}