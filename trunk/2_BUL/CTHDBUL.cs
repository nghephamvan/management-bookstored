using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;
using System.Data;

namespace _2_BUL
{
    public class CTHDBUL
    {
        public static DataTable SellectAllCTHDsBUL()
        {
            return CTHDDAL.SellectAllCTHDsDAL();
        }

        public static void DeleteCTHDsBUL(List<string> keys)
        {
            CTHDDAL.DeleteCTHDsDAL(keys);
        }

        public static CTHD SelectCTHDBUL(string key)
        {
            return CTHDDAL.SelectCTHDDAL(key);
        }

        public static void InsertCTHDBUL(CTHD item)
        {
            CTHDDAL.InsertCTHDDAL(item);
        }

        public static bool checkMaCTHDBUL(string key)
        {
            return CTHDDAL.checkMaCTHDDAL(key);
        }

        public static void UpdateCTHDBUL(CTHD item)
        {
            CTHDDAL.UpdateCTHDDAL(item);
        }

        public static DataTable SearchCTHDsBUL(string key)
        {
            return CTHDDAL.SearchCTHDsDAL(key);
        }

        public static bool checkKH_CTHDBUL(string key)
        {
            return CTHDDAL.checkKH_CTHDDAL(key);
        }

        public static int? TakeSach_SL_TonBUL(string key)
        {
            return CTHDDAL.TakeSach_SL_TonDAL(key);
        }
    }
}
