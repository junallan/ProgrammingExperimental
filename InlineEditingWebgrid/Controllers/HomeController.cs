using InlineEditingWebgrid.Models;
using InlineEditingWebgrid.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InlineEditingWebgrid.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<SiteUserModel> list = new List<SiteUserModel>();

            using(MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                list.AddRange(
                                dc.SiteUsers.Join(dc.UserRoles,
                                            su => su.RoleID,
                                            ur => ur.Id, 
                                            (su, ur) => new SiteUserModel { ID = su.Id,
                                                                            FirstName = su.FirstName,
                                                                            LastName = su.LastName,
                                                                            DOB = su.DOB,
                                                                            RoleID = su.RoleID,
                                                                            RoleName = ur.RoleName
                                            })
                              );
            }
                    
            return View(list);
        }

        [HttpPost]
        public ActionResult SaveUser(int id, string propertyName, string value)
        {
            var status = false;
            var message = string.Empty;

            using(MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var user = dc.SiteUsers.Find(id);

                if(user != null)
                {
                    dc.Entry(user).Property(propertyName).CurrentValue = value;
                    dc.SaveChanges();
                    status = true;
                }
                else
                {
                    message = "Error!";
                }
            }

            var response = new { value = value, status = status, message = message };

            JObject o = JObject.FromObject(response);
            return Content(o.ToString());
        }

    }
}