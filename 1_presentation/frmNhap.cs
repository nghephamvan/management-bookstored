using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_Presentation
{
    public partial class frmNhap : Form
    {
        public frmNhap()
        {
            InitializeComponent();
        }
        bool _chkAdd = false;

        private void frmNhap_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            DGV_Result.DataSource = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals("Thêm"))
            {
                btnAdd.Text = "Hủy";
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                txtMaPN.Text = String.Empty;

                txtMaPN.Enabled = true;
                dtpicker.Enabled = true;


                _chkAdd = true;

            }
            else
            {
                btnAdd.Text = "Thêm";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;

                txtMaPN.Text = String.Empty;

                txtMaPN.Enabled = false;
                dtpicker.Enabled = false;

                _chkAdd = false;

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


                txtMaPN.Enabled = false;
                dtpicker.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtMaPN.Enabled = false;
                dtpicker.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int temp = 0;

            List<string> phieunhaps = new List<string>();
            //Duyet cac dong trong DataGridView
            foreach (DataGridViewRow row in DGV_Result.Rows)
            {
                //O dau tien la checkbox se chuyen trang thai true hoac false
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true)
                {
                    phieunhaps.Add(row.Cells[1].Value.ToString());
                    temp++;
                }
            }

            if (temp == 0)
            {
                MessageBox.Show("Hãy chọn dữ liệu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa thông tin phiếu nhập?\nKhi xóa phiếu nhập sẽ xóa các chi tiết phiếu nhập", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    //Delete list book
                    //Foreign Key
                    //CTHDBUL.DeleteCTHDsBUL(books);
                    //CTPNBUL.DeleteCTPNsBUL(books);
                    //SachBUL.DeleteBooksBUL(books);

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
                if (txtMaPN.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn hãy nhập mã của sách muốn thêm!", "Thông báo");
                }
                else
                {
                    //if (SachBUL.checkMaSachDAL(txtMaSach.Text.Trim()))
                    //{
                    //    SACH sach = new SACH();
                    //    sach.MASACH = txtMaSach.Text;
                    //    sach.TENSACH = txtTenSach.Text;
                    //    sach.THELOAI = txtTheLoai.Text;
                    //    sach.TACGIA = txtTacGia.Text;
                    //    sach.SL_TON = Convert.ToInt16(txtSL_Ton.Text);
                    //    sach.DONGIA = Convert.ToDecimal(txtDonGia.Text);

                    //    SachBUL.InsertBookBUL(sach);

                    //    MessageBox.Show("Bạn đã thêm sách với mã [" + sach.MASACH + "] thành công", "Thông báo");

                    //    txtMaSach.Text = String.Empty;
                    //    txtTenSach.Text = String.Empty;
                    //    txtTheLoai.Text = String.Empty;
                    //    txtTacGia.Text = String.Empty;
                    //    txtSL_Ton.Text = String.Empty;
                    //    txtDonGia.Text = String.Empty;

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Mã sách đã  tồn tại, bạn hãy nhâp một mã sách khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
            }
            else
            {
                //if (txtMaSach.Text.Trim() != string.Empty)
                //{
                //    SACH sach = new SACH();
                //    sach.MASACH = txtMaSach.Text;
                //    sach.TENSACH = txtTenSach.Text;
                //    sach.THELOAI = txtTheLoai.Text;
                //    sach.TACGIA = txtTacGia.Text;
                //    sach.SL_TON = Convert.ToInt16(txtSL_Ton.Text);
                //    sach.DONGIA = Convert.ToDecimal(txtDonGia.Text);

                //    //Sửa thông tin của sách
                //    SachBUL.UpdateSachBUL(sach);

                //    MessageBox.Show("Bạn đã cập nhật thông tin của sách [" + txtMaSach.Text + "] thành công!", "Thông báo");

                //}
                //else
                //{
                //    MessageBox.Show("Không tìm được mã sách để cập nhật!", "Thông báo");
                //}

            }

            Reload();
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
                //exl.WriteDataTableToExcel(SachBUL.SelectAllBooksBUL(), "Books", path, "Details");

                MessageBox.Show("Excel created !");
            }
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_chkAdd)
            {
                string MaPN = DGV_Result.Rows[e.RowIndex].Cells[1].Value.ToString();

                //SACH sc = SachBUL.SelectBookBUL(MaPN);

                //txtMaSach.Text = sc.MASACH;
                //txtTenSach.Text = sc.TENSACH;
                //txtTheLoai.Text = sc.THELOAI;
                //txtTacGia.Text = sc.TACGIA;
                //txtSL_Ton.Text = sc.SL_TON.ToString();
                //txtDonGia.Text = sc.DONGIA.ToString();
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
                //DGV_Result.DataSource = SachBUL.SearchBooksDAL(txtSearch.Text);
                txtSearch.BackColor = Color.LightGreen;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
