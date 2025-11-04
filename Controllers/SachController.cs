using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SachController : Controller
    {
        private Model1 db = new Model1();

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


    }
}
