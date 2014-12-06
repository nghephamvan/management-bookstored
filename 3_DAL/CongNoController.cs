using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class CongNoController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static DataTable SellectAllCongNosDAL()
        {
            DataTable dt = new DataTable();


            var query = from i in db.HOADONs
                        join j in db.CTHDs on i.MaHD equals j.MaHD
                        join k in db.SACHes on j.MaSach equals k.MaSach
                        group new { i, j, k } by new { i.NgayHD.Value.Month, i.NgayHD.Value.Year, i.MaKH, k.MaSach} into g
                        select new
                        {
                            MaKH = g.Key.MaKH,
                            MaSach = g.Key.MaSach,
                            Thang = g.Key.Month,
                            Nam = g.Key.Year,
                            CongNo = g.Sum(h=>(h.j.SL_BAN*h.k.DonGia))
                        };

            var query1 = from i in query
                         group i by new {i.MaKH, i.Thang, i.Nam} into g
                         select new
                         {
                             g.Key.MaKH,
                             g.Key.Thang,
                             g.Key.Nam,
                             CongNo = g.Sum(k=>k.CongNo)
                         };

            var query2 = from i in db.PHIEUTHUTIENs
                         group i by new { i.NgayThu.Value.Month, i.NgayThu.Value.Year, i.MaKH } into g
                         select new 
                         {
                             g.Key.MaKH,
                             g.Key.Month,
                             g.Key.Year,
                             TienThu = g.Sum(k=>k.SoTienThu)
                         };
            var query3 = from i in query1
                         join j in query2 on i.MaKH equals j.MaKH
                         join k in db.KHACHHANGs on i.MaKH equals k.MaKH
                         select new 
                         {
                             k.HoTen,
                             i.Thang,
                             i.Nam,
                             CongNo = i.CongNo - j.TienThu
                         };


            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Tháng", typeof(int));
            dt.Columns.Add("Năm", typeof(int));
            dt.Columns.Add("Công Nợ", typeof(decimal));
            int stt = 1;
            foreach (var item in query3)
            {
                dt.Rows.Add(stt, item.HoTen, item.Thang, item.Nam, item.CongNo);
                stt++;
            }

            return dt;
        }

        public static CONGNO SelectCongNoDAL(string key)
        {
            CONGNO query = db.CONGNOs.Single(i => i.MaCongNo.Equals(key));
            //CONGNO no = new CONGNO();
            //no.MACONGNO = query.MACONGNO;
            //no.MAKH = query.MAKH;
            //no.NODAU = query.NODAU;
            //no.NOPHATSINH = query.NOPHATSINH;
            //no.NOCUOI = query.NOCUOI;
            //no.THANG = query.THANG;

            //CONGNO temp = query;
            return query;
        }

        public static DataTable SelectCongNo_MonthDAL(string key)
        {
            DataTable dt = new DataTable();


            var query = from i in db.HOADONs
                        join j in db.CTHDs on i.MaHD equals j.MaHD
                        join k in db.SACHes on j.MaSach equals k.MaSach
                        group new { i, j, k } by new { i.NgayHD.Value.Month, i.NgayHD.Value.Year, i.MaKH, k.MaSach } into g
                        select new
                        {
                            MaKH = g.Key.MaKH,
                            MaSach = g.Key.MaSach,
                            Thang = g.Key.Month,
                            Nam = g.Key.Year,
                            CongNo = g.Sum(h => (h.j.SL_BAN * h.k.DonGia))
                        };

            var query1 = from i in query
                         group i by new { i.MaKH, i.Thang, i.Nam } into g
                         select new
                         {
                             g.Key.MaKH,
                             g.Key.Thang,
                             g.Key.Nam,
                             CongNo = g.Sum(k => k.CongNo)
                         };

            var query2 = from i in db.PHIEUTHUTIENs
                         group i by new { i.NgayThu.Value.Month, i.NgayThu.Value.Year, i.MaKH } into g
                         select new
                         {
                             g.Key.MaKH,
                             g.Key.Month,
                             g.Key.Year,
                             TienThu = g.Sum(k => k.SoTienThu)
                         };
            var query3 = from i in query1
                         join j in query2 on i.MaKH equals j.MaKH
                         join k in db.KHACHHANGs on i.MaKH equals k.MaKH
                         where i.Thang == Convert.ToInt16(key)
                         select new
                         {
                             k.HoTen,
                             i.Thang,
                             i.Nam,
                             CongNo = i.CongNo - j.TienThu
                         };


            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Tháng", typeof(int));
            dt.Columns.Add("Năm", typeof(int));
            dt.Columns.Add("Công Nợ", typeof(decimal));
            int stt = 1;
            foreach (var item in query3)
            {
                dt.Rows.Add(stt, item.HoTen, item.Thang, item.Nam, item.CongNo);
                stt++;
            }

            return dt;
        }
    }
}
