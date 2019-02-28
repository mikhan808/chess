using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class DostupnyyHod
    {
        public Cells cells;
        public bool rokirovka;
        public Cells ladyaBegin;
        public Cells ladyaEnd;
        public DostupnyyHod(Cells cells)
        {
            rokirovka = false;
            this.cells = cells;
        }
        public DostupnyyHod(Cells cells,Cells ladyaBegin,Cells ladyaEnd)
        {
            rokirovka = true;
            this.cells = cells;
            this.ladyaBegin = ladyaBegin;
            this.ladyaEnd = ladyaEnd;
        }
        public static bool Contains(List<DostupnyyHod> list,Cells cells)
        {
            foreach(DostupnyyHod dh in list)
            {
                if (dh.cells == cells)
                    return true;
            
            }
            return false;
        }
        public static DostupnyyHod getOfCells(List<DostupnyyHod> list, Cells cells)
        {
            foreach (DostupnyyHod dh in list)
            {
                if (dh.cells == cells)
                    return dh;

            }
            return null;
        }
    }
}
