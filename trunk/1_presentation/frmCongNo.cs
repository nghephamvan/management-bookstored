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
    public partial class frmCongNo : Form
    {
        bool _chkAdd = false;

        public frmCongNo()
        {
            InitializeComponent();
        }

        private void frmCongNo_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            try
            {
                cmbKH.DataSource = KhachHangBUL.SellectAllCustomerBUL();
                cmbKH.DisplayMember = "Tên KH";
                cmbKH.ValueMember = "Mã KH";

                DGV_Result.DataSource = CongNoBUL.SellectAllCongNosBUL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text.Equals("Thêm"))
                {
                    btnAdd.Text = "Hủy";
                    btnSave.Enabled = true;
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;

                    txtKey.Text = String.Empty;
                    cmbKH.SelectedIndex = -1;
                    cmbThangNo.SelectedIndex = -1;
                    txtNoDau.Text = String.Empty;
                    txtNoPhatSinh.Text = String.Empty;
                    txtNoCuoi.Text = String.Empty;

                    txtKey.Enabled = true;
                    cmbKH.Enabled = true;
                    cmbThangNo.Enabled = true;
                    txtNoDau.Enabled = true;
                    txtNoPhatSinh.Enabled = true;
                    txtNoCuoi.Enabled = true;

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
                    cmbThangNo.SelectedIndex = -1;
                    txtNoDau.Text = String.Empty;
                    txtNoPhatSinh.Text = String.Empty;
                    txtNoCuoi.Text = String.Empty;

                    txtKey.Enabled = false;
                    cmbKH.Enabled = false;
                    cmbThangNo.Enabled = false;
                    txtNoDau.Enabled = false;
                    txtNoPhatSinh.Enabled = false;
                    txtNoCuoi.Enabled = false;

                    _chkAdd = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
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
                    cmbThangNo.Enabled = true;
                    txtNoDau.Enabled = true;
                    txtNoPhatSinh.Enabled = true;
                    txtNoCuoi.Enabled = true;

                }
                else
                {
                    btnUpdate.Text = "Sửa";
                    btnSave.Enabled = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;

                    txtKey.Enabled = false;
                    cmbKH.Enabled = false;
                    cmbThangNo.Enabled = false;
                    txtNoDau.Enabled = false;
                    txtNoPhatSinh.Enabled = false;
                    txtNoCuoi.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
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
                    DialogResult dialog = MessageBox.Show("Bạn có muốn xóa công nợ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        //Delete list book
                        //Foreign Key
                        //Delete TON, CONGNO, HOADON and CTHD

                        CongNoBUL.DeleteCongNosBUL(keys);

                        MessageBox.Show("Bạn đã xóa công nợ thành công!", "Thông báo");
                    }


                }

                //Reload
                Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_chkAdd)
                {
                    if (txtKey.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Bạn hãy nhập mã công nợ muốn thêm!", "Thông báo");
                    }
                    else
                    {
                        if (CongNoBUL.checkMaCongNoBUL(txtKey.Text.Trim()))
                        {
                            CONGNO item = new CONGNO();
                            item.MaCongNo = txtKey.Text;
                            item.MaKH = cmbKH.SelectedValue.ToString();
                            item.Thang = Convert.ToInt16(cmbThangNo.Text);
                            item.NoDau = Convert.ToDecimal(txtNoDau.Text);
                            item.NoPhatSinh = Convert.ToDecimal(txtNoPhatSinh.Text);
                            item.NoCuoi = Convert.ToDecimal(txtNoCuoi.Text);

                            //insert into database
                            CongNoBUL.InsertCongNoBUL(item);
                            MessageBox.Show("Bạn đã thêm công nợ [" + txtKey.Text + "] thành công", "Thông báo");

                            txtKey.Text = String.Empty;
                            cmbKH.SelectedIndex = -1;
                            cmbThangNo.SelectedIndex = -1;
                            txtNoDau.Text = String.Empty;
                            txtNoPhatSinh.Text = String.Empty;
                            txtNoCuoi.Text = String.Empty;

                        }
                        else
                        {
                            MessageBox.Show("Mã công nợ đã tồn tại, bạn hãy nhâp một mã công nợ khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (txtKey.Text.Trim() != string.Empty)
                    {
                         DialogResult dialog = MessageBox.Show("Bạn có muốn sửa công nợ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                         if (dialog == DialogResult.OK)
                         {
                             CONGNO item = new CONGNO();
                             item.MaCongNo = txtKey.Text;
                             item.MaKH = cmbKH.SelectedValue.ToString();
                             item.Thang = Convert.ToInt16(cmbThangNo.Text);
                             item.NoDau = Convert.ToDecimal(txtNoDau.Text);
                             item.NoPhatSinh = Convert.ToDecimal(txtNoPhatSinh.Text);
                             item.NoCuoi = Convert.ToDecimal(txtNoCuoi.Text);

                             //insert into database
                             CongNoBUL.UpdateCongNoBUL(item);

                             MessageBox.Show("Bạn đã sửa [" + txtKey.Text + "] thành công", "Thông báo");
                         }

                    }
                    else
                    {
                        MessageBox.Show("Không tìm được mã công nợ để cập nhật!", "Thông báo");
                    }

                }

                Reload();

                btnAdd.Text = "Thêm";
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtKey.Enabled = false;
                cmbKH.Enabled = false;
                cmbThangNo.Enabled = false;
                txtNoDau.Enabled = false;
                txtNoPhatSinh.Enabled = false;
                txtNoCuoi.Enabled = false;

                _chkAdd = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Duyet cac dong trong DataGridView
                foreach (DataGridViewRow row in DGV_Result.Rows)
                {
                    //O dau tien la checkbox se chuyen trang thai true hoac false
                    ((DataGridViewCheckBoxCell)row.Cells[0]).Value = chkAll.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text.Trim().Equals(string.Empty))
                {
                    txtSearch.BackColor = Color.White;
                    Reload();
                }
                else
                {
                    //Tìm sách và đổ vào DataGridView
                    DGV_Result.DataSource = CongNoBUL.SearchCongNosBUL(txtSearch.Text.Trim());
                    txtSearch.BackColor = Color.LightGreen;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!_chkAdd)
                {
                    //Lay ma sach tren DataGridView
                    string key = DGV_Result.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //Select Sach bằng mã sách

                    CONGNO item = CongNoBUL.SelectCongNoBUL(key);

                    txtKey.Text = item.MaCongNo;
                    cmbKH.SelectedValue = item.MaKH;
                    cmbThangNo.Text = item.Thang.ToString();
                    txtNoDau.Text = item.NoDau.ToString();
                    txtNoPhatSinh.Text = item.NoPhatSinh.ToString();
                    txtNoCuoi.Text = item.NoCuoi.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.OverwritePrompt = true;
                savefile.DefaultExt = "*.xlsx";
                savefile.Filter = "Execl Workbook(*.xlsx)|*.xlsx";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(savefile.FileName);
                    Excelutlity exl = new Excelutlity();
                    exl.WriteDataTableToExcel(CongNoBUL.SellectAllCongNosBUL(), "Customers", path, "Details");

                    MessageBox.Show("Excel đã được tạo !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNoDau_TextChanged(object sender, EventArgs e)
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

        private void txtNoPhatSinh_TextChanged(object sender, EventArgs e)
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

        private void txtNoCuoi_TextChanged(object sender, EventArgs e)
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

        private void cmbBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBBaoCao.Checked)
                {
                    DGV_Result.DataSource = CongNoBUL.SelectCongNo_MonthBUL(cmbBaoCao.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkBBaoCao_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
