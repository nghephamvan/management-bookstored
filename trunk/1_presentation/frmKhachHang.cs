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
    public partial class frmKhachHang : Form
    {
        bool _chkAdd = false;
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            try
            {
                DGV_Result.DataSource = KhachHangBUL.SellectAllCustomerBUL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            Reload();
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

                    txtMaKH.Text = String.Empty;
                    txtTen.Text = String.Empty;
                    txtDiaChi.Text = String.Empty;
                    txtSDT.Text = String.Empty;
                    txtEmail.Text = String.Empty;
                    txtSoTienNo.Text = String.Empty;

                    txtMaKH.Enabled = true;
                    txtTen.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtSDT.Enabled = true;
                    txtEmail.Enabled = true;
                    txtSoTienNo.Enabled = true;


                    _chkAdd = true;

                }
                else
                {
                    btnAdd.Text = "Thêm";
                    btnSave.Enabled = false;
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;

                    txtMaKH.Text = String.Empty;
                    txtTen.Text = String.Empty;
                    txtDiaChi.Text = String.Empty;
                    txtSDT.Text = String.Empty;
                    txtEmail.Text = String.Empty;
                    txtSoTienNo.Text = String.Empty;

                    txtMaKH.Enabled = false;
                    txtTen.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtSDT.Enabled = false;
                    txtEmail.Enabled = false;
                    txtSoTienNo.Enabled = false;

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


                    txtMaKH.Enabled = false;
                    txtTen.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtSDT.Enabled = true;
                    txtEmail.Enabled = true;
                    txtSoTienNo.Enabled = true;

                }
                else
                {
                    btnUpdate.Text = "Sửa";
                    btnSave.Enabled = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;

                    txtMaKH.Enabled = false;
                    txtTen.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtSDT.Enabled = false;
                    txtEmail.Enabled = false;
                    txtSoTienNo.Enabled = false;

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

                List<string> customers = new List<string>();
                //Duyet cac dong trong DataGridView
                foreach (DataGridViewRow row in DGV_Result.Rows)
                {
                    //O dau tien la checkbox se chuyen trang thai true hoac false
                    if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true)
                    {
                        customers.Add(row.Cells[2].Value.ToString());
                        temp++;
                    }
                }



                if (temp == 0)
                {
                    MessageBox.Show("Hãy chọn dữ liệu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn xóa thông tin khách hàng?\nKhi xóa khách hàng sẽ xóa các thông tin liên quan ở hóa đơn, chi tiết hóa đơn và công nợ!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        //Delete list book
                        //Foreign Key
                        //Delete TON, CONGNO, HOADON and CTHD
                        KhachHangBUL.DeleteCustomersBUL(customers);

                        MessageBox.Show("Bạn đã xóa khách hàng thành công!", "Thông báo");
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
                    if (txtMaKH.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Bạn hãy nhập mã khách hàng muốn thêm!", "Thông báo");
                    }
                    else
                    {
                        if (KhachHangBUL.checkMaKHBUL(txtMaKH.Text.Trim()))
                        {
                            KHACHHANG kh = new KHACHHANG();
                            kh.MaKH = txtMaKH.Text;
                            kh.HoTen = txtTen.Text;
                            kh.DiaChi = txtDiaChi.Text;
                            kh.DienThoai = txtSDT.Text;
                            kh.Email = txtEmail.Text;
                            kh.SoTienNo = Convert.ToDecimal(txtSoTienNo.Text);

                            KhachHangBUL.InsertCustomerBUL(kh);

                            MessageBox.Show("Bạn đã thêm khách hàng với mã [" + txtMaKH.Text + "] thành công", "Thông báo");

                            txtMaKH.Text = String.Empty;
                            txtTen.Text = String.Empty;
                            txtDiaChi.Text = String.Empty;
                            txtSDT.Text = String.Empty;
                            txtEmail.Text = String.Empty;
                            txtSoTienNo.Text = String.Empty;

                        }
                        else
                        {
                            MessageBox.Show("Mã khách hàng đã tồn tại, bạn hãy nhâp một mã khách hàng khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (txtMaKH.Text.Trim() != string.Empty)
                    {
                        DialogResult dialog = MessageBox.Show("Bạn có muốn sửa thông tin khách hàng?\nKhi xóa khách hàng sẽ xóa các thông tin liên quan ở hóa đơn, chi tiết hóa đơn và công nợ!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.OK)
                        {
                            KHACHHANG kh = new KHACHHANG();
                            kh.MaKH = txtMaKH.Text;
                            kh.HoTen = txtTen.Text;
                            kh.DiaChi = txtDiaChi.Text;
                            kh.DienThoai = txtSDT.Text;
                            kh.Email = txtEmail.Text;
                            kh.SoTienNo = Convert.ToDecimal(txtSoTienNo.Text);

                            //Sửa thông tin của sách
                            KhachHangBUL.UpdateCustomerBUL(kh);

                            MessageBox.Show("Bạn đã cập nhật thông tin của khách hàng [" + txtMaKH.Text + "] thành công!", "Thông báo");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm được mã khách hàng để cập nhật!", "Thông báo");
                    }

                }

                Reload();
                _chkAdd = false;

                btnAdd.Text = "Thêm";
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtMaKH.Enabled = false;
                txtTen.Enabled = false;
                txtDiaChi.Enabled = false;
                txtSDT.Enabled = false;
                txtEmail.Enabled = false;
                txtSoTienNo.Enabled = false;
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
                    DGV_Result.DataSource = KhachHangBUL.SearchCustomersBUL(txtSearch.Text);
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
                    string MaKH = DGV_Result.Rows[e.RowIndex].Cells[2].Value.ToString();
                    //Select Sach bằng mã sách
                    KHACHHANG kh = KhachHangBUL.SelectCustomerBUL(MaKH);

                    txtMaKH.Text = kh.MaKH;
                    txtTen.Text = kh.HoTen;
                    txtDiaChi.Text = kh.DiaChi;
                    txtSDT.Text = kh.DienThoai;
                    txtEmail.Text = kh.Email;
                    txtSoTienNo.Text = kh.SoTienNo.ToString();
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
                    exl.WriteDataTableToExcel(KhachHangBUL.SellectAllCustomerBUL(), "Customers", path, "Details");

                    MessageBox.Show("Excel created !");
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

        private void txtSoTienNo_TextChanged(object sender, EventArgs e)
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

    }
}
