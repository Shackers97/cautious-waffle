using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class ShapeFactory
    {
        public Shape GetShape(String shape)
        {
            if (shape.Equals("rectangle"))
            {
                return new Rectangle();
            }
            else
            {
                System.ArgumentException argex = new System.ArgumentException("Test Exception");
                throw argex;
            }
        }

    }
}
