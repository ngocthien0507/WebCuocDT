using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ChiTietHoaDonDAO
    {
        CuocDbContext db = null;
        public ChiTietHoaDonDAO()
        {
            db = new CuocDbContext();
        }
        public List<ChiTietHoaDonCuoc> ListAll()
        {
            return db.ChiTietHoaDonCuocs.ToList();
        }
    }
}
