using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QKQStore.Models;
using PagedList;

namespace QKQStore.Controllers
{
    public class AdminController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: Admin
        public ActionResult IndexAdmin()
        {
            var user = Session["TaiKhoan"] as Users;
            if (user == null || user.RoleId != 1)
                return RedirectToAction("Login", "User");
            var product = database.Products.ToList();
            return View(product);
        }
        //Hiển thị danh sách sản phẩm và phân trang
        public ActionResult SanPham(int? page) 
        {
            var listProduct = database.Products.ToList();
            //Tạo Biến cho biết số sản phẩm mỗi trang 
            int pageSize = 7;
            //Tạo Biến số trang 
            int pageNum = (page ?? 1);
            return View(listProduct.OrderBy(sp => sp.Id).ToPagedList(pageNum,pageSize));
         
        }
    }
}