using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using _2_BUL;
using _4_DTO;

namespace _1_Presentation
{
    public partial class frmSach : Form
    {
        bool _chkAdd = false;

        public frmSach()
        {
            InitializeComponent();
        }

        private void frmSach_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            //Lay du lieu do ra DGV_Sach
            DGV_Result.DataSource = SachBUL.SelectAllBooksBUL();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals("Thêm"))
            {
                btnAdd.Text = "Hủy";
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                txtMaSach.Text = String.Empty;
                txtTenSach.Text = String.Empty;
                txtTheLoai.Text = String.Empty;
                txtTacGia.Text = String.Empty;
                txtSL_Ton.Text = String.Empty;
                txtDonGia.Text = String.Empty;

                txtMaSach.Enabled = true;
                txtTenSach.Enabled = true;
                txtTheLoai.Enabled = true;
                txtTacGia.Enabled = true;
                txtSL_Ton.Enabled = true;
                txtDonGia.Enabled = true;
                

                _chkAdd = true;

            }
            else
            {
                btnAdd.Text = "Thêm";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;

                txtMaSach.Text = String.Empty;
                txtTenSach.Text = String.Empty;
                txtTheLoai.Text = String.Empty;
                txtTacGia.Text = String.Empty;
                txtSL_Ton.Text = String.Empty;
                txtDonGia.Text = String.Empty;

                txtMaSach.Enabled = false;
                txtTenSach.Enabled = false;
                txtTheLoai.Enabled = false;
                txtTacGia.Enabled = false;
                txtSL_Ton.Enabled = false;
                txtDonGia.Enabled = false;

                _chkAdd = false;

            }


        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //Duyet cac dong trong DataGridView
            foreach (DataGridViewRow row in DGV_Result.Rows)
            {
                //O dau tien la checkbox se chuyen trang thai true hoac false
                ((DataGridViewCheckBoxCell)row.Cells[0]).Value = chkAll.Checked;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _chkAdd = false;
            if (btnUpdate.Text.Equals("Sửa"))
            {
                btnUpdate.Text = "Hủy";
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnAdd.Enabled = false;


                txtMaSach.Enabled = false;
                txtTenSach.Enabled = true;
                txtTheLoai.Enabled = true;
                txtTacGia.Enabled = true;
                txtSL_Ton.Enabled = true;
                txtDonGia.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtMaSach.Enabled = false;
                txtTenSach.Enabled = false;
                txtTheLoai.Enabled = false;
                txtTacGia.Enabled = false;
                txtSL_Ton.Enabled = false;
                txtDonGia.Enabled = false;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int temp = 0;

            List<string> books = new List<string>();
            //Duyet cac dong trong DataGridView
            foreach (DataGridViewRow row in DGV_Result.Rows)
            {
                //O dau tien la checkbox se chuyen trang thai true hoac false
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true)
                {
                    books.Add(row.Cells[2].Value.ToString());
                    temp++;
                }
            }



            if (temp == 0)
            {
                MessageBox.Show("Hãy chọn dữ liệu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa thông tin sách?\nKhi xóa sách sẽ xóa các chi tiết hóa đơn và chi tiết phiếu nhập", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    //Delete list book
                    //Foreign Key
                    SachBUL.DeleteBooksBUL(books);

                    MessageBox.Show("Bạn đã xóa sách thành công!", "Thông báo");
                }


            }

            //Reload
            Reload();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_chkAdd)
            {
                if (txtMaSach.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn hãy nhập mã của sách muốn thêm!", "Thông báo");
                }
                else
                {
                    if (SachBUL.checkMaSachBUL(txtMaSach.Text.Trim()))
                    {
                        SACH sach = new SACH();
                        sach.MaSach = txtMaSach.Text;
                        sach.TenSach = txtTenSach.Text;
                        sach.TheLoai = txtTheLoai.Text;
                        sach.TacGia = txtTacGia.Text;
                        sach.SL_Ton = Convert.ToInt16(txtSL_Ton.Text);
                        sach.DonGia = Convert.ToDecimal(txtDonGia.Text);

                        SachBUL.InsertBookBUL(sach);

                        MessageBox.Show("Bạn đã thêm sách với mã [" + sach.MaSach + "] thành công", "Thông báo");

                        txtMaSach.Text = String.Empty;
                        txtTenSach.Text = String.Empty;
                        txtTheLoai.Text = String.Empty;
                        txtTacGia.Text = String.Empty;
                        txtSL_Ton.Text = String.Empty;
                        txtDonGia.Text = String.Empty;

                    }
                    else
                    {
                        MessageBox.Show("Mã sách đã  tồn tại, bạn hãy nhâp một mã sách khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else 
            {
                if (txtMaSach.Text.Trim() != string.Empty)
                {
                     DialogResult dialog = MessageBox.Show("Bạn có muốn sửa thông tin sách?\nKhi xóa sách sẽ xóa các chi tiết hóa đơn và chi tiết phiếu nhập", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                     if (dialog == DialogResult.OK)
                     {
                         SACH sach = new SACH();
                         sach.MaSach = txtMaSach.Text;
                         sach.TenSach = txtTenSach.Text;
                         sach.TheLoai = txtTheLoai.Text;
                         sach.TacGia = txtTacGia.Text;
                         sach.SL_Ton = Convert.ToInt16(txtSL_Ton.Text);
                         sach.DonGia = Convert.ToDecimal(txtDonGia.Text);

                         //Sửa thông tin của sách
                         SachBUL.UpdateSachBUL(sach);

                         MessageBox.Show("Bạn đã cập nhật thông tin của sách [" + txtMaSach.Text + "] thành công!", "Thông báo");
                     }

                }
                else
                {
                    MessageBox.Show("Không tìm được mã sách để cập nhật!","Thông báo");
                }

            }

            Reload();
            _chkAdd = false;

            btnAdd.Text = "Thêm";
            btnUpdate.Text = "Sửa";
            btnSave.Enabled = false;
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;

            txtMaSach.Enabled = false;
            txtTenSach.Enabled = false;
            txtTheLoai.Enabled = false;
            txtTacGia.Enabled = false;
            txtSL_Ton.Enabled = false;
            txtDonGia.Enabled = false;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.OverwritePrompt = true;
            savefile.DefaultExt = "*.xlsx";
            savefile.Filter = "Execl Workbook(*.xlsx)|*.xlsx";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(savefile.FileName);
                Excelutlity exl = new Excelutlity();
                exl.WriteDataTableToExcel(SachBUL.SelectAllBooksBUL(), "Books", path, "Details");

                MessageBox.Show("Excel created !");
            }
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_chkAdd)
            {
                //Lay ma sach tren DataGridView
                string MaSach = DGV_Result.Rows[e.RowIndex].Cells[2].Value.ToString();
                //Select Sach bằng mã sách
                SACH sc = SachBUL.SelectBookBUL(MaSach);

                txtMaSach.Text = sc.MaSach;
                txtTenSach.Text = sc.TenSach;
                txtTheLoai.Text = sc.TheLoai;
                txtTacGia.Text = sc.TacGia;
                txtSL_Ton.Text = sc.SL_Ton.ToString();
                txtDonGia.Text = sc.DonGia.ToString();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Equals(string.Empty))
            {
                txtSearch.BackColor = Color.White;
                Reload();
            }
            else
            {
                //Tìm sách và đổ vào DataGridView
                DGV_Result.DataSource = SachBUL.SearchBooksBUL(txtSearch.Text);
                txtSearch.BackColor = Color.LightGreen;
            }
        }

        //Kiểm soát dữ liệu nhập vào của SL_Ton là kiểu int
        private void txtSL_Ton_TextChanged(object sender, EventArgs e)
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

        //Kiểm soát dữ liệu nhập vào là kiểu decimal
        private void txtDonGia_TextChanged(object sender, EventArgs e)
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
