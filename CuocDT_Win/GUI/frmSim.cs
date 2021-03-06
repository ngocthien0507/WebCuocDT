﻿using CuocDT_Win.BIZ;
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
    public partial class frmSim : Form
    {
        public CustomerBIZ cus = new CustomerBIZ();
        public BillBIZ bill = new BillBIZ();
        public frmSim()
        {


            InitializeComponent();
            load();
        }
        void load()
        {
            SimDAO sim = new SimDAO();
            dataGridView1.DataSource = sim.GetListSim();

            textBox1.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Phone"));
        }
        public frmSim(int id, string name, string address, string job, string CMND, string email) : this()
        {

            txtName.Text = name;
            txtAddress.Text = address;
            txtPass.Text = CMND;
            txtPay.Text = " 50.000 VNĐ";
            textBox2.Text = job;
            textBox3.Text = id.ToString();
            textBox4.Text = email;


        }

        private void button1_Click(object sender, EventArgs e) // Đăng kí Sim
        {

            string name = txtName.Text;
            string address = txtAddress.Text;
            string CMND = txtPass.Text;
            string phone = textBox1.Text;
            string job = textBox2.Text;
            string email = textBox4.Text;


            int id = int.Parse(textBox3.Text);
            if (cus.AddCus(id, name, address, job, CMND, phone, email) == true && cus.SaveCusSim(name, phone) == true && bill.AddCusBill(name, CMND, address) == true)
            {
                MessageBox.Show(" Thêm dữ liệu thành công");
                txtName.Text = "";
                txtAddress.Text = "";
                CMND = txtPass.Text = "";
                phone = textBox1.Text = "";
                job = textBox2.Text = "";
                email = textBox4.Text = "";
            }
            else
            {
                MessageBox.Show(" Lưu thất bại ");
            }




        }


    }
}
