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

        public BindingSource GetBillByMonth(string thang)
        {
            var bill = from p in db.HoaDonCuocs
                       where p.Month.ToString() == thang
                       select new { ID = p.idHD, Phone = p.SoDT, TotalPrice = p.TongTien, Month = p.Month, Year = p.Year };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }
        public BindingSource GetBillByPass(string pass)
        {
            var bill = from p in db.HoaDonDKs
                       where p.CMND.Contains(pass)
                       select new { ID = p.idHD, Name = p.TenKH, Passport = p.CMND, Address = p.DiaChi, ConnectionFee = p.PhiHoaMang };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }

    }
}
