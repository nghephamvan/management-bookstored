using _2_BUL;
using _4_DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _1_Presentation
{
    public partial class frmTon : Form
    {
        bool _chkAdd = false;

        public frmTon()
        {
            InitializeComponent();
        }

        private void frmTon_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            cmbSach.DataSource = SachBUL.SelectAllBooksBUL();
            cmbSach.DisplayMember = "Tên Sách";
            cmbSach.ValueMember = "Mã Sách";

            DGV_Result.DataSource = TonBUL.SellectAllTonsBUL();
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
                cmbSach.SelectedIndex = -1;
                cmbThangTon.SelectedIndex = -1;
                txtTonDau.Text = String.Empty;
                txtTonPhatSinh.Text = String.Empty;
                txtTonCuoi.Text = String.Empty;

                txtKey.Enabled = true;
                cmbSach.Enabled = true;
                cmbThangTon.Enabled = true;
                txtTonDau.Enabled = true;
                txtTonPhatSinh.Enabled = true;
                txtTonCuoi.Enabled = true;

                _chkAdd = true;
            }
            else
            {
                btnAdd.Text = "Thêm";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;


                txtKey.Text = String.Empty;
                cmbSach.SelectedIndex = -1;
                cmbThangTon.SelectedIndex = -1;
                txtTonDau.Text = String.Empty;
                txtTonPhatSinh.Text = String.Empty;
                txtTonCuoi.Text = String.Empty;

                txtKey.Enabled = false;
                cmbSach.Enabled = false;
                cmbThangTon.Enabled = false;
                txtTonDau.Enabled = false;
                txtTonPhatSinh.Enabled = false;
                txtTonCuoi.Enabled = false;

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
                cmbSach.Enabled = true;
                cmbThangTon.Enabled = true;
                txtTonDau.Enabled = true;
                txtTonPhatSinh.Enabled = true;
                txtTonCuoi.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtKey.Enabled = false;
                cmbSach.Enabled = false;
                cmbThangTon.Enabled = false;
                txtTonDau.Enabled = false;
                txtTonPhatSinh.Enabled = false;
                txtTonCuoi.Enabled = false;

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
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa tồn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    //Delete list book
                    //Foreign Key
                    //Delete TON, CONGNO, HOADON and CTHD

                    TonBUL.DeleteTonsBUL(keys);

                    MessageBox.Show("Bạn đã xóa tồn thành công!", "Thông báo");
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
                    MessageBox.Show("Bạn hãy nhập mã tồn muốn thêm!", "Thông báo");
                }
                else
                {
                    if (TonBUL.checkMaTonBUL(txtKey.Text.Trim()))
                    {
                        PHIEUTON item = new PHIEUTON();
                        item.MaTon = txtKey.Text;
                        item.MaSach = cmbSach.SelectedValue.ToString();
                        item.Thang = Convert.ToInt16(cmbThangTon.Text);
                        item.TonDau = Convert.ToInt16(txtTonDau.Text);
                        item.TonPhatSinh = Convert.ToInt16(txtTonPhatSinh.Text);
                        item.TonCuoi = Convert.ToInt16(txtTonCuoi.Text);

                        //insert into database
                        TonBUL.InsertTonBUL(item);
                        MessageBox.Show("Bạn đã thêm tồn [" + txtKey.Text + "] thành công", "Thông báo");

                        txtKey.Text = String.Empty;
                        cmbSach.SelectedIndex = -1;
                        cmbThangTon.SelectedIndex = -1;
                        txtTonDau.Text = String.Empty;
                        txtTonPhatSinh.Text = String.Empty;
                        txtTonCuoi.Text = String.Empty;

                    }
                    else
                    {
                        MessageBox.Show("Mã tồn đã tồn tại, bạn hãy nhâp một mã tồn khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                if (txtKey.Text.Trim() != string.Empty)
                {
                    PHIEUTON item = new PHIEUTON();
                    item.MaTon = txtKey.Text;
                    item.MaSach = cmbSach.SelectedValue.ToString();
                    item.Thang = Convert.ToInt16(cmbThangTon.Text);
                    item.TonDau = Convert.ToInt16(txtTonDau.Text);
                    item.TonPhatSinh = Convert.ToInt16(txtTonPhatSinh.Text);
                    item.TonCuoi = Convert.ToInt16(txtTonCuoi.Text);

                    //update into database

                    TonBUL.UpdateTonBUL(item);
                    MessageBox.Show("Bạn đã sửa [" + txtKey.Text + "] thành công", "Thông báo");

                }
                else
                {
                    MessageBox.Show("Không tìm được mã tồn để cập nhật!", "Thông báo");
                }

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
                DGV_Result.DataSource = TonBUL.SearchTonsBUL(txtSearch.Text.Trim());
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


                PHIEUTON item = TonBUL.SelectTonBUL(key);

                txtKey.Text = item.MaTon;
                cmbSach.SelectedValue = item.MaSach;
                cmbThangTon.Text = item.Thang.ToString();
                txtTonDau.Text = item.TonDau.ToString();
                txtTonPhatSinh.Text = item.TonPhatSinh.ToString();
                txtTonCuoi.Text = item.TonCuoi.ToString();
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
                exl.WriteDataTableToExcel(TonBUL.SellectAllTonsBUL(), "Customers", path, "Details");

                MessageBox.Show("Excel đã được tạo !");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTonDau_TextChanged(object sender, EventArgs e)
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

        private void txtTonPhatSinh_TextChanged(object sender, EventArgs e)
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

        private void txtTonCuoi_TextChanged(object sender, EventArgs e)
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

        private void cmbBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkBBaoCao.Checked)
            {
                DGV_Result.DataSource = TonBUL.SelectTon_MonthBUL(cmbBaoCao.Text);
            }
        }

        private void chkBBaoCao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBBaoCao.Checked)
            {
                cmbBaoCao.Enabled = true;
            }
            else
            {
                cmbBaoCao.Enabled = false;

                Reload();
            }
        }
    }
}
