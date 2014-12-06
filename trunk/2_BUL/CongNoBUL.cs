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

        public static CONGNO SelectCongNoBUL(string key)
        {
            return CongNoController.SelectCongNoDAL(key);
        }

        public static DataTable SelectCongNo_MonthBUL(string key)
        {
            return CongNoController.SelectCongNo_MonthDAL(key);
        }
    }
}
