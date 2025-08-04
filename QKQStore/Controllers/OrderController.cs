using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QKQStore.Models;

namespace QKQStore.Controllers
{
    public class OrderController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: OrderDetails
        public ActionResult IndexDonHang()
        {
            return View(database.Orders.ToList());
        }

        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Orders order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    order.OrderDate = DateTime.Now;
                    database.Orders.Add(order);
                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var order = database.Orders.Find(id);
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(Orders order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orderDB = database.Orders.Find(order.Id);
                    orderDB.Status = order.Status;
                    
                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(order);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var order = database.Orders.Find(id);
            return View(order);
        }
        [HttpPost]
        public ActionResult Delete(Orders order, int id)
        {
            try
            {
                order = database.Orders.Find(id);
                database.Orders.Remove(order);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(order);
            }
        }
    }
}