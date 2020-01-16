using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoApps.Models;
using DemoApps.Repository;

namespace DemoApps.Controllers
{
    public class EventMasterController : Controller
    {
        private EventMasterRepository EventRepo = new EventMasterRepository();
        private IEventMasterRepository iEventRepo;
        public EventMasterController(IEventMasterRepository iEventRepo)
        {
            this.iEventRepo = iEventRepo;
        }

        // GET: EventMaster
        public ActionResult Index()
        {
            // return View(EventRepo.GetAllEvent()); // without Interface
            return View(iEventRepo.GetAllEvent());
            //return View(EventRepo.GetAllEvent());
        }

        [HttpGet]
        public ActionResult GetAllEvent()
        {
            var result = iEventRepo.GetAllEvent().Select(x => new EventsMasterResponse
            {
                ID = x.ID,
                Name = x.Name
            }).ToList();
            return Json(iEventRepo.GetAllEvent(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(EventsMasterResponse req)
        {
            // EventRepo.AddEvent(req); // Without Interface
            iEventRepo.AddEvent(req);

            //Without Interface
            // return RedirectToAction("Index","EventMaster",EventRepo.GetAllEvent());

            //With Interface
            return RedirectToAction("Index", "EventMaster", iEventRepo.GetAllEvent());

        }

        [HttpGet]
        public ActionResult EditEvent(int id)
        {
            //Without Interface
            //return View(EventRepo.GetEvents(id));

            //With Interface
            return View(iEventRepo.GetEvents(id));

        }

        [HttpPost]
        public ActionResult EditEvent(EventsMasterResponse res)
        {
            //Without Interface
            //EventRepo.UpdateEvent(res);
            // return RedirectToAction("Index", "EventMaster", EventRepo.GetAllEvent());

            //With Interface
            iEventRepo.UpdateEvent(res);
            return RedirectToAction("Index", "EventMaster", iEventRepo.GetAllEvent());
        }

        [HttpGet]
        public ActionResult DeleteEvent(int id)
        {
            //Without Interface
            //EventRepo.DeleteEvent(id);
            //return RedirectToAction("Index", "EventMaster", EventRepo.GetAllEvent());

            //With Interface
            iEventRepo.DeleteEvent(id);
            return RedirectToAction("Index", "EventMaster", iEventRepo.GetAllEvent());
        }
    }
}