using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsCommandParser
{
    abstract class Shape : Shapes
    {
        protected Color c;
        protected int x, y;
        

        public Shape()
        {
            c = Color.Black;
            x = y = 100;
            
        }
        public Shape(Color c,int x, int y) 
        {
            this.c = c;
            this.x = x;
            this.y = y;
        }

        public abstract void draw(Graphics g, Pen p); //public abstract void draw(Graphics g);

        public abstract double calcArea();

        public virtual void set(Color c, params int[] list)
        {
            this.c = c;
            this.x = list[0];
            this.y = list[1];  
        }
    }
}
