using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BabyToys
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content");
            routes.MapRoute(
            name: "danhmuc", // Route name
               url: "danh-muc/{id}/{slug}", // URL with parameters
               defaults: new { controller = "SanPham", action = "SanPhamTheoDanhMuc", id = UrlParameter.Optional, slug = "" } // Parameter defaults
           );
            routes.MapRoute(
           name: "content", // Route name
              url: "content", // URL with parameters
              defaults: new { controller = "Content", action = "Index"} // Parameter defaults
          );
            routes.MapRoute(
           name: "thuonghieu", // Route name
              url: "thuong-hieu/{id}/", // URL with parameters
              defaults: new { controller = "SanPham", action = "SanPhamTheoThuongHieu", id = UrlParameter.Optional } // Parameter defaults
          );
            routes.MapRoute(
            name: "sanpham", // Route name
               url: "san-pham/{id}/{slug}", // URL with parameters
               defaults: new { controller = "SanPham", action = "ChiTiet", id = UrlParameter.Optional, slug = "" } // Parameter defaults
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}