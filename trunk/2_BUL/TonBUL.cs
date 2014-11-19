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

        public static void DeleteTonsBUL(List<string> keys)
        {
            TonController.DeleteTonsDAL(keys);
        }

        public static TON SelectTonBUL(string key)
        {
            return TonController.SelectTonDAL(key);
        }

        public static void InsertTonBUL(TON item)
        {
            TonController.InsertTonDAL(item);
        }

        public static bool checkMaTonBUL(string key)
        {
            return TonController.checkMaTonDAL(key);
        }

        public static void UpdateTonBUL(TON item)
        {
            TonController.UpdateTonDAL(item);
        }

        public static DataTable SearchTonsBUL(string key)
        {
            return TonController.SearchTonsDAL(key);
        }
    }
}
