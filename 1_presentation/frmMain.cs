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

namespace _1_Presentation
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnTon_Click(object sender, EventArgs e)
        {
            try
            {
                frmTon frm;
                if ((Application.OpenForms["frmTon"] as frmTon) != null)
                {
                    //frmPhieuNhap frm = new frmPhieuNhap();
                    //frm.Show();
                    Application.OpenForms["frmTon"].BringToFront();
                    Application.OpenForms["frmTon"].MaximizeBox = true;
                    Application.OpenForms["frmTon"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    // Form is not open
                    //frm.Show();
                    frm = new frmTon();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            try
            {
                frmSach frm;
                if ((Application.OpenForms["frmSach"] as frmSach) != null)
                {
                    Application.OpenForms["frmSach"].BringToFront();
                    Application.OpenForms["frmSach"].MaximizeBox = true;
                    Application.OpenForms["frmSach"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmSach();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCTPN_Click(object sender, EventArgs e)
        {
            try
            {
                frmCTPN frm;
                if ((Application.OpenForms["frmCTPN"] as frmCTPN) != null)
                {
                    Application.OpenForms["frmCTPN"].BringToFront();
                    Application.OpenForms["frmCTPN"].MaximizeBox = true;
                    Application.OpenForms["frmCTPN"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmCTPN();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCTHD_Click(object sender, EventArgs e)
        {
            try
            {
                frmCTHD frm;
                if ((Application.OpenForms["frmCTHD"] as frmCTHD) != null)
                {
                    Application.OpenForms["frmCTHD"].BringToFront();
                    Application.OpenForms["frmCTHD"].MaximizeBox = true;
                    Application.OpenForms["frmCTHD"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmCTHD();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPN_Click(object sender, EventArgs e)
        {
            try
            {
                frmPhieuNhap frm;
                if ((Application.OpenForms["frmPhieuNhap"] as frmPhieuNhap) != null)
                {
                    Application.OpenForms["frmPhieuNhap"].BringToFront();
                    Application.OpenForms["frmPhieuNhap"].MaximizeBox = true;
                    Application.OpenForms["frmPhieuNhap"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmPhieuNhap();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            try
            {
                frmHD frm;
                if ((Application.OpenForms["frmHD"] as frmHD) != null)
                {
                    Application.OpenForms["frmHD"].BringToFront();
                    Application.OpenForms["frmHD"].MaximizeBox = true;
                    Application.OpenForms["frmHD"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmHD();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            try
            {
                frmThamSo frm;
                if ((Application.OpenForms["frmThamSo"] as frmThamSo) != null)
                {
                    Application.OpenForms["frmThamSo"].BringToFront();
                    Application.OpenForms["frmThamSo"].MaximizeBox = true;
                    Application.OpenForms["frmThamSo"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmThamSo();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCN_Click(object sender, EventArgs e)
        {
            try
            {
                frmCongNo frm;
                if ((Application.OpenForms["frmCongNo"] as frmCongNo) != null)
                {
                    Application.OpenForms["frmCongNo"].BringToFront();
                    Application.OpenForms["frmCongNo"].MaximizeBox = true;
                    Application.OpenForms["frmCongNo"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmCongNo();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            try
            {
                frmKhachHang frm;
                if ((Application.OpenForms["frmKhachHang"] as frmKhachHang) != null)
                {
                    Application.OpenForms["frmKhachHang"].BringToFront();
                    Application.OpenForms["frmKhachHang"].MaximizeBox = true;
                    Application.OpenForms["frmKhachHang"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmKhachHang();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThuTien_Click(object sender, EventArgs e)
        {
            try
            {
                frmThuTien frm;
                if ((Application.OpenForms["frmThuTien"] as frmThuTien) != null)
                {
                    Application.OpenForms["frmThuTien"].BringToFront();
                    Application.OpenForms["frmThuTien"].MaximizeBox = true;
                    Application.OpenForms["frmThuTien"].WindowState = FormWindowState.Maximized;
                }
                else
                {
                    frm = new frmThuTien();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (h == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

    }
}
