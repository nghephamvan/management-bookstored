using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_DAL
{
    public class SachDAL
    {

         static QLNSModel db = new QLNSModel();

         public static void InsertBookDAL(SACH temp) 
         {
             db.SACHes.InsertOnSubmit(temp);

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
         public static SACH SelectBookDAL(string MaSach)
         {
             var query = from b in db.SACHes
                         where b.MASACH == MaSach
                         select new
                         {
                             b.MASACH,
                             b.TENSACH,
                             b.THELOAI,
                             b.TACGIA,
                             b.SL_TON,
                             b.DONGIA
                         };
             SACH sc = new SACH();
             foreach (var s in query)
             {
                 sc.MASACH = s.MASACH;
                 sc.TENSACH = s.TENSACH;
                 sc.THELOAI = s.THELOAI;
                 sc.TACGIA = s.TACGIA;
                 sc.SL_TON = s.SL_TON;
                 sc.DONGIA = s.DONGIA;
             }
             return sc;
         }
         public static void DeleteBooksDAL(List<string> books)
         {
             //Take books in Saches which have Masach equal listbooks

             var lstbooks = from b in db.SACHes
                            where books.Contains(b.MASACH)
                            select b;
             var lstctpns = from b in db.CTPNs
                            join lb in lstbooks on b.MASACH equals lb.MASACH
                            select b;

             var lstcthds = from b in db.CTHDs
                            join lb in lstbooks on b.MASACH equals lb.MASACH
                            select b;

             //Delete
             db.CTHDs.DeleteAllOnSubmit(lstcthds);

             //Delete
             db.CTPNs.DeleteAllOnSubmit(lstctpns);
             //Delete


             db.SACHes.DeleteAllOnSubmit(lstbooks);

             //Confirm database
             db.SubmitChanges();

         }
         public static bool checkMaSachDAL(string masach)
         {
             var query = from sc in db.SACHes
                         where sc.MASACH.Equals(masach)
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
         public static void UpdateSachDAL(SACH temp)
         {
             var query = db.SACHes.Single(sa=>sa.MASACH==temp.MASACH);
             query.TENSACH = temp.TENSACH;
             query.THELOAI = temp.THELOAI;
             query.TACGIA = temp.TACGIA;
             query.SL_TON = temp.SL_TON;
             query.DONGIA = temp.DONGIA;

             db.SubmitChanges();
         }

         public static DataTable SearchBooksDAL(string temp)
         {
             var query = from sc in db.SACHes
                         where
                         (
                            sc.MASACH.StartsWith(temp) ||
                            sc.MASACH.EndsWith(temp) ||
                            sc.MASACH.Contains(temp) ||
                            sc.TENSACH.StartsWith(temp) ||
                            sc.TENSACH.EndsWith(temp) ||
                            sc.TENSACH.Contains(temp) ||
                            sc.THELOAI.StartsWith(temp) ||
                            sc.THELOAI.EndsWith(temp) ||
                            sc.THELOAI.Contains(temp) ||
                            sc.TACGIA.StartsWith(temp) ||
                            sc.TACGIA.EndsWith(temp) ||
                            sc.TACGIA.Contains(temp) ||
                            sc.SL_TON.ToString().Contains(temp) ||
                            sc.SL_TON.ToString().StartsWith(temp) ||
                            sc.SL_TON.ToString().EndsWith(temp) ||
                            sc.DONGIA.ToString().Contains(temp) ||
                            sc.DONGIA.ToString().StartsWith(temp) ||
                            sc.DONGIA.ToString().EndsWith(temp)
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
