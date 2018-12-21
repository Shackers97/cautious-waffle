using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Triangle : Shape
    {
        Point[] points;
        public Triangle() : base()
        {
            points[0] = new Point(10, 10);
            points[1] = new Point(100, 10);
            points[2] = new Point(50, 100);
        }
        public Triangle(Color c, int x, int y, params Point[] points) : base(c, x, y)
        {
            this.points = points;
        }
        public override double calcArea()
        {
            throw new NotImplementedException();
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush sb = new SolidBrush(c);
            //g.FillPolygon(sb, points);
            g.DrawPolygon(p, points);
        }

        public override void set(Color c, params int[] list)
        {
            base.set(c, list[0], list[1]);
            //this.points = list[2];
        }
    }
}
