using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PresentationApp.Controllers
{
    public class SupportController : Controller
    {
      //  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        
        [HttpGet] //role is to load the page
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost] //role is to receive data from a Form submission
        public ActionResult Contact(string email, string query)
        {
            //store in db or inform the person in charge

            ViewData["feedback"] = "Thanks for your query.  We will revert back with a reply very soon";
            
         
            return View();
        }


    }
}
