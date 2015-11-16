using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using System.Data;
using System.Data.Entity;
namespace BabyToys.Utilities
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Add your acction filter's tasks here

            // Log Action Filter Call
            DatabaseContext storeDB = new DatabaseContext();
            if (filterContext.HttpContext.Session["admin"] != null)
            {
                User su = (User)filterContext.HttpContext.Session["admin"];
                var user = storeDB.Users.SingleOrDefault(u => u.Id == su.Id);
                storeDB.Entry(su).State = EntityState.Detached;
                Log log = new Log()
                {
                    Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    Action = filterContext.ActionDescriptor.ActionName,
                    IP = filterContext.HttpContext.Request.UserHostAddress,
                    User=user,
                    Time = filterContext.HttpContext.Timestamp
                };

                storeDB.Logs.Add(log);
                storeDB.SaveChanges();
            }
            this.OnActionExecuting(filterContext);
        }
    }
}