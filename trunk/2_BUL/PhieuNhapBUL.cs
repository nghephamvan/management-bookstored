using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;

namespace _2_BUL
{
    public class PhieuNhapBUL
    {
        public static DataTable SelectAllPhieuNhapsBUL()
        {
            return PhieuNhapDAL.SellectAllPhieuNhaps_DAL();
        }

        public static void DeletePNsBUL(List<string> PNs)
        {
            PhieuNhapDAL.DeletePNsDAL(PNs);
        }

        public static NHAP SelectPhieuNhapBUL(string MaPN)
        {
            return PhieuNhapDAL.SelectPhieuNhapDAL(MaPN);
        }

        public static void InsertPNBUL(NHAP temp)
        {
            PhieuNhapDAL.InsertPNDAL(temp);
        }

        public static bool checkMaPNBUL(string mapn)
        {
            return PhieuNhapDAL.checkMaPNDAL(mapn);
        }

        public static void UpdatePNBUL(NHAP temp)
        {
            PhieuNhapDAL.UpdatePNDAL(temp);
        }

        public static DataTable SearchPNsBUL(string temp)
        {
            return PhieuNhapDAL.SearchPNsDAL(temp);
        }
    }
}
