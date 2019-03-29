using CuocDT.Models;
using CuocDT_Win.BIZ;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuocDT_Win
{
    public partial class Form1 : Form
    {
        public CuocBIZ a = new CuocBIZ();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = a.ListAll();

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string SDT = txt_SDT.Text;
            if (SDT == "")
            {
                MessageBox.Show("SDT Không được rỗng", "Cảnh báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<LogInfo> list = new List<LogInfo>();
                list = a.ListBySDT(SDT);
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                }
                else
                {
                    MessageBox.Show("SDT nhập không chính xác", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txt_SDT.Text != "")
            {
                int thang = int.Parse(txt_thang.Text);
                decimal? CuocThang = a.TinhCuocThang(thang);
                txt_giacuocthang.Text = CuocThang.GetValueOrDefault(0).ToString("N0") + "VNĐ";
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
