using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QKQStore.Models;

namespace QKQStore.Controllers
{
    public class CategoriesController : Controller
    {
        private QKQStoreEntities database = new QKQStoreEntities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(database.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = database.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                categories.CreateAt = DateTime.Now;
                database.Categories.Add(categories);
                database.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = database.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreateAt,UpdateAt")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                categories.UpdateAt = DateTime.Now;
                database.Entry(categories).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index", "Categories");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = database.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = database.Categories.Find(id);
            database.Categories.Remove(categories);
            database.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }

    }
}
