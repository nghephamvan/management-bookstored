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
       
        public static void InsertTonDAL(PHIEUTON item)
        {
            db.PHIEUTONs.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public static DataTable SellectAllTonsDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.PHIEUTONs
                        join item1 in db.SACHes on item.MaSach equals item1.MaSach
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MaTon,
                            item1.TenSach,
                            item.TonDau,
                            item.TonPhatSinh,
                            item.TonCuoi,
                            item.Thang
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Tồn", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Tồn Đầu", typeof(int));
            dt.Columns.Add("Tồn Phát Sinh", typeof(int));
            dt.Columns.Add("Tồn Cuối", typeof(int));
            dt.Columns.Add("Tháng", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaTon, item.TenSach, item.TonDau, item.TonPhatSinh, item.TonCuoi, item.Thang);
                stt++;
            }

            return dt;
        }

        public static PHIEUTON SelectTonDAL(string key)
        {
            PHIEUTON query = db.PHIEUTONs.Single(i => i.MaTon.Equals(key));
            return query;
        }

        public static void DeleteTonsDAL(List<string> keys)
        {
            //Detele
            db.PHIEUTONs
                .Where(i => keys.Contains(i.MaTon))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaTonDAL(string key)
        {
            var query = from item in db.PHIEUTONs
                        where item.MaTon.Equals(key)
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

        public static void UpdateTonDAL(PHIEUTON item)
        {
            var query = db.PHIEUTONs.Single(i => i.MaTon == item.MaTon);
            query.MaTon = item.MaTon;
            query.TonDau = item.TonDau;
            query.TonPhatSinh = item.TonPhatSinh;
            query.TonCuoi = item.TonCuoi;
            query.MaSach = item.MaSach;
            query.Thang = item.Thang;

            db.SubmitChanges();
        }

        public static DataTable SearchTonsDAL(string key)
        {
            var query = from item in db.PHIEUTONs
                        join item1 in db.SACHes on item.MaSach equals item1.MaSach
                        where
                        (
                           item.MaTon.StartsWith(key) ||
                           item.MaTon.EndsWith(key) ||
                           item.MaTon.Contains(key) ||
                           item1.TenSach.StartsWith(key) ||
                           item1.TenSach.EndsWith(key) ||
                           item1.TenSach.Contains(key) ||
                           item.TonDau.ToString().StartsWith(key) ||
                           item.TonDau.ToString().EndsWith(key) ||
                           item.TonDau.ToString().Contains(key) ||
                           item.TonPhatSinh.ToString().StartsWith(key) ||
                           item.TonPhatSinh.ToString().EndsWith(key) ||
                           item.TonPhatSinh.ToString().Contains(key) ||
                           item.TonCuoi.ToString().StartsWith(key) ||
                           item.TonCuoi.ToString().EndsWith(key) ||
                           item.TonCuoi.ToString().Contains(key) ||
                           item.Thang.ToString().StartsWith(key) ||
                           item.Thang.ToString().EndsWith(key) ||
                           item.Thang.ToString().Contains(key)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaTon,
                            item1.TenSach,
                            item.TonDau,
                            item.TonPhatSinh,
                            item.TonCuoi,
                            item.Thang
                        };

            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Tồn", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Tồn Đầu", typeof(int));
            dt.Columns.Add("Tồn Phát Sinh", typeof(int));
            dt.Columns.Add("Tồn Cuối", typeof(int));
            dt.Columns.Add("Tháng", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaTon, item.TenSach, item.TonDau, item.TonPhatSinh, item.TonCuoi, item.Thang);
                stt++;
            }

            return dt;
        }

        public static DataTable SelectTon_MonthDAL(string key)
        {
            DataTable dt = new DataTable();

            var query = from item in db.PHIEUTONs
                        join item1 in db.SACHes on item.MaSach equals item1.MaSach
                        where item.Thang == Convert.ToInt16(key)
                        select new
                        {
                            item.MaTon,
                            item1.TenSach,
                            item.TonDau,
                            item.TonPhatSinh,
                            item.TonCuoi,
                            item.Thang
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã Tồn", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Tồn Đầu", typeof(int));
            dt.Columns.Add("Tồn Phát Sinh", typeof(int));
            dt.Columns.Add("Tồn Cuối", typeof(int));
            dt.Columns.Add("Tháng", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaTon, item.TenSach, item.TonDau, item.TonPhatSinh, item.TonCuoi, item.Thang);
                stt++;
            }

            return dt;
        }
    }
}
