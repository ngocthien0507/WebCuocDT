using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Models.DAO
{
    public class KhachHangDao
    {

        CuocDbContext db = null;
        public KhachHangDao()
        {
            db = new CuocDbContext();
        }

        // lấy list khách hàng
        public List<KhachHang> ListAll()
        {
            return db.KhachHangs.ToList();
        }
        public BindingSource GetAllCus()
        {
            var Cus = from p in db.KhachHangs select new { ID = p.idKhachHang , Name = p.TenKH , Passport = p.CMND , Job = p.NgheNghiep , Address = p.DiaChi , Phone = p.SoDT, Email = p.Email  };
            BindingSource result = new BindingSource();
            result.DataSource = Cus.ToList();
            return result;
        }
        // lấy list theo sdt
        public KhachHang ListTheoSDT(string sdt)
        {
            if(sdt == "")
            {
                sdt = "eror404";
            }
            var model = db.KhachHangs.Where(x => x.SoDT.Contains(sdt));
            return model.SingleOrDefault();
        }
        public bool SaveCus(int id, string name, string address, string job, string CMND, string sodt, string email)
        {

            KhachHang Cus = db.KhachHangs.Single(a => a.idKhachHang == id);
            Cus.idKhachHang = id;
            Cus.TenKH = name;
            Cus.DiaChi = address;
            Cus.NgheNghiep = job;
            Cus.CMND = CMND;
            Cus.SoDT = sodt;
            Cus.Email = email;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch { return false; }

        }
        public bool AddCus(int id, string name, string address, string job, string CMND, string sodt, string email)
        {

            KhachHang Cus = new KhachHang();
            Cus.idKhachHang = id;
            Cus.TenKH = name;
            Cus.DiaChi = address;
            Cus.NgheNghiep = job;
            Cus.CMND = CMND;
            Cus.SoDT = sodt;
            Cus.Email = email;
            db.KhachHangs.Add(Cus);
            db.SaveChanges();
            return true;
        }

    }
}
