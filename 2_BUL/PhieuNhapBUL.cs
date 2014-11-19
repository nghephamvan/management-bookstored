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
    public class PhieuNhapBUL
    {
        public static DataTable SelectAllPhieuNhapsBUL()
        {
            return PhieuNhapController.SellectAllPhieuNhaps_DAL();
        }

        public static void DeletePNsBUL(List<string> PNs)
        {
            PhieuNhapController.DeletePNsDAL(PNs);
        }

        public static NHAP SelectPhieuNhapBUL(string MaPN)
        {
            return PhieuNhapController.SelectPhieuNhapDAL(MaPN);
        }

        public static void InsertPNBUL(NHAP temp)
        {
            PhieuNhapController.InsertPNDAL(temp);
        }

        public static bool checkMaPNBUL(string mapn)
        {
            return PhieuNhapController.checkMaPNDAL(mapn);
        }

        public static void UpdatePNBUL(NHAP temp)
        {
            PhieuNhapController.UpdatePNDAL(temp);
        }

        public static DataTable SearchPNsBUL(string temp)
        {
            return PhieuNhapController.SearchPNsDAL(temp);
        }
    }
}
