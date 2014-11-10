using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;

namespace _2_BUL
{
    public class CTPNBUL
    {
        //public static void DeleteCTPNs_BooksBUL(List<string> ctpns)
        //{
        //    CTPNDAL.DeleteCTPNs_BooksDAL(ctpns);
        //}

        //public static void DeleteCTPNs_PNsBUL(List<string> ctpns)
        //{
        //    CTPNDAL.DeleteCTPNs_PNsDAL(ctpns);
        //}

        public static DataTable SellectAllCTPNBUL()
        {
            return CTPNDAL.SellectAllCTPNDAL();
        }

        public static void DeleteCTPNBUL(List<string> keys)
        {
            CTPNDAL.DeleteCTPNDAL(keys);
        }

        public static CTPN SelectCTPNBUL(string key)
        {
            return CTPNDAL.SelectCTPNDAL(key);
        }

        public static void InsertCTPNBUL(CTPN item)
        {
            CTPNDAL.InsertCTPNDAL(item);
        }

        public static bool checkMaCTPNBUL(string key)
        {
            return CTPNDAL.checkMaCTPNDAL(key);
        }

        public static void UpdateCTPNBUL(CTPN item)
        {
            CTPNDAL.UpdateCTPNDAL(item);
        }

        public static DataTable SearchCTPNBUL(string key)
        {
            return CTPNDAL.SearchCTPNDAL(key);
        }

        public static bool checkSachSLTonBUL(string key)
        {
            return CTPNDAL.checkCTPN_SachSLTonDAL(key);
        }
    }
}
