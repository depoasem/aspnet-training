using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DemoApps.Models;
using DemoApps.Repository;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace DemoApps.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        // GET: Account
        readonly IAccountRepository iAccountRepo;

        public AccountController(IAccountRepository iAccountRepo)
        {
            this.iAccountRepo = iAccountRepo;
        }
        public ActionResult Index()
        {
            return View(iAccountRepo.GetAllUser());
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult Login(loginResponse model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var userProfile = iAccountRepo.CheckUser(model);

        //    if (userProfile != null)
        //    {
        //        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userProfile.Username, DateTime.Now, DateTime.Now.AddMinutes(60), true, userProfile.Role);

        //        string encryptedTicket = FormsAuthentication.Encrypt(ticket);
        //        HttpCookie cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //        cookies.HttpOnly = true;

        //        Response.Cookies.Add(cookies);

        //        return Redirect(FormsAuthentication.GetRedirectUrl(userProfile.Username, true));
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Error", "Invalid login attemp!");
        //        return View();
        //    }
        //}

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserResponse model)
        {
            // EventRepo.AddEvent(req); // Without Interface
            if(ModelState.IsValid)
            {
                return View(model);
            }
            iAccountRepo.AddUser(model);
            return View();
            //Without Interface
            // return RedirectToAction("Index","EventMaster",EventRepo.GetAllEvent());

            //With Interface
            // return RedirectToAction("Index", "EventMaster", iEventRepo.GetAllEvent());

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //public ActionResult Register(UserResponse model)
        public async Task<ActionResult> Login(loginResponse model)
        {
            // EventRepo.AddEvent(req); // Without Interface
            // model.Role = "Contributor";

            if (ModelState.IsValid)
            {
                return View(model);
            }
            //Without WebAPI
            // iAccountRepo.AddUser(model);
            // return View();
            //// Without Interface
            //// return RedirectToAction("Index","EventMaster",EventRepo.GetAllEvent());

            ////With Interface
            //// return RedirectToAction("Index", "EventMaster", iEventRepo.GetAllEvent());

            UserResponse userProfile = new UserResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53822/");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var plainText = Encoding.UTF8.GetBytes("admin:123");
                string txtBasic = Convert.ToBase64String(plainText);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + txtBasic);

                string inputJSON = (new JavaScriptSerializer()).Serialize(model);
                HttpContent inputContent = new StringContent(inputJSON, Encoding.UTF8, "application/json");

                var Res = await client.PostAsync("api/users/CheckUser", inputContent);
                if(Res.IsSuccessStatusCode)
                {
                    var vUserProfile = Res.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<UserResponse>(vUserProfile);
                    userProfile = result;
                }
            }

            if (userProfile != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userProfile.Username, DateTime.Now, DateTime.Now.AddMinutes(60), true, userProfile.Role);

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookies.HttpOnly = true;

                Response.Cookies.Add(cookies);

                return Redirect(FormsAuthentication.GetRedirectUrl(userProfile.Username, true));
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid login attemp!");
                return View();
            }

        }
    }
}