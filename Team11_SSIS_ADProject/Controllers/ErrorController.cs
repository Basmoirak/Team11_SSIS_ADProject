﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team11_SSIS_ADProject.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}