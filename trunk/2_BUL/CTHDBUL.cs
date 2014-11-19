using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;
using _4_DTO;
using System.Data;

namespace _2_BUL
{
    public class CTHDBUL
    {
        public static DataTable SellectAllCTHDsBUL()
        {
            return CTHDController.SellectAllCTHDsDAL();
        }

        public static void DeleteCTHDsBUL(List<string> keys)
        {
            CTHDController.DeleteCTHDsDAL(keys);
        }

        public static CTHD SelectCTHDBUL(string key)
        {
            return CTHDController.SelectCTHDDAL(key);
        }

        public static void InsertCTHDBUL(CTHD item)
        {
            CTHDController.InsertCTHDDAL(item);
        }

        public static bool checkMaCTHDBUL(string key)
        {
            return CTHDController.checkMaCTHDDAL(key);
        }

        public static void UpdateCTHDBUL(CTHD item)
        {
            CTHDController.UpdateCTHDDAL(item);
        }

        public static DataTable SearchCTHDsBUL(string key)
        {
            return CTHDController.SearchCTHDsDAL(key);
        }

        public static bool checkKH_CTHDBUL(string key)
        {
            return CTHDController.checkKH_CTHDDAL(key);
        }

        public static int? TakeSach_SL_TonBUL(string key)
        {
            return CTHDController.TakeSach_SL_TonDAL(key);
        }
    }
}
