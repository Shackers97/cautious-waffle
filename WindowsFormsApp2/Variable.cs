using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Variable
    {
        string name;
        int value;

        public Variable()
        {
            name = null;
            value = 0;
        }

        public Variable(String name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public string GetName()
        {
            return name;
        }
        public int GetValue()
        {
            return value;
        }
        public void SetValue(int value)
        {
            this.value = value;
        }
    }
}
