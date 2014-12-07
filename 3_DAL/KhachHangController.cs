using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class KhachHangController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static void InsertCustomerDAL(KHACHHANG temp)
        {
            temp.XoaDuLieu = false;
            db.KHACHHANGs.InsertOnSubmit(temp);

            db.SubmitChanges();
        }

        public static DataTable SellectAllCustomerDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.KHACHHANGs
                        where item.XoaDuLieu == false
                        select new
                        {
                            item.MaKH,
                            item.HoTen,
                            item.DiaChi,
                            item.DienThoai,
                            item.Email,
                            item.SoTienNo
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã KH", typeof(string));
            dt.Columns.Add("Tên KH", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Số điện thoại", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Số tiền nợ", typeof(long));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaKH, item.HoTen, item.DiaChi, item.DienThoai, item.Email, item.SoTienNo);
                stt++;
            }

            return dt;
        }

        //DataGridview select KH by MaKH
        public static KHACHHANG SelectCustomerDAL(string key)
        {
            KHACHHANG query = db.KHACHHANGs.Single(i => i.MaKH.Equals(key));
            return query;
        }

        public static void DeleteCustomersDAL(List<string> keys)
        {
            //Take custoners in KHACHHANGs which have MAKH

            var queryHD = from item in db.HOADONs
                          where keys.Contains(item.MaKH)
                          select item;
            
            //Delete HOADON in CTHD
            var queryCTHD = from item in db.CTHDs
                            join hd in queryHD on item.MaHD equals hd.MaHD
                            select new { item.MaHD};

            List<string> lst = new List<string>();
            foreach (var i in queryCTHD)
            {
                lst.Add(i.MaHD);
            }

            //Detele
            db.CTHDs
                .Where(i => lst.Contains(i.MaHD))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);
            db.HOADONs
                .Where(i => keys.Contains(i.MaKH))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);
            db.CONGNOs
                .Where(i => keys.Contains(i.MaKH))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);
            db.KHACHHANGs
                .Where(i => keys.Contains(i.MaKH))
                .ToList()
                .ForEach(i => i.XoaDuLieu = true);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkMaKHDAL(string makh)
        {
            var query = from item in db.KHACHHANGs
                        where item.MaKH.Equals(makh)
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
        public static void UpdateCustomerDAL(KHACHHANG item)
        {
            var query = db.KHACHHANGs.Single(sa => sa.MaKH == item.MaKH);
            query.HoTen = item.HoTen;
            query.DiaChi = item.DiaChi;
            query.DienThoai = item.DienThoai;
            query.Email = item.Email;
            query.SoTienNo = item.SoTienNo;

            db.SubmitChanges();
        }

        public static DataTable SearchCustomersDAL(string temp)
        {
            var query = from item in db.KHACHHANGs
                        where
                        (
                           item.MaKH.StartsWith(temp) ||
                           item.MaKH.EndsWith(temp) ||
                           item.MaKH.Contains(temp) ||
                           item.HoTen.StartsWith(temp) ||
                           item.HoTen.EndsWith(temp) ||
                           item.HoTen.Contains(temp) ||
                           item.DiaChi.StartsWith(temp) ||
                           item.DiaChi.EndsWith(temp) ||
                           item.DiaChi.Contains(temp) ||
                           item.DienThoai.StartsWith(temp) ||
                           item.DienThoai.EndsWith(temp) ||
                           item.DienThoai.Contains(temp) ||
                           item.Email.StartsWith(temp) ||
                           item.Email.EndsWith(temp) ||
                           item.Email.Contains(temp) ||
                           item.SoTienNo.ToString().Contains(temp) ||
                           item.SoTienNo.ToString().StartsWith(temp) ||
                           item.SoTienNo.ToString().EndsWith(temp)
                        ) && item.XoaDuLieu == false
                        select new
                        {
                            item.MaKH,
                            item.HoTen,
                            item.DiaChi,
                            item.DienThoai,
                            item.Email,
                            item.SoTienNo
                        };


            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Mã KH", typeof(string));
            dt.Columns.Add("Tên KH", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Số điện thoại", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Số tiền nợ", typeof(long));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.MaKH, item.HoTen, item.DiaChi, item.DienThoai, item.Email, item.SoTienNo);
                stt++;
            }

            return dt;
        }
    }
}