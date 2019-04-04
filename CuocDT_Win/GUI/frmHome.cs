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

namespace CuocDT_Win.GUI
{
    public partial class frmHome : Form
    {
        SimDAO d = new SimDAO();
        public static frmHome fr;
        public CuocBIZ a = new CuocBIZ();
        public BillBIZ bill = new BillBIZ();
        public CuocDbContext db = new CuocDbContext();
        public frmHome()
        {
            InitializeComponent();
            SimDAO d = new SimDAO();
            dataGridView1.DataSource = a.ListAll();
            fr = this;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            this.Hide();
            home.ShowDialog();
            Application.Exit();

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

        private void txt_thang_TextChanged(object sender, EventArgs e)
        {
            if (txt_SDT.Text != "")
            {
                int thang = int.Parse(txt_thang.Text);
                decimal? CuocThang = a.TinhCuocThang(thang, txt_SDT.Text);
                txt_giacuocthang.Text = CuocThang.GetValueOrDefault(0).ToString("N0") + "VNĐ";
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cuocthang_Click(object sender, EventArgs e)// chưa xong
        {
            CuocBIZ c = new CuocBIZ();
            BillDAO a = new BillDAO();
            List<HoaDonCuoc> count = db.HoaDonCuocs.Select(b => b).ToList();
            var q = a.ListTableBill();
            if (q.Count == 0)
            {
                c.AddData();  
            }

            else
            {
                bill.DeletePayBill();
                bill.DeleteBillInf();
                bill.DeleteBill();
                c.AddData();
            }
        }


        /*  public void AddData()
          {
              BillBIZ bills = new BillBIZ();
              HoaDonCuoc bill = new HoaDonCuoc();
              SimDAO d = new SimDAO();
              DateTime date = DateTime.Now;
              int thang = date.Month;
              foreach (var item in d.GetPhone())
              {
                  var MonthStart = (DateTime)item.NgayKichHoat;
                  for (int i = MonthStart.Month; i < thang; i++)
                  {
                      string phone = item.idSim;
                      decimal? totalprice = a.TinhCuocThang(i ,item.idSim);
                      int month = i;

                      bills.AddBill(phone, totalprice, month);
                      var idhd = new BillDAO().InsertBill(bill);
                      var ListBill = from all in a.ListAll()
                                     where all.SoDT == bill.SoDT.ToString()
                                     where all.TGBD.Month == i
                                     select all;
                      foreach (var items in ListBill)
                      {  
                          DateTime TGBD = items.TGBD;
                          DateTime TGKT = items.TGKT;
                          decimal? totalPrice = items.ThanhTien;
                          int id = idhd;
                          bills.AddBillInf(id, phone, TGBD, TGKT, totalPrice);

                      }
                  }
              }

      
    }

}*/
        private void txt_SDT_TextChanged(object sender, EventArgs e)
        {
            string SDT = txt_SDT.Text;
            if (SDT == "")
            {
                dataGridView1.DataSource = a.ListAll();

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

        private void txt_thang_TextChanged_1(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            int txtthang = 0;
            if (txt_thang.Text == "")
                txtthang = date.Month + 1;
            else
                txtthang = int.Parse(txt_thang.Text);
            if (txt_SDT.Text != "" && txtthang <= date.Month)
            {
                int thang = int.Parse(txt_thang.Text);
                decimal? CuocThang = a.TinhCuocThang(thang, txt_SDT.Text);
                txt_giacuocthang.Text = CuocThang.GetValueOrDefault(0).ToString("N0") + "VNĐ";
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại Hoặc Sai tháng ", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmCustomer cus = new frmCustomer();
            cus.MdiParent = this;
            cus.FormBorderStyle = FormBorderStyle.None;
            content.Controls.Clear();
            content.Controls.Add(cus);
            cus.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmBill cus = new frmBill();

            cus.MdiParent = this;
            cus.FormBorderStyle = FormBorderStyle.None;
            content.Controls.Clear();
            content.Controls.Add(cus);
            cus.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmSim cus = new frmSim();
            cus.MdiParent = this;
            cus.FormBorderStyle = FormBorderStyle.None;
            content.Controls.Clear();
            content.Controls.Add(cus);
            cus.Show();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }
        internal void test(int id, string name, string address, string job, string CMND)
        {
            frmSim cus = new frmSim(id, name, address, job, CMND);
            cus.MdiParent = this;
            cus.FormBorderStyle = FormBorderStyle.None;
            content.Controls.Clear();
            content.Controls.Add(cus);
            cus.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            frmHome.fr.Controls.Clear();
            InitializeComponent();
            SimDAO d = new SimDAO();
            dataGridView1.DataSource = a.ListAll();
            fr = this;
        }

        private void thoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomer cus = new frmCustomer();
            cus.MdiParent = this;
            cus.FormBorderStyle = FormBorderStyle.None;
            content.Controls.Clear();
            content.Controls.Add(cus);
            cus.Show();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBill cus = new frmBill();

            cus.MdiParent = this;
            cus.FormBorderStyle = FormBorderStyle.None;
            content.Controls.Clear();
            content.Controls.Add(cus);
            cus.Show();
        }

        private void tínhCướcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHome.fr.Controls.Clear();
            InitializeComponent();
            SimDAO d = new SimDAO();
            dataGridView1.DataSource = a.ListAll();
            fr = this;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void thêmSimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddSim sim = new frmAddSim();
            this.Hide();
            sim.ShowDialog();
            this.Show();
        }
    }
}
