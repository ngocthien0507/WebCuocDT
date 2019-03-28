using CuocDT.Models;
using Models.DAO;
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
        // GET: Cuoc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string sdt)
        {
            ReadData(sdt);
            var thuebao = new KhachHangDao().ListTheoSDT(sdt);
            ViewBag.SoDT = thuebao;
            IEnumerable<LogInfo> query = ViewBag.query;
            //Hiển thị kết quả bằng hộp thoại
            return View(query.ToList());
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
            string _filename = @"C:\Users\Thien\source\repos\CuocDT\Log.txt";
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
                if ((item.TGBD > morning && item.TGBD <= night)&&(item.TGKT > morning && item.TGKT <= night))
                {
                    TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                    item.TimeCall7h = Convert.ToInt32(TimeCall.TotalMinutes);
                    item.TimeCall23h = 0;
                }
                else if ((item.TGBD > night || item.TGBD <= morning) && (item.TGKT > night || item.TGKT <= morning))
                {
                    if(item.TGBD <=morning && item.TGKT > night)
                    {
                        item.TimeCall7h = 16 * 60;
                        TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                        int timetemp = Convert.ToInt32(TimeCall.TotalMinutes);
                        item.TimeCall23h = timetemp - item.TimeCall7h;

                    }
                    else
                    {
                        TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                        item.TimeCall23h = Convert.ToInt32(TimeCall.TotalMinutes);
                        item.TimeCall7h = 0;
                    }

                }
                else
                {
                    if (item.TGBD > morning && item.TGBD <= night)
                    {
                        TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                        int timetemp = Convert.ToInt32(TimeCall.TotalMinutes);
                        TimeSpan TimeCallBD = night.Subtract(item.TGBD);
                        item.TimeCall7h = Convert.ToInt32(TimeCallBD.TotalMinutes);
                        item.TimeCall23h = timetemp - item.TimeCall7h;
                    }
                    if(item.TGBD > night || item.TGBD <= morning)
                    {
                        TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                        int timetemp = Convert.ToInt32(TimeCall.TotalMinutes);
                        TimeSpan TimeCallBD = item.TGKT.Subtract(morning);
                        item.TimeCall7h = Convert.ToInt32(TimeCallBD.TotalMinutes);
                        item.TimeCall23h = timetemp - item.TimeCall7h;
                    }
                }


                //   TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                item.TotalTimeCall = item.TimeCall7h + item.TimeCall23h;
                item.ThanhTien =item.TimeCall7h * 200 + item.TimeCall23h * 150;
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