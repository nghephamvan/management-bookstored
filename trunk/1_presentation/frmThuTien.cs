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
using _2_BUL;
using _4_DTO;

namespace _1_Presentation
{
    public partial class frmThuTien : Form
    {
        bool _chkAdd = false;

        public frmThuTien()
        {
            InitializeComponent();
        }

        private void frmThuTien_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            //
            cmbKH.DataSource = KhachHangBUL.SellectAllCustomerBUL();
            cmbKH.DisplayMember = "Tên KH";
            cmbKH.ValueMember = "Mã KH";

            DGV_Result.DataSource = ThuTienBUL.SellectAllThuTiensBUL();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals("Thêm"))
            {
                btnAdd.Text = "Hủy";
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                txtKey.Text = String.Empty;
                cmbKH.SelectedIndex = -1;
                dtpNgayThu.Value = DateTime.Now.Date;
                txtTienThu.Text = String.Empty;

                txtKey.Enabled = true;
                cmbKH.Enabled = true;
                dtpNgayThu.Enabled = true;
                txtTienThu.Enabled = true;



                _chkAdd = true;

            }
            else
            {
                btnAdd.Text = "Thêm";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;


                txtKey.Text = String.Empty;
                cmbKH.SelectedIndex = -1;
                dtpNgayThu.Value = DateTime.Now.Date;
                txtTienThu.Text = String.Empty;

                txtKey.Enabled = false;
                cmbKH.Enabled = false;
                dtpNgayThu.Enabled = false;
                txtTienThu.Enabled = false;

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


                txtKey.Enabled = false;
                cmbKH.Enabled = true;
                dtpNgayThu.Enabled = true;
                txtTienThu.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtKey.Enabled = false;
                cmbKH.Enabled = false;
                dtpNgayThu.Enabled = false;
                txtTienThu.Enabled = false;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int temp = 0;

            List<string> keys = new List<string>();
            //Duyet cac dong trong DataGridView
            foreach (DataGridViewRow row in DGV_Result.Rows)
            {
                //O dau tien la checkbox se chuyen trang thai true hoac false
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true)
                {
                    keys.Add(row.Cells[2].Value.ToString());
                    temp++;
                }
            }

            if (temp == 0)
            {
                MessageBox.Show("Hãy chọn dữ liệu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa thu tiền?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    //Delete list book
                    //Foreign Key
                    //Delete TON, CONGNO, HOADON and CTHD
                    ThuTienBUL.DeleteThuTiensBUL(keys);

                    MessageBox.Show("Bạn đã xóa thu tiền thành công!", "Thông báo");
                }


            }

            //Reload
            Reload();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_chkAdd)
            {
                if (txtKey.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn hãy nhập mã thu tiền muốn thêm!", "Thông báo");
                }
                else
                {
                    if (ThuTienBUL.checkMaThuTienBUL(txtKey.Text.Trim()))
                    {
                        PHIEUTHUTIEN item = new PHIEUTHUTIEN();
                        item.MaThuTien = txtKey.Text;
                        item.MaKH = cmbKH.SelectedValue.ToString();
                        item.NgayThu = dtpNgayThu.Value.Date;
                        item.SoTienThu = Convert.ToDecimal(txtTienThu.Text);

                        //insert into database
                        //Chỉ bán cho khách có số nợ dưới 20000
                        //SL_Ton sau >= 20
                        if (!ThuTienBUL.checkKH_ThuTienBUL(item.MaKH))
                        {
                            MessageBox.Show("Số tiền thu phải nhỏ hơn số tiền khách nợ", "Thông Báo");
                        }
                        else
                        {
                            ThuTienBUL.InsertThuTienBUL(item);
                            MessageBox.Show("Bạn đã thêm thu tiền [" + txtKey.Text + "] thành công", "Thông báo");

                            txtKey.Text = String.Empty;
                            cmbKH.SelectedIndex = -1;
                            dtpNgayThu.Value = DateTime.Now.Date;
                            txtTienThu.Text = String.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã thu tiền đã tồn tại, bạn hãy nhâp một mã thu tiền khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                if (txtKey.Text.Trim() != string.Empty)
                {
                     DialogResult dialog = MessageBox.Show("Bạn có muốn sửa thu tiền?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                     if (dialog == DialogResult.OK)
                     {
                         PHIEUTHUTIEN item = new PHIEUTHUTIEN();
                         item.MaThuTien = txtKey.Text;
                         item.MaKH = cmbKH.SelectedValue.ToString();
                         item.NgayThu = dtpNgayThu.Value.Date;
                         item.SoTienThu = Convert.ToDecimal(txtTienThu.Text);

                         //insert into database
                         //Chỉ bán cho khách có số nợ dưới 20000
                         //SL_Ton sau >= 20
                         if (!ThuTienBUL.checkKH_ThuTienBUL(item.MaKH))
                         {
                             MessageBox.Show("Số tiền thu phải nhỏ hơn số tiền khách nợ", "Thông Báo");
                         }
                         else
                         {
                             ThuTienBUL.UpdateThuTienBUL(item);
                             MessageBox.Show("Bạn đã sửa [" + txtKey.Text + "] thành công", "Thông báo");

                         }
                     }

                }
                else
                {
                    MessageBox.Show("Không tìm được mã thu tiền để cập nhật!", "Thông báo");
                }

            }

            Reload();
            _chkAdd = false;

            btnAdd.Text = "Thêm";
            btnUpdate.Text = "Sửa";
            btnSave.Enabled = false;
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;

            txtKey.Enabled = false;
            cmbKH.Enabled = false;
            dtpNgayThu.Enabled = false;
            txtTienThu.Enabled = false;
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
                DGV_Result.DataSource = ThuTienBUL.SearchThuTiensBUL(txtSearch.Text.Trim());
                txtSearch.BackColor = Color.LightGreen;
            }
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_chkAdd)
            {
                //Lay ma sach tren DataGridView
                string key = DGV_Result.Rows[e.RowIndex].Cells[2].Value.ToString();
                //Select Sach bằng mã sách

                PHIEUTHUTIEN item = ThuTienBUL.SelectThuTienBUL(key);

                txtKey.Text = item.MaThuTien;
                cmbKH.SelectedValue = item.MaKH;
                dtpNgayThu.Value = Convert.ToDateTime(item.NgayThu);
                txtTienThu.Text = item.SoTienThu.ToString();
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
                exl.WriteDataTableToExcel(ThuTienBUL.SellectAllThuTiensBUL(), "Customers", path, "Details");

                MessageBox.Show("Excel created !");
            }
        }

        private void txtTienThu_TextChanged(object sender, EventArgs e)
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
