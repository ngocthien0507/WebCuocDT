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
    public partial class frmCustomer : Form
    {
        public BillBIZ bill = new BillBIZ();
        public CustomerBIZ cus = new CustomerBIZ();
        KhachHangDao a = new KhachHangDao();
        public frmCustomer()
        {
            InitializeComponent();
            dtgvCus.DataSource = a.GetAllCus();
            AddBinding();

        }

        void AddBinding()
        {
            txtName.DataBindings.Add(new Binding("Text", dtgvCus.DataSource, "Name"));
            txtJob.DataBindings.Add(new Binding("Text", dtgvCus.DataSource, "Job"));
            txtPass.DataBindings.Add(new Binding("Text", dtgvCus.DataSource, "Passport"));
            txtPhone.DataBindings.Add(new Binding("Text", dtgvCus.DataSource, "Phone"));
            txtAddress.DataBindings.Add(new Binding("Text", dtgvCus.DataSource, "Address"));
        }

        private void bntSave_Click(object sender, EventArgs e) // Save Thông tinh khách
        {
            int id = int.Parse(dtgvCus.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            string name = txtName.Text;
            string address = txtAddress.Text;
            string CMND = txtPass.Text;
            string phone = txtPhone.Text;
            string job = txtJob.Text;

            if (cus.SaveCus(id, name, address, job, CMND , phone) == true && bill.SaveCusBill(name , address , CMND) == true)
            {
                MessageBox.Show(" Thay đổi dữ liệu thành công");
                dtgvCus.DataSource = a.GetAllCus();
            }
            else
            {
                MessageBox.Show(" Lưu thất bại ");
            }
            
        }

        private void button1_Click(object sender, EventArgs e) // Đăng kí Cus
        {
            int id = int.Parse(dtgvCus.SelectedCells[0].OwningRow.Cells["ID"].Value.ToString());
            string name = textBox2.Text;
            string address = textBox1.Text;
            string CMND = textBox4.Text;
            string job = textBox5.Text;
            if (  name == "" || address == "" || CMND =="" || job =="" )
            {
                MessageBox.Show("Vui lòng điền thông tin");
            }
            else
            {

                frmHome.fr.test(id, name, address, job, CMND);

            }
            

        }
    }
}
