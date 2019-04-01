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
        CuocDbContext db = new CuocDbContext();
        KhachHangDao cus = new KhachHangDao();
        public bool  GetAll()
        {           
           var Customer  = cus.ListAll();
           IEnumerable<KhachHang> query = from all in Customer select all ;
           return true;
        }
        public bool Save(int id , string name , string address , string job , string CMND , string sodt)
        {
          
            KhachHang Cus = db.KhachHangs.Single(a => a.idKhachHang == id);
            Cus.idKhachHang = id;
            Cus.TenKH = name;
            Cus.DiaChi = address;
            Cus.NgheNghiep = job;
            Cus.CMND = CMND;
            Cus.SoDT = sodt;
            db.SaveChanges();
            return true;
        }
        public bool SaveBill( string name, string address, string CMND)
        {

            HoaDonDK bill = db.HoaDonDKs.Single(a => a.CMND ==CMND );
            bill.TenKH = name;
            bill.DiaChi = address;
            bill.CMND = CMND;
            db.SaveChanges();
            return true;
        }

        public bool Add(int id, string name, string address, string job, string CMND , string sodt)
        {
           
            KhachHang Cus = new KhachHang();
            Cus.idKhachHang = id;
            Cus.TenKH = name;
            Cus.DiaChi = address;
            Cus.NgheNghiep = job;
            Cus.CMND = CMND;
            Cus.SoDT = sodt;
            db.KhachHangs.Add(Cus);
            return true;
        }
        public bool SaveSim( string name, string sodt)
        {
            DateTime date = DateTime.Now;
            SIM sim = db.SIMs.Single(a => a.idSim == sodt.ToString());
            sim.idSim = sodt;
            sim.ChuSoHuu = name;
            sim.NgayKichHoat = date;
            sim.TinhTrang = 1;
            db.SaveChanges();
            return true;
        }
        public bool AddSimBill(string name, string CMND , string DiaChi)
        {
            DateTime date = DateTime.Now;
            HoaDonDK bill = new HoaDonDK();
            bill.TenKH = name;
            bill.CMND = CMND;
            bill.DiaChi = DiaChi;
            bill.PhiHoaMang = 50000;
            db.HoaDonDKs.Add(bill);
            db.SaveChanges();
            return true;
        }

    }
}
