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
            var Cus = from p in db.KhachHangs select new { ID = p.idKhachHang , Name = p.TenKH , Passport = p.CMND , Job = p.NgheNghiep , Address = p.DiaChi , Phone = p.SoDT  };
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
    }
}
