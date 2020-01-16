using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using DemoApps.Models;
using DemoApps.Repository;


namespace DemoApps.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        readonly GuestRepository GuestRepo = new GuestRepository();
        private IGuestRepository iGuestRepo;

        public GuestController(IGuestRepository iGuestRepo)
        {
            this.iGuestRepo = iGuestRepo;
        }

        
        public ActionResult Index()
        {
            // return View();
            return View(iGuestRepo.GetAllRsvp());
        }

        [HttpGet]
        public ActionResult GetAllRsvp()
        {
            var result = iGuestRepo.GetAllRsvp().Select(x => new GuestResponse
            {
                ID = x.ID,
                NamaLengkap = x.NamaLengkap
            }).ToList();
            return Json(iGuestRepo.GetAllRsvp(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateRsvp()
        {
            try
            {
                RestClient client = new RestClient(Request.Url.GetLeftPart(UriPartial.Authority));
                RestRequest req = new RestRequest("/eventmaster/GetAllEvent", Method.GET);

                IRestResponse<List<EventsMasterResponse>> response = client.Execute<List<EventsMasterResponse>>(req);

                var result = new SelectList(response.Data, "ID", "Name");
                ViewData["DataEvent"] = result;


            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.ToString());
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateRsvp(GuestResponse req)
        {
            iGuestRepo.AddRsvp(req);
            return RedirectToAction("Index", "Guest", iGuestRepo.GetAllRsvp());
        }

        [HttpGet]
        public ActionResult EditRsvp(int id)
        {
            //Without Interface
            //return View(EventRepo.GetEvents(id));

            //With Interface
            //return View(iGuestRepo.GetRsvps(id));

            RestClient client = new RestClient(Request.Url.GetLeftPart(UriPartial.Authority));
            RestRequest req = new RestRequest("/guest/GetAllRsvp", Method.GET);

            IRestResponse<List<EventsMasterResponse>> response =
                client.Execute<List<EventsMasterResponse>>(req);

            var result = new SelectList(response.Data, "ID", "Name");
            ViewData["DataEvent"] = result;
            return View(iGuestRepo.GetRsvps(id));

        }

        [HttpPost]
        public ActionResult EditRsvp(GuestResponse res)
        {
            //Without Interface
            //EventRepo.UpdateEvent(res);
            // return RedirectToAction("Index", "EventMaster", EventRepo.GetAllEvent());

            //With Interface
            iGuestRepo.UpdateRsvp(res);
            return RedirectToAction("Index", "Guest", iGuestRepo.GetAllRsvp());
        }

        [HttpGet]
        public ActionResult DeleteRsvp(int id)
        {
            //Without Interface
            //EventRepo.DeleteEvent(id);
            //return RedirectToAction("Index", "EventMaster", EventRepo.GetAllEvent());

            //With Interface
            iGuestRepo.DeleteRsvp(id);
            return RedirectToAction("Index", "Guest", iGuestRepo.GetAllRsvp());
        }

    }
}