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
            item.XoaDuLieu = false;
            db.HOADONs.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static DataTable SellectAllHoaDonDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.HOADONs
                        join item2 in db.KHACHHANGs on item.MaKH equals item2.MaKH
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MaHD,
                            item2.HoTen,
                            item.NgayHD
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã HD", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Ngày Hóa Đơn", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaHD, item.HoTen, item.NgayHD);
                stt++;
            }

            return dt;
        }

        public static HOADON SelectHoaDonDAL(string key)
        {
            HOADON query = db.HOADONs.Single(i => i.MaHD.Equals(key));
            return query;
        }

        public static void DeleteHoaDonsDAL(List<string> keys)
        {

            db.HOADONs
                .Where(i => keys.Contains(i.MaHD))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            db.CTHDs
                .Where(i => keys.Contains(i.MaHD))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaHDDAL(string key)
        {
            var query = from item in db.HOADONs
                        where item.MaHD.Equals(key)
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
            var query = db.HOADONs.Single(i => i.MaHD == item.MaHD);
            query.MaKH = item.MaKH;
            query.NgayHD = item.NgayHD;

            db.SubmitChanges();
        }

        public static DataTable SearchHoaDonsDAL(string key)
        {
            var query = from item in db.HOADONs
                        join item2 in db.KHACHHANGs on item.MaKH equals item2.MaKH
                        where
                        (
                           item.MaHD.StartsWith(key) ||
                           item.MaHD.EndsWith(key) ||
                           item.MaHD.Contains(key) ||
                           item.MaKH.StartsWith(key) ||
                           item.MaKH.EndsWith(key) ||
                           item.MaKH.Contains(key) ||
                           item2.HoTen.StartsWith(key) ||
                           item2.HoTen.EndsWith(key) ||
                           item2.HoTen.Contains(key) ||
                           item.NgayHD.ToString().StartsWith(key) ||
                           item.NgayHD.ToString().EndsWith(key) ||
                           item.NgayHD.ToString().Contains(key)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaHD,
                            item2.HoTen,
                            item.NgayHD
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã HD", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Ngày Hóa Đơn", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaHD, item.HoTen, item.NgayHD);
                stt++;
            }

            return dt;
        }
    }
}
