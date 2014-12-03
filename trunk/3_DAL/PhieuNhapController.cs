using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class PhieuNhapController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static void InsertPNDAL(PHIEUNHAP item)
        {
            db.PHIEUNHAPs.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static DataTable SellectAllPhieuNhaps_DAL()
        {
            DataTable dt = new DataTable();

            var query = from pn in db.PHIEUNHAPs
                        where pn.XoaDuLieu == false
                        select new
                        {
                            pn.MaPN,
                            pn.NgayNhap
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã phiếu nhập", typeof(string));
            dt.Columns.Add("Ngày nhập", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaPN, item.NgayNhap);
                stt++;
            }

            return dt;
        }

        //Lay Phiếu nhập từ DataGridView
        public static PHIEUNHAP SelectPhieuNhapDAL(string key)
        {
            PHIEUNHAP query = db.PHIEUNHAPs.Single(i => i.MaPN.Equals(key));
            return query;
        }

        public static void DeletePNsDAL(List<string> keys)
        {

            //Delete on CTPN
            db.CTPNs
                .Where(i => keys.Contains(i.MaPN))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);


            //Delete on NHAP
            db.PHIEUNHAPs
                .Where(i => keys.Contains(i.MaPN))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaPNDAL(string key)
        {
            var query = from sc in db.PHIEUNHAPs
                        where sc.MaPN.Equals(key)
                        select sc;

            if (query.Count() <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //kiểm tra lại
        public static void UpdatePNDAL(PHIEUNHAP item)
        {
            var query = db.PHIEUNHAPs.Single(sa => sa.MaPN == item.MaPN);
            query.NgayNhap = item.NgayNhap;
            db.SubmitChanges();
        }

        public static DataTable SearchPNsDAL(string key)
        {
            var query = from pn in db.PHIEUNHAPs
                        where
                        (
                           pn.MaPN.StartsWith(key) ||
                           pn.MaPN.EndsWith(key) ||
                           pn.MaPN.Contains(key) ||
                           pn.NgayNhap.ToString().StartsWith(key) ||
                           pn.NgayNhap.ToString().EndsWith(key) ||
                           pn.NgayNhap.ToString().Contains(key)
                        ) && pn.XoaDuLieu == false
                        select new
                        {
                            pn.MaPN,
                            pn.NgayNhap,
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã phiếu nhập", typeof(string));
            dt.Columns.Add("Ngày Nhập", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaPN, item.NgayNhap);
                stt++;
            }

            return dt;
        }
    }
}
