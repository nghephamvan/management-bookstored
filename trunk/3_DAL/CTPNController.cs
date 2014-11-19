using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class CTPNController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

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
                            item3.TENSACH,
                            item3.THELOAI,
                            item3.TACGIA,
                            item.SL_NHAP,
                            item2.NGAYNHAP,
                            item3.DONGIA
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã CTPN", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Thể Loại", typeof(string));
            dt.Columns.Add("Tác Giả", typeof(string));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Ngày Nhập", typeof(DateTime));
            dt.Columns.Add("Đơn Giá Nhập", typeof(decimal));
            
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MACTPN, item.TENSACH, item.THELOAI, item.TACGIA, item.SL_NHAP, item.NGAYNHAP, item.DONGIA);
                stt++;
            }

            return dt;
        }

        //DataGridview select KH by MaKH
        public static CTPN SelectCTPNDAL(string key)
        {
            CTPN query = db.CTPNs.Single(i => i.MACTPN.Equals(key));
            return query;
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

        public static bool checkCTPN_SachSLTonDAL(string key)
        {
            //var query = from item in db.SACHes
            //            where item.MASACH == key
            //            select new
            //            {
            //                item.SL_TON
            //            };

            SACH query = db.SACHes.Single(i => i.MASACH.Equals(key));

            if (query.SL_TON > ThamSoController.SelectThamSoDAL().SL_TONTOIDATRUOCNHAP)
            {
                return false;
            }

            //foreach (var item in query)
            //{
            //    if (item.SL_TON > 300)
            //    {
            //        return false;
            //    }
            //}

            return true;
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
                           item3.TENSACH.StartsWith(temp) ||
                           item3.TENSACH.EndsWith(temp) ||
                           item3.TENSACH.Contains(temp) ||
                           item3.THELOAI.StartsWith(temp) ||
                           item3.THELOAI.EndsWith(temp) ||
                           item3.THELOAI.Contains(temp) ||
                           item3.TACGIA.StartsWith(temp) ||
                           item3.TACGIA.EndsWith(temp) ||
                           item3.TACGIA.Contains(temp) ||
                           item2.NGAYNHAP.ToString().StartsWith(temp) ||
                           item2.NGAYNHAP.ToString().EndsWith(temp) ||
                           item2.NGAYNHAP.ToString().Contains(temp) ||
                           item.SL_NHAP.ToString().Contains(temp) ||
                           item.SL_NHAP.ToString().StartsWith(temp) ||
                           item.SL_NHAP.ToString().EndsWith(temp)||
                           item2.NGAYNHAP.ToString().StartsWith(temp) ||
                           item2.NGAYNHAP.ToString().EndsWith(temp) ||
                           item2.NGAYNHAP.ToString().Contains(temp) ||
                           item3.DONGIA.ToString().StartsWith(temp) ||
                           item3.DONGIA.ToString().EndsWith(temp) ||
                           item3.DONGIA.ToString().Contains(temp)
                        )
                        select new
                        {
                            item.MACTPN,
                            item3.TENSACH,
                            item3.THELOAI,
                            item3.TACGIA,
                            item.SL_NHAP,
                            item2.NGAYNHAP,
                            item3.DONGIA
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã CTPN", typeof(string));
            dt.Columns.Add("Tên Sách", typeof(string));
            dt.Columns.Add("Thể Loại", typeof(string));
            dt.Columns.Add("Tác Giả", typeof(string));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Ngày Nhập", typeof(DateTime));
            dt.Columns.Add("Đơn Giá Nhập", typeof(decimal));

            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MACTPN, item.TENSACH, item.THELOAI, item.TACGIA, item.SL_NHAP, item.NGAYNHAP, item.DONGIA);
                stt++;
            }

            return dt;
        }

        public static bool checkSL_NhapItNhat(int? key)
        {
            if (key < ThamSoController.SelectThamSoDAL().SL_NHAPITNHAT)
            {
                return false;
            }

            return true;
        }
    }
}
