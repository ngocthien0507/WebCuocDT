using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    class RandomDateTime
    {
        DateTime start;
        Random gen;
        int range;

        public RandomDateTime()
        {
            start = new DateTime(2019, 1, 1, 0, 0, 0);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
        public DateTime NextKT(DateTime DS)
        {
            return DS.AddSeconds(gen.Next(30, 2000));
        }
    }
}
