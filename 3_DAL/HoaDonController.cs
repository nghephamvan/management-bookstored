using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class HoaDonController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static void InsertHoaDonDAL(HOADON item)
        {
            db.HOADONs.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static DataTable SellectAllHoaDonDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.HOADONs
                        join item2 in db.KHACHHANGs on item.MAKH equals item2.MAKH
                        select new
                        {
                            item.MAHD,
                            item2.HOTEN,
                            item.NGAYHD
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã HD", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Ngày Hóa Đơn", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MAHD, item.HOTEN, item.NGAYHD);
                stt++;
            }

            return dt;
        }

        public static HOADON SelectHoaDonDAL(string key)
        {
            HOADON query = db.HOADONs.Single(i => i.MAHD.Equals(key));
            return query;
        }

        public static void DeleteHoaDonsDAL(List<string> keys)
        {

            var query = from item in db.HOADONs
                        where keys.Contains(item.MAHD)
                        select item;
            //Detele MAHD in CTHD
            var queryCTHD = from item in db.CTHDs
                          where keys.Contains(item.MAHD)
                          select item;

            //Detele
            db.CTHDs.DeleteAllOnSubmit(queryCTHD);
            db.HOADONs.DeleteAllOnSubmit(query);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaHDDAL(string key)
        {
            var query = from item in db.HOADONs
                        where item.MAHD.Equals(key)
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

        public static void UpdateHoaDonDAL(HOADON item)
        {
            var query = db.HOADONs.Single(i => i.MAHD == item.MAHD);
            query.MAKH = item.MAKH;
            query.NGAYHD = item.NGAYHD;

            db.SubmitChanges();
        }

        public static DataTable SearchHoaDonsDAL(string key)
        {
            var query = from item in db.HOADONs
                        join item2 in db.KHACHHANGs on item.MAKH equals item2.MAKH
                        where
                        (
                           item.MAHD.StartsWith(key) ||
                           item.MAHD.EndsWith(key) ||
                           item.MAHD.Contains(key) ||
                           item.MAKH.StartsWith(key) ||
                           item.MAKH.EndsWith(key) ||
                           item.MAKH.Contains(key) ||
                           item2.HOTEN.StartsWith(key) ||
                           item2.HOTEN.EndsWith(key) ||
                           item2.HOTEN.Contains(key) ||
                           item.NGAYHD.ToString().StartsWith(key) ||
                           item.NGAYHD.ToString().EndsWith(key) ||
                           item.NGAYHD.ToString().Contains(key)
                        )
                        select new
                        {
                            item.MAHD,
                            item2.HOTEN,
                            item.NGAYHD
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã HD", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Ngày Hóa Đơn", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MAHD, item.HOTEN, item.NGAYHD);
                stt++;
            }

            return dt;
        }
    }
}
