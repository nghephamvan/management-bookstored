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
    public class CongNoBUL
    {
        public static DataTable SellectAllCongNosBUL()
        {
            return CongNoController.SellectAllCongNosDAL();
        }

        public static void DeleteCongNosBUL(List<string> keys)
        {
            CongNoController.DeleteCongNosDAL(keys);
        }

        public static CONGNO SelectCongNoBUL(string key)
        {
            return CongNoController.SelectCongNoDAL(key);
        }

        public static void InsertCongNoBUL(CONGNO item)
        {
            CongNoController.InsertCongNoDAL(item);
        }

        public static bool checkMaCongNoBUL(string key)
        {
            return CongNoController.checkMaCongNoDAL(key);
        }

        public static void UpdateCongNoBUL(CONGNO item)
        {
            CongNoController.UpdateCongNoDAL(item);
        }

        public static DataTable SearchCongNosBUL(string key)
        {
            return CongNoController.SearchCongNosDAL(key);
        }
    }
}
