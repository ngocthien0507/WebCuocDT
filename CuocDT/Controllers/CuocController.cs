using CuocDT.Models;
using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CuocDT.Controllers
{
    public class CuocController : Controller
    {
        CuocDbContext db = new CuocDbContext();
        // GET: Cuoc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string sdt)
        {
            ReadData(sdt);
            var HoaDonbySDT = new BillDAO().ListBySDT(sdt);
            ViewBag.HoaDonBySDT = HoaDonbySDT;
            var thuebao = new KhachHangDao().ListTheoSDT(sdt);
            ViewBag.SoDT = thuebao;
            IEnumerable<LogInfo> query = ViewBag.query;
            //Hiển thị kết quả bằng hộp thoại
            return View(query.ToList());
        }
        public ActionResult Xemchitiet(int Month , string sdt)
        {
            ReadData(sdt);
            ViewBag.SDT = sdt;
            ViewBag.Month = Month;
            IEnumerable<LogInfo> query = ViewBag.query;
            var ListChiTiet = query.ToList();
            IEnumerable<LogInfo> querymini = from row in ListChiTiet
                                             where row.TGBD.Month ==  Month
                                             orderby row.TGBD
                                             select row;
            return View(querymini.ToList());
        }

        public JsonResult CheckSDT(string sdt)
        {
            var row = new KhachHangDao().ListTheoSDT(sdt);
            if (row == null)
            {
                return Json(new
                {
                    val = sdt,
                    status = false
                });
            }
            return Json(new
            {
                val = sdt,
                status = true
            });
        }
        

        public void ReadData(string sdt)
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
                    if(CheckDate.Date > morning.Date)
                    {
                        morning = morning.AddDays(1);
                        night = night.AddDays(1);
                    }
                    CheckDate = CheckDate.AddMinutes(1);
                    if(CheckDate <= morning )
                    {
                        item.ThanhTien += 150;
                        item.TimeCall23h += 1;
                    }
                    else if(CheckDate > morning && CheckDate <=night)
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
            IEnumerable<LogInfo> query = from row in list
                                         where row.SoDT == sdt
                                         orderby row.TGBD
                                         select row;
            ViewBag.Query = query;
        }
    }
}