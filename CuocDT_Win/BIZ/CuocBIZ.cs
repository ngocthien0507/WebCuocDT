using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuocDT.Controllers;
using CuocDT.Models;
using Models.DAO;
using Models.EF;

namespace CuocDT_Win.BIZ
{
    public class CuocBIZ
    {
        public CuocDbContext db = new CuocDbContext();
        public List<LogInfo> BagList = new List<LogInfo>();
        public List<LogInfo> BagListBySDT = new List<LogInfo>();
        public List<LogInfo> ListAll()
        {
            return ReadData();
        }
        public List<LogInfo> ListBySDT(string sdt)
        {
            IEnumerable<LogInfo> query = from row in ListAll()
                                         where row.SoDT.Equals(sdt)
                                         orderby row.TGBD
                                         select row;
            BagListBySDT = query.ToList();
            return query.ToList();

        }

        public decimal? TinhCuocThang(int thang, string SDT)
        {
            ListBySDT(SDT);

            int Time7h = 0;
            int Time23h = 0;
            decimal? CostMonth = 50000;
            
            foreach (var item in BagListBySDT)
            {
                if (item.TGBD.Month == thang)
                {
                    Time7h += item.TimeCall7h;
                    Time23h += item.TimeCall23h;
                    CostMonth += item.ThanhTien;
                }
            }

            return CostMonth;
        }

        public List<LogInfo> ReadData()
        {
            string _filename = @"C:\Users\Thien\source\repos\WebCuocDT-master\Log.txt";
            string[] words;
            words = System.IO.File.ReadAllLines(_filename, Encoding.Unicode);
            //Encoding.Default: đọc theo mã mặc định của file text
            // đọc file
            int limit = words.Length;
            var list = new List<LogInfo>();
            //Chạy từng dòng trong file text
            for (int i = 0; i < limit - 1; i++)
            {
                string[] str = words[i + 1].Split('\t');
                var item = new LogInfo();
                item.SoDT = str[0].ToString();
                item.TGBD = DateTime.ParseExact(str[1], "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                item.TGKT = DateTime.ParseExact(str[2], "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string[] strmini = str[1].Split(' ');
                item.StartDate = strmini[0].ToString();
                string timeMorning = item.StartDate + " " + "07:00:00";
                string timeNight = item.StartDate + " " + "23:00:00";
                DateTime morning = DateTime.ParseExact(timeMorning, "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime night = DateTime.ParseExact(timeNight, "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                item.ThanhTien = 0;
                item.TimeCall23h = 0;
                item.TimeCall7h = 0;
                DateTime CheckDate = item.TGBD;
                while (CheckDate <= item.TGKT)
                {
                    if (CheckDate.Date > morning.Date)
                    {
                        morning = morning.AddDays(1);
                        night = night.AddDays(1);
                    }
                    CheckDate = CheckDate.AddMinutes(1);
                    if (CheckDate <= morning)
                    {
                        item.ThanhTien += 150;
                        item.TimeCall23h += 1;
                    }
                    else if (CheckDate > morning && CheckDate <= night)
                    {
                        item.ThanhTien += 200;
                        item.TimeCall7h += 1;
                    }
                    else
                    {
                        item.ThanhTien += 150;
                        item.TimeCall23h += 1;
                    }
                }
                item.TotalTimeCall = item.TimeCall23h + item.TimeCall7h;
                list.Add(item);
            }
            BagList = list;
            return list;
        }

        public bool AddSim(string phone)
        {
            SimDAO sim = new SimDAO();
            return sim.AddSim(phone) ;
        }

        public void AddData()
        {
            
            BillBIZ bills = new BillBIZ();

            SimDAO sim = new SimDAO();

            DateTime date = DateTime.Now;
            int thang = date.Month;

            foreach (var item in sim.GetPhone())
            {
                var MonthStart = (DateTime)item.NgayKichHoat;
                for (int i = MonthStart.Month; i < thang; i++)
                {
                    string phone = item.idSim;
                    decimal? totalprice = TinhCuocThang(i, item.idSim); 
                    int month = i;
                    bills.AddBill(phone, totalprice, month);

                    var ListBillInf = from all in ListAll()
                                   where all.SoDT == item.idSim
                                   where all.TGBD.Month == i
                                   select all;
                    foreach (var items in ListBillInf)
                    {

                        int id = db.HoaDonCuocs.Select(g => g.idHD).DefaultIfEmpty(0).Max();
                        DateTime TGBD = items.TGBD;
                        DateTime TGKT = items.TGKT;
                        decimal? totalPrice = items.ThanhTien;
                        bills.AddBillInf(id, phone, TGBD, TGKT, totalPrice);

                    }
                }
            }

        }
    }
}