using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class Figura
    {
        public const int WHITE = 0;
        public const int BLACK = 1;
        public const int PESHKA = 0;
        public const int LADYA = 1;
        public const int KON = 2;
        public const int SLON = 3;
        public const int FERZ = 4;
        public const int KOROL = 5;
        public const string PESHKA_NAME = "Пешка";
        public const string LADYA_NAME = "Ладья";
        public const string KON_NAME = "Конь";
        public const string SLON_NAME = "Слон";
        public const string FERZ_NAME = "Ферзь";
        public const string KOROL_NAME = "Король";
        public static string[] COLOR = { "Белый", "Черный" };
        public static string[] NAME_FIGURY = { PESHKA_NAME, LADYA_NAME, KON_NAME, SLON_NAME, FERZ_NAME, KOROL_NAME };
        public static int[] OCHKI_FIGURY = { 1, 5, 3, 3, 10, 0};
        public string BorW;
        public Opisanie_figura name;
        public static Figura[,] figury;
        public Figura(string cvet, Opisanie_figura imya)
        {
            BorW = cvet;
            name = new Opisanie_figura(imya);

        }
        public Figura(Figura fig)
        {
            if (fig != null)
            {
                BorW = fig.BorW;
                name = new Opisanie_figura(fig.name);
            }

        }
        public bool Compar(Figura f)
        {
            if (f == null)
                return false;
            else return (BorW == f.BorW && name.Compar(f.name));
        }
        public static void init_figurs()
        {
            figury = new Figura[COLOR.Length, NAME_FIGURY.Length];
            for (int i = 0; i < COLOR.Length; i++)
                for (int g = 0; g < NAME_FIGURY.Length; g++)
                {
                    Opisanie_figura fig = new Opisanie_figura(NAME_FIGURY[g], OCHKI_FIGURY[g]);
                    figury[i, g] = new Figura(COLOR[i], fig);
                }
        }
    }
}
