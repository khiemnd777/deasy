using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deasy.Test.Mvc4.Services;
using Deasy.Infrastructure;

namespace Deasy.Test.Mvc4.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFooService _fooService;

        public HomeController(IFooService fooService)
        {
            _fooService = fooService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = _fooService.Foo();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
