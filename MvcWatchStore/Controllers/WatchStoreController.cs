using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWatchStore;
using MvcWatchStore.Models;

using PagedList;
using PagedList.Mvc;

namespace MvcWatchStore.Controllers
{
    public class WatchStoreController : Controller
    {
        phongDataContext data = new phongDataContext();

        private List<DONGHO> Laydonghomoi(int count)
        {
            return data.DONGHOs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        // GET: WatchStore
        public ActionResult Index(int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var donghomoi = Laydonghomoi(15);
            return View(donghomoi.ToPagedList(pageNum, pageSize));
        }

        public ActionResult loaisp()
        {
            var loaisp = from cd in data.LOAISPs select cd;
            return PartialView(loaisp);
        }

        public ActionResult thuonghieu()
        {
            var thuonghieu = from cd in data.THUONGHIEUs select cd;
            return PartialView(thuonghieu);
        }
        public ActionResult SPTheoloai(int id)
        {
            var dongho = from s in data.DONGHOs where s.MaLoai==id select s;
            return View(dongho);
        }

        public ActionResult SPTheothuonghieu(int id)
        {
            var dongho = from s in data.DONGHOs where s.MaTH == id select s;
            return View(dongho);
        }

        public ActionResult Details(int id)
        {
            var dongho = from s in data.DONGHOs where s.Madongho == id select s;
            return View(dongho.Single());
        }
    }
}