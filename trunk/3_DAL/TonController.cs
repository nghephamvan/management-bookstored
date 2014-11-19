using _4_DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace _3_DAL
{
    public class TonController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static void InsertTonDAL(TON item)
        {
            db.TONs.InsertOnSubmit(item);
            db.SubmitChanges();
        }

        public static DataTable SellectAllTonsDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.TONs
                        join item1 in db.SACHes on item.MASACH equals item1.MASACH
                        select new
                        {
                            item.MATON,
                            item1.TENSACH,
                            item.TONDAU,
                            item.TONPHATSINH,
                            item.TONCUOI,
                            item.THANG
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
                dt.Rows.Add(stt, item.MATON, item.TENSACH, item.TONDAU, item.TONPHATSINH, item.TONCUOI, item.THANG);
                stt++;
            }

            return dt;
        }

        public static TON SelectTonDAL(string key)
        {
            TON query = db.TONs.Single(i => i.MATON.Equals(key));
            return query;
        }

        public static void DeleteTonsDAL(List<string> keys)
        {

            var query = from item in db.TONs
                        where keys.Contains(item.MATON)
                        select item;

            //Detele
            db.TONs.DeleteAllOnSubmit(query);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaTonDAL(string key)
        {
            var query = from item in db.TONs
                        where item.MATON.Equals(key)
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

        public static void UpdateTonDAL(TON item)
        {
            var query = db.TONs.Single(i => i.MATON == item.MATON);
            query.MATON = item.MATON;
            query.TONDAU = item.TONDAU;
            query.TONPHATSINH = item.TONPHATSINH;
            query.TONCUOI = item.TONCUOI;
            query.MASACH = item.MASACH;
            query.THANG = item.THANG;

            db.SubmitChanges();
        }

        public static DataTable SearchTonsDAL(string key)
        {
            var query = from item in db.TONs
                        join item1 in db.SACHes on item.MASACH equals item1.MASACH
                        where
                        (
                           item.MATON.StartsWith(key) ||
                           item.MATON.EndsWith(key) ||
                           item.MATON.Contains(key) ||
                           item1.TENSACH.StartsWith(key) ||
                           item1.TENSACH.EndsWith(key) ||
                           item1.TENSACH.Contains(key) ||
                           item.TONDAU.ToString().StartsWith(key) ||
                           item.TONDAU.ToString().EndsWith(key) ||
                           item.TONDAU.ToString().Contains(key) ||
                           item.TONPHATSINH.ToString().StartsWith(key) ||
                           item.TONPHATSINH.ToString().EndsWith(key) ||
                           item.TONPHATSINH.ToString().Contains(key) ||
                           item.TONCUOI.ToString().StartsWith(key) ||
                           item.TONCUOI.ToString().EndsWith(key) ||
                           item.TONCUOI.ToString().Contains(key) ||
                           item.THANG.ToString().StartsWith(key) ||
                           item.THANG.ToString().EndsWith(key) ||
                           item.THANG.ToString().Contains(key)
                        )
                        select new
                        {
                            item.MATON,
                            item1.TENSACH,
                            item.TONDAU,
                            item.TONPHATSINH,
                            item.TONCUOI,
                            item.THANG
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
                dt.Rows.Add(stt, item.MATON, item.TENSACH, item.TONDAU, item.TONPHATSINH, item.TONCUOI, item.THANG);
                stt++;
            }

            return dt;
        }
    }
}
