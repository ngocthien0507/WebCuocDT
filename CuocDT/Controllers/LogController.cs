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
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GhiLog()
        {
            FileText ft = new FileText();
            ft.FileName = @"C:\Users\Thien\source\repos\WebCuocDT-master\Log.txt";
            ft.WriteData();
            return Json(new
            {
                status = true
            });
        }

        public ActionResult SuccessGhiLog()
        {
            return View();
        }

        public ActionResult LogData()
        {
            ReadData();
            IEnumerable<LogInfo> query = ViewBag.query;
            //Hiển thị kết quả bằng hộp thoại
            return View(query.ToList());
        }

        public void ReadData()
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
                TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                item.TotalTimeCall = Convert.ToInt32(TimeCall.TotalMinutes);
                list.Add(item);
            }
            IEnumerable<LogInfo> query = from row in list
                                         orderby row.SoDT
                                         select row;
            ViewBag.Query = query;
        }

    }
}