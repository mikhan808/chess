using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class Figura
    {
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
            figury = new Figura[2, 6];
            for (int i = 0; i < 2; i++)
                for (int g = 0; g < 6; g++)
                {
                    Opisanie_figura fig;
                    switch (g)
                    {
                        case 0:
                            fig = new Opisanie_figura("Пешка", 1);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new Figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new Figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 1:
                            fig = new Opisanie_figura("Ладья", 5);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new Figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new Figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 2:
                            fig = new Opisanie_figura("Конь", 3);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new Figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new Figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 3:
                            fig = new Opisanie_figura("Слон", 3);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new Figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new Figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 4:
                            fig = new Opisanie_figura("Ферзь", 10);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new Figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new Figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 5:
                            fig = new Opisanie_figura("Король", 0);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new Figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new Figura("Черный", fig);
                                    break;
                            }
                            break;
                    }
                }
        }
    }
}
