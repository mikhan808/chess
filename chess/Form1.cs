using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections;

namespace chess
{
    public partial class Form1 : Form
    {
        Coordinata W_povtor;
        Coordinata B_povtor;
        int W_ochki = 0;
        int B_ochki = 0;
        public Form1()
        {
            InitializeComponent();//Расставляем итемы по форме
            Figura.init_figurs();
            W_povtor = new Coordinata(new Point(-1, -1), new Point(-1, -1));
            B_povtor = new Coordinata(new Point(-1, -1), new Point(-1, -1));

        }


        List<DostupnyyHod> allow_pole = new List<DostupnyyHod>();
        public struct FlangRokirovka
        {
            public Rokirovka right;
            public Rokirovka left;
        }
        public struct Rokirovka
        {
            public bool flag;
            public Point coordinata;
        }
        FlangRokirovka[] rok;
        int full_ochki = 0;

        private static Cells[,] pole; //выделяем память для структуры
        private static Point position; //тут будут находится координаты выбранной для хода фигуры
        private static bool stat = false; //переменная состояния
        private static int daeth_white = 0, daeth_black = 0;//счетчики срубленных фигур
        private static string player = "Белый";// если тру то ходит белый
        private void fill_figurs(bool flag) //расставляем фиогуры на поле
        {
            if (flag) //если flag = true расставляем черные фигуры
            {
                rok[1].left.flag = true;
                rok[1].left.coordinata = new Point(2, 0);
                rok[1].right.flag = true;
                rok[1].right.coordinata = new Point(6, 0);
                pole[0, 0].name_figur = Figura.figury[1, 1]; //Каждой фигуре присваеваем имя..
                pole[1, 0].name_figur = Figura.figury[1, 2];
                pole[2, 0].name_figur = Figura.figury[1, 3];
                pole[3, 0].name_figur = Figura.figury[1, 4];
                pole[4, 0].name_figur = Figura.figury[1, 5];
                pole[5, 0].name_figur = Figura.figury[1, 3];
                pole[6, 0].name_figur = Figura.figury[1, 2];
                pole[7, 0].name_figur = Figura.figury[1, 1];
                for (int i = 0; i < 8; i++)
                {
                    pole[i, 1].name_figur = Figura.figury[1, 0];
                }
            }
            else//белые
            {
                rok[0].left.flag = true;
                rok[0].left.coordinata = new Point(2, 7);
                rok[0].right.flag = true;
                rok[0].right.coordinata = new Point(6, 7);
                pole[0, 7].name_figur = Figura.figury[0, 1]; //Каждой фигуре присваеваем имя..
                pole[1, 7].name_figur = Figura.figury[0, 2];
                pole[2, 7].name_figur = (Figura.figury[0, 3]);
                pole[3, 7].name_figur = (Figura.figury[0, 4]);
                pole[4, 7].name_figur = (Figura.figury[0, 5]);
                pole[5, 7].name_figur = (Figura.figury[0, 3]);
                pole[6, 7].name_figur = (Figura.figury[0, 2]);
                pole[7, 7].name_figur = (Figura.figury[0, 1]);
                for (int i = 0; i < 8; i++)
                {
                    pole[i, 6].name_figur = (Figura.figury[0, 0]);
                }
            }
        }

        private void fill_empty_pole()
        {

            for (int y = 0; y < 8; y++) //тут идет заполнение поля черными и белыми клетками
            {
                for (int x = 0; x < 8; x++)
                {
                    pole[x, y].name_figur = null;

                }
            }
            drawing();
        }

        private void start()//Начальная расстановка
        {
            W_ochki = 0;
            B_ochki = 0;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            pole = new Cells[8, 8]; //pole[x,y] //Выделяем динамическую память для шахматной доски
            rok = new FlangRokirovka[2];
            for (int y = 0; y < 8; y++) //тут идет заполнение поля черными и белыми клетками
            {
                for (int x = 0; x < 8; x++)
                {
                    pole[x, y] = new Cells();
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
            foreach (DostupnyyHod p in allow_pole)
                if (p.cells.name_figur != null) //если поле name элемента не пустое значит это какаято фигура
                {//загружаем клетку с соответствующей фигурой
                    temp = new Bitmap(@"figurs\" + p.cells.name_figur.BorW + p.cells.name_figur.name.name + "На" + p.cells.pos_BorW + "Выд.bmp");
                    board.DrawImage(temp, p.cells.positionX, p.cells.positionY); //рисуем
                    temp.Dispose();//освобождаем временную переменную
                }
                else //если поле name элемента пустое значит это просто клетка
                {
                    if (p.cells.pos_BorW == "White") //Определяем цвет клетки
                        board.DrawImage(white_check, p.cells.positionX, p.cells.positionY); //рисуем
                    else
                        board.DrawImage(black_check, p.cells.positionX, p.cells.positionY);
                }

            white_check.Dispose(); //освобождаем занимаемую память
            black_check.Dispose();
            board.Dispose();
        }
        Dictionary<Cells, List<DostupnyyHod>> available_course = new Dictionary<Cells, List<DostupnyyHod>>();//доступные фигуры
        void dostup_hod()
        {
            available_course = dostup_hod(pole, player, rok);
        }
        Dictionary<Cells, List<DostupnyyHod>> dostup_hod(Cells[,] poles, string playeer, FlangRokirovka[] rok)
        {
            Dictionary<Cells, List<DostupnyyHod>> available_course = new Dictionary<Cells, List<DostupnyyHod>>();
            for (int i = 0; i < 8; i++)
                for (int g = 0; g < 8; g++)
                {
                    if (poles[i, g].name_figur != null)
                        if (poles[i, g].name_figur.BorW == playeer)
                        {

                            List<DostupnyyHod> allow_pole = alow_hod(poles, playeer, i, g, rok);
                            if (allow_pole.Count > 0)
                                available_course.Add(poles[i, g], allow_pole);
                        }
                }
            return available_course;
        }
        private void go(DostupnyyHod dh) //Функция перемещения фигур по полю
        {
            //и фигуру какого цвета он хочет передвинуть
            if (pole[dh.cells.X, dh.cells.Y].name_figur != null) //если игрок хоче кого-то срубить
            {

                if (pole[dh.cells.X, dh.cells.Y].name_figur.BorW != pole[position.X, position.Y].name_figur.BorW)//проверяем не хочит ли игрок срубить свою фигуру
                {
                    //MessageBox.Show("Вы срубили фигуру под названием: " + pole[x, y].name_figur.name+":)");//если все нормально выводим сообщение
                    if (pole[dh.cells.X, dh.cells.Y].name_figur.BorW == "Белый") //срубили, теперь повышаем счетчик срубленных фигур
                    {
                        daeth_white += pole[dh.cells.X, dh.cells.Y].name_figur.name.ochki;
                        W_ochki -= pole[dh.cells.X, dh.cells.Y].name_figur.name.ochki;
                    }
                    else
                    {
                        daeth_black += pole[dh.cells.X, dh.cells.Y].name_figur.name.ochki;
                        B_ochki -= pole[dh.cells.X, dh.cells.Y].name_figur.name.ochki;
                    }

                    pole[dh.cells.X, dh.cells.Y].name_figur = new Figura(pole[position.X, position.Y].name_figur); //передвигаем фигур
                    pole[position.X, position.Y].name_figur = null; //зануляем больше ненужные поля

                }
            }
            else //если игрок просто ходит
            {
                pole[dh.cells.X, dh.cells.Y].name_figur = new Figura(pole[position.X, position.Y].name_figur); //передвигаем фигур
                pole[position.X, position.Y].name_figur = null;
            }
            if (dh.cells.Y == 0 || dh.cells.Y == 7)
            {
                if (Figura.figury[Figura.WHITE, Figura.PESHKA].Compar(pole[dh.cells.X, dh.cells.Y].name_figur))
                    pole[dh.cells.X, dh.cells.Y].name_figur = new Figura(Figura.figury[0, 4]);
                if (Figura.figury[Figura.BLACK, Figura.PESHKA].Compar(pole[dh.cells.X, dh.cells.Y].name_figur))
                    pole[dh.cells.X, dh.cells.Y].name_figur = new Figura(Figura.figury[1, 4]);
            }
            if (dh.rokirovka)
            {
                pole[dh.ladyaEnd.X, dh.ladyaEnd.Y].name_figur = new Figura(pole[dh.ladyaBegin.X, dh.ladyaBegin.Y].name_figur);
                pole[dh.ladyaBegin.X, dh.ladyaBegin.Y].name_figur = null;
            }

            if (position.X == 0 && position.Y == 7 || dh.cells.X == 0 && dh.cells.Y == 7)
                rok[0].left.flag = false;
            if (position.X == 7 && position.Y == 7 || dh.cells.X == 7 && dh.cells.Y == 7)
                rok[0].right.flag = false;
            if (position.X == 0 && position.Y == 0 || dh.cells.X == 0 && dh.cells.Y == 0)
                rok[1].left.flag = false;
            if (position.X == 7 && position.Y == 0 || dh.cells.X == 7 && dh.cells.Y == 0)
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
            Invoke(new Action(() =>
            {
                label1.Text = "Взято белых " + daeth_white.ToString();//Выводим всякую информацию
                label2.Text = "Взято черных " + daeth_black.ToString();
                label3.Text = "Сейчас ходит " + player + " Игрок";
            }));
            drawing();

            //если игрок пытается передвинуть чужую фигуру,выводим напоминание
        }


        private Cells[,] go(int X, int Y, DostupnyyHod dh, Cells[,] poles, FlangRokirovka[] rok) //Функция перемещения фигур по полю
        {
            //проверяем какой игрок сейчас ходит
            //и фигуру какого цвета он хочет передвинуть

            if (poles[dh.cells.X, dh.cells.Y].name_figur != null)
                if (poles[dh.cells.X, dh.cells.Y].name_figur.name.name == Figura.KOROL_NAME)
                    MessageBox.Show("Пытаемся есть короля");
            poles[dh.cells.X, dh.cells.Y].name_figur = new Figura(poles[X, Y].name_figur); //передвигаем фигур
            poles[X, Y].name_figur = null;
            if (dh.cells.X == 0 || dh.cells.Y == 7)
            {
                if (Figura.figury[Figura.WHITE, Figura.PESHKA].Compar(poles[dh.cells.X, dh.cells.Y].name_figur))
                    poles[dh.cells.X, dh.cells.Y].name_figur = new Figura(Figura.figury[Figura.WHITE, Figura.FERZ]);
                if (Figura.figury[Figura.BLACK, Figura.PESHKA].Compar(poles[dh.cells.X, dh.cells.Y].name_figur))
                    poles[dh.cells.X, dh.cells.Y].name_figur = new Figura(Figura.figury[Figura.BLACK, Figura.FERZ]);
            }
            if (dh.rokirovka)
            {
                poles[dh.ladyaEnd.X, dh.ladyaEnd.Y].name_figur = new Figura(poles[dh.ladyaBegin.X, dh.ladyaBegin.Y].name_figur);
                poles[dh.ladyaBegin.X, dh.ladyaBegin.Y].name_figur = null;
            }
            if (X == 0 && Y == 7 || dh.cells.X == 0 && dh.cells.Y == 7)
                rok[0].left.flag = false;
            if (X == 7 && Y == 7 || dh.cells.X == 7 && dh.cells.Y == 7)
                rok[0].right.flag = false;
            if (X == 0 && Y == 0 || dh.cells.X == 0 && dh.cells.Y == 0)
                rok[1].left.flag = false;
            if (X == 7 && Y == 0 || dh.cells.X == 7 && dh.cells.Y == 0)
                rok[1].right.flag = false;
            if (X == 4 && Y == 7)
            {
                rok[0].left.flag = false;
                rok[0].right.flag = false;
            }
            if (X == 4 && Y == 0)
            {
                rok[1].left.flag = false;
                rok[1].right.flag = false;
            }
            return poles;
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
                new Thread(() => randi()).Start();
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
        string string_hod(Point begin, Point end)
        {
            return "" + char_from_number(begin.X) + (8 - begin.Y) + " - " + char_from_number(end.X) + (8 - end.Y);
        }
        private void check_moove(int x, int y) //X Y координаты, куда ходить
        {//Функция проверки корректности ходов всех фигур
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                if (DostupnyyHod.Contains(allow_pole, pole[x, y]))
                {
                    DostupnyyHod dh = DostupnyyHod.getOfCells(allow_pole, pole[x, y]);
                    if (player == "Белый")
                    {
                        W_povtor = new Coordinata(position, new Point(x, y));
                    }
                    else
                    {
                        B_povtor = new Coordinata(position, new Point(x, y));
                    }
                    string log = string_hod(position, new Point(x, y)) + ";";
                    Ctrl_Z z = new Ctrl_Z(pole[position.X, position.Y], pole[x, y], false, rok, player, W_ochki, B_ochki);
                    otmena.Push(z);
                    if (dh.rokirovka)
                    {
                        Ctrl_Z zx = new Ctrl_Z(pole[dh.ladyaBegin.X, dh.ladyaBegin.Y], pole[dh.ladyaEnd.X, dh.ladyaEnd.Y], true, rok, player, W_ochki, B_ochki);
                        otmena.Push(zx);
                    }
                    go(dh);
                    Invoke(new Action(() => { listBox1.Items.Insert(0, log); }));
                    dostup_hod();


                    if (!check_no_shah(pole, player))
                        if (available_course.Count == 0)
                            MessageBox.Show("Мат " + player);
                        else
                        {
                            MessageBox.Show("Шах " + player);
                            next_hod();

                        }
                    else
                        if (available_course.Count == 0)
                        MessageBox.Show("Пат");
                    else
                    {
                        next_hod();
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
        public void select_figura(int x, int y, Figura f)
        {
            pole[x, y].name_figur = f;
            drawing();
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)//обработчик события, который вызвается при
        //нажатии на клетку поля
        {
            if (check_Koordinate(e.X) >= 0 && check_Koordinate(e.Y) >= 0)
                if (rasstavit)
                {
                    int x = check_Koordinate(e.X);
                    int y = check_Koordinate(e.Y);
                    Select_figura select_Figura = new Select_figura(this, x, y);
                    select_Figura.Show();

                }
                else if (current_player() != "Компьютер")
                {
                    //проверяем не выходит ли если координата за границы
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
        }
        List<DostupnyyHod> kon_hod(int x, int y, Cells[,] poles)
        {
            List<DostupnyyHod> kon = new List<DostupnyyHod>();
            if (x - 1 >= 0 && y - 2 >= 0)
                kon.Add(new DostupnyyHod(poles[x - 1, y - 2]));
            if (x + 1 < 8 && y - 2 >= 0)
                kon.Add(new DostupnyyHod(poles[x + 1, y - 2]));
            if (x - 2 >= 0 && y - 1 >= 0)
                kon.Add(new DostupnyyHod(poles[x - 2, y - 1]));
            if (x + 2 < 8 && y - 1 >= 0)
                kon.Add(new DostupnyyHod(poles[x + 2, y - 1]));
            if (x - 2 >= 0 && y + 1 < 8)
                kon.Add(new DostupnyyHod(poles[x - 2, y + 1]));
            if (x + 2 < 8 && y + 1 < 8)
                kon.Add(new DostupnyyHod(poles[x + 2, y + 1]));
            if (x - 1 >= 0 && y + 2 < 8)
                kon.Add(new DostupnyyHod(poles[x - 1, y + 2]));
            if (x + 1 < 8 && y + 2 < 8)
                kon.Add(new DostupnyyHod(poles[x + 1, y + 2]));
            return kon;
        }
        List<DostupnyyHod> korol_hod(int posX, int posY, Cells[,] poles)
        {
            List<DostupnyyHod> allow_pole = new List<DostupnyyHod>();
            if (posX - 1 >= 0 && posY - 1 >= 0)
                allow_pole.Add(new DostupnyyHod(poles[posX - 1, posY - 1]));
            if (posY - 1 >= 0)
                allow_pole.Add(new DostupnyyHod(poles[posX, posY - 1]));
            if (posX + 1 < 8 && posY - 1 >= 0)
                allow_pole.Add(new DostupnyyHod(poles[posX + 1, posY - 1]));
            if (posX + 1 < 8)
                allow_pole.Add(new DostupnyyHod(poles[posX + 1, posY]));
            if (posX + 1 < 8 && posY + 1 < 8)
                allow_pole.Add(new DostupnyyHod(poles[posX + 1, posY + 1]));
            if (posY + 1 < 8)
                allow_pole.Add(new DostupnyyHod(poles[posX, posY + 1]));
            if (posX - 1 >= 0 && posY + 1 < 8)
                allow_pole.Add(new DostupnyyHod(poles[posX - 1, posY + 1]));
            if (posX - 1 >= 0)
                allow_pole.Add(new DostupnyyHod(poles[posX - 1, posY]));
            return allow_pole;
        }
        List<DostupnyyHod> korol_hod(int posX, int posY, Cells[,] poles, string playeer, FlangRokirovka[] rok)
        {
            List<DostupnyyHod> allow_pole = korol_hod(posX, posY, poles);
            if (check_no_shah(poles, playeer))
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
                        List<DostupnyyHod> hod = new List<DostupnyyHod>();
                        hod = hod_figury(posX, posY, -1, 0, poles, hod);
                        if (poles[0, Y].name_figur != null)
                            if (DostupnyyHod.Contains(hod, poles[0, Y]) && poles[0, Y].name_figur.name.name == Figura.LADYA_NAME)
                                allow_pole.Add(new DostupnyyHod(poles[rok[ind].left.coordinata.X, rok[ind].left.coordinata.Y], poles[0, Y], poles[rok[ind].left.coordinata.X + 1, Y]));


                    }
                    if (rok[ind].right.flag)
                    {
                        List<DostupnyyHod> hod = new List<DostupnyyHod>();
                        hod = hod_figury(posX, posY, 1, 0, poles, hod);
                        if (poles[7, Y].name_figur != null)
                            if (DostupnyyHod.Contains(hod, poles[7, Y]) && poles[7, Y].name_figur.name.name == Figura.LADYA_NAME)
                                allow_pole.Add(new DostupnyyHod(poles[rok[ind].right.coordinata.X, rok[ind].right.coordinata.Y], poles[7, Y], poles[rok[ind].right.coordinata.X - 1, Y]));
                    }
                }
            }

            return allow_pole;
        }
        List<DostupnyyHod> hod_figury(int x, int y, int xx, int yy, Cells[,] poles, List<DostupnyyHod> hod)
        {
            if (x + xx < 8 && x + xx >= 0 && y + yy < 8 && y + yy >= 0)
            {
                hod.Add(new DostupnyyHod(poles[x + xx, y + yy]));
                if (poles[x + xx, y + yy].name_figur == null)
                    return hod_figury(x + xx, y + yy, xx, yy, poles, hod);
                else return hod;

            }
            else return hod;
        }
        List<DostupnyyHod> slon_hod(int x, int y, Cells[,] poles)
        {
            List<DostupnyyHod> slon = new List<DostupnyyHod>();
            slon = hod_figury(x, y, -1, -1, poles, slon);
            slon = hod_figury(x, y, -1, 1, poles, slon);
            slon = hod_figury(x, y, 1, -1, poles, slon);
            slon = hod_figury(x, y, 1, 1, poles, slon);
            return slon;

        }
        List<DostupnyyHod> ferz_hod(int x, int y, Cells[,] poles)
        {
            List<DostupnyyHod> allow_pole = new List<DostupnyyHod>();
            allow_pole = hod_figury(x, y, 0, -1, poles, allow_pole);
            allow_pole = hod_figury(x, y, 0, 1, poles, allow_pole);
            allow_pole = hod_figury(x, y, -1, 0, poles, allow_pole);
            allow_pole = hod_figury(x, y, 1, 0, poles, allow_pole);
            allow_pole = hod_figury(x, y, -1, -1, poles, allow_pole);
            allow_pole = hod_figury(x, y, -1, 1, poles, allow_pole);
            allow_pole = hod_figury(x, y, 1, -1, poles, allow_pole);
            allow_pole = hod_figury(x, y, 1, 1, poles, allow_pole);
            return allow_pole;

        }
        List<DostupnyyHod> ladya_hod(int x, int y, Cells[,] poles)
        {
            List<DostupnyyHod> alow_pole = new List<DostupnyyHod>();
            alow_pole = hod_figury(x, y, 0, -1, poles, alow_pole);
            alow_pole = hod_figury(x, y, 0, 1, poles, alow_pole);
            alow_pole = hod_figury(x, y, -1, 0, poles, alow_pole);
            alow_pole = hod_figury(x, y, 1, 0, poles, alow_pole);
            return alow_pole;

        }
        List<DostupnyyHod> peska_hod(int posX, int posY, Cells[,] poles, string playeer)
        {
            List<DostupnyyHod> allow_pole = new List<DostupnyyHod>();
            if (Figura.figury[Figura.BLACK, Figura.PESHKA].Compar(poles[posX, posY].name_figur))
            {
                if (poles[posX, posY + 1].name_figur == null)
                    allow_pole.Add(new DostupnyyHod(poles[posX, posY + 1]));
                if (posY == 1)
                    if (poles[posX, posY + 2].name_figur == null && poles[posX, posY + 1].name_figur == null)
                        allow_pole.Add(new DostupnyyHod(poles[posX, posY + 2]));
                if (posX - 1 >= 0)
                    if (poles[posX - 1, posY + 1].name_figur != null)
                        if (poles[posX - 1, posY + 1].name_figur.BorW != playeer)
                            allow_pole.Add(new DostupnyyHod(poles[posX - 1, posY + 1]));
                if (posX + 1 < 8)
                    if (poles[posX + 1, posY + 1].name_figur != null)
                        if (poles[posX + 1, posY + 1].name_figur.BorW != playeer)
                            allow_pole.Add(new DostupnyyHod(poles[posX + 1, posY + 1]));

            }
            if (Figura.figury[Figura.WHITE, Figura.PESHKA].Compar(poles[posX, posY].name_figur))
            {
                if (poles[posX, posY - 1].name_figur == null)
                    allow_pole.Add(new DostupnyyHod(poles[posX, posY - 1]));
                if (posY == 6)
                    if (poles[posX, posY - 2].name_figur == null && poles[posX, posY - 1].name_figur == null)
                        allow_pole.Add(new DostupnyyHod(poles[posX, posY - 2]));
                if (posX - 1 >= 0)
                    if (poles[posX - 1, posY - 1].name_figur != null)
                        if (poles[posX - 1, posY - 1].name_figur.BorW != playeer)
                            allow_pole.Add(new DostupnyyHod(poles[posX - 1, posY - 1]));
                if (posX + 1 < 8)
                    if (poles[posX + 1, posY - 1].name_figur != null)
                        if (poles[posX + 1, posY - 1].name_figur.BorW != playeer)
                            allow_pole.Add(new DostupnyyHod(poles[posX + 1, posY - 1]));

            }
            return allow_pole;

        }
        bool check_no_shah(Cells[,] poles, string playeer)
        {
            int ix = -1; int gy = -1;
            Figura korol;
            if (playeer == "Белый")
                korol = Figura.figury[Figura.WHITE, Figura.KOROL];
            else korol = Figura.figury[Figura.BLACK, Figura.KOROL];
            for (int i = 0; i < 8; i++)
                for (int g = 0; g < 8; g++)
                {
                    if (korol.Compar(poles[i, g].name_figur))
                    {
                        ix = i;
                        gy = g;
                    }

                }
            if (ix == -1 || gy == -1)
            {
                MessageBox.Show(@" Как-то съели короля)");
                return false;
            }
            return check_no_ugroza(poles, ix, gy, playeer);
        }
        bool check_no_ugroza(Cells[,] poles, int ix, int gy, string playeer)
        {
            if (ix == -1 || gy == -1)
            {
                MessageBox.Show(@"Проверка на угрозу не существующей фигуры)");
                return false;
            }
            if (playeer == "Белый")
            {
                if (ix - 1 >= 0 && gy - 1 >= 0)
                    if (Figura.figury[1, 0].Compar(poles[ix - 1, gy - 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy - 1 >= 0)
                    if (Figura.figury[1, 0].Compar(poles[ix + 1, gy - 1].name_figur))
                        return false;
            }
            else
            {
                if (ix - 1 >= 0 && gy + 1 < 8)
                    if (Figura.figury[0, 0].Compar(poles[ix - 1, gy + 1].name_figur))
                        return false;
                if (ix + 1 < 8 && gy + 1 < 8d)
                    if (Figura.figury[0, 0].Compar(poles[ix + 1, gy + 1].name_figur))
                        return false;
            }
            int ind = 0;
            if (playeer == "Белый")
                ind = 1;
            else ind = 0;

            List<DostupnyyHod> figure = ladya_hod(ix, gy, poles);
            //bool rez=true;
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, Figura.LADYA].Compar(p.cells.name_figur))
                    return false;
            }
            figure = kon_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, Figura.KON].Compar(p.cells.name_figur))
                    return false;
            }
            figure = slon_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, Figura.SLON].Compar(p.cells.name_figur))
                    return false;
            }
            figure = ferz_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, Figura.FERZ].Compar(p.cells.name_figur))
                    return false;
            }
            figure = korol_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, Figura.KOROL].Compar(p.cells.name_figur))
                    return false;
            }
            return true;
        }
        Point check_coord_shah(Cells[,] poles, int ix, int gy, string playeer)
        {
            if (ix == -1 || gy == -1)
                MessageBox.Show(@"Несуществующее поле");
            if (playeer == "Белый")
            {
                if (ix - 1 >= 0 && gy - 1 >= 0)
                    if (Figura.figury[1, 0].Compar(poles[ix - 1, gy - 1].name_figur))
                        return new Point(ix - 1, gy - 1);
                if (ix + 1 < 8 && gy - 1 >= 0)
                    if (Figura.figury[1, 0].Compar(poles[ix + 1, gy - 1].name_figur))
                        return new Point(ix + 1, gy - 1);
            }
            else
            {
                if (ix - 1 >= 0 && gy + 1 < 8)
                    if (Figura.figury[0, 0].Compar(poles[ix - 1, gy + 1].name_figur))
                        return new Point(ix - 1, gy + 1);
                if (ix + 1 < 8 && gy + 1 < 8d)
                    if (Figura.figury[0, 0].Compar(poles[ix + 1, gy + 1].name_figur))
                        return new Point(ix + 1, gy + 1);
            }
            int ind = 0;
            if (playeer == "Белый")
                ind = 1;
            else ind = 0;

            List<DostupnyyHod> figure = ladya_hod(ix, gy, poles);
            //bool rez=true;
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, 1].Compar(p.cells.name_figur))
                    return new Point(p.cells.X, p.cells.Y);
            }
            figure = kon_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, 2].Compar(p.cells.name_figur))
                    return new Point(p.cells.X, p.cells.Y);
            }
            figure = slon_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, 3].Compar(p.cells.name_figur))
                    return new Point(p.cells.X, p.cells.Y);
            }
            figure = ferz_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, 4].Compar(p.cells.name_figur))
                    return new Point(p.cells.X, p.cells.Y);
            }
            figure = korol_hod(ix, gy, poles);
            foreach (DostupnyyHod p in figure)
            {
                if (Figura.figury[ind, 5].Compar(p.cells.name_figur))
                    return new Point(p.cells.X, p.cells.Y);
            }
            return new Point(-1, -1);
        }

        Random rand = new Random();
        void randi()
        {
            List<Hod> best_hodss = new List<Hod>();
            int evr = -999999;
            Point Begin = new Point(-1, -1);
            Point End = new Point(-1, -1);
            Dictionary<Hod, int> map_coefficents = new Dictionary<Hod, int>();
            dostup_hod();
            Invoke(new Action(() => { listBox2.Items.Insert(0, "---Следующий Ход---"); }));
            int count_threads = 0;
            foreach (Cells c in available_course.Keys)
            {
                List<DostupnyyHod> alow = available_course[c];
                foreach (DostupnyyHod k in alow)
                {
                    vychislenie_cofficienta(new Hod(c, k), map_coefficents);
                    //new Thread(() => vychislenie_cofficienta(new Hod(c, k), map_coefficents)).Start();
                    count_threads++;
                }

            }
            Invoke(new Action(() => { listBox2.Items.Insert(0, count_threads + " доступных ходов"); }));
            while (count_threads != map_coefficents.Count)
            {
                Thread.Sleep(10);
            }
            foreach (Hod hodd in map_coefficents.Keys)
            {
                int e = map_coefficents[hodd];
                if (e > evr)
                {

                    evr = e;

                    best_hodss.Clear();
                    best_hodss.Add(hodd);



                }
                else if (e == evr)
                {

                    best_hodss.Add(hodd);


                }
                if (evr == 100000)
                {
                    break;
                }



            }


            Hod hod = best_hodss[rand.Next(best_hodss.Count - 1)];
            position = new Point(hod.begin.X, hod.begin.Y);
            alow_hod(pole);
            check_moove(hod.hod.cells.X, hod.hod.cells.Y);
        }

        void vychislenie_cofficienta(Hod hodd, Dictionary<Hod, int> map_coefficents)
        {

            int e;
            int depth = 0;
            try
            {
                depth = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            //new Thread(() => randi()).Start();
            e = calculate(0, hodd.begin.X, hodd.begin.Y, hodd.hod, Cells.copy_cells(pole), copy_rokirovka(rok), player, depth);
            Invoke(new Action(() => { map_coefficents.Add(hodd, e); }));
            Invoke(new Action(() => { listBox2.Items.Insert(0, string_hod(new Point(hodd.begin.X, hodd.begin.Y), new Point(hodd.hod.cells.X, hodd.hod.cells.Y)) + " = " + e + ";"); }));
        }

        int calculate(int count, int X, int Y, DostupnyyHod dh, Cells[,] poles, FlangRokirovka[] rok, string playeer, int depth)
        {
            int now_evr = evristic(X, Y, dh, playeer, poles, rok);
            if (depth == count)
            {
                return now_evr;
            }
            else
            {
                FlangRokirovka[] neo_rok = copy_rokirovka(rok);
                Cells[,] neo_pole = go(X, Y, dh, Cells.copy_cells(poles), neo_rok);
                Dictionary<Cells, List<DostupnyyHod>> av_cc = dostup_hod(neo_pole, anti_player(playeer), neo_rok);
                if (check_mat(av_cc, neo_pole, anti_player(playeer)))
                    return 100000 - count;
                if (check_pat(av_cc, neo_pole, anti_player(playeer)))
                    return -100000;
                int evr = -999999;
                List<Hod> max_coords_antiplayer = new List<Hod>();
                foreach (Cells cc in av_cc.Keys)
                {
                    List<DostupnyyHod> aloww = av_cc[cc];
                    foreach (DostupnyyHod kk in aloww)
                    {
                        int e = evristic(cc.X, cc.Y, kk, anti_player(playeer), neo_pole, neo_rok);
                        if (e > evr)
                        {

                            evr = e;
                            max_coords_antiplayer.Clear();
                            Hod hodd = new Hod(cc, kk);
                            max_coords_antiplayer.Add(hodd);



                        }
                        else if (e == evr)
                        {


                            Hod hodd = new Hod(cc, kk);
                            max_coords_antiplayer.Add(hodd);


                        }

                    }
                }
                Hod hod = max_coords_antiplayer[rand.Next(max_coords_antiplayer.Count - 1)];
                neo_rok = copy_rokirovka(neo_rok);
                neo_pole = go(hod.begin.X, hod.begin.Y, hod.hod, Cells.copy_cells(neo_pole), neo_rok);
                av_cc = dostup_hod(neo_pole, playeer, neo_rok);
                if (check_mat(av_cc, neo_pole, playeer))
                    return -100000;
                av_cc = dostup_hod(neo_pole, playeer, neo_rok);
                evr = -999999;
                foreach (Cells cc in av_cc.Keys)
                {
                    List<DostupnyyHod> aloww = av_cc[cc];
                    foreach (DostupnyyHod kk in aloww)
                    {
                        int e = calculate(count + 1, cc.X, cc.Y, kk, Cells.copy_cells(neo_pole), copy_rokirovka(neo_rok), playeer, depth);
                        if (e == 100000 - count - 1)
                            return e;
                        if (e > evr)
                            evr = e;

                    }
                }
                return now_evr + evr;
            }
        }
        string anti_player(string playeer)
        {
            string aplayer;
            if (playeer == "Белый")
                aplayer = "Черный";
            else aplayer = "Белый";
            return aplayer;
        }
        bool check_mat(Dictionary<Cells, List<DostupnyyHod>> dost_hod, Cells[,] poles, string playeer)
        {
            if (dost_hod.Count == 0)
                if (!check_no_shah(poles, playeer))
                    return true;
            return false;
        }
        bool check_pat(Dictionary<Cells, List<DostupnyyHod>> dost_hod, Cells[,] poles, string playeer)
        {
            if (dost_hod.Count == 0)
                if (check_no_shah(poles, playeer))
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

        int evristic(int X, int Y, DostupnyyHod dh, string playeer, Cells[,] pole, FlangRokirovka[] rok)
        {
            int evr = 0;
            string aplayer = anti_player(playeer);
            Cells[,] poles = Cells.copy_cells(pole);//если что изменить
            FlangRokirovka[] roks = copy_rokirovka(rok);
            int eda = 0;
            int you_mat = 0;
            if (poles[dh.cells.X, dh.cells.Y].name_figur != null)
                eda = poles[dh.cells.X, dh.cells.Y].name_figur.name.ochki;
            poles = go(X, Y, dh, poles, roks);
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
            Dictionary<Cells, List<DostupnyyHod>> dost_hod_aplayer = dostup_hod(poles, aplayer, roks);
            Dictionary<Cells, List<DostupnyyHod>> dost_hod_player = dostup_hod(poles, player, roks);
            if (dost_hod_aplayer.Count == 0)
                if (check_no_shah(poles, aplayer))
                    pat = 1;
                else
                    mat = 1;
            else if (!check_no_shah(poles, aplayer))
                shah = 1;
            if (playeer == "Белый")
            {
                if (W_povtor.Compar(X, Y, dh.cells.X, dh.cells.Y))
                    povt = -100;
            }
            else
            {
                if (B_povtor.Compar(X, Y, dh.cells.X, dh.cells.Y))
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
                                if (!check_no_ugroza(poles, i, g, playeer))
                                    my_udar += poles[i, g].name_figur.name.ochki;
                                if (dost_hod_player.ContainsKey(poles[i, g]))
                                {
                                    List<DostupnyyHod> a = dost_hod_player[poles[i, g]];
                                    if (poles[i, g].name_figur.name.name == Figura.KOROL_NAME)
                                        my_cells_korol = a.Count;
                                }
                                else
                                    my_svyas++;

                            }
                            else
                            {
                                if (!check_no_ugroza(poles, i, g, aplayer))
                                {
                                    Point pnt = check_coord_shah(poles, i, g, aplayer);
                                    if (check_no_ugroza(poles, pnt.X, pnt.Y, player))
                                        you_udar += poles[i, g].name_figur.name.ochki;
                                }
                                if (dost_hod_aplayer.ContainsKey(poles[i, g]))
                                {
                                    List<DostupnyyHod> al_hod = dost_hod_aplayer[poles[i, g]];
                                    if (Figura.figury[0, 5].name.Compar(poles[i, g].name_figur.name))
                                        you_cells_korol = al_hod.Count;
                                    foreach (DostupnyyHod p in al_hod)
                                    {
                                        FlangRokirovka[] neo_rok = copy_rokirovka(roks);
                                        Cells[,] neo_pole = go(i, g, p, Cells.copy_cells(poles), neo_rok);
                                        Dictionary<Cells, List<DostupnyyHod>> dost_hod_neo = dostup_hod(neo_pole, playeer, neo_rok);
                                        if (dost_hod_neo.Count == 0)
                                            if (!check_no_shah(neo_pole, playeer))
                                                you_mat = 1;


                                    }
                                }
                                else
                                    you_svyas++;

                            }
                        }
                        else
                        {
                            if (!check_no_ugroza(poles, i, g, player))
                                you_cells++;
                            if (!check_no_ugroza(poles, i, g, aplayer))
                                my_cells++;

                        }

                    }

            evr = k_mat * mat + shah + povt + k_pat * pat - 1000 * you_mat - 20 * my_udar - 4 * my_svyas
                + 8 * you_udar + 4 * you_svyas + 20 * eda + my_cells - you_cells + 8 * my_cells_korol - 8 * you_cells_korol;
            return evr;
        }

        List<DostupnyyHod> add_alow(List<DostupnyyHod> figure, string playeer, int PX, int PY, Cells[,] poles, FlangRokirovka[] rok)
        {

            List<DostupnyyHod> allow_pole = new List<DostupnyyHod>();
            foreach (DostupnyyHod p in figure)
            {
                if (p.cells.name_figur == null)
                {
                    if (check_no_shah(go(PX, PY, p, Cells.copy_cells(poles), copy_rokirovka(rok)), playeer))
                    {
                        allow_pole.Add(p);
                    }
                }
                else
                {
                    if (p.cells.name_figur.BorW != playeer && p.cells.name_figur.name.name != Figura.KOROL_NAME)
                        if (check_no_shah(go(PX, PY, p, Cells.copy_cells(poles), copy_rokirovka(rok)), playeer))
                        {
                            allow_pole.Add(p);
                        }
                }
            }
            return allow_pole;
        }

        void alow_hod(Cells[,] pole)
        {
            allow_pole = alow_hod(pole, player, position.X, position.Y, rok);
        }


        List<DostupnyyHod> alow_hod(Cells[,] poles, string playeer, int posX, int posY, FlangRokirovka[] rok)
        {
            List<DostupnyyHod> allow_pole = new List<DostupnyyHod>();
            ArrayList hod_rok = new ArrayList();
            if (poles[posX, posY].name_figur != null)
            {
                switch (poles[posX, posY].name_figur.name.name)
                {
                    case Figura.PESHKA_NAME:
                        allow_pole = peska_hod(posX, posY, poles, playeer);
                        break;
                    case Figura.LADYA_NAME:
                        allow_pole = ladya_hod(posX, posY, poles);
                        break;
                    case Figura.KON_NAME:
                        allow_pole = kon_hod(posX, posY, poles);
                        break;
                    case Figura.SLON_NAME:
                        allow_pole = slon_hod(posX, posY, poles);
                        break;
                    case Figura.FERZ_NAME:
                        allow_pole = ferz_hod(posX, posY, poles);
                        break;
                    case Figura.KOROL_NAME:
                        allow_pole = korol_hod(posX, posY, poles, playeer, rok);
                        break;
                    default: break;
                }
            }
            allow_pole = add_alow(allow_pole, playeer, posX, posY, poles, rok);
            return allow_pole;

        }

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

        void next_hod()
        {
            Invoke(new Action(() =>
            {
                if (current_player() == "Компьютер")
                {
                    new Thread(() => randi()).Start();
                }
            }));
        }

        string current_player()
        {
            if (player == "Черный")
            {
                return (string)black_c.SelectedItem;
            }
            else return (string)white_c.SelectedItem;
        }


        bool rasstavit = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (!rasstavit)
            {
                fill_empty_pole();
                rasstavit = true;
                button3.Text = "Закончить расстановку";
                drawing();

            }
            else
            {
                rasstavit = false;
                for (int i = 0; i < rok.Length; i++)
                {
                    rok[i].left.flag = false;
                    rok[i].right.flag = false;

                }
                button3.Text = "Расставить фигуры";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            player = anti_player(player);
            label3.Text = "Сейчас ходит " + player + " Игрок";
            drawing();
            next_hod();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (otmena.Count != 0)
            {
                Ctrl_Z z = (Ctrl_Z)otmena.Pop();
                pole[z.Begin.X, z.Begin.Y] = z.Begin;
                pole[z.End.X, z.End.Y] = z.End;
                if (z.rok)
                {
                    z = (Ctrl_Z)otmena.Pop();
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

        private FlangRokirovka[] copy_rokirovka(FlangRokirovka[] rok)
        {
            FlangRokirovka[] copy_rok = new FlangRokirovka[2];
            copy_rok[0] = rok[0];
            copy_rok[1] = rok[1];
            return copy_rok;
        }

    }




}//КОНЕЦ