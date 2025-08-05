using QKQStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QKQStore.Controllers
{
    public class ProductsController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: Products
        public ActionResult Index()
        {
            return View(database.Products.Include("Categories").ToList());
        }
        //Hiển Thị Sản Phẩm Theo Danh Mục 
        public ActionResult TimDanhMuc(int categoryId)
        {
            var products = database.Products.Where(p => p.CategoryID == categoryId).ToList();
            return View("TimDanhMuc", products);
        }
        [HttpGet]
        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Products products, HttpPostedFileBase imageUpload)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(imageUpload != null)
                    {
                        string fileName = Path.GetFileName(imageUpload.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Pic/"), fileName);
                        imageUpload.SaveAs(path);
                        products.Image = fileName;
                    }
                    products.Title = products.Title?.Trim();
                    products.CreateAt = DateTime.Now;
                    database.Products.Add(products);
                    database.SaveChanges();
                }
                return RedirectToAction("SanPham","Admin");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var porduct = database.Products.Find(id);
            return View(porduct);
        }
        public ActionResult DetailsAdmin(int id)
        {
            var porduct = database.Products.Find(id);
            return View(porduct);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = database.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Products products, HttpPostedFileBase imageUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var proDB = database.Products.Find(products.Id);
                    proDB.Title = products.Title;
                    proDB.Description = products.Description;
                    proDB.Price = products.Price;
                    proDB.Discount = products.Discount;
                    proDB.CategoryID = products.CategoryID;

                    if (imageUpload != null)
                    {
                        string fileName = Path.GetFileName(imageUpload.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Pic/"), fileName);
                        imageUpload.SaveAs(path);
                        proDB.Image = fileName;
                    }
                    

                    proDB.UpdateAt = DateTime.Now;
                    database.SaveChanges();
                }
                return RedirectToAction("SanPham", "Admin");
            }
            catch (Exception ex)
            {
                return View(products);
            }
        }

       
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var porduct = database.Products.Find(id);
            return View(porduct);
        }
        [HttpPost]
        public ActionResult Delete(Products products,int id)
        {
            try
            {
                products= database.Products.Find(id);
                database.Products.Remove(products);
                products.UpdateAt = DateTime.Now;
                database.SaveChanges();
                return RedirectToAction("SanPham", "Admin");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        
        //Chức năng tìm kiếm
        public ActionResult Search(string query)
        {
            var result = database.Products.Where(p => p.Title.Contains(query)).ToList();
            ViewBag.Query = query;
            return View("Kqtimkiem", result);
        }
        public ActionResult Kqtimkiem()
        {
            return View();
        }
    }

}

    
