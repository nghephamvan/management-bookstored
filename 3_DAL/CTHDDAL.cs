using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_DAL
{
    public class CTHDDAL
    {
        static QLNSModel db = new QLNSModel();

        public static void InsertCTHDDAL(CTHD item)
        {
            db.CTHDs.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static DataTable SellectAllCTHDsDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.CTHDs
                        join item1 in db.HOADONs on item.MAHD equals item1.MAHD
                        join item2 in db.KHACHHANGs on item1.MAKH equals item2.MAKH
                        join item3 in db.SACHes on item.MASACH equals item3.MASACH
                        select new
                        {
                            item.MACTHD,
                            item3.TENSACH,
                            item3.THELOAI,
                            item.MAHD,
                            item.SL_BAN,
                            item2.HOTEN,
                            item1.NGAYHD,
                            DONGIA = item.SL_BAN * item3.DONGIA * (decimal)1.05
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
                dt.Rows.Add(stt, item.MACTHD, item.TENSACH, item.THELOAI, item.MAHD, item.SL_BAN, item.HOTEN, item.NGAYHD, item.DONGIA);
                stt++;
            }

            return dt;
        }

        public static CTHD SelectCTHDDAL(string key)
        {
            var query = from item in db.CTHDs
                        where item.MACTHD == key
                        select new
                        {
                            item.MACTHD,
                            item.MASACH,
                            item.MAHD,
                            item.SL_BAN
                        };
            CTHD cthd = new CTHD();
            foreach (var item in query)
            {
                cthd.MACTHD = item.MACTHD;
                cthd.MASACH = item.MASACH;
                cthd.MAHD = item.MAHD;
                cthd.SL_BAN = item.SL_BAN;
            }
            return cthd;
        }

        public static void DeleteCTHDsDAL(List<string> keys)
        {

            var query = from item in db.CTHDs
                        where keys.Contains(item.MACTHD)
                        select item;

            //Detele
            db.CTHDs.DeleteAllOnSubmit(query);

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
            query.MASACH = item.MASACH;
            query.MAHD = item.MAHD;
            query.SL_BAN = item.SL_BAN;

            db.SubmitChanges();
        }

        public static DataTable SearchCTHDsDAL(string key)
        {
            var query = from item in db.CTHDs
                        join item1 in db.HOADONs on item.MAHD equals item1.MAHD
                        join item2 in db.KHACHHANGs on item1.MAKH equals item2.MAKH
                        join item3 in db.SACHes on item.MASACH equals item3.MASACH
                        where
                        (
                           item.MACTHD.StartsWith(key) ||
                           item.MACTHD.EndsWith(key) ||
                           item.MACTHD.Contains(key) ||
                           item.MAHD.StartsWith(key) ||
                           item.MAHD.EndsWith(key) ||
                           item.MAHD.Contains(key) ||
                           item3.TENSACH.StartsWith(key) ||
                           item3.TENSACH.EndsWith(key) ||
                           item3.TENSACH.Contains(key) ||
                           item3.THELOAI.StartsWith(key) ||
                           item3.THELOAI.EndsWith(key) ||
                           item3.THELOAI.Contains(key) ||
                           item2.HOTEN.StartsWith(key) ||
                           item2.HOTEN.EndsWith(key) ||
                           item2.HOTEN.Contains(key) ||
                           item.SL_BAN.ToString().StartsWith(key) ||
                           item.SL_BAN.ToString().EndsWith(key) ||
                           item.SL_BAN.ToString().Contains(key) ||
                           item1.NGAYHD.ToString().StartsWith(key) ||
                           item1.NGAYHD.ToString().EndsWith(key) ||
                           item1.NGAYHD.ToString().Contains(key)
                        )
                        select new
                        {
                            item.MACTHD,
                            item3.TENSACH,
                            item3.THELOAI,
                            item.MAHD,
                            item.SL_BAN,
                            item2.HOTEN,
                            item1.NGAYHD,
                            DONGIA = item.SL_BAN * item3.DONGIA * (decimal)1.05
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
                dt.Rows.Add(stt, item.MACTHD, item.TENSACH, item.THELOAI, item.MAHD, item.SL_BAN, item.HOTEN, item.NGAYHD, item.DONGIA);
                stt++;
            }

            return dt;
        }

        public static bool checkKH_CTHDDAL(string key)
        {
            //kiểm tra khách hàng có Nợ<=20000
            var query = db.HOADONs.Single(item => item.MAHD == key);

            var queryKH = db.KHACHHANGs.Single(item => item.MAKH == query.MAKH);

            if (queryKH.SOTIENNO > 20000)
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
            var query = db.SACHes.Single(item => item.MASACH == key);

            return query.SL_TON;
        }
    }
}
