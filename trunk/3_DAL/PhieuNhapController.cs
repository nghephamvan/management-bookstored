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

        public static void InsertPNDAL(NHAP item)
        {
            db.NHAPs.InsertOnSubmit(item);

            db.SubmitChanges();
        }

        public static DataTable SellectAllPhieuNhaps_DAL()
        {
            DataTable dt = new DataTable();

            var query = from pn in db.NHAPs
                        select new
                        {
                            pn.MAPN,
                            pn.NGAYNHAP
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã phiếu nhập", typeof(string));
            dt.Columns.Add("Ngày nhập", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MAPN, item.NGAYNHAP);
                stt++;
            }

            return dt;
        }

        //Lay Phiếu nhập từ DataGridView
        public static NHAP SelectPhieuNhapDAL(string key)
        {
            NHAP query = db.NHAPs.Single(i => i.MAPN.Equals(key));
            return query;
        }

        public static void DeletePNsDAL(List<string> keys)
        {
            //Take PNs in Nhaps which have maPN equal listPN
            var lstPNs = from b in db.NHAPs
                           where keys.Contains(b.MAPN)
                           select b;
            //Take PNs in CTPN which have maPN equal lstPNs.MAPN
            var lstctpns = from b in db.CTPNs
                           join ln in lstPNs on b.MAPN equals ln.MAPN
                           select b;

            //Delete on CTPN
            db.CTPNs.DeleteAllOnSubmit(lstctpns);


            //Delete on NHAP
            db.NHAPs.DeleteAllOnSubmit(lstPNs);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaPNDAL(string key)
        {
            var query = from sc in db.NHAPs
                        where sc.MAPN.Equals(key)
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
        public static void UpdatePNDAL(NHAP item)
        {
            var query = db.NHAPs.Single(sa => sa.MAPN == item.MAPN);
            query.NGAYNHAP = item.NGAYNHAP;
            db.SubmitChanges();
        }

        public static DataTable SearchPNsDAL(string key)
        {
            var query = from pn in db.NHAPs
                        where
                        (
                           pn.MAPN.StartsWith(key) ||
                           pn.MAPN.EndsWith(key) ||
                           pn.MAPN.Contains(key) ||
                           pn.NGAYNHAP.ToString().StartsWith(key) ||
                           pn.NGAYNHAP.ToString().EndsWith(key) ||
                           pn.NGAYNHAP.ToString().Contains(key)
                        )
                        select new
                        {
                            pn.MAPN,
                            pn.NGAYNHAP,
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã phiếu nhập", typeof(string));
            dt.Columns.Add("Ngày Nhập", typeof(DateTime));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MAPN, item.NGAYNHAP);
                stt++;
            }

            return dt;
        }
    }
}
