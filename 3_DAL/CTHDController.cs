using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class CTHDController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static void InsertCTHDDAL(CTHD item)
        {
            db.CTHDs.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static DataTable SellectAllCTHDsDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.CTHDs
                        join item1 in db.HOADONs on item.MaHD equals item1.MaHD
                        join item2 in db.KHACHHANGs on item1.MaKH equals item2.MaKH
                        join item3 in db.SACHes on item.MaSach equals item3.MaSach
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MACTHD,
                            item3.TenSach,
                            item3.TheLoai,
                            item.MaHD,
                            item.SL_BAN,
                            item2.HoTen,
                            item1.NgayHD,
                            DonGia = item.SL_BAN * item3.DonGia * (decimal)1.05
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã CTHD", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Thể Loại", typeof(string));
            dt.Columns.Add("Mã HD", typeof(string));
            dt.Columns.Add("Số Lượng", typeof(int));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Ngày Hóa Đơn", typeof(DateTime));
            dt.Columns.Add("Đơn Giá", typeof(decimal));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MACTHD, item.TenSach, item.TheLoai, item.MaHD, item.SL_BAN, item.HoTen, item.NgayHD, item.DonGia);
                stt++;
            }

            return dt;
        }

        public static CTHD SelectCTHDDAL(string key)
        {
            CTHD query = db.CTHDs.Single(i => i.MACTHD.Equals(key));
            return query;
        }

        public static void DeleteCTHDsDAL(List<string> keys)
        {

            db.CTHDs
                .Where(i => keys.Contains(i.MACTHD))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaCTHDDAL(string key)
        {
            var query = from item in db.CTHDs
                        where item.MACTHD.Equals(key)
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

        public static void UpdateCTHDDAL(CTHD item)
        {
            var query = db.CTHDs.Single(i => i.MACTHD == item.MACTHD);
            query.MaSach = item.MaSach;
            query.MaHD = item.MaHD;
            query.SL_BAN = item.SL_BAN;

            db.SubmitChanges();
        }

        public static DataTable SearchCTHDsDAL(string key)
        {
            var query = from item in db.CTHDs
                        join item1 in db.HOADONs on item.MaHD equals item1.MaHD
                        join item2 in db.KHACHHANGs on item1.MaKH equals item2.MaKH
                        join item3 in db.SACHes on item.MaSach equals item3.MaSach
                        where item.XoaDuLieu == false
                        where
                        (
                           item.MACTHD.StartsWith(key) ||
                           item.MACTHD.EndsWith(key) ||
                           item.MACTHD.Contains(key) ||
                           item.MaHD.StartsWith(key) ||
                           item.MaHD.EndsWith(key) ||
                           item.MaHD.Contains(key) ||
                           item3.TenSach.StartsWith(key) ||
                           item3.TenSach.EndsWith(key) ||
                           item3.TenSach.Contains(key) ||
                           item3.TheLoai.StartsWith(key) ||
                           item3.TheLoai.EndsWith(key) ||
                           item3.TheLoai.Contains(key) ||
                           item2.HoTen.StartsWith(key) ||
                           item2.HoTen.EndsWith(key) ||
                           item2.HoTen.Contains(key) ||
                           item.SL_BAN.ToString().StartsWith(key) ||
                           item.SL_BAN.ToString().EndsWith(key) ||
                           item.SL_BAN.ToString().Contains(key) ||
                           item1.NgayHD.ToString().StartsWith(key) ||
                           item1.NgayHD.ToString().EndsWith(key) ||
                           item1.NgayHD.ToString().Contains(key)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MACTHD,
                            item3.TenSach,
                            item3.TheLoai,
                            item.MaHD,
                            item.SL_BAN,
                            item2.HoTen,
                            item1.NgayHD,
                            DONGIA = item.SL_BAN * item3.DonGia * (decimal)1.05
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã CTHD", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Thể Loại", typeof(string));
            dt.Columns.Add("Mã HD", typeof(string));
            dt.Columns.Add("Số Lượng", typeof(int));
            dt.Columns.Add("Tên Khách Hàng", typeof(string));
            dt.Columns.Add("Ngày Hóa Đơn", typeof(DateTime));
            dt.Columns.Add("Đơn Giá", typeof(decimal));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MACTHD, item.TenSach, item.TheLoai, item.MaHD, item.SL_BAN, item.HoTen, item.NgayHD, item.DONGIA);
                stt++;
            }

            return dt;
        }

        public static bool checkKH_CTHDDAL(string key)
        {
            //kiểm tra khách hàng có Nợ<=20000
            var query = db.HOADONs.Single(item => item.MaHD == key);

            var queryKH = db.KHACHHANGs.Single(item => item.MaKH == query.MaKH);

            if (queryKH.SoTienNo > ThamSoController.SelectThamSoDAL().SoTienNoToiDa)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int? TakeSach_SL_TonDAL(string key)
        {
            var query = db.SACHes.Where(i=>i.XoaDuLieu==false).Single(item => item.MaSach == key);

            return query.SL_Ton;
        }
    }
}
