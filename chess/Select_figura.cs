using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chess
{
    public partial class Select_figura : Form
    {
        public Select_figura(Form1 form,int x,int y)
        {
            InitializeComponent();
            this.form = form;
            this.x = x;
            this.y = y;
        }
        Form1 form;
        int x, y;
        private void belaya_peshka_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[0, 0]);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[0, 1]);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[0, 2]);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[0, 3]);
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[0, 4]);
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[0, 5]);
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[1, 0]);
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[1, 1]);
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[1, 2]);
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[1, 3]);
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[1, 4]);
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            form.select_figura(x, y, null);
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            form.select_figura(x,y,Figura.figury[1, 5]);
            this.Close();
        }
    }
}
