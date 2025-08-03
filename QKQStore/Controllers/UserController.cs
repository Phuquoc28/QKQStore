using QKQStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Data.Entity.Validation;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.ModelBinding;

namespace QKQStore.Controllers
{
    public class UserController : Controller
    {
        QKQStoreEntities database = new QKQStoreEntities();
        // GET: User
        public ActionResult Index()
        {
            return View(database.Users.ToList());
        }
        
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                //Kiểm tra email và tên có chưa
                var KhachHang = database.Users.FirstOrDefault(kh => kh.Fullname == user.Fullname || kh.Email == user.Email);
                if(KhachHang != null)
                {
                    ModelState.AddModelError(string.Empty, "Tên Tài Khoản Hoặc Email Đã Được Sử Dụng");
                    return View(user);
                }
                user.RoleId = 2;
                user.CreateAt = DateTime.Now;
                user.UpdateAt = null;
               
            }
            database.Users.Add(user);
            database.SaveChanges();
            return RedirectToAction("Login");

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View() ;
        }
        [HttpPost]
        public ActionResult Login(Users user)
        {
                var khachHang = database.Users.FirstOrDefault(kh => kh.Fullname == user.Fullname && kh.Password == user.Password);
                if(khachHang != null)
                {
                    ViewBag.ThongBao = "Đăng Nhập Thành Công";
                    Session["TaiKhoan"] = khachHang;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng Vui Lòng Nhập Lại";
                    return View();
                }
            return View();
        }
        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.CreateAt = DateTime.Now;
                    database.Users.Add(user);

                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
      
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var users = database.Users.Find(id);
            return View(users);
        }
        [HttpPost]
        public ActionResult Edit(Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDB = database.Users.Find(user.Id);
                    userDB.Address = user.Address;
                    userDB.Password = user.Password;
                    userDB.Email = user.Email;
                    userDB.Fullname = user.Fullname;
                    userDB.PhoneNumber = user.PhoneNumber;
                    userDB.UpdateAt = DateTime.Now;
                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = database.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Delete(Users users, int id)
        {
            try
            {
                users = database.Users.Find(id);
                database.Users.Remove(users);
                users.UpdateAt = DateTime.Now;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


    }
}