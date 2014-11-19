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
                        join item1 in db.KHACHHANGs on item.MAKH equals item1.MAKH
                        select new
                        {
                            item.MACONGNO,
                            item1.HOTEN,
                            item.NODAU,
                            item.NOPHATSINH,
                            item.NOCUOI,
                            item.THANG
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
                dt.Rows.Add(stt, item.MACONGNO, item.HOTEN, item.NODAU, item.NOPHATSINH, item.NOCUOI, item.THANG);
                stt++;
            }

            return dt;
        }

        public static CONGNO SelectCongNoDAL(string key)
        {
            CONGNO query = db.CONGNOs.Single(i => i.MACONGNO.Equals(key));
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

            var query = from item in db.CONGNOs
                        where keys.Contains(item.MACONGNO)
                        select item;

            //Detele
            db.CONGNOs.DeleteAllOnSubmit(query);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaCongNoDAL(string key)
        {
            var query = from item in db.CONGNOs
                        where item.MACONGNO.Equals(key)
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
            var query = db.CONGNOs.Single(i => i.MACONGNO == item.MACONGNO);
            query.MACONGNO = item.MACONGNO;
            query.MAKH = item.MAKH;
            query.NODAU = item.NODAU;
            query.NOPHATSINH = item.NOPHATSINH;
            query.NOCUOI = item.NOCUOI;
            query.THANG = item.THANG;

            db.SubmitChanges();
        }

        public static DataTable SearchCongNosDAL(string key)
        {
            var query = from item in db.CONGNOs
                        join item1 in db.KHACHHANGs on item.MAKH equals item1.MAKH
                        where
                        (
                           item.MACONGNO.StartsWith(key) ||
                           item.MACONGNO.EndsWith(key) ||
                           item.MACONGNO.Contains(key) ||
                           item1.HOTEN.StartsWith(key) ||
                           item1.HOTEN.EndsWith(key) ||
                           item1.HOTEN.Contains(key) ||
                           item.NODAU.ToString().StartsWith(key) ||
                           item.NODAU.ToString().EndsWith(key) ||
                           item.NODAU.ToString().Contains(key) ||
                           item.NOPHATSINH.ToString().StartsWith(key) ||
                           item.NOPHATSINH.ToString().EndsWith(key) ||
                           item.NOPHATSINH.ToString().Contains(key) ||
                           item.NOCUOI.ToString().StartsWith(key) ||
                           item.NOCUOI.ToString().EndsWith(key) ||
                           item.NOCUOI.ToString().Contains(key) ||
                           item.THANG.ToString().StartsWith(key) ||
                           item.THANG.ToString().EndsWith(key) ||
                           item.THANG.ToString().Contains(key)
                        )
                        select new
                        {
                            item.MACONGNO,
                            item1.HOTEN,
                            item.NODAU,
                            item.NOPHATSINH,
                            item.NOCUOI,
                            item.THANG
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
                dt.Rows.Add(stt, item.MACONGNO, item.HOTEN, item.NODAU, item.NOPHATSINH, item.NOCUOI, item.THANG);
                stt++;
            }

            return dt;
        }
    }
}
