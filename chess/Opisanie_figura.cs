using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class Opisanie_figura
    {
        public string name;
        public int ochki;
        public Opisanie_figura(string imya, int ochko)
        {

            name = imya;
            ochki = ochko;
        }
        public Opisanie_figura(Opisanie_figura fig)
        {
            if (fig != null)
            {
                name = fig.name;
                ochki = fig.ochki;
            }

        }
        public bool Compar(Opisanie_figura f)
        {
            if (f == null)
                return false;
            else return (name == f.name);
        }
    }
}
