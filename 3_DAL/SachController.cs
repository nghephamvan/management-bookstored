using System.Collections.Generic;
using System.Data;
using System.Linq;
using _4_DTO;

namespace _3_DAL
{
    public class SachController
    {

        static QLNSModelDataContext db = new QLNSModelDataContext();

         public static void InsertBookDAL(SACH item) 
         {
             db.SACHes.InsertOnSubmit(item);

             db.SubmitChanges();
         }

         public static DataTable SellectAllBooksDAL()
         {
             DataTable dt = new DataTable();

             var query = from sc in db.SACHes
                         select new
                         {
                             sc.MASACH,
                             sc.TENSACH,
                             sc.THELOAI,
                             sc.TACGIA,
                             sc.SL_TON,
                             sc.DONGIA
                         };
             dt.Columns.Add("STT", typeof(int));
             dt.Columns.Add("Mã Sách", typeof(string));
             dt.Columns.Add("Tên Sách", typeof(string));
             dt.Columns.Add("Thể Loại", typeof(string));
             dt.Columns.Add("Tác Giả", typeof(string));
             dt.Columns.Add("Số lượng Tồn", typeof(int));
             dt.Columns.Add("Đơn giá", typeof(long));
             int stt = 1;
             foreach (var item in query)
             {
                 dt.Rows.Add(stt, item.MASACH, item.TENSACH, item.THELOAI, item.TACGIA, item.SL_TON, item.DONGIA);
                 stt++;
             }

             return dt;
         }

         //DataGridview select Sach by MaSach
         public static SACH SelectBookDAL(string key)
         {
             SACH query = db.SACHes.Single(i => i.MASACH.Equals(key));
             return query;
         }

         public static void DeleteBooksDAL(List<string> keys)
         {
             //Take books in Saches which have Masach equal listbooks

             var lstbooks = from item in db.SACHes
                            where keys.Contains(item.MASACH)
                            select item;
             var lstctpns = from item in db.CTPNs
                            where keys.Contains(item.MASACH)
                            select item;

             var lstcthds = from item in db.CTHDs
                            where keys.Contains(item.MASACH)
                            select item;
             //Delete MAKH in TON
             var lsttons = from item in db.TONs
                           where keys.Contains(item.MASACH)
                           select item;

             //Delete
             db.CTHDs.DeleteAllOnSubmit(lstcthds);

             //Delete
             db.CTPNs.DeleteAllOnSubmit(lstctpns);

             //Delete
             db.TONs.DeleteAllOnSubmit(lsttons);

             db.SACHes.DeleteAllOnSubmit(lstbooks);

             //Confirm database
             db.SubmitChanges();

         }

         public static bool checkMaSachDAL(string key)
         {
             var query = from sc in db.SACHes
                         where sc.MASACH.Equals(key)
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
         public static void UpdateSachDAL(SACH item)
         {
             var query = db.SACHes.Single(sa=>sa.MASACH==item.MASACH);
             query.TENSACH = item.TENSACH;
             query.THELOAI = item.THELOAI;
             query.TACGIA = item.TACGIA;
             query.SL_TON = item.SL_TON;
             query.DONGIA = item.DONGIA;

             db.SubmitChanges();
         }

         public static DataTable SearchBooksDAL(string key)
         {
             var query = from sc in db.SACHes
                         where
                         (
                            sc.MASACH.StartsWith(key) ||
                            sc.MASACH.EndsWith(key) ||
                            sc.MASACH.Contains(key) ||
                            sc.TENSACH.StartsWith(key) ||
                            sc.TENSACH.EndsWith(key) ||
                            sc.TENSACH.Contains(key) ||
                            sc.THELOAI.StartsWith(key) ||
                            sc.THELOAI.EndsWith(key) ||
                            sc.THELOAI.Contains(key) ||
                            sc.TACGIA.StartsWith(key) ||
                            sc.TACGIA.EndsWith(key) ||
                            sc.TACGIA.Contains(key) ||
                            sc.SL_TON.ToString().Contains(key) ||
                            sc.SL_TON.ToString().StartsWith(key) ||
                            sc.SL_TON.ToString().EndsWith(key) ||
                            sc.DONGIA.ToString().Contains(key) ||
                            sc.DONGIA.ToString().StartsWith(key) ||
                            sc.DONGIA.ToString().EndsWith(key)
                         )
                         select new
                         {
                             sc.MASACH,
                             sc.TENSACH,
                             sc.THELOAI,
                             sc.TACGIA,
                             sc.SL_TON,
                             sc.DONGIA
                         };


             DataTable dt = new DataTable();
             dt.Columns.Add("STT", typeof(int));
             dt.Columns.Add("Mã Sách", typeof(string));
             dt.Columns.Add("Tên Sách", typeof(string));
             dt.Columns.Add("Thể Loại", typeof(string));
             dt.Columns.Add("Tác Giả", typeof(string));
             dt.Columns.Add("Số lượng Tồn", typeof(int));
             dt.Columns.Add("Đơn giá", typeof(long));
             int stt = 1;
             foreach (var item in query)
             {
                 dt.Rows.Add(stt, item.MASACH, item.TENSACH, item.THELOAI, item.TACGIA, item.SL_TON, item.DONGIA);
                 stt++;
             }

             return dt;
         }
    }
}
