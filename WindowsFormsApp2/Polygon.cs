using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsCommandParser
{
    class Polygon : Shape
    {
        Point[] points;

        public Polygon() : base()
        {
            points = new Point[] { new Point { X = 10, Y = 10 }, new Point { X = 20, Y = 10 }, new Point { X = 20, Y = 20 } };
        }
        public Polygon(Color color, Point[] points)
        {
            this.points = points;
        }
        public override double calcArea()
        {
            throw new NotImplementedException();
        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(base.c);
            g.DrawPolygon(p, points);
        }
    }
}
