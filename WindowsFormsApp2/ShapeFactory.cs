using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsCommandParser
{
    class ShapeFactory
    {
        public Shape GetShape(String shape)
        {
            if (shape.Equals("rectangle"))
            {
                return new Rectangle();
            }
            else if (shape.Equals("circle"))
            {
                return new Circle();
            }
            else if (shape.Equals("triangle"))
            {
                return new Triangle();
            }
            
            else
            {
                System.ArgumentException argex = new System.ArgumentException("Shape does not exist");
                throw argex;
            }
        }

    }
}
