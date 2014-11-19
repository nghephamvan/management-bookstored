using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;
using System.Data;
using _4_DTO;

namespace _2_BUL
{
    public class ThamSoBUL
    {
        public static DataTable SellectAllThamSosBUL()
        {
            return ThamSoController.SellectAllThamSosDAL();
        }

        public static THAMSO SelectThamSoBUL(string key)
        {
            return ThamSoController.SelectThamSoDAL(key);
        }

        public static THAMSO SelectThamSoBUL()
        {
            return ThamSoController.SelectThamSoDAL();
        }

        public static void UpdateThamSoBUL(THAMSO item)
        {
            ThamSoController.UpdateThamSoDAL(item);
        }
    }
}
