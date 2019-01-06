using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsCommandParser
{
    class Triangle : Shape
    {
        int a, b;
        double hyp;

        public Triangle() : base()
        {
            a = 50;
            b = 50;
            hyp = Math.Sqrt(a ^ 2 + b ^ 2);
        }
        public Triangle(Color c, int x, int y, int a, int b) : base(c, x, y)
        {
            this.a = a;
            this.b = b;
            hyp = Math.Sqrt(this.a ^ 2 + this.b ^ 2);
        }
        public override double calcArea()
        {
            throw new NotImplementedException();
        }

        public override void draw(Graphics g, Pen p)//public override void draw(Graphics g)
        {
            //Pen p = new Pen(base.c);
            
            SolidBrush sb = new SolidBrush(c);
            Point[] dims = this.GetPoints();
            //g.FillPolygon(sb, dims);
            g.DrawPolygon(p, dims);
        }

        public override void set(Color c, params int[] list)
        {
            base.set(c, list[0], list[1]);
            this.a = list[2];
            this.b = list[3];
            
        }

        public Point[] GetPoints()
        {
            Point[] points = new Point[] { new Point(base.x, base.y), new Point(base.x, base.y+a), new Point(base.x+b, base.y+a) };
            return points;
        }
    }
}
