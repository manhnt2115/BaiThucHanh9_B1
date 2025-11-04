using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // Khai báo DbContext
        private Model1 data = new Model1();

        // Trang chủ: hiển thị 5 sách mới nhất
        public ActionResult Index()
        {
            var dSSach = data.SACHes
                             .OrderByDescending(s => s.NgayCapNhat)
                             .Take(5)
                             .ToList();
            return View(dSSach);
        }

        // Danh sách chủ đề (hiển thị trong menu)
        public ActionResult DSMenu_ChuDe()
        {
            var dSCD = data.CHUDEs
                           .OrderBy(cd => cd.TenChuDe)
                           .Take(10)
                           .ToList();
            return PartialView(dSCD);
        }

        // Danh sách nhà xuất bản (hiển thị trong menu)
        public ActionResult DSMenu_NXB()
        {
            var dSNXB = data.NHAXUATBANs
                            .OrderBy(nxb => nxb.TenNXB)
                            .Take(10)
                            .ToList();
            return PartialView(dSNXB);
        }

        // Trang hiển thị sách theo chủ đề
        public ActionResult SachTheoChuDe(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Không tìm thấy chủ đề này.");
            }
            string idString = id.ToString();
            var sach = data.SACHes.Where(s => s.MaChuDe == idString).ToList();
            var chude = data.CHUDEs.SingleOrDefault(c => c.MaChuDe == idString);
            ViewBag.TenChuDe = chude != null ? chude.TenChuDe : "Không xác định";
            return View(sach);
        }

        // Trang hiển thị sách theo nhà xuất bản
        public ActionResult SachTheoNXB(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Không tìm thấy nhà xuất bản này.");
            }
            string idString = id.ToString();
            var sach = data.SACHes.Where(s => s.MaNXB == idString).ToList();
            var nxb = data.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == idString);
            ViewBag.TenNXB = nxb != null ? nxb.TenNXB : "Không xác định";
            return View(sach);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Thông tin về ứng dụng.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Liên hệ với chúng tôi.";
            return View();
        }
    }
}
