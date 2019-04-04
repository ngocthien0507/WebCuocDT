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

        public bool Insert(ChiTietHoaDonCuoc chitiet)
        {
            try
            {
                db.ChiTietHoaDonCuocs.Add(chitiet);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ChiTietHoaDonCuoc> ListAll()
        {
            return db.ChiTietHoaDonCuocs.ToList();
        }
    }
}
