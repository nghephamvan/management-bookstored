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
                            item.Id,
                            item.SL_NhapItNhat,
                            item.SL_TonToiDaTruocNhap,
                            item.SL_TonSauToiThieu,
                            item.SoTienNoToiDa,
                            item.SuDungQuyDinh
                        };
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Nhập Ít Nhất", typeof(string));
            dt.Columns.Add("SL_Tồn Tối Đa Trước Nhập", typeof(int));
            dt.Columns.Add("SL_Tồn Sau Tối Thiểu", typeof(int));
            dt.Columns.Add("Số Tiền Nợ Tối Đa", typeof(int));
            dt.Columns.Add("Sử Dụng", typeof(bool));
            int stt = 1;
            foreach (var item in query)
            {
                dt.Rows.Add(stt, item.Id, item.SL_NhapItNhat, item.SL_TonToiDaTruocNhap, item.SL_TonSauToiThieu, item.SoTienNoToiDa, item.SuDungQuyDinh);
                stt++;
            }

            return dt;
        }

        public static THAMSO SelectThamSoDAL(string key)
        {
            THAMSO query = db.THAMSOs.Single(i => i.Id.Equals(key));
            return query;
        }

        public static void UpdateThamSoDAL(THAMSO item)
        {
            var query = db.THAMSOs.Single(i => i.Id == item.Id);
            query.SL_NhapItNhat = item.SL_NhapItNhat;
            query.SL_TonToiDaTruocNhap = item.SL_TonToiDaTruocNhap;
            query.SL_TonSauToiThieu = item.SL_TonSauToiThieu;
            query.SoTienNoToiDa = item.SoTienNoToiDa;
            query.SuDungQuyDinh = item.SuDungQuyDinh;

            db.SubmitChanges();
        }

        public static THAMSO SelectThamSoDAL()
        {
            THAMSO query = db.THAMSOs.First();
            return query;
        }

    }
}
