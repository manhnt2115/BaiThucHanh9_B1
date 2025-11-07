using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class KhachHangController : Controller
    {
        private Model1 db = new Model1();
        // GET: KhachHang
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string taiKhoan,string matKhau)
        {
            var kh=db.KHACHHANGs.SingleOrDefault(k => k.TaiKhoan == taiKhoan && k.MatKhau == matKhau);
            if(kh!= null)
            {
                Session["KhachHang"] = kh;// lưu thông tin đăng nhập
                ViewBag.ThongBao = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng!";
                return View();
            }
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                var khachHangTonTai = db.KHACHHANGs.SingleOrDefault(k => k.TaiKhoan == kh.TaiKhoan);
                if (khachHangTonTai != null)
                {
                    ViewBag.ThongBao = "Tài khoản đã tồn tại!";
                    return View();
                }
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                ViewBag.ThongBao = "Đăng ký thành công!";
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["KhachHang"] = null;// xóa thông tin đăng nhập
            return RedirectToAction("Index", "Home");
        }
    }
}