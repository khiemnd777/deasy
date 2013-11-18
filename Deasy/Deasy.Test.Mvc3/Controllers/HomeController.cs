using Deasy.Infrastructure;
using Deasy.Test.Mvc3.Services.Foo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Deasy.Test.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFooService _fooService;

        public HomeController(IFooService fooService)
        {
            _fooService = fooService;
        }

        //public HomeController()
        //{
            
        //}

        public ActionResult Index()
        {
            ViewBag.Message = _fooService.Foo();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
