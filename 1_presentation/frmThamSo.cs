using _2_BUL;
using _4_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_Presentation
{
    public partial class frmThamSo : Form
    {
        public frmThamSo()
        {
            InitializeComponent();
        }

        private void frmThamSo_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            DGV_Result.DataSource = ThamSoBUL.SellectAllThamSosBUL();
        }

        private void txtNhatItNhat_TextChanged(object sender, EventArgs e)
        {
            Exception X = new Exception();

            TextBox T = (TextBox)sender;

            T.Text = T.Text.Trim();

            try
            {

                int x = int.Parse(T.Text);

                //Customizing Condition (Only numbers larger than zero are permitted)
                if (x <= 0)
                    throw X;
            }
            catch (Exception)
            {
                try
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
                catch (Exception) { }

            }
        }

        private void txtTonToiDa_TextChanged(object sender, EventArgs e)
        {
            Exception X = new Exception();

            TextBox T = (TextBox)sender;

            T.Text = T.Text.Trim();

            try
            {

                int x = int.Parse(T.Text);

                //Customizing Condition (Only numbers larger than zero are permitted)
                if (x <= 0)
                    throw X;
            }
            catch (Exception)
            {
                try
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
                catch (Exception) { }

            }
        }

        private void txtTonSauToiThieu_TextChanged(object sender, EventArgs e)
        {
            Exception X = new Exception();

            TextBox T = (TextBox)sender;

            T.Text = T.Text.Trim();

            try
            {

                int x = int.Parse(T.Text);

                //Customizing Condition (Only numbers larger than zero are permitted)
                if (x <= 0)
                    throw X;
            }
            catch (Exception)
            {
                try
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
                catch (Exception) { }

            }
        }

        private void NoToiDa_TextChanged(object sender, EventArgs e)
        {
            Exception X = new Exception();

            TextBox T = (TextBox)sender;

            T.Text = T.Text.Trim();

            try
            {

                decimal x = decimal.Parse(T.Text);

                //Customizing Condition (Only numbers larger than or equal to zero are permitted)
                if (x < 0 || T.Text.Contains(','))
                    throw X;

            }
            catch (Exception)
            {
                try
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
                catch (Exception) { }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Text.Equals("Sửa"))
            {
                btnUpdate.Text = "Hủy";
                btnSave.Enabled = true;


                txtNhapItNhat.Enabled = true;
                txtTonToiDa.Enabled = true;
                txtTonSauToiThieu.Enabled = true;
                txtNoToiDa.Enabled = true;
                chkBSuDung.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;

                txtNhapItNhat.Enabled = false;
                txtTonToiDa.Enabled = false;
                txtTonSauToiThieu.Enabled = false;
                txtNoToiDa.Enabled = false;
                chkBSuDung.Enabled = false;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtKey.Text.Trim() != string.Empty)
            {
                THAMSO item = new THAMSO();
                item.Id = Convert.ToInt16(txtKey.Text);
                item.SL_NhapItNhat = Convert.ToInt16(txtNhapItNhat.Text);
                item.SL_TonToiDaTruocNhap = Convert.ToInt16(txtTonToiDa.Text);
                item.SL_TonSauToiThieu = Convert.ToInt16(txtTonSauToiThieu.Text);
                item.SoTienNoToiDa = Convert.ToDecimal(txtNoToiDa.Text);
                item.SuDungQuyDinh = chkBSuDung.Checked;

                //update into database

                ThamSoBUL.UpdateThamSoBUL(item);
                MessageBox.Show("Bạn đã sửa [" + txtKey.Text + "] thành công", "Thông báo");

            }
            else
            {
                MessageBox.Show("Không tìm được mã quy định để cập nhật!", "Thông báo");
            }

            Reload();
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Lay ma sach tren DataGridView
            string key = DGV_Result.Rows[e.RowIndex].Cells[1].Value.ToString();
            //Select Sach bằng mã sách


            THAMSO item = ThamSoBUL.SelectThamSoBUL(key);

            txtKey.Text = item.Id.ToString();
            txtNhapItNhat.Text = item.SL_NhapItNhat.ToString();
            txtTonToiDa.Text = item.SL_TonToiDaTruocNhap.ToString();
            txtTonSauToiThieu.Text = item.SL_TonSauToiThieu.ToString();
            txtNoToiDa.Text = item.SoTienNoToiDa.ToString();
            chkBSuDung.Checked = (bool)item.SuDungQuyDinh;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
