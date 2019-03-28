using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuocDT.Models
{
    public class LogInfo
    {
        public string SoDT { get; set; }
        public DateTime TGBD { get; set; }
        public DateTime TGKT { get; set; }
        public int TotalTimeCall { get; set; }
        public string StartDate { get; set; }
        public int TimeCall7h { get; set; }
        public int TimeCall23h { get; set; }
        public decimal? ThanhTien { get; set; }
    }
}