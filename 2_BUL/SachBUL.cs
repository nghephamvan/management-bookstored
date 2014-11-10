using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;

namespace _2_BUL
{
    public class SachBUL
    {
        public static DataTable SelectAllBooksBUL()
        {
            return SachDAL.SellectAllBooksDAL();
        }

        public static void DeleteBooksBUL(List<string> books)
        {
            SachDAL.DeleteBooksDAL(books);
        }

        public static SACH SelectBookBUL(string MaSach)
        {
            return SachDAL.SelectBookDAL(MaSach);
        }

        public static void InsertBookBUL(SACH temp)
        {
            SachDAL.InsertBookDAL(temp);
        }

        public static bool checkMaSachBUL(string masach)
        {
            return SachDAL.checkMaSachDAL(masach);
        }

        public static void UpdateSachBUL(SACH temp)
        {
            SachDAL.UpdateSachDAL(temp);
        }

        public static DataTable SearchBooksBUL(string temp)
        {
            return SachDAL.SearchBooksDAL(temp);
        }
    }
}
