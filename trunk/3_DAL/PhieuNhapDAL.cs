using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_DAL
{
    public class PhieuNhapDAL
    {
        static QLNSModel db = new QLNSModel();

        public static void InsertPNDAL(NHAP temp)
        {
            db.NHAPs.InsertOnSubmit(temp);

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
        public static NHAP SelectPhieuNhapDAL(string MaPN)
        {
            var query = from n in db.NHAPs
                        where n.MAPN == MaPN
                        select new
                        {
                            n.MAPN,
                            n.NGAYNHAP,
                        };
            NHAP pn = new NHAP();
            foreach (var s in query)
            {
                pn.MAPN = s.MAPN;
                pn.NGAYNHAP = s.NGAYNHAP;
            }
            return pn;
        }
        public static void DeletePNsDAL(List<string> PNs)
        {
            //Take PNs in Nhaps which have maPN equal listPN
            var lstPNs = from b in db.NHAPs
                           where PNs.Contains(b.MAPN)
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
        public static bool checkMaPNDAL(string mapn)
        {
            var query = from sc in db.NHAPs
                        where sc.MAPN.Equals(mapn)
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
        public static void UpdatePNDAL(NHAP temp)
        {
            var query = db.NHAPs.Single(sa => sa.MAPN == temp.MAPN);
            query.NGAYNHAP = temp.NGAYNHAP;
            db.SubmitChanges();
        }

        public static DataTable SearchPNsDAL(string temp)
        {
            var query = from pn in db.NHAPs
                        where
                        (
                           pn.MAPN.StartsWith(temp) ||
                           pn.MAPN.EndsWith(temp) ||
                           pn.MAPN.Contains(temp) ||
                           pn.NGAYNHAP.ToString().StartsWith(temp) ||
                           pn.NGAYNHAP.ToString().EndsWith(temp) ||
                           pn.NGAYNHAP.ToString().Contains(temp)
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
