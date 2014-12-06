using _4_DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;

namespace _3_DAL
{
    public class TonController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static DataTable SellectAllTonsDAL()
        {
            DataTable dt = new DataTable();

            var query = from i in db.PHIEUNHAPs
                        join j in db.CTPNs on i.MaPN equals j.MaPN
                        group new { i, j } by new { i.NgayNhap.Value.Month, i.NgayNhap.Value.Year, j.MaSach } into g
                        select new
                        {
                            MaSach = g.Key.MaSach,
                            Thang = g.Key.Month,
                            Nam = g.Key.Year,
                            Ton = g.Sum(k => k.j.SL_Nhap)
                        };

            var query1 = from i in db.HOADONs
                         join j in db.CTHDs on i.MaHD equals j.MaHD
                         group new { i, j } by new { i.NgayHD.Value.Month, i.NgayHD.Value.Year, j.MaSach } into g
                         select new
                         {
                             MaSach = g.Key.MaSach,
                             Thang = g.Key.Month,
                             Nam = g.Key.Year,
                             SL_Ban = g.Sum(k => k.j.SL_BAN)
                         };

            var query2 = from i in query
                         join j in query1 on i.MaSach equals j.MaSach
                         join s in db.SACHes on i.MaSach equals s.MaSach
                         where i.Nam == j.Nam && i.Thang == j.Thang
                         select new
                         {
                             s.TenSach,
                             i.Thang,
                             i.Nam,
                             Ton = i.Ton - j.SL_Ban
                         };
                         

            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Tháng", typeof(int));
            dt.Columns.Add("Năm", typeof(int));
            dt.Columns.Add("Tồn", typeof(int));
            int stt = 1;
            foreach (var item in query2)
            {
                dt.Rows.Add(stt, item.TenSach, item.Thang, item.Nam, item.Ton);
                stt++;
            }

            return dt;
        }

        public static PHIEUTON SelectTonDAL(string key)
        {
            PHIEUTON query = db.PHIEUTONs.Single(i => i.MaTon.Equals(key));
            return query;
        }

        public static DataTable SelectTon_MonthBUL(string p)
        {
            DataTable dt = new DataTable();

            var query = from i in db.PHIEUNHAPs
                        join j in db.CTPNs on i.MaPN equals j.MaPN
                        group new { i, j } by new { i.NgayNhap.Value.Month, i.NgayNhap.Value.Year, j.MaSach } into g
                        select new
                        {
                            MaSach = g.Key.MaSach,
                            Thang = g.Key.Month,
                            Nam = g.Key.Year,
                            Ton = g.Sum(k => k.j.SL_Nhap)
                        };

            var query1 = from i in db.HOADONs
                         join j in db.CTHDs on i.MaHD equals j.MaHD
                         group new { i, j } by new { i.NgayHD.Value.Month, i.NgayHD.Value.Year, j.MaSach } into g
                         select new
                         {
                             MaSach = g.Key.MaSach,
                             Thang = g.Key.Month,
                             Nam = g.Key.Year,
                             SL_Ban = g.Sum(k => k.j.SL_BAN)
                         };

            var query2 = from i in query
                         join j in query1 on i.MaSach equals j.MaSach
                         join s in db.SACHes on i.MaSach equals s.MaSach
                         where i.Nam == j.Nam && i.Thang == j.Thang && i.Thang == Convert.ToInt16(p)
                         select new
                         {
                             s.TenSach,
                             i.Thang,
                             i.Nam,
                             Ton = i.Ton - j.SL_Ban
                         };


            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Tháng", typeof(int));
            dt.Columns.Add("Năm", typeof(int));
            dt.Columns.Add("Tồn", typeof(int));
            int stt = 1;
            foreach (var item in query2)
            {
                dt.Rows.Add(stt, item.TenSach, item.Thang, item.Nam, item.Ton);
                stt++;
            }

            return dt;
        }
    }
}