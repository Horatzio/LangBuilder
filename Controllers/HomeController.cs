using System.Web.Mvc;
using LangBuilder.Web.Controllers.Shared;

namespace LangBuilder.Web.Controllers
{
    public class HomeController : BasePageController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}