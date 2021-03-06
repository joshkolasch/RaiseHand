﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaiseHand.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult HandRaised()
        {
            ViewBag.Message = "Your hand is raised page.";
            return View();
        }

        public ActionResult HandLowered()
        {
            ViewBag.Message = "Your hand lowered response page.";
            return View();
        }

        public ActionResult Feedback()
        {
            ViewBag.Message = "Your feedback page.";
            return View();
        }
    }
}