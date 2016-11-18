using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAvis.Models;
using MVCAvis.WcfService;

namespace MVCAvis.Controllers
{
    public class LoginController : Controller
    {
        private AVISserviceClient Refe = new AVISserviceClient();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection formula)
        {
            string user = formula["useBX"];
            string pass = formula["passBX"];

            if (user.Length <= 10 | pass.Length <= 10)
            {
                if (Refe.validateUser(user, pass))
                {
                    switch (Refe.validateAcess(user))
                    {
                        case 0:
                            Response.Write("You have no power here!!");
                            break;
                        case 69:
                            return RedirectToAction("createOrder", "Orders"); //redirect hen til den pågældende side
                        case 11:
                            return RedirectToAction("searchOrder", "Orders"); //redirect hen til den pågældende side
                        default:
                            break;
                    }
                }
                else
                {
                    Response.Write("Code error 42"); //Ændring så jeg kan sync
                }
            }
            else
            {
                Response.Write("Login failed. Please try again.");
            }

            return View();
        }
    }
}