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

        public static void InsertCongNoDAL(CONGNO item)
        {
            db.CONGNOs.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public static DataTable SellectAllCongNosDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.CONGNOs
                        join item1 in db.KHACHHANGs on item.MaKH equals item1.MaKH
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MaCongNo,
                            item1.HoTen,
                            item.NoDau,
                            item.NoPhatSinh,
                            item.NoCuoi,
                            item.Thang
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Công Nợ", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Nợ Đầu", typeof(decimal));
            dt.Columns.Add("Nợ Phát Sinh", typeof(decimal));
            dt.Columns.Add("Nợ Cuối", typeof(decimal));
            dt.Columns.Add("Tháng", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaCongNo, item.HoTen, item.NoDau, item.NoPhatSinh, item.NoCuoi, item.Thang);
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

        public static void DeleteCongNosDAL(List<string> keys)
        {

            db.CONGNOs
                .Where(i => keys.Contains(i.MaCongNo))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaCongNoDAL(string key)
        {
            var query = from item in db.CONGNOs
                        where item.MaCongNo.Equals(key)
                        select item;

            if (query.Count() <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void UpdateCongNoDAL(CONGNO item)
        {
            var query = db.CONGNOs.Single(i => i.MaCongNo == item.MaCongNo);
            query.MaCongNo = item.MaCongNo;
            query.MaKH = item.MaKH;
            query.NoDau = item.NoDau;
            query.NoPhatSinh = item.NoPhatSinh;
            query.NoCuoi = item.NoCuoi;
            query.Thang = item.Thang;

            db.SubmitChanges();
        }

        public static DataTable SearchCongNosDAL(string key)
        {
            var query = from item in db.CONGNOs
                        join item1 in db.KHACHHANGs on item.MaKH equals item1.MaKH
                        where
                        (
                           item.MaCongNo.StartsWith(key) ||
                           item.MaCongNo.EndsWith(key) ||
                           item.MaCongNo.Contains(key) ||
                           item1.HoTen.StartsWith(key) ||
                           item1.HoTen.EndsWith(key) ||
                           item1.HoTen.Contains(key) ||
                           item.NoDau.ToString().StartsWith(key) ||
                           item.NoDau.ToString().EndsWith(key) ||
                           item.NoDau.ToString().Contains(key) ||
                           item.NoPhatSinh.ToString().StartsWith(key) ||
                           item.NoPhatSinh.ToString().EndsWith(key) ||
                           item.NoPhatSinh.ToString().Contains(key) ||
                           item.NoCuoi.ToString().StartsWith(key) ||
                           item.NoCuoi.ToString().EndsWith(key) ||
                           item.NoCuoi.ToString().Contains(key) ||
                           item.Thang.ToString().StartsWith(key) ||
                           item.Thang.ToString().EndsWith(key) ||
                           item.Thang.ToString().Contains(key)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaCongNo,
                            item1.HoTen,
                            item.NoDau,
                            item.NoPhatSinh,
                            item.NoCuoi,
                            item.Thang
                        };

            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Công Nợ", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Nợ Đầu", typeof(decimal));
            dt.Columns.Add("Nợ Phát Sinh", typeof(decimal));
            dt.Columns.Add("Nợ Cuối", typeof(decimal));
            dt.Columns.Add("Tháng", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaCongNo, item.HoTen, item.NoDau, item.NoPhatSinh, item.NoCuoi, item.Thang);
                stt++;
            }

            return dt;
        }

        public static DataTable SelectCongNo_MonthDAL(string key)
        {
            DataTable dt = new DataTable();

            var query = from item in db.CONGNOs
                        join item1 in db.KHACHHANGs on item.MaKH equals item1.MaKH
                        where item.Thang == Convert.ToInt16(key) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaCongNo,
                            item1.HoTen,
                            item.NoDau,
                            item.NoPhatSinh,
                            item.NoCuoi,
                            item.Thang
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Công Nợ", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Nợ Đầu", typeof(decimal));
            dt.Columns.Add("Nợ Phát Sinh", typeof(decimal));
            dt.Columns.Add("Nợ Cuối", typeof(decimal));
            dt.Columns.Add("Tháng", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaCongNo, item.HoTen, item.NoDau, item.NoPhatSinh, item.NoCuoi, item.Thang);
                stt++;
            }

            return dt;
        }
    }
}
