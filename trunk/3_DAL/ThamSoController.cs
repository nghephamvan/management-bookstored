using _4_DTO;
using System.Data;
using System.Linq;

namespace _3_DAL
{
    public class ThamSoController
    {
        static QLNSModelDataContext db = new QLNSModelDataContext();

        public static DataTable SellectAllThamSosDAL()
        {
            DataTable dt = new DataTable();

            var query = from item in db.THAMSOs
                        select new
                        {
                            item.ID,
                            item.SL_NHAPITNHAT,
                            item.SL_TONTOIDATRUOCNHAP,
                            item.SL_TONSAUTOITHIEU,
                            item.SOTIENNOTOIDA,
                            item.SUDUNGQUYDINH
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("SL_NHAPITNHAT", typeof(string));
            dt.Columns.Add("SL_Tồn Tối Đa Trước Nhập", typeof(int));
            dt.Columns.Add("SL_Tồn Sau Tối Thiểu", typeof(int));
            dt.Columns.Add("Số Tiền Nợ Tối Đa", typeof(int));
            dt.Columns.Add("Sử Dụng", typeof(bool));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.ID, item.SL_NHAPITNHAT, item.SL_TONTOIDATRUOCNHAP, item.SL_TONSAUTOITHIEU, item.SOTIENNOTOIDA, item.SUDUNGQUYDINH);
                stt++;
            }

            return dt;
        }

        public static THAMSO SelectThamSoDAL(string key)
        {
            THAMSO query = db.THAMSOs.Single(i => i.ID.Equals(key));
            return query;
        }

        public static void UpdateThamSoDAL(THAMSO item)
        {
            var query = db.THAMSOs.Single(i => i.ID == item.ID);
            query.SL_NHAPITNHAT = item.SL_NHAPITNHAT;
            query.SL_TONTOIDATRUOCNHAP = item.SL_TONTOIDATRUOCNHAP;
            query.SL_TONSAUTOITHIEU = item.SL_TONSAUTOITHIEU;
            query.SOTIENNOTOIDA = item.SOTIENNOTOIDA;
            query.SUDUNGQUYDINH = item.SUDUNGQUYDINH;

            db.SubmitChanges();
        }

        public static THAMSO SelectThamSoDAL()
        {
            THAMSO query = db.THAMSOs.First();
            return query;
        }

    }
}
