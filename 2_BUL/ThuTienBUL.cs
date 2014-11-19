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
    public class ThuTienBUL
    {
        public static DataTable SellectAllThuTiensBUL()
        {
            return ThuTienController.SellectAllThuTiensDAL();
        }

        public static void DeleteThuTiensBUL(List<string> keys)
        {
            ThuTienController.DeleteThuTiensDAL(keys);
        }

        public static THUTIEN SelectThuTienBUL(string key)
        {
            return ThuTienController.SelectThuTienDAL(key);
        }

        public static void InsertThuTienBUL(THUTIEN item)
        {
            ThuTienController.InsertThuTienDAL(item);
        }

        public static bool checkMaThuTienBUL(string key)
        {
            return ThuTienController.checkMaThuTienDAL(key);
        }

        public static void UpdateThuTienBUL(THUTIEN item)
        {
            ThuTienController.UpdateThuTienDAL(item);
        }

        public static DataTable SearchThuTiensBUL(string key)
        {
            return ThuTienController.SearchThuTiensDAL(key);
        }

        public static bool checkKH_ThuTienBUL(string key)
        {
            return ThuTienController.checkKH_ThuTienDAL(key);
        }
    }
}
