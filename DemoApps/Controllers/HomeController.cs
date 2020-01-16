using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApps.Models;

namespace DemoApps.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greating = hour  < 12 ? "Good morning" : "Good afternoon";
            return View();
        }
        [HttpGet]
        public ActionResult RsvpForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RsvpForm(GuestResponse res)
        {
            //if(ModelState.IsValid)
            //{
            //    return View("Thanks", res);
            //}
            //else
            //{
            //    return View();
            //}

            if(ModelState.IsValid)
            {
                using (DemoDBEntities1 ctx = new DemoDBEntities1())
                {
                    Participant obj = new Participant();
                    obj.NamaLengkap = res.NamaLengkap;
                    obj.Email = res.Email;
                    obj.Handphone = res.Handphone;
                    obj.MasterEventId = res.MasterEventID;
                    obj.WillAttend = res.WillAttend == null ? true : false;
                    // obj.WillAttend = res.WillAttend ?? false;

                    ctx.Participants.Add(obj);
                    ctx.SaveChanges();
                }
                return View("Thanks", res);
            }
            else
            {
                return View();
            }
            
        }

        #region comment
        // public ActionResult About()
        // {
        //    ViewBag.Message = "Your application description page.";
        //
        //    return View();
        //}

        // public ActionResult Contact()
        // {
        //    ViewBag.Message = "Your contact page.";
        //
        //    return View();
        // }
        #endregion
    }
}