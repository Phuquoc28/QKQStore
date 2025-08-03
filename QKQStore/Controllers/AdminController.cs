using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QKQStore.Models;


namespace QKQStore.Controllers
{
    public class AdminController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: Admin
        public ActionResult IndexAdmin()
        {
            return View(database.Products.ToList());
        }
    }
}