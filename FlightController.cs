using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiPart.Controllers
{
    public class FlightController : Controller
    {
        public ActionResult GetFlights()
        {
            return new FilePathResult("~/Views/Flight/getFlights.html","text/html");
        }
    }
}