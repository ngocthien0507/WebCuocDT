using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuocDT_Win.BIZ
{
    public class CustomerBIZ
    {
        public CuocDbContext db = new CuocDbContext();
        KhachHangDao cus = new KhachHangDao();
        BillDAO bill = new BillDAO();
        SimDAO sim = new SimDAO();
        public bool  GetAll()
        {           
           var Customer  = cus.ListAll();
           IEnumerable<KhachHang> query = from all in Customer select all ;
           return true;
        }

        public bool SaveCus(int id , string name , string address , string job , string passport, string phone, string email)
        {
            return cus.SaveCus(id, name, address, job, passport, phone , email);
        }

        public bool AddCus(int id, string name, string address, string job, string passport, string phone, string email)
        {           
            return cus.AddCus(id, name, address,job, passport , phone, email);
        }

        public bool SaveCusSim( string name, string phone)
        {            
            return sim.SaveCusSim(name, phone);
        }

        

    }
}
