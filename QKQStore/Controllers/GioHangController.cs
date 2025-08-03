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

            Models.Mathangmua product = cards.FirstOrDefault(s => s.ProductId == Id);
            if (product == null)
            {
                product = new Models.Mathangmua(Id);
                cards.Add(product);
            }
            else
            {
                product.Quantity++;
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
        public ActionResult Xoamathang(int Id)
        {
            List<Models.Mathangmua> giohang = LayGioHang();

            var product = giohang.FirstOrDefault(s => s.ProductId == Id);
            if (product != null)
            {
                giohang.RemoveAll(s => s.ProductId == Id);
                return RedirectToAction("Index", "GioHang");
            }
            if (product == null)
                return RedirectToAction("Index", "GioHang");
            return RedirectToAction("Index", "GioHang");
        }


        //cap nhat gio hang 

        public ActionResult CapnhatMathang(int Id, int Quantity)
        {
            List<Models.Mathangmua> giohang = LayGioHang();

            var product = giohang.FirstOrDefault(s => s.ProductId == Id);
            if (product != null)
            {
                product.Quantity = Quantity;
            }
            return RedirectToAction("Index", "GioHang");

        }
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Login", "User");
            List<Models.Mathangmua> giohang = LayGioHang();
            if (giohang == null || giohang.Count == 0)
                return RedirectToAction("Index", "GioHang");


            ViewBag.TongSl = Quantity();
            ViewBag.TongTien = TinhTongTien();
            return View(giohang);
        }
    }

}
