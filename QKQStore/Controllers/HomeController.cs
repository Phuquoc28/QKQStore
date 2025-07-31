using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QKQStore.Models;

namespace QKQStore.Controllers
{
    public class HomeController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        public ActionResult Index()
        {
            var allProducts = database.Products.ToList();
            return View(allProducts);
        }

       
    }
}