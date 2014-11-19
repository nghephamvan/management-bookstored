using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class KhachHangController
    {
        static QLNSModel db = new QLNSModel();

        public static void InsertCustomerDAL(KHACHHANG temp)
        {
            db.KHACHHANGs.InsertOnSubmit(temp);

            db.SubmitChanges();
        }
        public static DataTable SellectAllCustomerDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.KHACHHANGs
                        select new
                        {
                            item.MAKH,
                            item.HOTEN,
                            item.DIACHI,
                            item.DIENTHOAI,
                            item.EMAIL,
                            item.SOTIENNO
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
                dt.Rows.Add(stt, item.MAKH, item.HOTEN, item.DIACHI, item.DIENTHOAI, item.EMAIL, item.SOTIENNO);
                stt++;
            }

            return dt;
        }

        //DataGridview select KH by MaKH
        public static KHACHHANG SelectCustomerDAL(string MaKH)
        {
            var query = from item in db.KHACHHANGs
                        where item.MAKH == MaKH
                        select new
                        {
                            item.MAKH,
                            item.HOTEN,
                            item.DIACHI,
                            item.DIENTHOAI,
                            item.EMAIL,
                            item.SOTIENNO
                        };
            KHACHHANG cus = new KHACHHANG();
            foreach (var item in query)
            {
                cus.MAKH=  item.MAKH;
                cus.HOTEN = item.HOTEN;
                cus.DIACHI = item.DIACHI;
                cus.DIENTHOAI= item.DIENTHOAI;
                cus.EMAIL= item.EMAIL;
                cus.SOTIENNO = item.SOTIENNO;
            }
            return cus;
        }
        public static void DeleteCustomersDAL(List<string> customers)
        {
            //Take custoners in KHACHHANGs which have MAKH

            var query = from item in db.KHACHHANGs
                        where customers.Contains(item.MAKH)
                        select item;
            //Detele MAKH in CONGNO
            var queryCN = from item in db.CONGNOs
                          where customers.Contains(item.MAKH)
                          select item;
            //Delete MAKH in TON
            var queryTON = from item in db.TONs
                           where customers.Contains(item.MAKH)
                           select item;
            //Delete MAKH in HOADON 
            var queryHD = from item in db.HOADONs
                          where customers.Contains(item.MAKH)
                          select item;
            
            //Delete HOADON in CTHD
            var queryCTHD = from item in db.CTHDs
                            join hd in queryHD on item.MAHD equals hd.MAHD
                            select item;

            //Detele
            db.CTHDs.DeleteAllOnSubmit(queryCTHD);
            db.HOADONs.DeleteAllOnSubmit(queryHD);
            db.TONs.DeleteAllOnSubmit(queryTON);
            db.CONGNOs.DeleteAllOnSubmit(queryCN);
            db.KHACHHANGs.DeleteAllOnSubmit(query);

            //Confirm database
            db.SubmitChanges();

        }
        public static bool checkMaKHDAL(string makh)
        {
            var query = from item in db.KHACHHANGs
                        where item.MAKH.Equals(makh)
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
            var query = db.KHACHHANGs.Single(sa => sa.MAKH == item.MAKH);
            query.HOTEN = item.HOTEN;
            query.DIACHI = item.DIACHI;
            query.DIENTHOAI = item.DIENTHOAI;
            query.EMAIL = item.EMAIL;
            query.SOTIENNO = item.SOTIENNO;

            db.SubmitChanges();
        }
        public static DataTable SearchCustomersDAL(string temp)
        {
            var query = from item in db.KHACHHANGs
                        where
                        (
                           item.MAKH.StartsWith(temp) ||
                           item.MAKH.EndsWith(temp) ||
                           item.MAKH.Contains(temp) ||
                           item.HOTEN.StartsWith(temp) ||
                           item.HOTEN.EndsWith(temp) ||
                           item.HOTEN.Contains(temp) ||
                           item.DIACHI.StartsWith(temp) ||
                           item.DIACHI.EndsWith(temp) ||
                           item.DIACHI.Contains(temp) ||
                           item.DIENTHOAI.StartsWith(temp) ||
                           item.DIENTHOAI.EndsWith(temp) ||
                           item.DIENTHOAI.Contains(temp) ||
                           item.EMAIL.StartsWith(temp) ||
                           item.EMAIL.EndsWith(temp) ||
                           item.EMAIL.Contains(temp) ||
                           item.SOTIENNO.ToString().Contains(temp) ||
                           item.SOTIENNO.ToString().StartsWith(temp) ||
                           item.SOTIENNO.ToString().EndsWith(temp)
                        )
                        select new
                        {
                            item.MAKH,
                            item.HOTEN,
                            item.DIACHI,
                            item.DIENTHOAI,
                            item.EMAIL,
                            item.SOTIENNO
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
                dt.Rows.Add(stt, item.MAKH, item.HOTEN, item.DIACHI, item.DIENTHOAI, item.EMAIL, item.SOTIENNO);
                stt++;
            }

            return dt;
        }
    }
}
