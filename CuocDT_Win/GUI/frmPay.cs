using Common;
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
using System.Web;

namespace CuocDT_Win.GUI
{
    public partial class frmPay : Form
    {
        public CuocDbContext db = new CuocDbContext();
        public BillBIZ bill = new BillBIZ();
        public frmPay()
        {
            InitializeComponent();
        }

        public frmPay(int id) : this()
        {
           DateTime date = DateTime.Now;
           var ListBill = bill.GetBillByIDHD(id);
           textBox1.Text = id.ToString();            
           foreach( var items in ListBill)
            {
                textBox2.Text = items.SIM.ChuSoHuu;
                textBox3.Text = items.SoDT.ToString();
                textBox5.Text = items.Month.ToString();
                textBox4.Text = items.TongTien.ToString();
                textBox6.Text = date.ToString();
            }
           
        }

        
        private void button1_Click(object sender, EventArgs e) // lỗi sau khi thanh toán k load liền đc status bên Bill
        {
            int id = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            string phone=   textBox3.Text;
            int month =  int.Parse(textBox5.Text);
            string email = textBox7.Text;
            decimal totalprice =   decimal.Parse(textBox4.Text) ;
            decimal? mailprice = totalprice;
            var end = from all in db.HDThanhToans
                      where all.idHD == id
                      select all;
            try
            {
                if (end.Count() == 0)
                {

                    string content = System.IO.File.ReadAllText((@"C:\Users\Thien\source\repos\WebCuocDT-master\CuocDT\Template\newBill.html"));
                    content = content.Replace("{{CustomerName}}", "Nguyễn Ngọc Thiện");
                    content = content.Replace("{{idHD}}", id.ToString());
                    content = content.Replace("{{Phone}}", phone);
                    content = content.Replace("{{Month}}", month.ToString());
                    content = content.Replace("{{Year}}", "2019");
                    content = content.Replace("{{Money}}", mailprice.GetValueOrDefault(0).ToString("N0"));
                    content = content.Replace("{{PayDate}}", DateTime.Now.ToString());
                    string title = "Hóa đơn cước tháng" + month;
                    new MailHelper().SendMail(email, title, content);
                    bill.AddPayBill(id, name, month, phone, totalprice);
                    MessageBox.Show("Đã lưu và gửi Mail cho khách hàng");
                }
                else
                {
                    MessageBox.Show("Hóa đơn này đã lưu và gửi thông báo rồi!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi");
            }



           
        }
    }
}
