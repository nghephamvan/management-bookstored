using _2_BUL;
using _4_DTO;
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
    public partial class frmCTHD : Form
    {
        bool _chkAdd = false;

        public frmCTHD()
        {
            InitializeComponent();
        }

        private void txtSL_Ban_TextChanged(object sender, EventArgs e)
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
                    cmbHD.SelectedIndex = -1;
                    cmbSach.SelectedIndex = -1;
                    txtSL_Ban.Text = String.Empty;

                    txtKey.Enabled = true;
                    cmbHD.Enabled = true;
                    cmbSach.Enabled = true;
                    txtSL_Ban.Enabled = true;



                    _chkAdd = true;

                }
                else
                {
                    btnAdd.Text = "Thêm";
                    btnSave.Enabled = false;
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;


                    txtKey.Text = String.Empty;
                    cmbHD.SelectedIndex = -1;
                    cmbSach.SelectedIndex = -1;
                    txtSL_Ban.Text = String.Empty;

                    txtKey.Enabled = false;
                    cmbHD.Enabled = false;
                    cmbSach.Enabled = false;
                    txtSL_Ban.Enabled = false;

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
                    cmbHD.Enabled = true;
                    cmbSach.Enabled = true;
                    txtSL_Ban.Enabled = true;

                }
                else
                {
                    btnUpdate.Text = "Sửa";
                    btnSave.Enabled = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;

                    txtKey.Enabled = false;
                    cmbHD.Enabled = false;
                    cmbSach.Enabled = false;
                    txtSL_Ban.Enabled = false;

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
                    DialogResult dialog = MessageBox.Show("Bạn có muốn xóa chi tiết hóa đơn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        //Delete list book
                        //Foreign Key
                        //Delete TON, CONGNO, HOADON and CTHD
                        CTHDBUL.DeleteCTHDsBUL(keys);

                        MessageBox.Show("Bạn đã xóa chi tiết hóa đơn thành công!", "Thông báo");
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
                        MessageBox.Show("Bạn hãy nhập mã khách hàng muốn thêm!", "Thông báo");
                    }
                    else
                    {
                        if (CTHDBUL.checkMaCTHDBUL(txtKey.Text.Trim()))
                        {
                            CTHD item = new CTHD();
                            item.MACTHD = txtKey.Text;
                            item.MaSach = cmbSach.SelectedValue.ToString();
                            item.MaHD = cmbHD.SelectedValue.ToString();
                            item.SL_BAN = Convert.ToInt16(txtSL_Ban.Text);

                            //insert into database
                            //Chỉ bán cho khách có số nợ dưới 20000
                            //SL_Ton sau >= 20
                            if (ThamSoBUL.SelectThamSoBUL() != null && ThamSoBUL.SelectThamSoBUL().SuDungQuyDinh == true)
                            {

                                if (!CTHDBUL.checkKH_CTHDBUL(item.MaHD))
                                {
                                    MessageBox.Show("Do khách hàng có số nợ lớn hơn 20000\nNên không thể thực hiện giao dịch này", "Thông Báo");
                                }
                                else if ((CTHDBUL.TakeSach_SL_TonBUL(item.MaSach) - item.SL_BAN) < ThamSoBUL.SelectThamSoBUL().SL_TonSauToiThieu)
                                {
                                    MessageBox.Show("Số lượng tồn sau khi bán nhỏ hơn 20\n Nên không thể thực hiện giao dịch này ", "Thông Báo");
                                }
                                else
                                {
                                    CTHDBUL.InsertCTHDBUL(item);
                                    MessageBox.Show("Bạn đã thêm chi tiết hóa đơn [" + txtKey.Text + "] thành công", "Thông báo");

                                    txtKey.Text = String.Empty;
                                    cmbHD.SelectedIndex = -1;
                                    cmbSach.SelectedIndex = -1;
                                    txtSL_Ban.Text = String.Empty;
                                }
                            }
                            else
                            {
                                CTHDBUL.InsertCTHDBUL(item);
                                MessageBox.Show("Bạn đã thêm chi tiết hóa đơn [" + txtKey.Text + "] thành công", "Thông báo");

                                txtKey.Text = String.Empty;
                                cmbHD.SelectedIndex = -1;
                                cmbSach.SelectedIndex = -1;
                                txtSL_Ban.Text = String.Empty;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã CTHD đã tồn tại, bạn hãy nhâp một mã CTHD khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (txtKey.Text.Trim() != string.Empty)
                    {
                        DialogResult dialog = MessageBox.Show("Bạn có muốn sửa chi tiết hóa đơn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.OK)
                        {
                            CTHD item = new CTHD();
                            item.MACTHD = txtKey.Text;
                            item.MaSach = cmbSach.ValueMember;
                            item.MaHD = cmbHD.ValueMember;
                            item.SL_BAN = Convert.ToInt16(txtSL_Ban.Text);

                            //insert into database
                            //Chỉ bán cho khách có số nợ dưới 20000
                            //SL_Ton sau >= 20
                            if (ThamSoBUL.SelectThamSoBUL() != null && ThamSoBUL.SelectThamSoBUL().SuDungQuyDinh == true)
                            {
                                if (!CTHDBUL.checkKH_CTHDBUL(item.MaHD))
                                {
                                    MessageBox.Show("Do khách hàng có số nợ lớn hơn 20000\nNên không thể thực hiện giao dịch này", "Thông Báo");
                                }
                                else if ((CTHDBUL.TakeSach_SL_TonBUL(item.MaSach) - item.SL_BAN) < ThamSoBUL.SelectThamSoBUL().SL_TonSauToiThieu)
                                {
                                    MessageBox.Show("Số lượng tồn sau khi bán nhỏ hơn 20\n Nên không thể thực hiện giao dịch này ", "Thông Báo");
                                }
                                else
                                {
                                    CTHDBUL.UpdateCTHDBUL(item);
                                    MessageBox.Show("Bạn đã sửa chi tiết hóa đơn [" + txtKey.Text + "] thành công", "Thông báo");
                                }
                            }
                            else
                            {
                                CTHDBUL.UpdateCTHDBUL(item);
                                MessageBox.Show("Bạn đã sửa chi tiết hóa đơn [" + txtKey.Text + "] thành công", "Thông báo");
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không tìm được mã CTHD để cập nhật!", "Thông báo");
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
                cmbHD.Enabled = false;
                cmbSach.Enabled = false;
                txtSL_Ban.Enabled = false;

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
                    DGV_Result.DataSource = CTHDBUL.SearchCTHDsBUL(txtSearch.Text.Trim());
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

                    CTHD item = CTHDBUL.SelectCTHDBUL(key);

                    txtKey.Text = item.MACTHD;
                    cmbSach.SelectedValue = item.MaSach;
                    cmbHD.SelectedValue = item.MaHD;
                    txtSL_Ban.Text = item.SL_BAN.ToString();
                    //txtTenKH.Text = DGV_Result.Rows[e.RowIndex].Cells[7].Value.ToString();
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
                    exl.WriteDataTableToExcel(CTHDBUL.SellectAllCTHDsBUL(), "Customers", path, "Details");

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

        private void frmCTHD_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            try
            {
                cmbSach.DataSource = SachBUL.SelectAllBooksBUL();
                cmbSach.DisplayMember = "Tên Sách";
                cmbSach.ValueMember = "Mã Sách";

                cmbHD.DataSource = HoaDonBUL.SellectAllHoaDonBUL();
                cmbHD.DisplayMember = "Mã HD";
                cmbHD.ValueMember = "Mã HD";
                cmbHD.SelectedIndex = -1;

                DGV_Result.DataSource = CTHDBUL.SellectAllCTHDsBUL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHD.SelectedIndex >= 0 && cmbHD.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                HOADON temp = HoaDonBUL.SelectHoaDonBUL(cmbHD.SelectedValue.ToString());
                dtpNgayHD.Value= Convert.ToDateTime(temp.NgayHD);
                KHACHHANG kh = HoaDonBUL.SelectHoaDon_KHBUL(temp.MaKH);

                txtTenKH.Text = kh.HoTen;
            }
           
        }
    }
}
