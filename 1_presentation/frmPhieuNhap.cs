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
    public partial class frmPhieuNhap : Form
    {
        public frmPhieuNhap()
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
            DGV_Result.DataSource = PhieuNhapBUL.SelectAllPhieuNhapsBUL();
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
                dtpicker.Value = DateTime.Now.Date;

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
                    phieunhaps.Add(row.Cells[2].Value.ToString());
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
                    //Delete list pn
                    //Foreign Key
                    PhieuNhapBUL.DeletePNsBUL(phieunhaps);

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
                    if (PhieuNhapBUL.checkMaPNBUL(txtMaPN.Text.Trim()))
                    {

                        PHIEUNHAP pn = new PHIEUNHAP();
                        pn.MaPN = txtMaPN.Text;
                        pn.NgayNhap = dtpicker.Value;

                        PhieuNhapBUL.InsertPNBUL(pn);

                        MessageBox.Show("Bạn đã thêm phiếu nhập với mã [" + txtMaPN.Text + "] thành công", "Thông báo");

                        txtMaPN.Text = String.Empty;
                        dtpicker.Value = DateTime.Now.Date;

                    }
                    else
                    {
                        MessageBox.Show("Mã phiếu nhập đã tồn tại, bạn hãy nhâp một mã sách khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                if (txtMaPN.Text.Trim() != string.Empty)
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn sửa thông tin phiếu nhập?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK)
                    {
                        PHIEUNHAP pn = new PHIEUNHAP();
                        pn.MaPN = txtMaPN.Text;
                        pn.NgayNhap = dtpicker.Value.Date;

                        //Sửa thông tin của phiếu nhập
                        PhieuNhapBUL.UpdatePNBUL(pn);

                        MessageBox.Show("Bạn đã cập nhật thông tin của phiếu nhập [" + txtMaPN.Text + "] thành công!", "Thông báo");
                    }

                }
                else
                {
                    MessageBox.Show("Không tìm được mã phiếu nhập để cập nhật!", "Thông báo");
                }

            }

            Reload();
            _chkAdd = false;

            btnAdd.Text = "Thêm";
            btnUpdate.Text = "Sửa";
            btnSave.Enabled = false;
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;

            txtMaPN.Enabled = false;
            dtpicker.Enabled = false;
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
                exl.WriteDataTableToExcel(PhieuNhapBUL.SelectAllPhieuNhapsBUL(), "Books", path, "Details");

                MessageBox.Show("Excel created !");
            }
        }

        private void DGV_Result_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_chkAdd)
            {
                string MaPN = DGV_Result.Rows[e.RowIndex].Cells[2].Value.ToString();

                PHIEUNHAP pn = PhieuNhapBUL.SelectPhieuNhapBUL(MaPN);

                txtMaPN.Text = pn.MaPN;
                dtpicker.Value = (DateTime)pn.NgayNhap;
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
                DGV_Result.DataSource = PhieuNhapBUL.SearchPNsBUL(txtSearch.Text);
                txtSearch.BackColor = Color.LightGreen;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
