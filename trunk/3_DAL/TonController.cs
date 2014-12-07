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

            //Ngay Nhap nho nhat
            var temp0 = from i in db.CTPNs
                        join j in db.PHIEUNHAPs on i.MaPN equals j.MaPN
                        group new { i, j } by new { i.MaSach, j.NgayNhap.Value.Month } into g
                        select new { MaSach = g.Key.MaSach, g.Key.Month, MinDate = g.Min(t => t.j.NgayNhap), SL_TonCuoi = g.Sum(t => t.i.SL_Nhap) };


            var temp2 = from i in temp0
                        join j in db.CTPNs on i.MaSach equals j.MaSach
                        select new
                        {
                            i.MaSach,
                            i.Month,
                            TonDau = j.SL_Nhap,
                            TonPhatSinh = i.SL_TonCuoi - j.SL_Nhap,
                            TonCuoi = i.SL_TonCuoi
                        };
                         

            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Tháng", typeof(int));
            dt.Columns.Add("Tồn Đầu", typeof(int));
            dt.Columns.Add("Tồn Phát Sinh", typeof(int));
            dt.Columns.Add("Tồn Cuối", typeof(int));
            int stt = 1;
            foreach (var item in temp2)
            {
                dt.Rows.Add(stt, item.MaSach, item.Month, item.TonDau, item.TonPhatSinh, item.TonCuoi);
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