using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Rectangle : Shape
    {
        int width, height;
        public Rectangle() : base()
        {
            width = 100;
            height = 100;
        }
        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width;
            this.height = height;
        }
        public override double calcArea()
        {
            return width * height;
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush sb = new SolidBrush(c);
            g.FillRectangle(sb, x, y, width, height);
            g.DrawRectangle(p, x, y, width, height);
        }

        public override void set(Color c, params int[] list)
        {
            base.set(c, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];
        }
    }
}
