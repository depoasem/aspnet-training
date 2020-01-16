using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoApps.Models;

namespace DemoApps.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private static readonly List<UserResponse> listAccount = new List<UserResponse>();
        public UserResponse AddUser(UserResponse item)
        {
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                ctx.insertUser(item.Username, item.Email, item.Name, item.Password, item.Role);
                ctx.SaveChanges();
            }
            return item;
            // throw new NotImplementedException();
        }

        public bool DeleteUser(int id)
        {
            //throw new NotImplementedException();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                ctx.deleteUser(id);
                ctx.SaveChanges();
            }
            return true;
        }

        // public IEnumerable<tblUser> GetAllUser()
        public IEnumerable<UserResponse> GetAllUser()
        {
            //throw new NotImplementedException();
            //List<tblUser> tblUsers = new List<tblUser>();
            //using (DemoDBEntities1 ctx = new DemoDBEntities1())
            List<UserResponse> result = new List<UserResponse>();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                //listAccounts = (from a in ctx.tblUsers
                //                select new UserResponse
                //                {

                //                }
                //                    )
                result = ctx.getAllUser().Select(x => new UserResponse
                //result = ctx.getAllUser().Select(x => new UserResponse
                {
                    ID = x.id,
                    Username = x.Username,
                    Email = x.Email,
                    Name = x.Name,
                    Password = x.Password,
                    Role = x.Role
                }).ToList();
                ctx.SaveChanges();
            }
            return result;
        }

        public UserResponse CheckUser(loginResponse item)
        {
            //tblUser tblUsers = new tblUser();
            UserResponse res = new UserResponse();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                res = ctx.checkUser(item.Username, item.Password).Select(x => new UserResponse
                {
                    Username = x.Username,
                    Email = x.Email,
                    Name = x.Name,
                    Role = x.Role
                }).FirstOrDefault();
            }
            return res;
            //tblUsers = ctx.checkUser()
            //return tblUsers;
        }

        public UserResponse GetUser(int id)
        {
            UserResponse res = new UserResponse();
            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                res = ctx.getUserByID(id).Select(x => new UserResponse
                {
                    ID = x.id,
                    Username = x.Username,
                    Email = x.Email,
                    Name = x.Name,
                    Password = x.Password,
                    Role = x.Role
                }).FirstOrDefault();
                ctx.SaveChanges();
            }
            return res;
        }

        public bool UpdateUser(UserResponse item)
        {

            using (DemoDBEntities1 ctx = new DemoDBEntities1())
            {
                ctx.editUser(item.ID, item.Username, item.Password, item.Email, item.Name, item.Role);
                ctx.SaveChanges();
            }
            return true;
        }
    }
}
