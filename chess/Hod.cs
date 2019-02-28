using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class Hod
    {
        public Cells begin;
        public DostupnyyHod hod;
        public Hod(Cells begin,DostupnyyHod hod)
        {
            this.begin = begin;
            this.hod = hod;
        }
    }
}
