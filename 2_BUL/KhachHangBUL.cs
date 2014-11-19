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
    public class KhachHangBUL
    {
        public static DataTable SellectAllCustomerBUL()
        {
            return KhachHangController.SellectAllCustomerDAL();
        }

        public static void DeleteCustomersBUL(List<string> customers)
        {
            KhachHangController.DeleteCustomersDAL(customers);
        }

        public static KHACHHANG SelectCustomerBUL(string MaKH)
        {
            return KhachHangController.SelectCustomerDAL(MaKH);
        }

        public static void InsertCustomerBUL(KHACHHANG temp)
        {
            KhachHangController.InsertCustomerDAL(temp);
        }

        public static bool checkMaKHBUL(string makh)
        {
            return KhachHangController.checkMaKHDAL(makh);
        }

        public static void UpdateCustomerBUL(KHACHHANG temp)
        {
            KhachHangController.UpdateCustomerDAL(temp);
        }

        public static DataTable SearchCustomersBUL(string temp)
        {
            return KhachHangController.SearchCustomersDAL(temp);
        }
    }
}
