using _2_BUL;
using _4_DTO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _1_Presentation
{
    public partial class frmTon : Form
    {

        public frmTon()
        {
            InitializeComponent();
        }

        QLNSModelDataContext db = new QLNSModelDataContext();
        private void frmTon_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
           

            DGV_Result.DataSource = TonBUL.SellectAllTonsBUL();
            //DGV_Result.DataSource = temp2;
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
