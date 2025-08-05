using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QKQStore.Models;

namespace QKQStore.Controllers
{
    public class CategoriesController : Controller
    {
        private QKQStoreEntities database = new QKQStoreEntities();

        // GET: Categories
        public ActionResult IndexAdmin()
        {
            return RedirectToAction("IndexDanhMuc","Categories");
        }

        //Hiển thị danh sách Danh Mục và phân trang
        public ActionResult IndexDanhMuc(int? page)
        {
            var listCategories = database.Categories.ToList();
            //Tạo Biến cho biết số sản phẩm mỗi trang 
            int pageSize = 3;
            //Tạo Biến số trang 
            int pageNum = (page ?? 1);
            return View(listCategories.OrderBy(sp => sp.Id).ToPagedList(pageNum, pageSize));
        }

        //Index
        //public ActionResult Index()
        //{
          //  return View(database.Categories.ToList());
        //}


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categories.CreateAt = DateTime.Now;
                    database.Categories.Add(categories);
                    database.SaveChanges();
                }
                return RedirectToAction("IndexDanhMuc","Categories");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var categories = database.Categories.Find(id);
            return View(categories);
        }

        [HttpPost]
        public ActionResult Edit(Categories categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoriesDB = database.Categories.Find(categories.Id);
                    categoriesDB.Name = categories.Name;
                    categoriesDB.UpdateAt = DateTime.Now;
                    database.SaveChanges();
                }
                return RedirectToAction("IndexDanhMuc", "Categories");
            }
            catch (Exception ex)
            {
                return View(categories);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var categories = database.Categories.Find(id);
            return View(categories);
        }

    
        [HttpPost]
        
        public ActionResult Delete(Categories categories, int id)
        {
            try
            {
                categories = database.Categories.Find(id);
                database.Categories.Remove(categories);
                database.SaveChanges();
                return RedirectToAction("IndexDanhMuc", "Categories");
            }
            catch (Exception ex)
            {
                return View(categories);
            }
        
        }

    }
}
