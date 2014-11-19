using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;
using _4_DTO;

namespace _2_BUL
{
    public class SachBUL
    {
        public static DataTable SelectAllBooksBUL()
        {
            return SachController.SellectAllBooksDAL();
        }

        public static void DeleteBooksBUL(List<string> books)
        {
            SachController.DeleteBooksDAL(books);
        }

        public static SACH SelectBookBUL(string MaSach)
        {
            return SachController.SelectBookDAL(MaSach);
        }

        public static void InsertBookBUL(SACH temp)
        {
            SachController.InsertBookDAL(temp);
        }

        public static bool checkMaSachBUL(string masach)
        {
            return SachController.checkMaSachDAL(masach);
        }

        public static void UpdateSachBUL(SACH temp)
        {
            SachController.UpdateSachDAL(temp);
        }

        public static DataTable SearchBooksBUL(string temp)
        {
            return SachController.SearchBooksDAL(temp);
        }
    }
}
