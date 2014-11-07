using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_DAL
{
    public class CTHDDAL
    {
        static QLNSModel db = new QLNSModel();
        public static void DeleteCTHDsDAL(List<string> cthds)
        {
            //Take books in Saches which have Masach equal listbooks

            var lstcthds = from b in db.CTHDs
                           where cthds.Contains(b.MASACH)
                           select b;

            //Delete
            db.CTHDs.DeleteAllOnSubmit(lstcthds);

            //Confirm database
            db.SubmitChanges();

        }

        public static bool checkSachSLTonDAL(string key)
        {
            var query = from item in db.SACHes
                       where item.MASACH == key
                       select new
                       {
                           item.SL_TON
                       };

            foreach (var item in query)
            {
                if (item.SL_TON > 300)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
