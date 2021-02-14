using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LangBuilder.Data;

namespace LangBuilder.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly LangBuilderContext builder;

        public HomeController(LangBuilderContext context)
        {
            this.builder = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}