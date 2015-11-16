using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BabyToys.Models;

namespace BabyToys
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<DatabaseContext>(new DatabaseInnitializer());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            //var error = Server.GetLastError();
            //Server.ClearError();
            //Response.ContentType = "text/plain";
            //Response.Write(error.ToString());
            //Response.Redirect("/PageNotFound");
            //Response.End();
        }
    }
}