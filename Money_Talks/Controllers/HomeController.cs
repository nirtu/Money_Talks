﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Money_Talks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Transaction");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
