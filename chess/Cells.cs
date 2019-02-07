using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chess
{
    public class Cells //описываем структуру шахматной доски
    {
        public Figura name_figur; //имя фигуры
        public int positionX;//Координаты по Х
        public int positionY;// Координаты по У
        public int X;
        public int Y;
        public string pos_BorW;//Цвет клетки на которой стоит фигура
        public Cells()
        {
            name_figur = null;
            positionX = -1;
            positionY = -1;
            X = -1;
            Y = -1;
            pos_BorW = null;
        }
        public Cells(Figura f, int posX, int posY, int x, int y, string BorW)
        {
            name_figur = f;
            positionX = posX;
            positionY = posY;
            X = x;
            Y = y;
            pos_BorW = BorW;
        }
        public Cells(Cells c)
        {
            if (c != null)
            {
                if (c.name_figur == null)
                    name_figur = null;
                else
                    name_figur = new Figura(c.name_figur);
                positionX = c.positionX;
                positionY = c.positionY;
                X = c.X;
                Y = c.Y;
                pos_BorW = c.pos_BorW;
            }
        }

    }
}
