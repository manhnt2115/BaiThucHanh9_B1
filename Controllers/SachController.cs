using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class SachController : Controller
    {
        private Model1 db = new Model1();
        // Fix for CS1061: Ensure that the 'Model1' class has a DbSet<MAUSAC> property defined.
        // Add the following property to the 'Model1' class definition.

        public virtual DbSet<MAUSAC> MAUSACs { get; set; }

        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            id = id.Trim();
            var sach = db.SACHes.SingleOrDefault(s => s.MaSach == id);
            if (sach == null)
                return HttpNotFound();

            ViewBag.TenChuDe = db.CHUDEs.SingleOrDefault(cd => cd.MaChuDe == sach.MaChuDe)?.TenChuDe;
            ViewBag.TenNXB = db.NHAXUATBANs.SingleOrDefault(nxb => nxb.MaNXB == sach.MaNXB)?.TenNXB;

            ViewBag.ListTacGia = (from tg in db.TACGIAs
                                  join thamgia in db.THAMGIAs on tg.MaTacGia equals thamgia.MaTacGia.ToString()
                                  where thamgia.MaSach == id
                                  select tg).ToList();

            ViewBag.SachLienQuan = db.SACHes
                .Where(s => s.MaChuDe == sach.MaChuDe && s.MaNXB == sach.MaNXB && s.MaSach != id)
                .Take(5)
                .ToList();

            return View(sach);
        }

        public ActionResult ChuDe(int id)
        {
            var chude = db.CHUDEs.Find(id);
            if (chude == null)
                return HttpNotFound();

            var sachTheoChuDe = db.SACHes
                .Where(s => s.MaChuDe == id.ToString())
                .Include(s => s.CHUDE)
                .Include(s => s.NHAXUATBAN)
                .ToList();

            ViewBag.TieuDe = "Sách thuộc chủ đề: " + chude.TenChuDe;
            return View("DanhSachSach", sachTheoChuDe);
        }
        public ActionResult NhaXuatBan(int id)
        {
            var nxb = db.NHAXUATBANs.Find(id);
            if (nxb == null)
                return HttpNotFound();

            var sachTheoNXB = db.SACHes
                .Where(s => s.MaNXB == id.ToString()) 
                .Include(s => s.CHUDE)
                .Include(s => s.NHAXUATBAN)
                .ToList();

            ViewBag.TieuDe = "Sách của nhà xuất bản: " + nxb.TenNXB;
            return View("DanhSachSach", sachTheoNXB);
        }
        public ActionResult SachTheoChuDe(int id)
        {
            var sach = db.SACHes.Where(s => s.MaChuDe == id.ToString()).ToList(); 
            var chude = db.CHUDEs.SingleOrDefault(c => c.MaChuDe == id.ToString()); 
            ViewBag.TenChuDe = chude != null ? chude.TenChuDe : "Không xác định";
            return View(sach);
        }
        public ActionResult SachTheoNXB(int id)
        {
            var sach = db.SACHes.Where(s => s.MaNXB == id.ToString()).ToList(); 
            var nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id.ToString());
            ViewBag.TenNXB = nxb != null ? nxb.TenNXB : "Không xác định";
            return View(sach);
        }
        public ActionResult ThemSanPham()
        {
            return View();
        }

        // POST: Thêm sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham(SACH sanPham, List<string> TenMau)
        {
            if (ModelState.IsValid)
            {
                db.SACHes.Add(sanPham);
                db.SaveChanges();

                // Lưu danh sách màu
                if (TenMau != null)
                {
                    foreach (var mau in TenMau)
                    {
                        if (!string.IsNullOrEmpty(mau))
                        {
                            db.THAMGIAs.Add(new MAUSAC)
                            {
                                MaSach = int.Parse(sanPham.MaSach), // Convert MaSach to int
                                TenMau = mau
                            });
                        }
                    }
                    db.SaveChanges();
                }

                ViewBag.ThongBao = "Thêm sản phẩm và màu thành công!";
                return RedirectToAction("DanhSachSanPham");
            }

            return View(sanPham);
        }

        // Danh sách sản phẩm để xem thử
        public ActionResult DanhSachSanPham()
        {
            var ds = db.SACHes.Include("MAUSACs").ToList();
            return View(ds);
        }

    }
}
