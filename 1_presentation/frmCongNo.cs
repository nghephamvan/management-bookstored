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
                DGV_Result.DataSource = CongNoBUL.SellectAllCongNosBUL();
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
