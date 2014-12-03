using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class ThuTienController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();
        
        public static void InsertThuTienDAL(PHIEUTHUTIEN item)
        {
            db.PHIEUTHUTIENs.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public static DataTable SellectAllThuTiensDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.PHIEUTHUTIENs
                        join item1 in db.KHACHHANGs on item.MaKH equals item1.MaKH
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MaThuTien,
                            item1.HoTen,
                            item1.DiaChi,
                            item1.DienThoai,
                            item1.Email,
                            item.NgayThu,
                            item.SoTienThu
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
                dt.Rows.Add(stt, item.MaThuTien, item.HoTen, item.DiaChi, item.DienThoai, item.Email, item.NgayThu, item.SoTienThu);
                stt++;
            }

            return dt;
        }

        public static PHIEUTHUTIEN SelectThuTienDAL(string key)
        {
            PHIEUTHUTIEN query = db.PHIEUTHUTIENs.Single(i => i.MaThuTien.Equals(key));
            return query;
        }

        public static void DeleteThuTiensDAL(List<string> keys)
        {


            //Detele
            db.PHIEUTHUTIENs
                .Where(i => keys.Contains(i.MaThuTien))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaThuTienDAL(string key)
        {
            var query = from item in db.PHIEUTHUTIENs
                        where item.MaThuTien.Equals(key)
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

        public static void UpdateThuTienDAL(PHIEUTHUTIEN item)
        {
            var query = db.PHIEUTHUTIENs.Single(i => i.MaThuTien == item.MaThuTien);
            query.MaKH = item.MaKH;
            query.NgayThu = item.NgayThu;
            query.SoTienThu = item.SoTienThu;

            db.SubmitChanges();
        }

        public static DataTable SearchThuTiensDAL(string key)
        {
            var query = from item in db.PHIEUTHUTIENs
                        join item1 in db.KHACHHANGs on item.MaKH equals item1.MaKH
                        where
                        (
                           item.MaThuTien.StartsWith(key) ||
                           item.MaThuTien.EndsWith(key) ||
                           item.MaThuTien.Contains(key) ||
                           item1.HoTen.StartsWith(key) ||
                           item1.HoTen.EndsWith(key) ||
                           item1.HoTen.Contains(key) ||
                           item1.DiaChi.StartsWith(key) ||
                           item1.DiaChi.EndsWith(key) ||
                           item1.DiaChi.Contains(key) ||
                           item1.DienThoai.StartsWith(key) ||
                           item1.DienThoai.EndsWith(key) ||
                           item1.DienThoai.Contains(key) ||
                           item1.Email.StartsWith(key) ||
                           item1.Email.EndsWith(key) ||
                           item1.Email.Contains(key) ||
                           item.NgayThu.ToString().StartsWith(key) ||
                           item.NgayThu.ToString().EndsWith(key) ||
                           item.NgayThu.ToString().Contains(key) ||
                           item.SoTienThu.ToString().StartsWith(key) ||
                           item.SoTienThu.ToString().EndsWith(key) ||
                           item.SoTienThu.ToString().Contains(key)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaThuTien,
                            item1.HoTen,
                            item1.DiaChi,
                            item1.DienThoai,
                            item1.Email,
                            item.NgayThu,
                            item.SoTienThu
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
                dt.Rows.Add(stt, item.MaThuTien, item.HoTen, item.DiaChi, item.DienThoai, item.Email, item.NgayThu, item.SoTienThu);
                stt++;
            }

            return dt;
        }

        public static bool checkKH_ThuTienDAL(string key)
        {
            var queryTT = db.PHIEUTHUTIENs.Single(i => i.MaKH == key);
            var queryKH = db.KHACHHANGs.Single(i => i.MaKH == key);

            if (queryTT.SoTienThu <= queryKH.SoTienNo)
            {
                return true;
            }


            return false;
        }
    }
}
