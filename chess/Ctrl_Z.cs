using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class Ctrl_Z
    {
        public Cells Begin;
        public Cells End;
        public bool rok;
        public Form1.rokirovka[] r;
        public string playeer;
        public int w_ochki;
        public int b_ochki;
        public Ctrl_Z(Cells b, Cells e, bool rk, Form1.rokirovka[] rr, string pl, int wo, int bo)
        {
            Begin = new Cells(b);
            End = new Cells(e);
            rok = rk;
            r = new Form1.rokirovka[rr.Length];
            for (int i = 0; i < rr.Length; i++)
            {
                r[i].left.Coord = rr[i].left.Coord;
                r[i].left.flag = rr[i].left.flag;
                r[i].right.Coord = rr[i].right.Coord;
                r[i].right.flag = rr[i].right.flag;

            }
            playeer = pl;
            w_ochki = wo;
            b_ochki = bo;
        }


    }
}
