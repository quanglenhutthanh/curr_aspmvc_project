using BabyToys.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BabyToys.Controllers
{
    public class SystemController :  admin_baseController
    {
        //
        // GET: /System/
        private List<string> lstFile;
        public void loadFileSlider()
        {
            string sliderImagesPath = Server.MapPath("~/Content/Images/slider");
            DirectoryInfo di = new DirectoryInfo(sliderImagesPath);
            FileInfo[] files = di.GetFiles();

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(Server.MapPath("~/Content/File/slider.txt"));
            while ((line = file.ReadLine()) != null)
            {
                //if (files.Any(x=>"~/Content/Images/slider"+x.FullName == line))
                //{
                    lstFile.Add(line);
                //}
            }
            file.Close();
        }
        public ActionResult Index()
        {
            //string sliderImagesPath = Server.MapPath("~/Content/Images/slider");
            //DirectoryInfo di = new DirectoryInfo(sliderImagesPath);
            //FileInfo[] files = di.GetFiles();
           
            lstFile = new List<string>();
            loadFileSlider();
            string txtInHeaderFilePath = Server.MapPath("~/Content/File/header.txt");
            if (System.IO.File.Exists(txtInHeaderFilePath))
            {
                ViewBag.txtHeader = System.IO.File.ReadAllText(txtInHeaderFilePath,Encoding.UTF8);
            }

            string txtInFooterFilePath = Server.MapPath("~/Content/File/footer.txt");
            if (System.IO.File.Exists(txtInFooterFilePath))
            {
                ViewBag.txtFooter = System.IO.File.ReadAllText(txtInFooterFilePath, Encoding.UTF8);
            }

            string logoPath = Server.MapPath("~/Content/File/logo.txt");
            if (System.IO.File.Exists(logoPath))
            {
                ViewBag.logo = System.IO.File.ReadAllText(logoPath, Encoding.UTF8);
            }

            //string banerPath = Server.MapPath("~/Content/File/baner.txt");
            //if (System.IO.File.Exists(banerPath))
            //{
            //    ViewBag.baner = System.IO.File.ReadAllText(logoPath, Encoding.UTF8);
            //}
            ViewBag.message = TempData["message"];
            return View(lstFile);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection collection)
        {
            try 
            {
                var image = WebImage.GetImageFromRequest("newimage");
                if (image != null)
                {
                    string imageName = Path.GetFileName(image.FileName);
                    //string imageName = "slider" + System.DateTime.Now + ".jpg";
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/Images/slider/"), imageName);
                    image.Save(pathToSave);
                }
                //string logo = collection["txtBaner"];
                string slider = collection["txtSlider"];
                string txtHeader = collection["txtInHeader"];
                string txtFooter = collection["txtInFooter"];
                //string txtBaner = collection["txtBaner"];

                string txtInHeaderFilePath = Server.MapPath("~/Content/File/header.txt");
                string txtInFooterFilePath = Server.MapPath("~/Content/File/footer.txt");
                string txtLogoPath = Server.MapPath("~/Content/File/logo.txt");
                string txtSliderPath = Server.MapPath("~/Content/File/slider.txt");
                //string txtBanerPath = Server.MapPath("~/Content/File/baner.txt");

                System.IO.File.WriteAllText(txtInHeaderFilePath, txtHeader);
                System.IO.File.WriteAllText(txtInFooterFilePath, txtFooter);
                //System.IO.File.WriteAllText(txtLogoPath, logo);
                //System.IO.File.WriteAllText(txtBaner, txtBaner);
                if(slider != "Thêm ảnh")
                {
                    lstFile = new List<string>();
                    loadFileSlider();
                    lstFile.Add(slider);
                    System.IO.File.WriteAllLines(txtSliderPath, lstFile.ToArray());
                }
                TempData["message"] = "Lưu thành công";
                
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.ToString();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteImage(string name) 
        {
           try
           {
               lstFile = new List<string>();
               loadFileSlider();
               lstFile.Remove(name);
               string sliderPath = Server.MapPath("~/Content/File/slider.txt");
             
               System.IO.File.WriteAllLines(sliderPath, lstFile.ToArray());
               //if (!String.IsNullOrEmpty(name))
               //{
               //    var path = Path.Combine(Server.MapPath("~/Content/Images/slider"), name);
               //    if (System.IO.File.Exists(path))
               //    {
               //        System.IO.File.Delete(path);
               //    }
               //    TempData["message"] = "Xóa ảnh thành công";
               //}
           }
            catch(Exception ex)
           {
               TempData["message"] = ex.ToString();
           }
            return RedirectToAction("Index");
        }
    }
}
