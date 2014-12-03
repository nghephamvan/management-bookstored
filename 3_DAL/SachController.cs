using System;
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
                         where sc.XoaDuLieu == false
                         select new
                         {
                             sc.MaSach,
                             sc.TenSach,
                             sc.TheLoai,
                             sc.TacGia,
                             sc.SL_Ton,
                             sc.DonGia
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
                 dt.Rows.Add(stt, item.MaSach, item.TenSach, item.TheLoai, item.TacGia, item.SL_Ton, item.DonGia);
                 stt++;
             }

             return dt;
         }

         //DataGridview select Sach by MaSach
         public static SACH SelectBookDAL(string key)
         {
             SACH query = db.SACHes.Single(i => i.MaSach.Equals(key));
             return query;
         }

         public static void DeleteBooksDAL(List<string> keys)
         {
             //Take books in Saches which have Masach equal listbooks
              ////
             db.CTPNs
                 .Where(i => keys.Contains(i.MaSach))
                 .ToList()
                 .ForEach(i => i.XoaDuLieu = true);
             db.CTHDs
                 .Where(i => keys.Contains(i.MaSach))
                 .ToList()
                 .ForEach(i => i.XoaDuLieu = true);
             db.PHIEUTONs
                 .Where(i => keys.Contains(i.MaSach))
                 .ToList()
                 .ForEach(i => i.XoaDuLieu = true);

             db.SACHes
                 .Where(i => keys.Contains(i.MaSach))
                 .ToList()
                 .ForEach(i => i.XoaDuLieu = true);


             db.SubmitChanges();

         }

         public static bool checkMaSachDAL(string key)
         {
             var query = from sc in db.SACHes
                         where sc.MaSach.Equals(key)
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
             var query = db.SACHes.Single(sa => sa.MaSach == item.MaSach);
             query.MaSach = item.MaSach;
             query.TheLoai = item.TheLoai;
             query.TacGia = item.TacGia;
             query.SL_Ton = item.SL_Ton;
             query.DonGia = item.DonGia;

             db.SubmitChanges();
         }

         public static DataTable SearchBooksDAL(string key)
         {
             var query = from sc in db.SACHes
                         where
                         (
                            sc.MaSach.StartsWith(key) ||
                            sc.MaSach.EndsWith(key) ||
                            sc.MaSach.Contains(key) ||
                            sc.TenSach.StartsWith(key) ||
                            sc.TenSach.EndsWith(key) ||
                            sc.TenSach.Contains(key) ||
                            sc.TheLoai.StartsWith(key) ||
                            sc.TheLoai.EndsWith(key) ||
                            sc.TheLoai.Contains(key) ||
                            sc.TacGia.StartsWith(key) ||
                            sc.TacGia.EndsWith(key) ||
                            sc.TacGia.Contains(key) ||
                            sc.SL_Ton.ToString().Contains(key) ||
                            sc.SL_Ton.ToString().StartsWith(key) ||
                            sc.SL_Ton.ToString().EndsWith(key) ||
                            sc.DonGia.ToString().Contains(key) ||
                            sc.DonGia.ToString().StartsWith(key) ||
                            sc.DonGia.ToString().EndsWith(key)
                         ) && sc.XoaDuLieu == false
                         select new
                         {
                             sc.MaSach,
                             sc.TenSach,
                             sc.TheLoai,
                             sc.TacGia,
                             sc.SL_Ton,
                             sc.DonGia
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
                 dt.Rows.Add(stt, item.MaSach, item.TenSach, item.TheLoai, item.TacGia, item.SL_Ton, item.DonGia);
                 stt++;
             }

             return dt;
         }
    }
}
