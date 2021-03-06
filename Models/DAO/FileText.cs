﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class FileText
    {
        private String _fileName;
        public string FileName
        {
            set
            {
                _fileName = value;
            }
            get
            {
                return _fileName;
            }
        }

        private System.IO.FileStream fs;
        // ghi file
        public void WriteData()
        {
            //goi ham random datetime
            
            //ghi file
            fs = new System.IO.FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter wt = new StreamWriter(fs, Encoding.Unicode);
            var model = new SimDAO().GetPhone();
            int limit = 100 / model.Count; // limit de duoc 100 log
            // tựa đề log
            wt.Write("Số điện thoại"); wt.Write("\t");
            wt.Write("TGBD"); wt.Write("\t\t\t");
            wt.Write("TGKT"); wt.WriteLine();
            // nội dung log
            foreach (var item in model)
            {
                RandomDateTime date = new RandomDateTime((DateTime)item.NgayKichHoat);
                for (int i = 0; i < limit; i++)
                {
                    DateTime TGBD = date.Next();
                    DateTime TGKT = date.NextKT(TGBD);
                    wt.Write(item.idSim); wt.Write("\t");
                    wt.Write("{0:d/M/yyyy HH:mm:ss}", TGBD); wt.Write("\t");
                    wt.Write("{0:d/M/yyyy HH:mm:ss}", TGKT); wt.WriteLine();
                }
            }
            wt.Flush();
            wt.Close();
            fs.Close();
        }
    }
}
