using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using TestEmployee.Authentication;

namespace TestEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        //[Auth]
        public ActionResult Index()
        {
            return View();
        }

       // [Authorize]
        public ActionResult Login()
        {
            return View();
        }

        //[Authorize(Users ="abc@gmail.com")]
        public ActionResult About()
        {
            return View();
        }
    }
}