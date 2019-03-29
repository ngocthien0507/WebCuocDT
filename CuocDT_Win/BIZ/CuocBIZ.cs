using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuocDT.Controllers;
using CuocDT.Models;

namespace CuocDT_Win.BIZ
{
    public class CuocBIZ
    {
        public List<LogInfo> ReadDataAll()
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
                TimeSpan TimeCall = item.TGKT.Subtract(item.TGBD);
                item.TotalTimeCall = Convert.ToInt32(TimeCall.TotalMinutes);
                list.Add(item);
            }
            IEnumerable<LogInfo> query = from row in list
                                         orderby row.SoDT
                                         select row;
            return query.ToList();
        }
        public List<LogInfo> ReadDataSDT(string sdt)
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
            IEnumerable<LogInfo> query = from row in list
                                         where row.SoDT == sdt
                                         orderby row.TGBD
                                         select row;
            return query.ToList();
        }
    }
}
