using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class ThuTienController
    {
        static QLNSModel db = new QLNSModel();
        
        public static void InsertThuTienDAL(THUTIEN item)
        {
            db.THUTIENs.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public static DataTable SellectAllThuTiensDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.THUTIENs
                        join item1 in db.KHACHHANGs on item.MAKH equals item1.MAKH
                        select new
                        {
                            item.MATHU,
                            item1.HOTEN,
                            item1.DIACHI,
                            item1.DIENTHOAI,
                            item1.EMAIL,
                            item.NGAYTHU,
                            item.SOTIENTHU
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Thu", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Điện Thoại", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Ngày Thu", typeof(DateTime));
            dt.Columns.Add("Số Tiền Thu", typeof(decimal));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MATHU, item.HOTEN, item.DIACHI, item.DIENTHOAI, item.EMAIL, item.NGAYTHU, item.SOTIENTHU);
                stt++;
            }

            return dt;
        }

        public static THUTIEN SelectThuTienDAL(string key)
        {
            var query = from item in db.THUTIENs
                        where item.MATHU == key
                        select new
                        {
                            item.MATHU,
                            item.MAKH,
                            item.NGAYTHU,
                            item.SOTIENTHU
                        };
            THUTIEN thu = new THUTIEN();
            foreach (var item in query)
            {
                thu.MATHU = item.MATHU;
                thu.MAKH = item.MAKH;
                thu.NGAYTHU = item.NGAYTHU;
                thu.SOTIENTHU = item.SOTIENTHU;
            }
            return thu;
        }

        public static void DeleteThuTiensDAL(List<string> keys)
        {

            var query = from item in db.THUTIENs
                        where keys.Contains(item.MATHU)
                        select item;

            //Detele
            db.THUTIENs.DeleteAllOnSubmit(query);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaThuTienDAL(string key)
        {
            var query = from item in db.THUTIENs
                        where item.MATHU.Equals(key)
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

        public static void UpdateThuTienDAL(THUTIEN item)
        {
            var query = db.THUTIENs.Single(i => i.MATHU == item.MATHU);
            query.MAKH = item.MAKH;
            query.NGAYTHU = item.NGAYTHU;
            query.SOTIENTHU = item.SOTIENTHU;

            db.SubmitChanges();
        }

        public static DataTable SearchThuTiensDAL(string key)
        {
            var query = from item in db.THUTIENs
                        join item1 in db.KHACHHANGs on item.MAKH equals item1.MAKH
                        where
                        (
                           item.MATHU.StartsWith(key) ||
                           item.MATHU.EndsWith(key) ||
                           item.MATHU.Contains(key) ||
                           item1.HOTEN.StartsWith(key) ||
                           item1.HOTEN.EndsWith(key) ||
                           item1.HOTEN.Contains(key) ||
                           item1.DIACHI.StartsWith(key) ||
                           item1.DIACHI.EndsWith(key) ||
                           item1.DIACHI.Contains(key) ||
                           item1.DIENTHOAI.StartsWith(key) ||
                           item1.DIENTHOAI.EndsWith(key) ||
                           item1.DIENTHOAI.Contains(key) ||
                           item1.EMAIL.StartsWith(key) ||
                           item1.EMAIL.EndsWith(key) ||
                           item1.EMAIL.Contains(key) ||
                           item.NGAYTHU.ToString().StartsWith(key) ||
                           item.NGAYTHU.ToString().EndsWith(key) ||
                           item.NGAYTHU.ToString().Contains(key) ||
                           item.SOTIENTHU.ToString().StartsWith(key) ||
                           item.SOTIENTHU.ToString().EndsWith(key) ||
                           item.SOTIENTHU.ToString().Contains(key)
                        )
                        select new
                        {
                            item.MATHU,
                            item1.HOTEN,
                            item1.DIACHI,
                            item1.DIENTHOAI,
                            item1.EMAIL,
                            item.NGAYTHU,
                            item.SOTIENTHU
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Thu", typeof(string));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Điện Thoại", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Ngày Thu", typeof(DateTime));
            dt.Columns.Add("Số Tiền Thu", typeof(decimal));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MATHU, item.HOTEN, item.DIACHI, item.DIENTHOAI, item.EMAIL, item.NGAYTHU, item.SOTIENTHU);
                stt++;
            }

            return dt;
        }

        public static bool checkKH_ThuTienDAL(string key)
        {
            var queryTT = db.THUTIENs.Single(i => i.MAKH == key);
            var queryKH = db.KHACHHANGs.Single(i => i.MAKH == key);

            if (queryTT.SOTIENTHU <= queryKH.SOTIENNO)
            {
                return true;
            }

            return false;
        }
    }
}
