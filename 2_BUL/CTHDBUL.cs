using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;

namespace _2_BUL
{
    public class CTHDBUL
    {
        public static void DeleteCTHDsBUL(List<string> cthds)
        {
            CTHDDAL.DeleteCTHDsDAL(cthds);
        }
    }
}
