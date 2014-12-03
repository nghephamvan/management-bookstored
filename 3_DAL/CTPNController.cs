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
                        join item2 in db.PHIEUNHAPs on item.MaPN equals item2.MaPN
                        join item3 in db.SACHes on item.MaSach equals item3.MaSach
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MaCTPN,
                            item3.TenSach,
                            item3.TheLoai,
                            item3.TacGia,
                            item.SL_Nhap,
                            item2.NgayNhap,
                            item3.DonGia
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
                dt.Rows.Add(stt, item.MaCTPN, item.TenSach, item.TheLoai, item.TacGia, item.SL_Nhap, item.NgayNhap, item.DonGia);
                stt++;
            }

            return dt;
        }

        //DataGridview select KH by MaKH
        public static CTPN SelectCTPNDAL(string key)
        {
            CTPN query = db.CTPNs.Single(i => i.MaCTPN.Equals(key));
            return query;
        }

        public static void DeleteCTPNDAL(List<string> keys)
        {

            //Detele
            db.CTPNs
                .Where(i => keys.Contains(i.MaCTPN))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaCTPNDAL(string mactpn)
        {
            var query = from item in db.CTPNs
                        where item.MaCTPN.Equals(mactpn)
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

            SACH query = db.SACHes.Where(i => i.XoaDuLieu == false).Single(i => i.MaSach.Equals(key));

            if (query.SL_Ton > ThamSoController.SelectThamSoDAL().SL_TonToiDaTruocNhap)
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
            var query = db.CTPNs.Single(sa => sa.MaCTPN == item.MaCTPN);
            query.MaPN = item.MaPN;
            query.MaSach = item.MaSach;
            query.SL_Nhap = item.SL_Nhap;

            db.SubmitChanges();
        }

        public static DataTable SearchCTPNDAL(string temp)
        {
            var query = from item in db.CTPNs
                        join item2 in db.PHIEUNHAPs on item.MaPN equals item2.MaPN
                        join item3 in db.SACHes on item.MaSach equals item3.MaSach
                        where
                        (
                           item.MaCTPN.StartsWith(temp) ||
                           item.MaCTPN.EndsWith(temp) ||
                           item.MaCTPN.Contains(temp) ||
                           item3.TenSach.StartsWith(temp) ||
                           item3.TenSach.EndsWith(temp) ||
                           item3.TenSach.Contains(temp) ||
                           item3.TheLoai.StartsWith(temp) ||
                           item3.TheLoai.EndsWith(temp) ||
                           item3.TheLoai.Contains(temp) ||
                           item3.TacGia.StartsWith(temp) ||
                           item3.TacGia.EndsWith(temp) ||
                           item3.TacGia.Contains(temp) ||
                           item2.NgayNhap.ToString().StartsWith(temp) ||
                           item2.NgayNhap.ToString().EndsWith(temp) ||
                           item2.NgayNhap.ToString().Contains(temp) ||
                           item.SL_Nhap.ToString().Contains(temp) ||
                           item.SL_Nhap.ToString().StartsWith(temp) ||
                           item.SL_Nhap.ToString().EndsWith(temp)||
                           item2.NgayNhap.ToString().StartsWith(temp) ||
                           item2.NgayNhap.ToString().EndsWith(temp) ||
                           item2.NgayNhap.ToString().Contains(temp) ||
                           item3.DonGia.ToString().StartsWith(temp) ||
                           item3.DonGia.ToString().EndsWith(temp) ||
                           item3.DonGia.ToString().Contains(temp)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaCTPN,
                            item3.TenSach,
                            item3.TheLoai,
                            item3.TacGia,
                            item.SL_Nhap,
                            item2.NgayNhap,
                            item3.DonGia
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
                dt.Rows.Add(stt, item.MaCTPN, item.TenSach, item.TheLoai, item.TacGia, item.SL_Nhap, item.NgayNhap, item.DonGia);
                stt++;
            }

            return dt;
        }

        public static bool checkSL_NhapItNhat(int? key)
        {
            if (key < ThamSoController.SelectThamSoDAL().SL_NhapItNhat)
            {
                return false;
            }

            return true;
        }
    }
}
