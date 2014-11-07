using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_DAL;

namespace _2_BUL
{
    public class KhachHangBUL
    {
        public static DataTable SellectAllCustomerBUL()
        {
            return KhachHangDAL.SellectAllCustomerDAL();
        }

        public static void DeleteCustomersBUL(List<string> customers)
        {
            KhachHangDAL.DeleteCustomersDAL(customers);
        }

        public static KHACHHANG SelectCustomerBUL(string MaKH)
        {
            return KhachHangDAL.SelectCustomerDAL(MaKH);
        }

        public static void InsertCustomerBUL(KHACHHANG temp)
        {
            KhachHangDAL.InsertCustomerDAL(temp);
        }

        public static bool checkMaKHBUL(string makh)
        {
            return KhachHangDAL.checkMaKHDAL(makh);
        }

        public static void UpdateCustomerBUL(KHACHHANG temp)
        {
            KhachHangDAL.UpdateCustomerDAL(temp);
        }

        public static DataTable SearchCustomersBUL(string temp)
        {
            return KhachHangDAL.SearchCustomersDAL(temp);
        }
    }
}
