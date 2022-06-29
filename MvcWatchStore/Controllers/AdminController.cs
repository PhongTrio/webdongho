using MvcWatchStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace MvcWatchStore.Controllers
{
    public class AdminController : Controller
    {
        phongDataContext db = new phongDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dongho(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.DONGHOs.ToList().OrderBy(n => n.Madongho).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = " Phải nhập tên đăng nhập :";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu :";
            }
            else
            {
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["UserAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = " Tên đăng nhập hoặc mật khẩu không đúng!!!";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Themmoidongho()
        {
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoidongho(DONGHO dongho, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại !";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    dongho.Anhbia = fileName;
                    db.DONGHOs.InsertOnSubmit(dongho);
                    db.SubmitChanges();
                }
                return RedirectToAction("Dongho");
            }

        }

        public ActionResult ChiTietDongho(int id)
        {
            DONGHO dongho = db.DONGHOs.SingleOrDefault(n => n.Madongho == id);
            ViewBag.Madongho = dongho.Madongho;
            if (dongho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dongho);
        }

        [HttpGet]
        public ActionResult XoaDongho(int id)
        {
            DONGHO dongho = db.DONGHOs.SingleOrDefault(n => n.Madongho == id);
            ViewBag.Madongho = dongho.Madongho;
            if (dongho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dongho);
        }

        [HttpPost, ActionName("XoaDongho")]
        public ActionResult Xacnhanxoa(int id)
        {
            DONGHO dongho = db.DONGHOs.SingleOrDefault(n => n.Madongho == id);
            ViewBag.Madongho = dongho.Madongho;
            if (dongho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DONGHOs.DeleteOnSubmit(dongho);
            db.SubmitChanges();
            return RedirectToAction("Dongho");
        }

        [HttpGet]
        public ActionResult SuaDongho(int id)
        {
            DONGHO dongho = db.DONGHOs.SingleOrDefault(n => n.Madongho == id);
            ViewBag.Madongho = dongho.Madongho;
            if (dongho == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", dongho.MaLoai);
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH", dongho.MaTH);
            return View(dongho);
        }

        [HttpPost, ActionName("SuaDongho")]
        [ValidateInput(false)]
        public ActionResult SuaDongho(DONGHO dongho, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View(dongho);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại !";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    dongho.Anhbia = fileName;
                    DONGHO sa = db.DONGHOs.SingleOrDefault(s => s.Madongho == dongho.Madongho);
                    sa.Anhbia = dongho.Anhbia;
                    sa.Tendongho = dongho.Tendongho;
                    sa.Mota = dongho.Mota;
                    sa.Ngaycapnhat = dongho.Ngaycapnhat;
                    sa.Soluongton = dongho.Soluongton;
                    sa.MaLoai = dongho.MaLoai;
                    sa.MaTH = dongho.MaTH;
                    sa.Giaban = dongho.Giaban;
                    db.SubmitChanges();
                }
                return RedirectToAction("Dongho");
            }

        }


        public ActionResult Thuonghieu(int? page)
        {
            int pageNumer = (page ?? 1);
            int pageSize = 7;
            return View(db.THUONGHIEUs.ToList().OrderBy(n => n.MaTH).ToPagedList(pageNumer, pageSize));
        }

        [HttpGet]
        public ActionResult Themmoithuonghieu()
        {
            //Dua du lieu vao Dropdownlist
            //Lay danh sach tu table chu de, sap xep tang dan theo ten chu de , chon lay ia tri MaCD, hien thi TenCD
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoithuonghieu(THUONGHIEU thuonghieu)
        {
            //Dua du lieu vao Dropdownlist
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            db.THUONGHIEUs.InsertOnSubmit(thuonghieu);
            db.SubmitChanges();

            return RedirectToAction("Thuonghieu");
        }

        [HttpGet]
        public ActionResult Suathuonghieu(int id)
        {
            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            ViewBag.MaTH = thuonghieu.MaTH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao Dropdownlist
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH", thuonghieu.MaTH);

            return View(thuonghieu);
        }

        [HttpPost, ActionName("Suathuonghieu")]
        [ValidateInput(false)]
        public ActionResult Suathuonghieu(THUONGHIEU thuonghieu)
        {
            //Dua du lieu vao Dropdownlist
            ViewBag.MaTH = new SelectList(db.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            THUONGHIEU sa = db.THUONGHIEUs.SingleOrDefault(n => n.MaTH == thuonghieu.MaTH);
            sa.Diachi = thuonghieu.Diachi;
            sa.DienThoai = thuonghieu.DienThoai;

            db.SubmitChanges();
            return RedirectToAction("Thuonghieu");
        }

        [HttpGet]
        public ActionResult Xoathuonghieu(int id)
        {

            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            ViewBag.MaTH = thuonghieu.MaTH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thuonghieu);
        }

        [HttpPost, ActionName("Xoathuonghieu")]
        public ActionResult Xoathuonnghieu(int id)
        {

            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            ViewBag.MaTH = thuonghieu.MaTH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.THUONGHIEUs.DeleteOnSubmit(thuonghieu);
            db.SubmitChanges();
            return RedirectToAction("Thuonghieu");
        }

        public ActionResult Chitietthuonghieu(int id)
        {

            THUONGHIEU thuonghieu = db.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            ViewBag.MaTH = thuonghieu.MaTH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thuonghieu);
        }


        public ActionResult Loai(int? page)
        {
            int pageNumer = (page ?? 1);
            int pageSize = 7;
            return View(db.LOAISPs.ToList().OrderBy(n => n.MaLoai).ToPagedList(pageNumer, pageSize));
        }

        [HttpGet]
        public ActionResult Themmoiloai()
        {
            
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoiloai(LOAISP lOAISP)
        {
            //Dua du lieu vao Dropdownlist
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            db.LOAISPs.InsertOnSubmit(lOAISP);
            db.SubmitChanges();

            return RedirectToAction("Loai");
        }

        [HttpGet]
        public ActionResult Sualoai(int id)
        {
            LOAISP lOAISP = db.LOAISPs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = lOAISP.MaLoai;
            if (lOAISP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao Dropdownlist
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", lOAISP.MaLoai);

            return View(lOAISP);
        }

        [HttpPost, ActionName("Sualoai")]
        [ValidateInput(false)]
        public ActionResult Sualoai(LOAISP lOAISP)
        {
            //Dua du lieu vao Dropdownlist
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            LOAISP sa = db.LOAISPs.SingleOrDefault(n => n.MaLoai == lOAISP.MaLoai);
            sa.TenLoai = lOAISP.TenLoai;

            db.SubmitChanges();
            return RedirectToAction("Loai");
        }


        public ActionResult Xoaloai(int id)
        {

            LOAISP lOAISP = db.LOAISPs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = lOAISP.MaLoai;
            if (lOAISP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lOAISP);
        }

        [HttpPost, ActionName("Xoaloai")]
        public ActionResult dongyxoa(int id)
        {

            LOAISP lOAISP = db.LOAISPs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = lOAISP.MaLoai;
            if (lOAISP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LOAISPs.DeleteOnSubmit(lOAISP);
            db.SubmitChanges();
            return RedirectToAction("Loai");
        }

        public ActionResult Chitietloai(int id)
        {

            LOAISP lOAISP = db.LOAISPs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = lOAISP.MaLoai;
            if (lOAISP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lOAISP);
        }
    }

}