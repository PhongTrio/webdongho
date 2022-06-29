using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWatchStore.Models
{
    public class Giohang
    {

        phongDataContext data = new phongDataContext();

        public int iMadongho { set; get; }
        public string sTendongho { set; get; }
        public string sAnhbia { set; get;  }
        public double dDongia { set; get; }
        public int iSoluong { set; get; }
        public double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        
        public Giohang(int Madongho)
        {
            iMadongho = Madongho;
            DONGHO dongho = data.DONGHOs.Single(n => n.Madongho == iMadongho);
            sTendongho = dongho.Tendongho;
            sAnhbia = dongho.Anhbia;
            dDongia = double.Parse(dongho.Giaban.ToString());
            iSoluong = 1;
        }

    }
}