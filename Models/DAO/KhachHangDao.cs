using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // lấy list theo sdt
        public KhachHang ListTheoSDT(string sdt)
        {
            var model = db.KhachHangs.Where(x => x.SoDT.Contains(sdt));
            return model.SingleOrDefault();
        }
    }
}
