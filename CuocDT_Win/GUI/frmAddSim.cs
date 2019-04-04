using CuocDT_Win.BIZ;
using Models.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuocDT_Win.GUI
{
    public partial class frmAddSim : Form
    {
        public frmAddSim()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string phone = textBox1.Text;
            if(phone =="")
            {
                MessageBox.Show("Số Điện thoại trống");
            }
            else
            {
                CuocBIZ cuoc = new CuocBIZ();
                cuoc.AddSim(phone);
                MessageBox.Show("Lưu Sim thành công");
            }

        }
    }
}
