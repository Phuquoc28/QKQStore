using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QKQStore.Models;

namespace QKQStore.Controllers
{
    public class GioHangController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: GioHang
        public ActionResult Index()
        {
                List<Models.Mathangmua> cards = LayGioHang();

                ViewBag.TongSl = Quantity();
                ViewBag.TongTien = TinhTongTien();

                return View(cards); // KHÔNG còn redirect nữa
           

        }
        public List<Models.Mathangmua> LayGioHang()
        {
            List<Models.Mathangmua> cards = Session["card"] as List<Models.Mathangmua>;
            //neu gio hang chua ton tai
            if (cards == null)
            {
                cards = new List<Models.Mathangmua>();
                Session["card"] = cards;

            }
            return cards;
        }
        public ActionResult Themsanpham(int Id)
        {
            List<Models.Mathangmua> cards = LayGioHang();

            Models.Mathangmua prodduct = cards.FirstOrDefault(s => s.ProductId == Id);
            if (prodduct == null)
            {
                prodduct = new Models.Mathangmua(Id);
                cards.Add(prodduct);
            }
            else
            {
                prodduct.Quantity++;
            }
            return RedirectToAction("Details", "Products", new { id = Id });
        }
        public int Quantity()
        {
            int Tongsl = 0;
            List<Models.Mathangmua> cards = LayGioHang();
            if (cards != null)
                Tongsl = cards.Sum(sp => sp.Quantity);
            return Tongsl;

        }
        public decimal TinhTongTien()
        {
            decimal TongTien = 0;
            List<Models.Mathangmua> cards = LayGioHang();
            if (cards != null)
                TongTien = cards.Sum(sp => sp.TotalPrice());
            return TongTien;
        }
        //thong tin ben trong gio hang
       
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSl = Quantity();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }

    }
}