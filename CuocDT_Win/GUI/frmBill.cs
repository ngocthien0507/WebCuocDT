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
        public BillBIZ bill = new BillBIZ();
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
 

        private void button5_Click(object sender, EventArgs e) // Xem chi tiết hóa đơn
        {
            int id = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            string sodt = dtgvBillP.SelectedCells[0].OwningRow.Cells["Phone"].Value.ToString();
            int thang = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["Month"].Value.ToString());

            frmBillInf bill = new frmBillInf(id , sodt , thang);
            
            bill.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Search theo số ĐT  chưa theo tháng
        {

            string phone = textBox1.Text;
            if(phone == "" )
            {
                dtgvBillP.DataSource = a.ListTableBill(); 
            }
            else
            {

                dtgvBillP.DataSource = bill.SearchByPhone(phone);
            }
            decimal? tinhtongtien()
            {
                decimal? sum = 0;
                var ListBill = from all in db.HoaDonCuocs
                               where all.SoDT == phone
                               select all;
                foreach (var item in ListBill)
                {
                    sum += item.TongTien;
                }
                return sum;
            }
            textBox2.Text = tinhtongtien().ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e) // Search theo CMND
        {
            string pass = textBox5.Text;
          dtgvBill.DataSource = bill.SearchByPass(pass);
        }
     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //  Search Bill Theo Tháng
        {
           
            string cb = comboBox1.SelectedItem.ToString();
            DateTime date = DateTime.Now;
            if( textBox1.Text =="")
            {
                if (int.Parse(cb) <= date.Month)
                {
                    dtgvBillP.DataSource = bill.SearchByMonth(cb.ToString());
                }
                else
                    MessageBox.Show("Thời gian không hợp lệ");
            }
            else
            {
                string SDT = textBox1.Text;
                dtgvBillP.DataSource = bill.SearchAll(SDT,cb.ToString());
            }
            
        }

        private void dtgvBillP_CellClick(object sender, DataGridViewCellEventArgs e)  // Show Tình trạng hóa đơn
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

        private void button1_Click(object sender, EventArgs e) // Thanh toán hóa đơn
        {
           
            int id = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            frmPay pay = new frmPay(id);
            pay.ShowDialog();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            int status = int.Parse(dtgvBillP.SelectedCells[0].OwningRow.Cells["Status"].Value.ToString());
            if(status == 1)
            {
                MessageBox.Show("Hóa đơn này đã thanh toán rồi");
            }
            else
            {
                bill.SaveStatus(id);
                MessageBox.Show("Thanh toán thành công");
            }
            LoadData();
        }
    }
}

