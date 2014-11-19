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
            return CTPNController.SellectAllCTPNDAL();
        }

        public static void DeleteCTPNBUL(List<string> keys)
        {
            CTPNController.DeleteCTPNDAL(keys);
        }

        public static CTPN SelectCTPNBUL(string key)
        {
            return CTPNController.SelectCTPNDAL(key);
        }

        public static void InsertCTPNBUL(CTPN item)
        {
            CTPNController.InsertCTPNDAL(item);
        }

        public static bool checkMaCTPNBUL(string key)
        {
            return CTPNController.checkMaCTPNDAL(key);
        }

        public static void UpdateCTPNBUL(CTPN item)
        {
            CTPNController.UpdateCTPNDAL(item);
        }

        public static DataTable SearchCTPNBUL(string key)
        {
            return CTPNController.SearchCTPNDAL(key);
        }

        public static bool checkSachSLTonBUL(string key)
        {
            return CTPNController.checkCTPN_SachSLTonDAL(key);
        }

        public static bool checkSL_NhapItNhat(int? key)
        {
            return CTPNController.checkSL_NhapItNhat(key);
        }
    }
}