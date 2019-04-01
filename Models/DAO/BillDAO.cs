using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Models.DAO
{
    public class BillDAO
    {
        CuocDbContext db = null;
        public BillDAO()
        {
            db = new CuocDbContext();
        }

        // lấy list Hóa đơn
        public List<HoaDonDK> ListAll()
        {
            return db.HoaDonDKs.ToList();
        }
        public BindingSource ListTableBillDK()
        { 
             var bill = from p in db.HoaDonDKs select new {ID = p.idHD, Name = p.TenKH,Passport = p.CMND,Address = p.DiaChi, ConnectionFee = p.PhiHoaMang };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }
        public BindingSource ListTableBill()
        {
            var bill = from p in db.HoaDonCuocs select new { ID =  p.idHD, Phone = p.SoDT, TotalPrice = p.TongTien, Month= p.Month , Year = p.Year };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }
        public List<HoaDonCuoc> ListBySDT(string sdt)
        {
            if (sdt == "")
            {
                sdt = "eror404";
            }
            var model = db.HoaDonCuocs.Where(x => x.SoDT.Contains(sdt));
            return model.ToList();
        }
        public List<HoaDonCuoc> GetAll()
        {
            return db.HoaDonCuocs.ToList();
        }
    }
}
