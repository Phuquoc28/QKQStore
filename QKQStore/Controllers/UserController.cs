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
                var fullName = database.Users.FirstOrDefault(kh => kh.Fullname == user.Fullname);
                var email = database.Users.FirstOrDefault(kh => kh.Email == user.Email);
                if(fullName != null)
                {
                    ModelState.AddModelError("Fullname", "Tên Tài Khoản Hoặc Email Đã Được Sử Dụng");
                    return View();
                }
                if(email != null)
                {
                    ModelState.AddModelError("Email", "Tên Tài Khoản Hoặc Email Đã Được Sử Dụng");
                    return View(user);
                }
                user.RoleId = 2;
                user.CreateAt = DateTime.Now;
                user.UpdateAt = null;
                database.Users.Add(user);
                database.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();

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
            if (khachHang != null)
            {
                Session["TaiKhoan"] = khachHang;
                Session["Fullname"] = khachHang.Fullname; 
                ViewBag.ThongBao = "Đăng Nhập Thành Công";

                if (khachHang.RoleId == 1)
                    return RedirectToAction("IndexAdmin", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng";
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
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