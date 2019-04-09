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
            if (phone == "")
            {
                MessageBox.Show("Số Điện thoại trống");
            }
            else
            {
                int temp = 0;
                CuocBIZ cuoc = new CuocBIZ();
                var listsim = new SimDAO().GetListPhone();
                foreach (var item in listsim)
                {
                    if (phone == item.idSim)
                    {
                        temp += 1;
                    }
                }
                if(temp ==0)
                {
                    cuoc.AddSim(phone);
                    MessageBox.Show("Lưu Sim thành công");
                }
                else
                {
                    MessageBox.Show("Số điện thoại đã tồn tại");
                }
            }

        }
    }
}
