using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_DAL
{
    public class CTPNDAL
    {
        static QLNSModel db = new QLNSModel();

        public static void InsertCTPNDAL(CTPN temp)
        {
            db.CTPNs.InsertOnSubmit(temp);

            db.SubmitChanges();
        }
        public static DataTable SellectAllCTPNDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.CTPNs
                        join item2 in db.NHAPs on item.MAPN equals item2.MAPN
                        join item3 in db.SACHes on item.MASACH equals item3.MASACH
                        select new
                        {
                            item.MACTPN,
                            item2.NGAYNHAP,
                            item3.TENSACH,
                            item.SL_NHAP
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã CTPN", typeof(string));
            dt.Columns.Add("Ngày Nhập", typeof(DateTime));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Số lượng nhập", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MACTPN, item.NGAYNHAP, item.TENSACH, item.SL_NHAP);
                stt++;
            }

            return dt;
        }

        //DataGridview select KH by MaKH
        public static CTPN SelectCTPNDAL(string MaCTPN)
        {
            var query = from item in db.CTPNs
                        where item.MACTPN == MaCTPN
                        select new
                        {
                            item.MACTPN,
                            item.MAPN,
                            item.MASACH,
                            item.SL_NHAP
                        };
            CTPN ctpn = new CTPN();
            foreach (var item in query)
            {
                ctpn.MACTPN = item.MACTPN;
                ctpn.MAPN = item.MAPN;
                ctpn.MASACH = item.MASACH;
                ctpn.SL_NHAP = item.SL_NHAP;
            }
            return ctpn;
        }
        public static void DeleteCTPNDAL(List<string> ctpns)
        {
            //Take ctpns in CTPNs which have mactpn

            var query = from item in db.CTPNs
                        where ctpns.Contains(item.MACTPN)
                        select item;

            //Detele
            db.CTPNs.DeleteAllOnSubmit(query);

            //Confirm database
            db.SubmitChanges();

        }
        public static bool checkMaCTPNDAL(string mactpn)
        {
            var query = from item in db.CTPNs
                        where item.MACTPN.Equals(mactpn)
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
        //kiểm tra lại
        public static void UpdateCTPNDAL(CTPN item)
        {
            var query = db.CTPNs.Single(sa => sa.MACTPN == item.MACTPN);
            query.MAPN = item.MAPN;
            query.MASACH = item.MASACH;
            query.SL_NHAP = item.SL_NHAP;

            db.SubmitChanges();
        }

        public static DataTable SearchCTPNDAL(string temp)
        {
            var query = from item in db.CTPNs
                        join item2 in db.NHAPs on item.MAPN equals item2.MAPN
                        join item3 in db.SACHes on item.MASACH equals item3.MASACH
                        where
                        (
                           item.MACTPN.StartsWith(temp) ||
                           item.MACTPN.EndsWith(temp) ||
                           item.MACTPN.Contains(temp) ||
                           item2.NGAYNHAP.ToString().StartsWith(temp) ||
                           item2.NGAYNHAP.ToString().EndsWith(temp) ||
                           item2.NGAYNHAP.ToString().Contains(temp) ||
                           item.MAPN.StartsWith(temp) ||
                           item.MAPN.EndsWith(temp) ||
                           item.MAPN.Contains(temp) ||
                           item3.TENSACH.StartsWith(temp) ||
                           item3.TENSACH.EndsWith(temp) ||
                           item3.TENSACH.Contains(temp) ||
                           item.MASACH.StartsWith(temp) ||
                           item.MASACH.EndsWith(temp) ||
                           item.MASACH.Contains(temp) ||
                           item.SL_NHAP.ToString().Contains(temp) ||
                           item.SL_NHAP.ToString().StartsWith(temp) ||
                           item.SL_NHAP.ToString().EndsWith(temp)
                        )
                        select new
                        {
                            item.MACTPN,
                            item2.NGAYNHAP,
                            item3.TENSACH,
                            item.SL_NHAP,
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã CTPN", typeof(string));
            dt.Columns.Add("Ngày Nhập", typeof(DateTime));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Số lượng nhập", typeof(int));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MACTPN, item.NGAYNHAP, item.TENSACH, item.SL_NHAP);
                stt++;
            }

            return dt;
        }
    }
}
