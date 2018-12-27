using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = null;
        }

        int Cx, Cy;
        int count = 0;
        ShapeFactory sf = new ShapeFactory();
        Regex regex = new Regex(@"\d+");
        List<Variable> localVars = new List<Variable>();
        Pen p = new Pen(Color.Red);
        
        private void button1_click(object sender, EventArgs e)
        {
            string test = textBox1.Text;
            string[] cmdLine = test.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("array length: " + cmdLine.Length);
            List<string> command = cmdLine.ToList();
            while (count < command.Count)
            {
                if (command[count].StartsWith("rectangle"))
                {
                    Regex pattern = new Regex(@"(?<command>\w+)\s(?<width>\d+)\s(?<height>\d+)");
                    Match match = pattern.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("rectangle"))
                    {
                        int width = int.Parse(match.Groups["width"].Value);
                        int height = int.Parse(match.Groups["height"].Value);
                        Shape s = sf.GetShape("rectangle");
                        s.set(Color.Black, Cx, Cy, width, height);
                        s.draw(panel1.CreateGraphics());
                    }
                    else
                    {
                        label1.Text = "Incorrect syntax for Rectangle at line " + (count+1) + ": Stopping execution";
                        return;
                    }
                }
                else if (command[count].StartsWith("circle"))
                {
                    Regex reg = new Regex(@"(?<command>\w+)\s(?<radius>\d+)");
                    Match match = reg.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("circle"))
                    {
                        int rad = int.Parse(match.Groups["radius"].Value);
                        Shape s = sf.GetShape("circle");
                        s.set(Color.Black, Cx, Cy, rad);
                        s.draw(panel1.CreateGraphics());
                    }
                    else
                    {
                        label1.Text = "Incorrect syntax for Circle at line " + (count + 1) + ": Stopping execution";
                        return;
                    }
                }
                else if (command[count].StartsWith("triangle"))
                {
                    Regex pat = new Regex(@"(?<command>\w+)\s(?<a>\d+)\s(?<b>\d+)");
                    Match match = pat.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("triangle"))
                    {
                        int a = int.Parse(match.Groups["a"].Value);
                        int b = int.Parse(match.Groups["b"].Value);
                        Shape s = sf.GetShape("triangle");
                        s.set(Color.Black, Cx, Cy, a, b);
                        s.draw(panel1.CreateGraphics());
                    }
                    else
                    {
                        label1.Text = "Incorrect syntax for Triangle at line " + (count + 1) + ": Stopping execution";
                        return;
                    }
                }
                else if (command[count].StartsWith("up")) //PEN UP
                {
                    Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
                    Match match = reg.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("up"))
                    {
                        int newY = Int32.Parse(match.Groups["param"].Value) - Cy;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, Cx, -newY);
                        Cy = -newY;
                    }
                    else
                        label1.Text = "Incorrect syntax for Up at line " + (count + 1) + ": Stopping execution";
                }
                else if (command[count].StartsWith("down")) //PEN DOWN
                {
                    Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
                    Match match = reg.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("down"))
                    {
                        int newY = Int32.Parse(match.Groups["param"].Value) + Cy;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, Cx, newY);
                        Cy = newY;
                    }
                    else
                        label1.Text = "Incorrect syntax for Up at line " + (count + 1) + ": Stopping execution";
                }
                else if (command[count].StartsWith("right")) //PEN RIGHT
                {
                    Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
                    Match match = reg.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("right"))
                    {
                        int newX = Int32.Parse(match.Groups["param"].Value) + Cx;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, newX, Cy);
                        Cx = newX;    
                    }
                    else
                        label1.Text = "Incorrect syntax for Right at line " + (count + 1) + ": Stopping execution";
                }
                else if (command[count].StartsWith("left")) //PEN LEFT
                {
                    Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
                    Match match = reg.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("left"))
                    {
                        int newX = Int32.Parse(match.Groups["param"].Value) + Cx;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, -newX, Cy);
                        Cx = -newX;  
                    }
                    else
                        label1.Text = "Incorrect syntax for Left at line " + (count + 1) + ": Stopping execution";
                }
                else if (command[count].StartsWith("movePen"))
                {
                    Regex pattern = new Regex(@"(?<command>\w+)\s(?<xcoord>\d+)\s(?<ycoord>\d+)");
                    Match match = pattern.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("movePen"))
                    {
                        int xcoord = int.Parse(match.Groups["xcoord"].Value);
                        int ycoord = int.Parse(match.Groups["ycoord"].Value);
                        Cx = xcoord;
                        Cy = ycoord;
                    }
                    else
                        label1.Text = "Incorrect syntax for movePen at line " + (count + 1) + ": Stopping execution";
                }
                else if (command[count].StartsWith("drawTo"))
                {
                    Regex pattern = new Regex(@"(?<command>\w+)\s(?<xcoord>\d+)\s(?<ycoord>\d+)");
                    Match match = pattern.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("drawTo"))
                    {
                        int xcoord = int.Parse(match.Groups["xcoord"].Value);
                        int ycoord = int.Parse(match.Groups["ycoord"].Value);
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, xcoord, ycoord);
                        Cx = xcoord;
                        Cy = ycoord;
                    }
                }
                else if (command[count].Equals("loop"))
                {
                    int loopcounter = count;
                    while (!command[loopcounter].StartsWith("endloop"))
                    {
                        Console.WriteLine(command[loopcounter]);
                        loopcounter++;
                    }
                    Console.WriteLine("Start loop at line " + (count + 1));
                    Console.WriteLine("end loop at line " + (loopcounter + 1));
                    int NoOfLines = loopcounter - count - 1;
                    List<string> looper = command.GetRange(count + 1, NoOfLines);
                    command.AddRange(looper);               //must insert at index not at end
                }
                else if (command[count].StartsWith("repeat"))
                {
                    Regex pattern = new Regex(@"(?<command>\w+)\s(?<num>\d+)\s(?<shape>\w+)\s(?<mod>\+|\-)(?<val>\d+)");
                    Match match = pattern.Match(command[count]);
                    string com = match.Groups["command"].Value;
                    if (match.Success && com.Equals("repeat"))
                    {
                        int repcounter = 0;
                        int numreps = int.Parse(match.Groups["num"].Value);
                        string shape = match.Groups["shape"].Value;
                        string modifier = match.Groups["mod"].Value;            //take into account negative increment
                        int increment = int.Parse(match.Groups["val"].Value);
                        int value = 50; //base shape size
                        int count2 = count;
                        while (repcounter < numreps)
                        {
                            if (modifier.Equals("-"))
                                value = value - increment;
                            else
                                value = value + increment;
                            string newCommand = null;
                            switch (shape)
                            {
                                case "rectangle":
                                    newCommand = shape + " " + value + " " + value;
                                    break;
                                case "triangle":
                                    newCommand = shape + " " + value + " " + value;
                                    break;
                                case "circle":
                                    newCommand = shape + " " + value;
                                    break;
                            }
                            command.Insert(count2+1, newCommand); //infinite loop
                            repcounter++;
                            count2++;
                        }
                    }
                }
                else if (command[count].StartsWith("var")) //variable creator
                {
                    Regex pattern = new Regex(@"(?<name>\w+)\s(?<value>\d+)");
                    Match match = pattern.Match(command[count]);
                    if (match.Success)
                    {
                        string name = match.Groups["name"].Value;
                        int value = int.Parse(match.Groups["value"].Value);
                        Console.WriteLine("name = " + name);
                        Console.WriteLine("value = " + value);
                        Variable var = new Variable(name, value);
                        localVars.Add(var);
                    }        
                }
                else if (command[count].StartsWith("if"))
                {

                }
                else
                {
                    int vcounter = 0;
                    foreach (Variable var in localVars)
                    {
                        if(command[count].StartsWith(localVars[vcounter].GetName()))
                        {
                            Console.WriteLine("MATCH");
                        }
                        else
                        {
                            Console.WriteLine("NO MATCH");
                        }
                        vcounter++;
                    }
                }
                count++;
            } 
        }

        private void export_click(object sender, EventArgs e)
        {
            Console.WriteLine("Exporting text");
        }

        private void button2_click(object sender, EventArgs e)
        {
            textBox1.Clear();
            panel1.Refresh();
            count = 0;
            Cx = Cy = 0;
        }
    }
}

    internal class TextRange
    {

    }