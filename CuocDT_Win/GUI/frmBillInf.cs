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
        public CuocBIZ a = new CuocBIZ();
        
        public frmBillInf(int id , string sodt , int thang) : this()
        {
            CuocDbContext db = new CuocDbContext();
            dtgvBillP.DataSource = (from e in a.ListAll()
                                    where e.TGBD.Month == thang
                                    where e.SoDT == sodt
                                    select e).ToList();

            textBox3.Text = "50 .000 VNĐ";
 
            HoaDonCuoc bill = new HoaDonCuoc();
            SimDAO d = new SimDAO();

            ChiTietHoaDonCuoc billinf = new ChiTietHoaDonCuoc();
            
         
            var ListBill = from all in a.ListAll()
                           where all.SoDT == sodt.ToString()
                           where all.TGBD.Month == thang
                           select all;
            var end = from e in db.ChiTietHoaDonCuocs
                      where e.idHD == id
                      where e.SoDT == sodt
                      select e;
          
            if (end.Count() == 0 )
            {
                foreach (var item in ListBill)
                {
                    billinf.idHD = id;
                    billinf.SoDT = sodt.ToString();
                    billinf.TGBD = item.TGBD;
                    billinf.TGKT = item.TGKT;
                    billinf.ThanhTien = item.ThanhTien; 
                    db.ChiTietHoaDonCuocs.Add(billinf);
                    db.SaveChanges();
                }
            }

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
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void frmBillInf_Load(object sender, EventArgs e)
        {

        }
    }
}
