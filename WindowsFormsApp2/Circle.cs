using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsCommandParser
{
    class Circle : Shape
    {
        int rad;

        public Circle() : base()
        {
            rad = 50;
        }
        public Circle(Color c, int x, int y, int rad) : base(c, x, y)
        {
            this.rad = rad;
        }

        public override double calcArea()
        {
            return Math.PI * (rad * rad);
        }

        public override void draw(Graphics g, Pen p) //public override void draw(Graphics g)
        {
            //Pen p = new Pen(base.c);
            SolidBrush sb = new SolidBrush(c);
            //g.FillEllipse(sb, x, y, rad * 2, rad * 2);
            g.DrawEllipse(p, x, y, rad * 2, rad * 2);
        }

        public override void set(Color c, params int[] list)
        {
            base.set(c, list[0], list[1]);
            this.rad = list[2];
            
        }
    }
}
