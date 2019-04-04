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
    public partial class frmBillInf : Form
    {
     
        public frmBillInf()
        {
            InitializeComponent();
        }
        public CuocDbContext db = new CuocDbContext();
        public CuocBIZ cal = new CuocBIZ();        
        public ChiTietHoaDonCuoc billinf = new ChiTietHoaDonCuoc();

        public frmBillInf(int id , string sodt , int thang) : this()
        {
            
            dtgvBillP.DataSource = (from e in cal.ListAll()
                                    where e.TGBD.Month == thang
                                    where e.SoDT == sodt
                                    select e).ToList();

            textBox3.Text = "50 .000 VNĐ";

            var ListBill = from all in cal.ListAll()
                           where all.SoDT == sodt.ToString()
                           where all.TGBD.Month == thang
                           select all;
           

             decimal? tinhtongtien()
            {
                decimal? sum = 0;

                foreach (var item in ListBill)
                {
                    sum += item.ThanhTien;
                }
                return sum;
            }
            textBox2.Text = tinhtongtien().ToString() + "VNĐ";
        }
        
    }
}
