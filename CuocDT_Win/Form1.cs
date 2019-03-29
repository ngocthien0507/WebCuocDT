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
        public Form1() {   
            InitializeComponent();
            CuocBIZ a = new CuocBIZ();
            string sdt = "0356775770";
            dataGridView1.DataSource = a.ReadDataSDT(sdt);

        }
    }
}
