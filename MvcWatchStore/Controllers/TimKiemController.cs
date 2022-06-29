using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWatchStore.Models;

using PagedList.Mvc;
using PagedList;

namespace MvcWatchStore.Controllers
{
    public class TimKiemController : Controller
    {
        phongDataContext data = new phongDataContext();
        [HttpPost]
        // GET: TimKiem
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<DONGHO> lstKQTK = data.DONGHOs.Where(n => n.Tendongho.Contains(sTuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            if (lstKQTK.Count == null)
            {
                ViewBag.Thongbao = "Không tìm thấy sản phẩm nào ";
                return View(data.DONGHOs.OrderBy(n => n.Tendongho).ToPagedList(pageNumber, pageSize));

            }
            ViewBag.Thongbao = "Đã tìm thấy  " + lstKQTK.Count + "  kết quả!";
            return View(lstKQTK.OrderBy(n => n.Tendongho).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            var lstKQTK = data.DONGHOs.Where(n => n.Tendongho.Contains(sTuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            if (lstKQTK.Count == null)
            {
                ViewBag.Thongbao = "Không tìm thấy sản phẩm nào ";
                return View(data.DONGHOs.OrderBy(n => n.Tendongho).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.Thongbao = "Đã tìm thấy  " + lstKQTK.Count + "  kết quả!";
            return View(lstKQTK.OrderBy(n => n.Tendongho).ToPagedList(pageNumber, pageSize));
        }
    }
}