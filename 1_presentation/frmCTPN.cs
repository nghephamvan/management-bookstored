using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2_BUL;
using System.IO;
using _4_DTO;

namespace _1_Presentation
{
    public partial class frmCTPN : Form
    {
        bool _chkAdd = false;
        public frmCTPN()
        {
            InitializeComponent();
        }

        private void frmCTPN_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            cmbMAPN.DataSource = PhieuNhapBUL.SelectAllPhieuNhapsBUL();
            cmbMAPN.DisplayMember = "Ngày nhập";
            cmbMAPN.ValueMember = "Mã phiếu nhập";

            cmbSach.DataSource = SachBUL.SelectAllBooksBUL();
            cmbSach.DisplayMember = "Tên Sách";
            cmbSach.ValueMember = "Mã Sách";

            DGV_Result.DataSource = CTPNBUL.SellectAllCTPNBUL();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Equals("Thêm"))
            {
                btnAdd.Text = "Hủy";
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                txtMaCTPN.Text = String.Empty;
                cmbMAPN.SelectedIndex = -1;
                cmbSach.SelectedIndex = -1;
                txtSL_Nhap.Text = String.Empty;

                txtMaCTPN.Enabled = true;
                cmbMAPN.Enabled = true;
                cmbSach.Enabled = true;
                txtSL_Nhap.Enabled = true;



                _chkAdd = true;

            }
            else
            {
                btnAdd.Text = "Thêm";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;


                txtMaCTPN.Text = String.Empty;
                cmbMAPN.SelectedIndex = -1;
                cmbSach.SelectedIndex = -1;
                txtSL_Nhap.Text = String.Empty;

                txtMaCTPN.Enabled = false;
                cmbMAPN.Enabled = false;
                cmbSach.Enabled = false;
                txtSL_Nhap.Enabled = false;

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


                txtMaCTPN.Enabled = false;
                cmbMAPN.Enabled = true;
                cmbSach.Enabled = true;
                txtSL_Nhap.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtMaCTPN.Enabled = false;
                cmbMAPN.Enabled = false;
                cmbSach.Enabled = false;
                txtSL_Nhap.Enabled = false;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int temp = 0;

            List<string> ctpns = new List<string>();
            //Duyet cac dong trong DataGridView
            foreach (DataGridViewRow row in DGV_Result.Rows)
            {
                //O dau tien la checkbox se chuyen trang thai true hoac false
                if (Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true)
                {
                    ctpns.Add(row.Cells[2].Value.ToString());
                    temp++;
                }
            }



            if (temp == 0)
            {
                MessageBox.Show("Hãy chọn dữ liệu trước khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa chi tiết phiếu nhập?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    //Delete list book
                    //Foreign Key
                    //Delete TON, CONGNO, HOADON and CTHD
                    CTPNBUL.DeleteCTPNBUL(ctpns);

                    MessageBox.Show("Bạn đã xóa chi tiết phiếu nhập thành công!", "Thông báo");
                }


            }

            //Reload
            Reload();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_chkAdd)
            {
                if (txtMaCTPN.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Bạn hãy nhập mã khách hàng muốn thêm!", "Thông báo");
                }
                else
                {
                    if (CTPNBUL.checkMaCTPNBUL(txtMaCTPN.Text.Trim()))
                    {
                        CTPN item = new CTPN();
                        item.MACTPN = txtMaCTPN.Text;
                        item.MAPN = cmbMAPN.SelectedValue.ToString();
                        item.MASACH = cmbSach.SelectedValue.ToString();
                        item.SL_NHAP = Convert.ToInt16(txtSL_Nhap.Text);

                        //insert into database
                        //check /*Chỉ nhập các đầu sách có sl_tồn<300*/
                        //SL_NHAP >= 150
                        if (ThamSoBUL.SelectThamSoBUL() != null && ThamSoBUL.SelectThamSoBUL().SUDUNGQUYDINH == true)
                        {
                            if (!CTPNBUL.checkSachSLTonBUL(item.MASACH))
                            {
                                MessageBox.Show("Chỉ nhập sách có số lượng tồn nhỏ hơn hoặc bằng 300", "Thông Báo");
                            }
                            else if (!CTPNBUL.checkSL_NhapItNhat(item.SL_NHAP))
                            {
                                MessageBox.Show("Số lượng nhập phải lớn hơn 150!", "Thông Báo");
                            }
                            else
                            {
                                CTPNBUL.InsertCTPNBUL(item);
                                MessageBox.Show("Bạn đã thêm chi tiết phiếu nhập [" + txtMaCTPN.Text + "] thành công", "Thông báo");

                                txtMaCTPN.Text = String.Empty;
                                cmbMAPN.SelectedIndex = -1;
                                cmbSach.SelectedIndex = -1;
                                txtSL_Nhap.Text = String.Empty;
                            }
                        }
                        else
                        {
                            CTPNBUL.InsertCTPNBUL(item);
                            MessageBox.Show("Bạn đã thêm chi tiết phiếu nhập [" + txtMaCTPN.Text + "] thành công", "Thông báo");

                            txtMaCTPN.Text = String.Empty;
                            cmbMAPN.SelectedIndex = -1;
                            cmbSach.SelectedIndex = -1;
                            txtSL_Nhap.Text = String.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã CTPN đã tồn tại, bạn hãy nhâp một mã CTPN khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                if (txtMaCTPN.Text.Trim() != string.Empty)
                {
                    CTPN item = new CTPN();
                    item.MACTPN = txtMaCTPN.Text;
                    item.MAPN = cmbMAPN.SelectedValue.ToString();
                    item.MASACH = cmbSach.SelectedValue.ToString();
                    item.SL_NHAP = Convert.ToInt16(txtSL_Nhap.Text);

                    //insert into database
                    //check /*Chỉ nhập các đầu sách có sl_tồn<300*/
                    //SL_NHAP >= 150
                    if (ThamSoBUL.SelectThamSoBUL() != null && ThamSoBUL.SelectThamSoBUL().SUDUNGQUYDINH == true)
                    {
                        if (!CTPNBUL.checkSachSLTonBUL(item.MASACH))
                        {
                            MessageBox.Show("Chỉ nhập sách có số lượng tồn nhỏ hơn hoặc bằng 300", "Thông Báo");
                        }
                        else if (!CTPNBUL.checkSL_NhapItNhat(item.SL_NHAP))
                        {
                            MessageBox.Show("Số lượng nhập phải lớn hơn 150!", "Thông Báo");
                        }
                        else
                        {
                            CTPNBUL.UpdateCTPNBUL(item);
                            MessageBox.Show("Bạn đã sửa chi tiết phiếu nhập [" + txtMaCTPN.Text + "] thành công", "Thông báo");
                        }
                    }
                    else
                    {
                        CTPNBUL.UpdateCTPNBUL(item);
                        MessageBox.Show("Bạn đã sửa chi tiết phiếu nhập [" + txtMaCTPN.Text + "] thành công", "Thông báo");
                    }

                }
                else
                {
                    MessageBox.Show("Không tìm được mã CTPN để cập nhật!", "Thông báo");
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

        private void txtSL_Nhap_TextChanged(object sender, EventArgs e)
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
                DGV_Result.DataSource = CTPNBUL.SearchCTPNBUL(txtSearch.Text);
                txtSearch.BackColor = Color.LightGreen;
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
                exl.WriteDataTableToExcel(CTPNBUL.SellectAllCTPNBUL(), "Customers", path, "Details");

                MessageBox.Show("Excel created !");
            }
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_chkAdd)
            {
                //Lay ma sach tren DataGridView
                string key = DGV_Result.Rows[e.RowIndex].Cells[2].Value.ToString();
                //Select Sach bằng mã sách
                CTPN item = CTPNBUL.SelectCTPNBUL(key);

                txtMaCTPN.Text = item.MACTPN;
                cmbMAPN.SelectedValue = item.MAPN;
                cmbSach.SelectedValue = item.MASACH;
                txtSL_Nhap.Text = item.SL_NHAP.ToString();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
