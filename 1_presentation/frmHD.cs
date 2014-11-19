﻿using _2_BUL;
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
    public partial class frmHD : Form
    {
        bool _chkAdd = false;
        public frmHD()
        {
            InitializeComponent();
        }

        private void frmHD_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {


            cmbKH.DataSource = KhachHangBUL.SellectAllCustomerBUL();
            cmbKH.DisplayMember = "Tên KH";
            cmbKH.ValueMember = "Mã KH";

            DGV_Result.DataSource = HoaDonBUL.SellectAllHoaDonBUL();
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
                dtpNgayHD.Value = DateTime.Now.Date;
                cmbKH.SelectedIndex = -1;

                txtKey.Enabled = true;
                dtpNgayHD.Enabled = true;
                cmbKH.Enabled = true;



                _chkAdd = true;

            }
            else
            {
                btnAdd.Text = "Thêm";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;


                txtKey.Text = String.Empty;
                dtpNgayHD.Value = DateTime.Now.Date;
                cmbKH.SelectedIndex = -1;

                txtKey.Enabled = false;
                dtpNgayHD.Enabled = false;
                cmbKH.Enabled = false;

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
                dtpNgayHD.Enabled = true;
                cmbKH.Enabled = true;

            }
            else
            {
                btnUpdate.Text = "Sửa";
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;

                txtKey.Enabled = false;
                dtpNgayHD.Enabled = false;
                cmbKH.Enabled = false;

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
                DialogResult dialog = MessageBox.Show("Bạn có muốn xóa hóa đơn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.OK)
                {
                    //Delete list book
                    //Foreign Key
                    //Delete TON, CONGNO, HOADON and CTHD
                    HoaDonBUL.DeleteHoaDonsBUL(keys);

                    MessageBox.Show("Bạn đã xóa hóa đơn thành công!", "Thông báo");
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
                    MessageBox.Show("Bạn hãy nhập mã hóa đơn muốn thêm!", "Thông báo");
                }
                else
                {
                    if (HoaDonBUL.checkMaHDBUL(txtKey.Text.Trim()))
                    {
                        HOADON item = new HOADON();
                        item.MAHD = txtKey.Text;
                        item.MAKH = cmbKH.SelectedValue.ToString();
                        item.NGAYHD = dtpNgayHD.Value.Date;

                        //insert into database
                        //check /*Chỉ nhập các đầu sách có sl_tồn<300*/
                        //SL_NHAP >= 150
                        HoaDonBUL.InsertHoaDonBUL(item);
                        MessageBox.Show("Bạn đã thêm hóa đơn [" + txtKey.Text + "] thành công", "Thông báo");

                        txtKey.Text = String.Empty;
                        dtpNgayHD.Value = DateTime.Now.Date;
                        cmbKH.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Mã hóa đơn đã tồn tại, bạn hãy nhâp một mã hóa đơn khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                if (txtKey.Text.Trim() != string.Empty)
                {
                    HOADON item = new HOADON();
                    item.MAHD = txtKey.Text;
                    item.MAKH = cmbKH.SelectedValue.ToString();
                    item.NGAYHD = dtpNgayHD.Value.Date;

                    //insert into database
                    //check /*Chỉ nhập các đầu sách có sl_tồn<300*/
                    //SL_NHAP >= 150
                    HoaDonBUL.UpdateHoaDonBUL(item);
                    MessageBox.Show("Bạn đã sửa hóa đơn [" + txtKey.Text + "] thành công", "Thông báo");

                }
                else
                {
                    MessageBox.Show("Không tìm được mã hóa đơn để cập nhật!", "Thông báo");
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
                DGV_Result.DataSource = HoaDonBUL.SearchHoaDonsBUL(txtSearch.Text);
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
                HOADON item = HoaDonBUL.SelectHoaDonBUL(key);

                txtKey.Text = item.MAHD;
                cmbKH.SelectedValue = item.MAKH;
                dtpNgayHD.Value = Convert.ToDateTime(item.NGAYHD);
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
                exl.WriteDataTableToExcel(HoaDonBUL.SellectAllHoaDonBUL(), "Customers", path, "Details");

                MessageBox.Show("Excel created !");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
