using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Resources;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections;

namespace chess
{
    public partial class Form1 : Form
    {
        figura[,] figury;
        Coordinata W_povtor;
        Coordinata B_povtor;
        ArrayList W_povtory = new ArrayList();
        ArrayList B_povtory = new ArrayList();
        int W_repeat = 0;
        int B_repeat = 0;
        int W_ochki = 0;
        int B_ochki = 0;
        int tik = 0;
        public Form1()
        {
            InitializeComponent();//Расставляем итемы по форме
            zapolnenie();
            W_povtor = new Coordinata(new Point(-1, -1), new Point(-1, -1));
            B_povtor = new Coordinata(new Point(-1, -1), new Point(-1, -1));

        }
        void zapolnenie()
        {
            figury = new figura[2, 6];
            for (int i = 0; i < 2; i++)
                for (int g = 0; g < 6; g++)
                {
                    figura_otd fig;
                    switch (g)
                    {
                        case 0:
                            fig = new figura_otd("Пешка", 1);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 1:
                            fig = new figura_otd("Ладья", 5);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 2:
                            fig = new figura_otd("Конь", 3);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 3:
                            fig = new figura_otd("Слон", 3);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 4:
                            fig = new figura_otd("Ферзь", 10);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new figura("Черный", fig);
                                    break;
                            }
                            break;
                        case 5:
                            fig = new figura_otd("Король", 0);
                            switch (i)
                            {
                                case 0:
                                    figury[i, g] = new figura("Белый", fig);
                                    break;
                                case 1:
                                    figury[i, g] = new figura("Черный", fig);
                                    break;
                            }
                            break;
                    }
                }
        }
        ArrayList allow_pole = new ArrayList();
        public struct rokirovka
        {
            public roki right;
            public roki left;
        }
        public struct roki
        {
            public bool flag;
            public Point Coord;
        }
        rokirovka[] rok;
        int full_ochki = 0;

        private static cells[,] pole; //выделяем память для структуры
        private static Point position; //тут будут находится координаты выбранной для хода фигуры
        private static bool stat = false; //переменная состояния
        private static int daeth_white = 0, daeth_black = 0;//счетчики срубленных фигур
        private static string player = "Белый";// если тру то ходит белый
        private void fill_figurs(bool flag) //расставляем фиогуры на поле
        {
            if (flag) //если flag = true расставляем черные фигуры
            {
                rok[1].left.flag = true;
                rok[1].left.Coord = new Point(2, 0);
                rok[1].right.flag = true;
                rok[1].right.Coord = new Point(6, 0);
                pole[0, 0].name_figur = figury[1, 1]; //Каждой фигуре присваеваем имя..
                pole[1, 0].name_figur = figury[1, 2];
                pole[2, 0].name_figur = figury[1, 3];
                pole[3, 0].name_figur = figury[1, 4];
                pole[4, 0].name_figur = figury[1, 5];
                pole[5, 0].name_figur = figury[1, 3];
                pole[6, 0].name_figur = figury[1, 2];
                pole[7, 0].name_figur = figury[1, 1];
                for (int i = 0; i < 8; i++)
                {
                    pole[i, 1].name_figur = figury[1, 0];
                }
            }
            else//белые
            {
                rok[0].left.flag = true;
                rok[0].left.Coord = new Point(2, 7);
                rok[0].right.flag = true;
                rok[0].right.Coord = new Point(6, 7);
                pole[0, 7].name_figur = figury[0, 1]; //Каждой фигуре присваеваем имя..
                pole[1, 7].name_figur = figury[0, 2];
                pole[2, 7].name_figur = (figury[0, 3]);
                pole[3, 7].name_figur = (figury[0, 4]);
                pole[4, 7].name_figur = (figury[0, 5]);
                pole[5, 7].name_figur = (figury[0, 3]);
                pole[6, 7].name_figur = (figury[0, 2]);
                pole[7, 7].name_figur = (figury[0, 1]);
                for (int i = 0; i < 8; i++)
                {
                    pole[i, 6].name_figur = (figury[0, 0]);
                }
            }
        }
        private void start()//Начальная расстановка
        {
            W_ochki = 0;
            B_ochki = 0;
            listBox1.Items.Clear();
            pole = new cells[8, 8]; //pole[x,y] //Выделяем динамическую память для шахматной доски
            rok = new rokirovka[2];
            for (int y = 0; y < 8; y++) //тут идет заполнение поля черными и белыми клетками
            {
                for (int x = 0; x < 8; x++)
                {
                    pole[x, y] = new cells();
                }
            }
            //На самом деле это просто матрица(массив) из структур типа cells размером 8 на 8
            fill_figurs(true); //Вызываем функцию расстановки для черных фигур
            fill_figurs(false);// для белых фигур
            int count = 1; //с помощью этой переменной будем определять какого цвета клетка
            int coordinataX = 0, coordinataY = 0; //Задаем начальные значения
            for (int y = 0; y < 8; y++) //тут идет заполнение поля черными и белыми клетками
            {
                for (int x = 0; x < 8; x++)
                {
                    if (pole[x, y].name_figur != null)
                    {
                        if (pole[x, y].name_figur.BorW == "Белый")
                        {
                            W_ochki += pole[x, y].name_figur.name.ochki;
                        }
                        else
                        {
                            B_ochki += pole[x, y].name_figur.name.ochki;
                        }
                    }
                    full_ochki = W_ochki;

                    //pole[x, y] = new cells();
                    if (count % 2 > 0)//если нечетное
                        pole[x, y].pos_BorW = "White"; //то белое
                    else
                        pole[x, y].pos_BorW = "Black"; // если четное то черное
                    count++; //плюсуем счетчик
                    pole[x, y].X = x;
                    pole[x, y].Y = y;
                    pole[x, y].positionX = coordinataX;// устанавливаем координаты для где должна находиться фигура
                    pole[x, y].positionY = coordinataY;//на поле
                    coordinataX += 30; //рисунки которые загружаются - это квадратики 30х30 поэтому +30
                }
                count++;
                coordinataY += 30;
                coordinataX = 0; //обнуляем счетчик для х
            }
        }
        private void drawing() //Функция которая будет поочередно перебирать элементы матриицы
        // и в зависимости от параметров и координат элемента будет 
        //загружать в память картинку с соответствующим изображением и
        //рисовать его на заданной графической области
        {
            Graphics board = this.panel1.CreateGraphics(); //обьявляем графическую область
            Bitmap white_check = new Bitmap(@"figurs\БелыйКлетка.bmp"); //загружаем картинки пустых клеток
            Bitmap black_check = new Bitmap(@"figurs\ЧерныйКлетка.bmp");
            Bitmap temp;//переменная для временного хранения загруженных картинок
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                    if (pole[x, y].name_figur != null) //если поле name элемента не пустое значит это какаято фигура
                    {//загружаем клетку с соответствующей фигурой
                        temp = new Bitmap(@"figurs\" + pole[x, y].name_figur.BorW + pole[x, y].name_figur.name.name + "На" + pole[x, y].pos_BorW + ".bmp");
                        board.DrawImage(temp, pole[x, y].positionX, pole[x, y].positionY); //рисуем
                        temp.Dispose();//освобождаем временную переменную
                    }
                    else //если поле name элемента пустое значит это просто клетка
                    {
                        if (pole[x, y].pos_BorW == "White") //Определяем цвет клетки
                            board.DrawImage(white_check, pole[x, y].positionX, pole[x, y].positionY); //рисуем
                        else
                            board.DrawImage(black_check, pole[x, y].positionX, pole[x, y].positionY);
                    }
            white_check.Dispose(); //освобождаем занимаемую память
            black_check.Dispose();
            board.Dispose();
        } //draw()
        private void drawing_hod()
        {
            Graphics board = this.panel1.CreateGraphics(); //обьявляем графическую область
            Bitmap white_check = new Bitmap(@"figurs\БелыйКлеткаВыд.bmp"); //загружаем картинки пустых клеток
            Bitmap black_check = new Bitmap(@"figurs\ЧерныйКлеткаВыд.bmp");
            Bitmap temp;//переменная для временного хранения загруженных картинок
            foreach (cells p in allow_pole)
                if (p.name_figur != null) //если поле name элемента не пустое значит это какаято фигура
                {//загружаем клетку с соответствующей фигурой
                    temp = new Bitmap(@"figurs\" + p.name_figur.BorW + p.name_figur.name.name + "На" + p.pos_BorW + "Выд.bmp");
                    board.DrawImage(temp, p.positionX, p.positionY); //рисуем
                    temp.Dispose();//освобождаем временную переменную
                }
                else //если поле name элемента пустое значит это просто клетка
                {
                    if (p.pos_BorW == "White") //Определяем цвет клетки
                        board.DrawImage(white_check, p.positionX, p.positionY); //рисуем
                    else
                        board.DrawImage(black_check, p.positionX, p.positionY);
                }
            foreach (cells p in hod_rok)
                if (p.name_figur != null) //если поле name элемента не пустое значит это какаято фигура
                {//загружаем клетку с соответствующей фигурой
                    temp = new Bitmap(@"figurs\" + p.name_figur.BorW + p.name_figur.name + "На" + p.pos_BorW + "Выд.bmp");
                    board.DrawImage(temp, p.positionX, p.positionY); //рисуем
                    temp.Dispose();//освобождаем временную переменную
                }
                else //если поле name элемента пустое значит это просто клетка
                {
                    if (p.pos_BorW == "White") //Определяем цвет клетки
                        board.DrawImage(white_check, p.positionX, p.positionY); //рисуем
                    else
                        board.DrawImage(black_check, p.positionX, p.positionY);
                }

            white_check.Dispose(); //освобождаем занимаемую память
            black_check.Dispose();
            board.Dispose();
        }
        ArrayList available_course = new ArrayList();//доступные фигуры
        void dostup_hod()
        {
            available_course.Clear();
            for (int i = 0; i < 8; i++)
                for (int g = 0; g < 8; g++)
                {
                    if (pole[i, g].name_figur != null)
                        if (pole[i, g].name_figur.BorW == player)
                        {
                            position.X = i;
                            position.Y = g;
                            alow_hod(pole);
                            if (allow_pole.Count > 0 || hod_rok.Count > 0)
                                available_course.Add(pole[i, g]);
                        }
                }
        }
        ArrayList dostup_hod(int r, cells[,] poles, string playeer)
        {
            ArrayList Available_course = new ArrayList();
            for (int i = 0; i < 8; i++)
                for (int g = 0; g < 8; g++)
                {
                    if (poles[i, g].name_figur != null)
                        if (poles[i, g].name_figur.BorW == playeer)
                        {

                            ArrayList Allow_pole = alow_hod(r, poles, playeer, i, g);
                            if (Allow_pole.Count > 0)
                                Available_course.Add(poles[i, g]);
                        }
                }
            return Available_course;
        }
        private void go(int x, int y) //Функция перемещения фигур по полю
        {
            //и фигуру какого цвета он хочет передвинуть
            if (pole[x, y].name_figur != null) //если игрок хоче кого-то срубить
            {

                if (pole[x, y].name_figur.BorW != pole[position.X, position.Y].name_figur.BorW)//проверяем не хочит ли игрок срубить свою фигуру
                {
                    //MessageBox.Show("Вы срубили фигуру под названием: " + pole[x, y].name_figur.name+":)");//если все нормально выводим сообщение
                    if (pole[x, y].name_figur.BorW == "Белый") //срубили, теперь повышаем счетчик срубленных фигур
                    {
                        daeth_white += pole[x, y].name_figur.name.ochki;
                        W_ochki -= pole[x, y].name_figur.name.ochki;
                    }
                    else
                    {
                        daeth_black += pole[x, y].name_figur.name.ochki;
                        B_ochki -= pole[x, y].name_figur.name.ochki;
                    }

                    pole[x, y].name_figur = new figura(pole[position.X, position.Y].name_figur); //передвигаем фигур
                    pole[position.X, position.Y].name_figur = null; //зануляем больше ненужные поля

                }
            }
            else //если игрок просто ходит
            {
                pole[x, y].name_figur = new figura(pole[position.X, position.Y].name_figur); //передвигаем фигур
                pole[position.X, position.Y].name_figur = null;
            }
            if (y == 0 || y == 7)
            {
                if (figury[0, 0].Compar(pole[x, y].name_figur))
                    pole[x, y].name_figur = new figura(figury[0, 4]);
                if (figury[1, 0].Compar(pole[x, y].name_figur))
                    pole[x, y].name_figur = new figura(figury[1, 4]);
            }

            if (position.X == 0 && position.Y == 7)
                rok[0].left.flag = false;
            if (position.X == 7 && position.Y == 7)
                rok[0].right.flag = false;
            if (position.X == 0 && position.Y == 0)
                rok[1].left.flag = false;
            if (position.X == 7 && position.Y == 0)
                rok[1].right.flag = false;
            if (position.X == 4 && position.Y == 7)
            {
                rok[0].left.flag = false;
                rok[0].right.flag = false;
            }
            if (position.X == 4 && position.Y == 0)
            {
                rok[1].left.flag = false;
                rok[1].right.flag = false;
            }
            player = anti_player(player);
            label1.Text = "Взято белых " + daeth_white.ToString();//Выводим всякую информацию
            label2.Text = "Взято черных " + daeth_black.ToString();
            label3.Text = "Сейчас ходит " + player + " Игрок";
            drawing();

            //если игрок пытается передвинуть чужую фигуру,выводим напоминание
        }

        private cells[,] go(int x, int y, cells[,] poles) //Функция перемещения фигур по полю
        {
            //проверяем какой игрок сейчас ходит
            //и фигуру какого цвета он хочет передвинуть



            poles[x, y].name_figur = new figura(poles[position.X, position.Y].name_figur); //передвигаем фигур
            poles[position.X, position.Y].name_figur = null;
            if (y == 0 || y == 7)
            {
                if (figury[0, 0].Compar(poles[x, y].name_figur))
                    poles[x, y].name_figur = new figura(figury[0, 4]);
                if (figury[1, 0].Compar(poles[x, y].name_figur))
                    poles[x, y].name_figur = new figura(figury[1, 4]);
            }
            return poles;
            //drawing();

            //если игрок пытается передвинуть чужую фигуру,выводим напоминание
        }
        private cells[,] go(int X, int Y, int x, int y, cells[,] poles) //Функция перемещения фигур по полю
        {
            //проверяем какой игрок сейчас ходит
            //и фигуру какого цвета он хочет передвинуть



            poles[x, y].name_figur = new figura(poles[X, Y].name_figur); //передвигаем фигур
            poles[X, Y].name_figur = null;
            if (y == 0 || y == 7)
            {
                if (figury[0, 0].Compar(poles[x, y].name_figur))
                    poles[x, y].name_figur = new figura(figury[0, 4]);
                if (figury[1, 0].Compar(poles[x, y].name_figur))
                    poles[x, y].name_figur = new figura(figury[1, 4]);
            }
            return poles;
            //drawing();

            //если игрок пытается передвинуть чужую фигуру,выводим напоминание
        }
        private void button1_Click_1(object sender, EventArgs e)//обработчик события при нажатии на кнопку "Начать сначала!"
        {
            start();//Заполняем шахматную доску
            drawing();//Выводим поле
            daeth_white = 0;//обнуляем счетчики
            daeth_black = 0;
            player = "Белый";//устанавливаем игрока который будет ходить первым
            label1.Text = "Взято белых " + daeth_white.ToString(); //выводим инфу
            label2.Text = "Взято черных " + daeth_black.ToString();
            label3.Text = "Сейчас ходит " + player + " Игрок";
            if (((string)(white_c.SelectedItem)) == "Компьютер")
            {
                dostup_hod();
                randi();
            }
        }


        string char_from_number(int x)
        {
            char m = 'A';
            m = (char)(m + x);
            string str = "" + m;
            return str;

        }
        Stack otmena = new Stack();
        private void check_moove(int x, int y) //X Y координаты, куда ходить
        {//Функция проверки корректности ходов всех фигур
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (allow_pole.Contains(pole[x, y]))
                {
                    if (player == "Белый")
                    {
                        W_povtor = new Coordinata(position, new Point(x, y));
                    }
                    else
                    {
                        B_povtor = new Coordinata(position, new Point(x, y));
                    }
                    string log = "" + char_from_number(position.X) + (8 - position.Y) + " - " + char_from_number(x) + (8 - y) + ";";
                    ctrl_z z = new ctrl_z(pole[position.X, position.Y], pole[x, y], false, rok, player, W_ochki, B_ochki);
                    otmena.Push(z);
                    go(x, y);
                    listBox1.Items.Insert(0, log);
                    dostup_hod();


                    if (!check_no_shah(pole))
                        if (available_course.Count == 0)
                            MessageBox.Show("Мат " + player);
                        else
                        {
                            MessageBox.Show("Шах " + player);
                            tik = 0;
                            timer1.Start();

                        }
                    else
                        if (available_course.Count == 0)
                            MessageBox.Show("Пат");
                        else
                        {
                            tik = 0;
                            timer1.Start();
                        }
                }
                else
                    if (hod_rok.Contains(pole[x, y]))
                    {
                        string log = "Рокировка " + char_from_number(position.X) + (8 - position.Y) + " - " + char_from_number(x) + (8 - y) + ";";
                        ctrl_z z = new ctrl_z(pole[position.X, position.Y], pole[x, y], false, rok, player, W_ochki, B_ochki);
                        otmena.Push(z);
                        go(x, y);
                        int Y = 0;
                        if (player == "Белый")
                        {
                            player = "Черный";
                            Y = 0;
                        }
                        else
                        {
                            player = "Белый";
                            Y = 7;
                        }
                        position.Y = Y;
                        if (x < 4)
                        {
                            position.X = 0;
                            ctrl_z zx = new ctrl_z(pole[position.X, position.Y], pole[3, Y], true, rok, player, W_ochki, B_ochki);
                            otmena.Push(zx);
                            go(3, Y);
                        }
                        else
                        {
                            position.X = 7;
                            ctrl_z zx = new ctrl_z(pole[position.X, position.Y], pole[5, Y], true, rok, player, W_ochki, B_ochki);
                            otmena.Push(zx);
                            go(5, Y);
                        }

                        dostup_hod();


                        if (!check_no_shah(pole))
                            if (available_course.Count == 0)
                                MessageBox.Show("Мат " + player);
                            else
                            {
                                MessageBox.Show("Шах " + player);
                                tik = 0;
                                timer1.Start();
                            }
                        else
                            if (available_course.Count == 0)
                                MessageBox.Show("Пат");
                            else
                            {
                                tik = 0;
                                timer1.Start();
                            }
                    }
            }


            stat = false;
            drawing();
            //return 0;        
        }//int check_moov(int x,y);


        private int check_Koordinate(int c) //в эту функцию передаются координаты клеток в пикселях
        {               //нам их надо перевести в индексы массива
            if (c < 30) return 0; //если переданное число меньше 30 то индекс равен 0
            if (c > 30 && c < 60) return 1;
            if (c > 60 && c < 90) return 2;
            if (c > 90 && c < 120) return 3;
            if (c > 120 && c < 150) return 4;
            if (c > 150 && c < 180) return 5;
            if (c > 180 && c < 210) return 6;
            if (c > 210 && c < 240) return 7;
            return -1;//если координата выходит за границы возвращаем -1

        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)//обработчик события, который вызвается при
        //нажатии на клетку поля
        {
            if (check_Koordinate(e.X) >= 0 && check_Koordinate(e.Y) >= 0)//проверяем не выходит ли если координата за границы
                if (stat) //если нет, проверяем была ли быврана фигура для хода
                {
                    //если была то вызваем функцию проверки корректности хода
                    check_moove(check_Koordinate(e.X), check_Koordinate(e.Y));
                    stat = false;
                }
                else
                {
                    if (pole[check_Koordinate(e.X), check_Koordinate(e.Y)].name_figur == null || pole[check_Koordinate(e.X), check_Koordinate(e.Y)].name_figur.BorW != player)
                        stat = false;//смотрим что бы пользователь за место фигуры не пытался двигать пустые клетки
                    else
                    {
                        position.X = check_Koordinate(e.X);//если пользователь этого делать не собирался
                        position.Y = check_Koordinate(e.Y);// тогда все хорошо
                        alow_hod(pole);
                        drawing_hod();
                        stat = true;//запоминаем координаты выбраной для хода фигуры и ждем пока игрок подумает куда поставить фигуру
                    }
                }
        }
        bool check_pole(int x, int y)
        {
            return false;
        }
        ArrayList Kon(int x, int y, cells[,] poles)
        {
            ArrayList kon = new ArrayList();
            if (x - 1 >= 0 && y - 2 >= 0)
                kon.Add(poles[x - 1, y - 2]);
            if (x + 1 < 8 && y - 2 >= 0)
                kon.Add(poles[x + 1, y - 2]);
            if (x - 2 >= 0 && y - 1 >= 0)
                kon.Add(poles[x - 2, y - 1]);
            if (x + 2 < 8 && y - 1 >= 0)
                kon.Add(poles[x + 2, y - 1]);
            if (x - 2 >= 0 && y + 1 < 8)
                kon.Add(poles[x - 2, y + 1]);
            if (x + 2 < 8 && y + 1 < 8)
                kon.Add(poles[x + 2, y + 1]);
            if (x - 1 >= 0 && y + 2 < 8)
                kon.Add(poles[x - 1, y + 2]);
            if (x + 1 < 8 && y + 2 < 8)
                kon.Add(poles[x + 1, y + 2]);
            return kon;
        }
        ArrayList Korol(int x, int y, cells[,] poles)
        {
            ArrayList kon = new ArrayList();
            if (x - 1 >= 0 && y - 1 >= 0)
                kon.Add(poles[x - 1, y - 1]);
            if (y - 1 >= 0)
                kon.Add(poles[x, y - 1]);
            if (x + 1 < 8 && y - 1 >= 0)
                kon.Add(poles[x + 1, y - 1]);
            if (x + 1 < 8)
                kon.Add(poles[x + 1, y]);
            if (x + 1 < 8 && y + 1 < 8)
                kon.Add(poles[x + 1, y + 1]);
            if (y + 1 < 8)
                kon.Add(poles[x, y + 1]);
            if (x - 1 >= 0 && y + 1 < 8)
                kon.Add(poles[x - 1, y + 1]);
            if (x - 1 >= 0)
                kon.Add(poles[x - 1, y]);
            return kon;
        }
        ArrayList hod_figury(int x, int y, int xx, int yy, cells[,] poles, ArrayList hod)
        {
            if (x + xx < 8 && x + xx >= 0 && y + yy < 8 && y + yy >= 0)
            {
                hod.Add(poles[x + xx, y + yy]);
                if (poles[x + xx, y + yy].name_figur == null)
                    return hod_figury(x + xx, y + yy, xx, yy, poles, hod);
                else return hod;

            }
            else return hod;
        }
        ArrayList Slon(int x, int y, cells[,] poles)
        {
            ArrayList slon = new ArrayList();
            slon = hod_figury(x, y, -1, -1, poles, slon);
            slon = hod_figury(x, y, -1, 1, poles, slon);
            slon = hod_figury(x, y, 1, -1, poles, slon);
            slon = hod_figury(x, y, 1, 1, poles, slon);
            return slon;

        }
        ArrayList Ferz(int x, int y, cells[,] poles)
        {
            ArrayList ladya = new ArrayList();
            ladya = hod_figury(x, y, 0, -1, poles, ladya);
            ladya = hod_figury(x, y, 0, 1, poles, ladya);
            ladya = hod_figury(x, y, -1, 0, poles, ladya);
            ladya = hod_figury(x, y, 1, 0, poles, ladya);
            ladya = hod_figury(x, y, -1, -1, poles, ladya);
            ladya = hod_figury(x, y, -1, 1, poles, ladya);
            ladya = hod_figury(x, y, 1, -1, poles, ladya);
            ladya = hod_figury(x, y, 1, 1, poles, ladya);
            return ladya;

        }
        ArrayList Ladya(int x, int y, cells[,] poles)
        {
            ArrayList ladya = new ArrayList();
            ladya = hod_figury(x, y, 0, -1, poles, ladya);
            ladya = hod_figury(x, y, 0, 1, poles, ladya);
            ladya = hod_figury(x, y, -1, 0, poles, ladya);
            ladya = hod_figury(x, y, 1, 0, poles, ladya);
            return ladya;

        }
        bool check_no_shah(cells[,] poles)
        {
            int ix = -1; int gy = -1;
            figura K;
            if (player == "Белый")
                K = figury[0, 5];
            else K = figury[1, 5];
            for (int i = 0; i < 8; i++)
                for (int g = 0; g < 8; g++)
                {
                    if (K.Compar(poles[i, g].name_figur))
                    {
                        ix = i;
                        gy = g;
                    }

                }
            if (ix == -1 || gy == -1)
                MessageBox.Show(@"bool check_no_shah(cells[,] poles)");
            if (player == "Белый")
            {
                if (ix - 1 >= 0 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix - 1, gy - 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix + 1, gy - 1].name_figur))
                        return false;
            }
            else
            {
                if (ix - 1 >= 0 && gy + 1 < 8)
                    if (figury[0, 0].Compar(poles[ix - 1, gy + 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy + 1 < 8d)
                    if (figury[0, 0].Compar(poles[ix + 1, gy + 1].name_figur))
                        return false;
            }
            int ind = 0;
            if (player == "Белый")
                ind = 1;
            else ind = 0;

            ArrayList figure = Ladya(ix, gy, poles);
            //bool rez=true;
            foreach (cells p in figure)
            {
                if (figury[ind, 1].Compar(p.name_figur))
                    return false;
            }
            figure = Kon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 2].Compar(p.name_figur))
                    return false;
            }
            figure = Slon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 3].Compar(p.name_figur))
                    return false;
            }
            figure = Ferz(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 4].Compar(p.name_figur))
                    return false;
            }
            figure = Korol(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 5].Compar(p.name_figur))
                    return false;
            }
            return true;
        }
        bool check_no_shah(cells[,] poles, string playeer, int r)
        {
            int ix = -1; int gy = -1;
            figura K;
            if (playeer == "Белый")
                K = figury[0, 5];
            else K = figury[1, 5];
            for (int i = 0; i < 8; i++)
                for (int g = 0; g < 8; g++)
                {
                    if (K.Compar(poles[i, g].name_figur))
                    {
                        ix = i;
                        gy = g;
                    }

                }
            if (ix == -1 || gy == -1)
                MessageBox.Show(@" bool check_no_shah(cells[,] poles,string playeer)");
            if (playeer == "Белый")
            {
                if (ix - 1 >= 0 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix - 1, gy - 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix + 1, gy - 1].name_figur))
                        return false;
            }
            else
            {
                if (ix - 1 >= 0 && gy + 1 < 8)
                    if (figury[0, 0].Compar(poles[ix - 1, gy + 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy + 1 < 8d)
                    if (figury[0, 0].Compar(poles[ix + 1, gy + 1].name_figur))
                        return false;
            }
            int ind = 0;
            if (playeer == "Белый")
                ind = 1;
            else ind = 0;

            ArrayList figure = Ladya(ix, gy, poles);
            //bool rez=true;
            foreach (cells p in figure)
            {
                if (figury[ind, 1].Compar(p.name_figur))
                    return false;
            }
            figure = Kon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 2].Compar(p.name_figur))
                    return false;
            }
            figure = Slon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 3].Compar(p.name_figur))
                    return false;
            }
            figure = Ferz(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 4].Compar(p.name_figur))
                    return false;
            }
            figure = Korol(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 5].Compar(p.name_figur))
                    return false;
            }
            return true;
        }
        bool check_no_shah(cells[,] poles, int ix, int gy, string playeer)
        {
            if (ix == -1 || gy == -1)
                MessageBox.Show(@"bool check_no_shah(cells[,] poles,int ix,int gy, string playeer)");
            if (playeer == "Белый")
            {
                if (ix - 1 >= 0 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix - 1, gy - 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix + 1, gy - 1].name_figur))
                        return false;
            }
            else
            {
                if (ix - 1 >= 0 && gy + 1 < 8)
                    if (figury[0, 0].Compar(poles[ix - 1, gy + 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy + 1 < 8d)
                    if (figury[0, 0].Compar(poles[ix + 1, gy + 1].name_figur))
                        return false;
            }
            int ind = 0;
            if (playeer == "Белый")
                ind = 1;
            else ind = 0;

            ArrayList figure = Ladya(ix, gy, poles);
            //bool rez=true;
            foreach (cells p in figure)
            {
                if (figury[ind, 1].Compar(p.name_figur))
                    return false;
            }
            figure = Kon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 2].Compar(p.name_figur))
                    return false;
            }
            figure = Slon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 3].Compar(p.name_figur))
                    return false;
            }
            figure = Ferz(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 4].Compar(p.name_figur))
                    return false;
            }
            figure = Korol(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 5].Compar(p.name_figur))
                    return false;
            }
            return true;
        }
        Point check_coord_shah(cells[,] poles, int ix, int gy, string playeer)
        {
            if (ix == -1 || gy == -1)
                MessageBox.Show(@"bool check_no_shah(cells[,] poles,int ix,int gy, string playeer)");
            if (playeer == "Белый")
            {
                if (ix - 1 >= 0 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix - 1, gy - 1].name_figur))
                        return new Point(ix - 1, gy - 1);
                if (ix + 1 < 8 && gy - 1 >= 0)
                    if (figury[1, 0].Compar(poles[ix + 1, gy - 1].name_figur))
                        return new Point(ix + 1, gy - 1);
            }
            else
            {
                if (ix - 1 >= 0 && gy + 1 < 8)
                    if (figury[0, 0].Compar(poles[ix - 1, gy + 1].name_figur))
                        return new Point(ix - 1, gy + 1);
                if (ix + 1 < 8 && gy + 1 < 8d)
                    if (figury[0, 0].Compar(poles[ix + 1, gy + 1].name_figur))
                        return new Point(ix + 1, gy + 1);
            }
            int ind = 0;
            if (playeer == "Белый")
                ind = 1;
            else ind = 0;

            ArrayList figure = Ladya(ix, gy, poles);
            //bool rez=true;
            foreach (cells p in figure)
            {
                if (figury[ind, 1].Compar(p.name_figur))
                    return new Point(p.X, p.Y);
            }
            figure = Kon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 2].Compar(p.name_figur))
                    return new Point(p.X, p.Y);
            }
            figure = Slon(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 3].Compar(p.name_figur))
                    return new Point(p.X, p.Y);
            }
            figure = Ferz(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 4].Compar(p.name_figur))
                    return new Point(p.X, p.Y);
            }
            figure = Korol(ix, gy, poles);
            foreach (cells p in figure)
            {
                if (figury[ind, 5].Compar(p.name_figur))
                    return new Point(p.X, p.Y);
            }
            return new Point(-1, -1);
        }
        cells[,] Copy_mas(cells[,] mas)
        {
            cells[,] poles = new cells[8, 8];
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                {
                    poles[x, y] = new cells(mas[x, y]);

                }

            return poles;
        }

        Random rand = new Random();
        void randi()
        {
            ArrayList Coord = new ArrayList();
            int evr = -999999;
            Point Begin = new Point(-1, -1);
            Point End = new Point(-1, -1);
            dostup_hod();


            foreach (cells c in available_course)
            {
                ArrayList alow = alow_hod(1, Copy_mas(pole), player, c.X, c.Y);


                foreach (cells k in alow)
                {
                    int e;

                    e = evristic(c.X, c.Y, k.X, k.Y, player,pole);

                    //e = calculate(0, c.X, c.Y, k.X, k.Y, Copy_mas(pole),player, 2);
                    if (e > evr)
                    {

                        evr = e;
                        Begin = new Point(c.X, c.Y);
                        End = new Point(k.X, k.Y);
                        Coord.Clear();
                        Coordinata coor = new Coordinata(Begin, End);
                        Coord.Add(coor);



                    }
                    else if (e == evr)
                    {


                        Begin = new Point(c.X, c.Y);
                        End = new Point(k.X, k.Y);
                        Coordinata coor = new Coordinata(Begin, End);
                        Coord.Add(coor);


                    }

                }

            }
            Coordinata coordin = ((Coordinata)Coord[rand.Next(Coord.Count - 1)]);
            position = coordin.Begin;
            alow_hod(pole);
            check_moove(coordin.End.X, coordin.End.Y);
        }

        int calculate(int count, int X, int Y, int x, int y, cells[,] poles, string playeer, int depth)
        {
            int sum = 0;
            if (depth == count)
            {
                return evristic(X, Y, x, y, playeer, poles);
            }
            else
            {
                if (check_mat(go(X, Y, x, y, Copy_mas(poles)), anti_player(playeer)))
                    return 100000;
                if (check_pat(go(X, Y, x, y, Copy_mas(poles)), anti_player(playeer)))
                    return -100000;
                cells[,] neo_pole = (go(X, Y, x, y, Copy_mas(poles)));
                ArrayList av_cc = dostup_hod(0, neo_pole, anti_player(playeer));
                foreach (cells cc in av_cc)
                {
                    ArrayList aloww = alow_hod(2, Copy_mas(neo_pole), anti_player(playeer), cc.X, cc.Y);
                    foreach (cells kk in aloww)
                    {
                        if (check_mat(go(cc.X, cc.Y, kk.X, kk.Y, Copy_mas(neo_pole)), playeer))
                            return -100000;
                        if (check_pat(go(cc.X, cc.Y, kk.X, kk.Y, Copy_mas(neo_pole)), anti_player(playeer)))
                            if (can_pat(playeer))
                                return 100000;
                            else
                                return -100000;
                        int e = calculate(count + 1, cc.X, cc.Y, kk.X, kk.Y, Copy_mas(neo_pole), playeer, depth);
                        sum += e;
                    }
                }
            }
            return sum;
        }
        string anti_player(string playeer)
        {
            string aplayer;
            if (playeer == "Белый")
                aplayer = "Черный";
            else aplayer = "Белый";
            return aplayer;
        }
        bool check_mat(cells[,] poles, string playeer)
        {
            ArrayList dost_hod = dostup_hod(1, poles, playeer);
            if (dost_hod.Count == 0)
                if (!check_no_shah(poles, playeer, 0))
                    return true;
            return false;
        }
        bool check_pat(cells[,] poles, string playeer)
        {
            ArrayList dost_hod = dostup_hod(2, poles, playeer);
            if (dost_hod.Count == 0)
                if (check_no_shah(poles, playeer, 0))
                    return true;
            return false;
        }
        bool can_pat(string playeer)
        {
            double w_k = ((double)W_ochki) * 0.75;
            double b_k = ((double)B_ochki) * 0.75;
            if (playeer == "Белый")

                return (((double)W_ochki) < b_k);

            else
                return (((double)B_ochki) < w_k);

        }

        int evristic(int X, int Y, int x, int y, string playeer,cells[,]pole)
        {
            int evr = 0;
            string aplayer = anti_player(playeer);

            cells[,] poles = Copy_mas(pole);//если что изменить
            int eda = 0;
            int you_mat = 0;
            if (poles[x, y].name_figur != null)
                eda = poles[x, y].name_figur.name.ochki;
            poles = go(X, Y, x, y, poles);
            int k_mat = 10000;
            int k_pat = 0;
            double w_k = ((double)W_ochki) * 0.75;
            double b_k = ((double)B_ochki) * 0.75;
            if (playeer == "Белый")
                if (((double)W_ochki) < b_k)
                    k_pat = 500;
                else k_pat = -500;
            else
                if (((double)B_ochki) < w_k)
                    k_pat = 500;
                else k_pat = -500;
            int pat = 0;
            int mat = 0;
            int shah = 0;
            int povt = 0;
            ArrayList dost_hod = dostup_hod(3, poles, aplayer);
            if (dost_hod.Count == 0)
                if (check_no_shah(poles, aplayer, 0))
                    pat = 1;
                else
                    mat = 1;
            else if (!check_no_shah(poles, aplayer, 1))
                shah = 1;
            if (playeer == "Белый")
            {
                if (W_povtor.Compar(X, Y, x, y))
                    povt = -100;
            }
            else
            {
                if (B_povtor.Compar(X, Y, x, y))
                    povt = -100;
            }
            int my_udar = 0;
            int you_udar = 0;
            int my_svyas = 0;
            int you_svyas = 0;
            int my_cells = 0;
            int you_cells = 0;
            int my_cells_korol = 0;
            int you_cells_korol = 0;
            if (mat == 0)
                for (int i = 0; i < 8; i++)
                    for (int g = 0; g < 8; g++)
                    {
                        if (poles[i, g].name_figur != null)
                        {
                            if (poles[i, g].name_figur.BorW == playeer)
                            {
                                if (!check_no_shah(poles, i, g, playeer))
                                    my_udar += poles[i, g].name_figur.name.ochki;
                                ArrayList a = alow_hod(4, poles, playeer, i, g);
                                if (figury[0, 5].name.Compar(poles[i, g].name_figur.name))
                                    my_cells_korol = a.Count;
                                if (a.Count == 0)
                                    my_svyas++;

                            }
                            else
                            {
                                if (!check_no_shah(poles, i, g, aplayer))
                                {
                                    Point pnt = check_coord_shah(poles, i, g, aplayer);
                                    if (check_no_shah(poles, pnt.X, pnt.Y, player))
                                        you_udar += poles[i, g].name_figur.name.ochki;
                                }
                                ArrayList al_hod = alow_hod(5, poles, aplayer, i, g);
                                if (figury[0, 5].name.Compar(poles[i, g].name_figur.name))
                                    you_cells_korol = al_hod.Count;
                                if (al_hod.Count == 0)
                                    you_svyas++;
                                else
                                {
                                    foreach (cells p in al_hod)
                                    {
                                        cells[,] neo_pole = go(i, g, p.X, p.Y, Copy_mas(poles));
                                        //if (neo_pole[4, 0].name_figur == null)
                                       // MessageBox.Show("OOO");
                                        ArrayList dost_hod_neo = dostup_hod(4, neo_pole, playeer);
                                        if (dost_hod_neo.Count == 0)
                                            if (!check_no_shah(neo_pole, playeer, 2))
                                                you_mat = 1;


                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!check_no_shah(poles, i, g, player))
                                you_cells++;
                            if (!check_no_shah(poles, i, g, aplayer))
                                my_cells++;

                        }

                    }
            
            evr = k_mat * mat + shah + povt + k_pat * pat - 1000 * you_mat - 20 * my_udar - 4 * my_svyas
                + 8 * you_udar + 4 * you_svyas + 20 * eda + my_cells - you_cells + 8*my_cells_korol-8*you_cells_korol;
            return evr;
        }
        ArrayList Add_alow(ArrayList figure)
        {
            figura_otd f = figury[0, 4].name;
            ArrayList allow_pole = new ArrayList();
            foreach (cells p in figure)
            {
                if (p.name_figur == null)
                {
                    if (check_no_shah(go(p.X, p.Y, Copy_mas(pole))))
                    {
                        allow_pole.Add(p);
                    }
                }
                else
                {
                    if (p.name_figur.BorW != player)
                        if (check_no_shah(go(p.X, p.Y, Copy_mas(pole))))
                        {
                            allow_pole.Add(p);
                        }
                }
            }
            return allow_pole;
        }
        ArrayList Add_alow(ArrayList figure, string playeer, int PX, int PY, cells[,] pole)
        {

            ArrayList allow_pole = new ArrayList();
            foreach (cells p in figure)
            {
                if (p.name_figur == null)
                {
                    if (check_no_shah(go(PX, PY, p.X, p.Y, Copy_mas(pole)), playeer, 3))
                    {
                        allow_pole.Add(p);
                    }
                }
                else
                {
                    if (p.name_figur.BorW != playeer)
                        if (check_no_shah(go(PX, PY, p.X, p.Y, Copy_mas(pole)), playeer, 4))
                        {
                            allow_pole.Add(p);
                        }
                }
            }
            return allow_pole;
        }

        void alow_hod(cells[,] pole)
        {
            allow_pole.Clear();
            hod_rok.Clear();
            figura_otd f = new figura_otd("Ферзь", 10);


            if (figury[1, 0].Compar(pole[position.X, position.Y].name_figur))
            {
                if (pole[position.X, position.Y + 1].name_figur == null && check_no_shah(go(position.X, position.Y + 1, Copy_mas(pole))))
                    allow_pole.Add(pole[position.X, position.Y + 1]);
                if (position.Y == 1)
                    if (pole[position.X, position.Y + 2].name_figur == null && pole[position.X, position.Y + 1].name_figur == null && check_no_shah(go(position.X, position.Y + 2, Copy_mas(pole))))
                        allow_pole.Add(pole[position.X, position.Y + 2]);
                if (position.X - 1 >= 0)
                    if (pole[position.X - 1, position.Y + 1].name_figur != null)
                        if (pole[position.X - 1, position.Y + 1].name_figur.BorW != player)
                            if (check_no_shah(go(position.X - 1, position.Y + 1, Copy_mas(pole))))
                                allow_pole.Add(pole[position.X - 1, position.Y + 1]);
                if (position.X + 1 < 8)
                    if (pole[position.X + 1, position.Y + 1].name_figur != null)
                        if (pole[position.X + 1, position.Y + 1].name_figur.BorW != player)
                            if (check_no_shah(go(position.X + 1, position.Y + 1, Copy_mas(pole))))
                                allow_pole.Add(pole[position.X + 1, position.Y + 1]);

            }
            if (figury[0, 0].Compar(pole[position.X, position.Y].name_figur))
            {
                if (pole[position.X, position.Y - 1].name_figur == null && check_no_shah(go(position.X, position.Y - 1, Copy_mas(pole))))
                    allow_pole.Add(pole[position.X, position.Y - 1]);
                if (position.Y == 6)
                    if (pole[position.X, position.Y - 2].name_figur == null && pole[position.X, position.Y - 1].name_figur == null && check_no_shah(go(position.X, position.Y - 2, Copy_mas(pole))))
                        allow_pole.Add(pole[position.X, position.Y - 2]);
                if (position.X - 1 >= 0)
                    if (pole[position.X - 1, position.Y - 1].name_figur != null)
                        if (pole[position.X - 1, position.Y - 1].name_figur.BorW != player)
                            if (check_no_shah(go(position.X - 1, position.Y - 1, Copy_mas(pole))))
                                allow_pole.Add(pole[position.X - 1, position.Y - 1]);
                if (position.X + 1 < 8)
                    if (pole[position.X + 1, position.Y - 1].name_figur != null)
                        if (pole[position.X + 1, position.Y - 1].name_figur.BorW != player)
                            if (check_no_shah(go(position.X + 1, position.Y - 1, Copy_mas(pole))))
                                allow_pole.Add(pole[position.X + 1, position.Y - 1]);

            }

            ArrayList figure;
            if (figury[0, 1].name.Compar(pole[position.X, position.Y].name_figur.name))
            {
                figure = Ladya(position.X, position.Y, pole);
                allow_pole = Add_alow(figure);

            }
            if (figury[0, 2].name.Compar(pole[position.X, position.Y].name_figur.name))
            {
                figure = Kon(position.X, position.Y, pole);
                allow_pole = Add_alow(figure);

            }
            if (figury[0, 3].name.Compar(pole[position.X, position.Y].name_figur.name))
            {
                figure = Slon(position.X, position.Y, pole);
                allow_pole = Add_alow(figure);

            }
            if (figury[0, 4].name.Compar(pole[position.X, position.Y].name_figur.name))
            {
                figure = Ferz(position.X, position.Y, pole);
                allow_pole = Add_alow(figure);

            }
            if (figury[0, 5].name.Compar(pole[position.X, position.Y].name_figur.name))
            {
                figure = Korol(position.X, position.Y, pole);
                allow_pole = Add_alow(figure);

                if (check_no_shah(pole))
                {
                    int ind = 0;
                    int Y = 0;
                    if (player == "Белый")
                    {
                        ind = 0;
                        Y = 7;
                    }
                    else
                    {
                        ind = 1;
                        Y = 0;
                    }
                    {
                        if (rok[ind].left.flag)
                        {
                            ArrayList hod = new ArrayList();
                            hod = hod_figury(position.X, position.Y, -1, 0, pole, hod);
                            if (hod.Contains(pole[0, Y]) && pole[0, Y].name_figur.name.name == "Ладья")
                                if (check_no_shah(go(2, Y, Copy_mas(pole))))

                                    hod_rok.Add(pole[rok[ind].left.Coord.X, rok[ind].left.Coord.Y]);


                        }
                        if (rok[ind].right.flag)
                        {
                            ArrayList hod = new ArrayList();
                            hod = hod_figury(position.X, position.Y, 1, 0, pole, hod);
                            if (hod.Contains(pole[7, Y]) && pole[7, Y].name_figur.name.name == "Ладья")
                                if (check_no_shah(go(6, Y, Copy_mas(pole))))
                                    hod_rok.Add(pole[rok[ind].right.Coord.X, rok[ind].right.Coord.Y]);

                        }
                    }
                }

            }

        }
        ArrayList alow_hod(int r, cells[,] pole, string playeer, int posX, int posY)
        {
            ArrayList allow_pole = new ArrayList();
            ArrayList hod_rok = new ArrayList();
            //figura_otd f = new figura_otd("Ферзь", 10);


            if (figury[1, 0].Compar(pole[posX, posY].name_figur))
            {
                if (pole[posX, posY + 1].name_figur == null && check_no_shah(go(posX, posY, posX, posY + 1, Copy_mas(pole)), playeer, r))
                    allow_pole.Add(pole[posX, posY + 1]);
                if (posY == 1)
                    if (pole[posX, posY + 2].name_figur == null && pole[posX, posY + 1].name_figur == null && check_no_shah(go(posX, posY, posX, posY + 2, Copy_mas(pole)), playeer, 6))
                        allow_pole.Add(pole[posX, posY + 2]);
                if (posX - 1 >= 0)
                    if (pole[posX - 1, posY + 1].name_figur != null)
                        if (pole[posX - 1, posY + 1].name_figur.BorW != playeer)
                            if (check_no_shah(go(posX, posY, posX - 1, posY + 1, Copy_mas(pole)), playeer, 7))
                                allow_pole.Add(pole[posX - 1, posY + 1]);
                if (posX + 1 < 8)
                    if (pole[posX + 1, posY + 1].name_figur != null)
                        if (pole[posX + 1, posY + 1].name_figur.BorW != playeer)
                            if (check_no_shah(go(posX, posY, posX + 1, posY + 1, Copy_mas(pole)), playeer, 8))
                                allow_pole.Add(pole[posX + 1, posY + 1]);

            }
            if (figury[0, 0].Compar(pole[posX, posY].name_figur))
            {
                if (pole[posX, posY - 1].name_figur == null && check_no_shah(go(posX, posY, posX, posY - 1, Copy_mas(pole)), playeer, 9))
                    allow_pole.Add(pole[posX, posY - 1]);
                if (posY == 6)
                    if (pole[posX, posY - 2].name_figur == null && pole[posX, posY - 1].name_figur == null && check_no_shah(go(posX, posY, posX, posY - 2, Copy_mas(pole)), playeer, 10))
                        allow_pole.Add(pole[posX, posY - 2]);
                if (posX - 1 >= 0)
                    if (pole[posX - 1, posY - 1].name_figur != null)
                        if (pole[posX - 1, posY - 1].name_figur.BorW != playeer)
                            if (check_no_shah(go(posX, posY, posX - 1, posY - 1, Copy_mas(pole)), playeer, 11))
                                allow_pole.Add(pole[posX - 1, posY - 1]);
                if (posX + 1 < 8)
                    if (pole[posX + 1, posY - 1].name_figur != null)
                        if (pole[posX + 1, posY - 1].name_figur.BorW != playeer)
                            if (check_no_shah(go(posX, posY, posX + 1, posY - 1, Copy_mas(pole)), playeer, 12))
                                allow_pole.Add(pole[posX + 1, posY - 1]);

            }

            if (figury[0, 1].name.Compar(pole[posX, posY].name_figur.name))
            {
                ArrayList figure = Ladya(posX, posY, pole);
                allow_pole = Add_alow(figure, playeer, posX, posY, pole);
            }
            if (figury[0, 2].name.Compar(pole[posX, posY].name_figur.name))
            {
                ArrayList figure = Kon(posX, posY, pole);
                allow_pole = Add_alow(figure, playeer, posX, posY, pole);
            }
            if (figury[0, 3].name.Compar(pole[posX, posY].name_figur.name))
            {
                ArrayList figure = Slon(posX, posY, pole);
                allow_pole = Add_alow(figure, playeer, posX, posY, pole);
            }
            if (figury[0, 4].name.Compar(pole[posX, posY].name_figur.name))
            {
                ArrayList figure = Ferz(posX, posY, pole);
                allow_pole = Add_alow(figure, playeer, posX, posY, pole);
            }
            if (figury[0, 5].name.Compar(pole[posX, posY].name_figur.name))
            {
                ArrayList figure = Korol(posX, posY, pole);
                allow_pole = Add_alow(figure, playeer, posX, posY, pole);
                if (check_no_shah(pole, playeer, 13) && r == 0)
                {
                    int ind = 0;
                    int Y = 0;
                    if (playeer == "Белый")
                    {
                        ind = 0;
                        Y = 7;
                    }
                    else
                    {
                        ind = 1;
                        Y = 0;
                    }
                    {
                        if (rok[ind].left.flag)
                        {
                            ArrayList hod = new ArrayList();
                            hod = hod_figury(posX, posY, -1, 0, pole, hod);
                            if (pole[0, Y].name_figur != null)
                                if (hod.Contains(pole[0, Y]) && pole[0, Y].name_figur.name.name == "Ладья")
                                    if (check_no_shah(go(posX, posY, 2, Y, Copy_mas(pole)), playeer, r))
                                        hod_rok.Add(pole[rok[ind].left.Coord.X, rok[ind].left.Coord.Y]);


                        }
                        if (rok[ind].right.flag)
                        {
                            ArrayList hod = new ArrayList();
                            hod = hod_figury(posX, posY, 1, 0, pole, hod);
                            if (pole[7, Y].name_figur != null)
                                if (hod.Contains(pole[7, Y]) && pole[7, Y].name_figur.name.name == "Ладья")
                                    if (check_no_shah(go(posX, posY, 6, Y, Copy_mas(pole)), playeer, r))
                                        hod_rok.Add(pole[rok[ind].right.Coord.X, rok[ind].right.Coord.Y]);

                        }
                    }
                }

            }
            foreach (cells c in hod_rok)
            {
                allow_pole.Add(c);
            }
            return allow_pole;

        }
        ArrayList hod_rok = new ArrayList();
        private void Form1_Resize(object sender, EventArgs e)
        {
            drawing();//обработчик события изменения размеров главной формы
            //это делается для того чтобы изображение шахматной доски ни куда не изчезало  
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            drawing();//рисуем доску при загрузке
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            white_c.SelectedIndex = 0;
            black_c.SelectedIndex = 0;
            start();//устанавливаем начальные значения при включении программы
            drawing();
            player = "Белый";
            label1.Text = "Взято белых " + daeth_white.ToString();
            label2.Text = "Взято черных " + daeth_black.ToString();
            label3.Text = "Сейчас ходит " + player + " Игрок";
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            //drawing(); //рисуем
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawing(); //при перерисовке формы изображение может пропасть, поэтому рисуем его заного
            //на всякий случай
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tik += timer1.Interval;
            if (tik > 0)
            {
                timer1.Stop();
                if (player == "Черный")
                {

                    if ((string)black_c.SelectedItem == "Компьютер") randi();
                }
                else
                    if ((string)white_c.SelectedItem == "Компьютер") randi();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (otmena.Count != 0)
            {
                ctrl_z z = (ctrl_z)otmena.Pop();
                pole[z.Begin.X, z.Begin.Y] = z.Begin;
                pole[z.End.X, z.End.Y] = z.End;
                if (z.rok)
                {
                    z = (ctrl_z)otmena.Pop();
                    pole[z.Begin.X, z.Begin.Y] = z.Begin;
                    pole[z.End.X, z.End.Y] = z.End;
                }
                rok = z.r;
                W_ochki = z.w_ochki;
                B_ochki = z.b_ochki;
                player = z.playeer;
                daeth_black = full_ochki - B_ochki;
                daeth_white = full_ochki - W_ochki;
                label1.Text = "Взято белых " + daeth_white.ToString();//Выводим всякую информацию
                label2.Text = "Взято черных " + daeth_black.ToString();
                label3.Text = "Сейчас ходит " + player + " Игрок";
                listBox1.Items.RemoveAt(0);
                drawing();
            }
        }

    }
    public class cells //описываем структуру шахматной доски
    {
        public figura name_figur; //имя фигуры
        public int positionX;//Координаты по Х
        public int positionY;// Координаты по У
        public int X;
        public int Y;
        public string pos_BorW;//Цвет клетки на которой стоит фигура
        public cells()
        {
            name_figur = null;
            positionX = -1;
            positionY = -1;
            X = -1;
            Y = -1;
            pos_BorW = null;
        }
        public cells(figura f, int posX, int posY, int x, int y, string BorW)
        {
            name_figur = f;
            positionX = posX;
            positionY = posY;
            X = x;
            Y = y;
            pos_BorW = BorW;
        }
        public cells(cells c)
        {
            if (c != null)
            {
                if (c.name_figur == null)
                    name_figur = null;
                else
                    name_figur = new figura(c.name_figur);
                positionX = c.positionX;
                positionY = c.positionY;
                X = c.X;
                Y = c.Y;
                pos_BorW = c.pos_BorW;
            }
        }

    }
    public class figura
    {
        public string BorW;
        public figura_otd name;

        public figura(string cvet, figura_otd imya)
        {
            BorW = cvet;
            name = new figura_otd(imya);

        }
        public figura(figura fig)
        {
            if (fig != null)
            {
                BorW = fig.BorW;
                name = new figura_otd(fig.name);
            }

        }
        public bool Compar(figura f)
        {
            if (f == null)
                return false;
            else return (BorW == f.BorW && name.Compar(f.name));
        }
    }
    public class figura_otd
    {
        public string name;
        public int ochki;
        public figura_otd(string imya, int ochko)
        {

            name = imya;
            ochki = ochko;
        }
        public figura_otd(figura_otd fig)
        {
            if (fig != null)
            {
                name = fig.name;
                ochki = fig.ochki;
            }

        }
        public bool Compar(figura_otd f)
        {
            if (f == null)
                return false;
            else return (name == f.name);
        }
    }
    public class Coordinata
    {
        public Point Begin;
        public Point End;
        public Coordinata(Point b, Point e)
        {
            Begin = new Point(b.X, b.Y);
            End = new Point(e.X, e.Y);
        }
        public Coordinata(Coordinata c)
        {
            Begin = new Point(c.Begin.X, c.Begin.Y);
            End = new Point(c.End.X, c.End.Y);
        }
        public bool Compar(Coordinata c)
        {
            return (Begin.X == c.End.X && Begin.Y == c.End.Y && End.X == c.Begin.X && End.Y == c.Begin.Y);
        }
        public bool Compar(Point b, Point e)
        {
            return (Begin.X == b.X && Begin.Y == b.Y && End.X == e.X && End.Y == e.Y);
        }
        public bool Compar(Point b, int x, int y)
        {
            return (Begin.X == x && Begin.Y == y && End.X == b.X && End.Y == b.Y);
        }
        public bool Compar(int X, int Y, Point e)
        {
            return (Begin.X == e.X && Begin.Y == e.Y && End.X == X && End.Y == Y);
        }
        public bool Compar(int X, int Y, int x, int y)
        {
            return (Begin.X == x && Begin.Y == y && End.X == X && End.Y == Y);
        }
    }
    public class ctrl_z
    {
        public cells Begin;
        public cells End;
        public bool rok;
        public Form1.rokirovka[] r;
        public string playeer;
        public int w_ochki;
        public int b_ochki;
        public ctrl_z(cells b, cells e, bool rk, Form1.rokirovka[] rr, string pl, int wo, int bo)
        {
            Begin = new cells(b);
            End = new cells(e);
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
}//КОНЕЦ