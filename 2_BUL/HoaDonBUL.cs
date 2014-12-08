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
    public class HoaDonBUL
    {
        public static DataTable SellectAllHoaDonBUL()
        {
            return HoaDonController.SellectAllHoaDonDAL();
        }

        public static void DeleteHoaDonsBUL(List<string> keys)
        {
            HoaDonController.DeleteHoaDonsDAL(keys);
        }

        public static HOADON SelectHoaDonBUL(string key)
        {
            return HoaDonController.SelectHoaDonDAL(key);
        }

        public static void InsertHoaDonBUL(HOADON item)
        {
            HoaDonController.InsertHoaDonDAL(item);
        }

        public static bool checkMaHDBUL(string key)
        {
            return HoaDonController.checkMaHDDAL(key);
        }

        public static void UpdateHoaDonBUL(HOADON item)
        {
            HoaDonController.UpdateHoaDonDAL(item);
        }

        public static DataTable SearchHoaDonsBUL(string key)
        {
            return HoaDonController.SearchHoaDonsDAL(key);
        }

        public static KHACHHANG SelectHoaDon_KHBUL(string p)
        {
            return HoaDonController.SelectHoaDon_KHDAL(p);
        }
    }
}
