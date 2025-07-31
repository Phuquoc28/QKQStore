using QKQStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QKQStore.Controllers
{
    public class VouchersController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: Vouchers
        public ActionResult Index()
        {
            return View(database.Vouchers.ToList());
        }
        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Vouchers vouchers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vouchers.CreateAt = DateTime.Now;
                    database.Vouchers.Add(vouchers);
                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var vouchers = database.Vouchers.Find(id);
            return View(vouchers);
        }
        public ActionResult Edit(int id)
        {
            var vouchers = database.Vouchers.Find(id);
            return View(vouchers);
        }
        [HttpPost]
        public ActionResult Edit(Vouchers vouchers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var voucherDB = database.Vouchers.Find(vouchers.Id);
                    voucherDB.Code = vouchers.Code;
                    voucherDB.DiscountPercent = vouchers.DiscountPercent;
                    voucherDB.MinOrderAmount = vouchers.MinOrderAmount;
                    voucherDB.IsActive = vouchers.IsActive;
                    voucherDB.UpdateAt = DateTime.Now;
                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vouchers);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var vouchers = database.Vouchers.Find(id);
            return View(vouchers);
        }
        [HttpPost]
        public ActionResult Delete(Vouchers vouchers, int id)
        {
            try
            {
                vouchers = database.Vouchers.Find(id);
                database.Vouchers.Remove(vouchers);
                vouchers.UpdateAt = DateTime.Now;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vouchers);
            }
        }
    }
}