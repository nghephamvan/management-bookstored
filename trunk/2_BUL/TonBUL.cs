using _3_DAL;
using _4_DTO;
using System.Collections.Generic;
using System.Data;

namespace _2_BUL
{
    public class TonBUL
    {
        public static DataTable SellectAllTonsBUL()
        {
            return TonController.SellectAllTonsDAL();
        }

        public static PHIEUTON SelectTonBUL(string key)
        {
            return TonController.SelectTonDAL(key);
        }


        public static DataTable SelectTon_MonthBUL(string p)
        {
            return TonController.SelectTon_MonthBUL(p);
        }
    }
}
