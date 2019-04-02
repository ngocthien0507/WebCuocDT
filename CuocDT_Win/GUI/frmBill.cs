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
    public partial class frmBill : Form
    {
        public  CuocDbContext db = new CuocDbContext();
        public HoaDonCuoc bills = new HoaDonCuoc();
        public BillBIZ cus = new BillBIZ();
        BillDAO a = new BillDAO();
        public frmBill()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
             dtgvBill.DataSource = a.ListTableBillDK();
            
             dtgvBillP.DataSource = a.ListTableBill();

        }
 

        private void button5_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            string sodt = dtgvBillP.SelectedCells[0].OwningRow.Cells["Phone"].Value.ToString();
            int thang = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["Month"].Value.ToString());

           
            frmBillInf bill = new frmBillInf(id , sodt , thang);
            
            bill.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string SDT = textBox1.Text;
            if(SDT =="" )
            {
                dtgvBillP.DataSource = a.ListTableBill();
            }
            else
            {
                                       
                dtgvBillP.DataSource = (from p in db.HoaDonCuocs
                                        where p.SoDT.Contains(SDT) 
                                       
                                        select new { ID = p.idHD, Phone = p.SoDT,Status = p.TinhTrang ,TotalPrice = p.TongTien, Month = p.Month, Year = p.Year }).ToList();
            }
            decimal? tinhtongtien()
            {
                decimal? sum = 0;
                var ListBill = from all in db.HoaDonCuocs
                               where all.SoDT == SDT
                               select all;
                foreach (var item in ListBill)
                {
                    sum += item.TongTien;
                }
                return sum;
            }
            textBox2.Text = tinhtongtien().ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string pass = textBox5.Text;
          dtgvBill.DataSource =   cus.GetBillByPass(pass);
        }
     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //  Search Bill Theo Tháng
        {
           
            string cb = comboBox1.SelectedItem.ToString();
            DateTime date = DateTime.Now;
            if( textBox1.Text =="")
            {
                if (int.Parse(cb) <= date.Month)
                {
                    dtgvBillP.DataSource = cus.GetBillByMonth(cb.ToString());
                }
                else
                    MessageBox.Show("Thời gian không hợp lệ");
            }
            else
            {
                string SDT = textBox1.Text;
                dtgvBillP.DataSource = (from p in db.HoaDonCuocs
                                        where p.SoDT.Contains(SDT) && p.Month.ToString() == cb

                                        select new { ID = p.idHD, Phone = p.SoDT, Status = p.TinhTrang, TotalPrice = p.TongTien, Month = p.Month, Year = p.Year }).ToList();
            }
            
        }

        private void dtgvBillP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string status = dtgvBillP.SelectedCells[0].OwningRow.Cells["Status"].Value.ToString();
            if(status =="0")
            {
              
                label2.Text = "Hóa đơn chưa thanh toán";
            }
           else
            {
                label2.Text = " Hóa đơn đã thanh toán";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int id = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            string status = dtgvBillP.SelectedCells[0].OwningRow.Cells["Status"].Value.ToString();
            if(status =="1")
            {
                MessageBox.Show("Hóa đơn đã thanh toán rồi");
            }
            else
            {
                cus.SaveStatus(id);
                MessageBox.Show("Thanh toán thành công");
                LoadData();
            }
            
        }
    }
}

