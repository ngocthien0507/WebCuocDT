using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuocDT_Win.BIZ
{
    public class BillBIZ
    {
        CuocDbContext db = new CuocDbContext();
        BillDAO bill = new BillDAO();
        public bool GetAll()
        {
            var Bill = bill.ListAll();
            IEnumerable<HoaDonDK> query = from all in Bill select all;
            return true;
        }

        public List<HoaDonCuoc> load(string thang)
        {
            
            var    HoaDonCuoc = db.HoaDonCuocs.Where(a => a.Month.ToString() == thang.ToString()).ToList();

            return HoaDonCuoc;
        }

        public BindingSource SearchByMonth(string thang)
        {
          
            return bill.GetBillByMonth(thang);
        }
        public BindingSource SearchAll(string phone , string month)
        {

            return bill.GetBillByAll(phone , month); 
        }
        public BindingSource SearchByPhone(string phone)// chauw
        {

            return bill.GetBillByPhone(phone);
        }
        public BindingSource SearchByPass(string pass)
        {
            return bill.GetBillByPass(pass);
        }
        public HoaDonCuoc GetBillByIDHD(int id)
        {

            return bill.GetBillByIDHD(id);
        }
        public bool SaveStatus(int id ) 
        {
            return bill.SaveStatusBill(id);
        }
        #region // Add
        public bool AddPayBill(int id, string name, int month, string phone, decimal totalprice)
        {
            return bill.AddPayBill(id, name, month, phone, totalprice);
        }

        public bool AddBillInf(int id, string phone, DateTime TGBD, DateTime TGKT, decimal? totalprice)
        {

            return bill.AddBillInf(id, phone, TGBD, TGKT, totalprice);
        }
        public bool AddBill(string phone, decimal? totalprice, int month)
        {

            return bill.AddBill(phone, totalprice, month);
        }
        #endregion
        #region // Save
        public bool AddCusBill(string name, string passport, string address)
        {
            return bill.AddCusBill(name, passport, address);
        }

        public bool SaveCusBill(string name, string address, string passport)
        {
            return bill.SaveCusBill(name, address, passport);
        }
        #endregion
        #region // delete
        public bool DeletePayBill()
        {
            return bill.DeletePayBill();
        }
        public bool DeleteBill()
        {
            return bill.DeleteBill();
        }
        public bool DeleteBillInf()
        {
            return bill.DeleteBillInf();
        }
        #endregion
    }
}
