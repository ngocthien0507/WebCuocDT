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
        public int InsertPayBill(HDThanhToan pay) // Thêm Hóa đơn thanh toán 
        {
            db.HDThanhToans.Add(pay);
            db.SaveChanges();
            return pay.idHDTT;
        }
        public int InsertBill(HoaDonCuoc hoadon) // Thêm Hóa đơn cước
        {
            db.HoaDonCuocs.Add(hoadon);
            db.SaveChanges();
            return hoadon.idHD;
        }
        public bool AddCusBill(string name, string CMND, string DiaChi)
        {
            HoaDonDK bill = new HoaDonDK();
            bill.TenKH = name;
            bill.CMND = CMND;
            bill.DiaChi = DiaChi;
            bill.PhiHoaMang = 50000;
            db.HoaDonDKs.Add(bill);
            db.SaveChanges();
            return true;
        }
        public bool SaveCusBill(string name, string address, string CMND)
        {

            HoaDonDK bill = db.HoaDonDKs.Single(a => a.CMND == CMND);
            bill.TenKH = name;
            bill.DiaChi = address;
            bill.CMND = CMND;
            db.SaveChanges();
            return true;
        }
        public bool AddPayBill(int id, string name, int month, string phone, decimal totalprice)
        {
            DateTime date = DateTime.Now;
            HDThanhToan paybill = new HDThanhToan();
            paybill.TenKH = name;
            paybill.SDT = phone;
            paybill.Month = month;
            paybill.NgayThanhToan = date;
            paybill.TongTien = totalprice;
            paybill.Year = date.Year;
            paybill.idHD = id;
            db.HDThanhToans.Add(paybill);
            db.SaveChanges();
            return true;
        }
        // lấy list Hóa đơn
        public List<HoaDonDK> ListAll()
        {
            return db.HoaDonDKs.ToList();
        }
        public BindingSource ListTableBillDK()
        {
            var bill = from p in db.HoaDonDKs select new { ID = p.idHD, Name = p.TenKH, Passport = p.CMND, Address = p.DiaChi, ConnectionFee = p.PhiHoaMang };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }
        public BindingSource ListTableBill()
        {
            var bill = from p in db.HoaDonCuocs select new { ID = p.idHD, Phone = p.SoDT, Status = p.TinhTrang, TotalPrice = p.TongTien, Month = p.Month, Year = p.Year };
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

        public HoaDonCuoc GetBillByIDHD(int id)
        {
            var result = db.HoaDonCuocs.Where(l => l.idHD == id);
            return result.SingleOrDefault();
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

        public BindingSource GetBillByMonth(string thang)
        {
            var bill = from p in db.HoaDonCuocs
                       where p.Month.ToString() == thang
                       select new { ID = p.idHD, Phone = p.SoDT, Status = p.TinhTrang, TotalPrice = p.TongTien, Month = p.Month, Year = p.Year };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }

        public BindingSource GetBillByPhone(string phone)
        {
            var bill = from p in db.HoaDonCuocs
                       where p.SoDT == phone
                       select new { ID = p.idHD, Phone = p.SoDT, Status = p.TinhTrang, TotalPrice = p.TongTien, Month = p.Month, Year = p.Year };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }

        public BindingSource GetBillByAll(string phone, string month)
        {
            var bill = from p in db.HoaDonCuocs
                       where p.SoDT.Contains(phone) && p.Month.ToString() == month
                       select new { ID = p.idHD, Phone = p.SoDT, Status = p.TinhTrang, TotalPrice = p.TongTien, Month = p.Month, Year = p.Year };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }

        public bool SaveStatusBill(int id)
        {
            HoaDonCuoc bill = db.HoaDonCuocs.Single(a => a.idHD == id);
            bill.TinhTrang = 1;
            db.SaveChanges();
            return true;
        }

        public bool DeletePayBill()
        {
            var rows = from all in db.HDThanhToans select all;
            foreach (var row in rows)
            {
                db.HDThanhToans.Remove(row);
            }
            db.SaveChanges();
            return true;
        }

        public bool DeleteBillInf()
        {
            var rows = from all in db.ChiTietHoaDonCuocs select all;
            foreach (var row in rows)
            {
                db.ChiTietHoaDonCuocs.Remove(row);
            }
            db.SaveChanges();
            return true;
        }

        public bool DeleteBill()
        {
            var rows = from all in db.HoaDonCuocs select all;
            foreach (var row in rows)
            {
                db.HoaDonCuocs.Remove(row);
            }
            db.SaveChanges();
            return true;
        }

        public bool AddBillInf(int id,  string phone, DateTime TGBD , DateTime TGKT , decimal? totalprice)
        {
            ChiTietHoaDonCuoc billinf = new ChiTietHoaDonCuoc();
            billinf.idHD = id;
            billinf.SoDT = phone;
            billinf.TGBD = TGBD;
            billinf.TGKT = TGKT;
            billinf.ThanhTien = totalprice;
            db.ChiTietHoaDonCuocs.Add(billinf);
            db.SaveChanges();
            return true;
        }

        public bool AddBill( string phone, decimal? totalprice , int month)
        {
            HoaDonCuoc bill = new HoaDonCuoc();
            bill.SoDT = phone;
            bill.TongTien = totalprice;
            bill.Month = month;
            bill.TinhTrang = 0;
            bill.Year = 2019;
            db.HoaDonCuocs.Add(bill);
            db.SaveChanges();

            return true;
        }
    }
}
