using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Models.DAO
{
    public class SimDAO
    {
        CuocDbContext db = null;
        public SimDAO()
        {
            db = new CuocDbContext();
        }

        // lấy list khách hàng
        public List<SIM> ListAll()
        {
            return db.SIMs.ToList();
        }
        public List<SIM> GetPhone()
        {

            var result = from all in db.SIMs
                         where all.TinhTrang == 1
                         select all;
            return result.ToList();
                   
                   
        }

        public BindingSource GetListSim()
        {
            var bill = from p in db.SIMs
                       where p.TinhTrang == 0
                       select new { Phone = p.idSim , Status = p.TinhTrang };
            BindingSource result = new BindingSource();
            result.DataSource = bill.ToList();
            return result;
        }
        public bool SaveCusSim(string name, string phone)
        {
            DateTime date = DateTime.Now;
            SIM sim = db.SIMs.Single(a => a.idSim == phone.ToString());
            sim.idSim = phone;
            sim.ChuSoHuu = name;
            sim.NgayKichHoat = date;
            sim.TinhTrang = 1;
            db.SaveChanges();
            return true;
        }
        public bool AddSim(string phone)
        {
            SIM sim = new SIM();
            sim.idSim = phone;
            sim.TinhTrang = 0;
            db.SIMs.Add(sim);
            db.SaveChanges();
            return true;
        }
    }
}
