using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebApi.Models;

namespace DemoWebApi.Controllers
{
    public class UsersController : ApiController
    {
        public UsersModel CheckUser(UsersModel model)
        {
            using (DemoDBEntities ctx = new DemoDBEntities())
            {
                var result = ctx.checkUser(model.Username, model.Password).Select(x => new UsersModel
                {
                    Username = x.Username,
                    Email = x.Email,
                    Name = x.Name,
                    Role = x.Role
                }).FirstOrDefault();

                if (result.Username.Length > 0)
                {
                    model = result;
                }
                else
                {
                    model = new UsersModel { Notes = "User not found" };
                }
                return model;

            }
        }
    }
}
