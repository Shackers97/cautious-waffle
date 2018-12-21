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
        }

        int Cx, Cy;
        int count = 0;
        ShapeFactory sf = new ShapeFactory();
        Regex regex = new Regex(@"\d+");

        private void button1_click(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.Red);
            string test = textBox1.Text;
            string[] cmdLine = test.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("array length: " + cmdLine.Length);
            List<string> command = cmdLine.ToList();
            while (count < command.Count)
            {
                if (command[count].StartsWith("rectangle"))
                {
                    Regex pattern = new Regex(@"(?<width>\d+)\s(?<height>\d+)");
                    Match match = pattern.Match(command[count]);
                    if (match.Success)
                    {
                        int width = int.Parse(match.Groups["width"].Value);
                        int height = int.Parse(match.Groups["height"].Value);
                        Shape s = sf.GetShape("rectangle");
                        s.set(Color.Black, Cx, Cy, width, height); //rectangle of height/width 100
                        s.draw(panel1.CreateGraphics());
                    }         
                }
                else if (command[count].StartsWith("circle"))
                {
                    Match match = regex.Match(command[count]);
                    if (match.Success)
                    {
                        int rad = int.Parse(match.Value);
                        Shape s = sf.GetShape("circle");
                        s.set(Color.Black, Cx, Cy, rad);
                        s.draw(panel1.CreateGraphics());
                    }                    
                }
                else if (command[count].StartsWith("triangle"))
                {
                    //Point[] points;
                    //points[0] = new Point(10, 10);
                    //points[0] = new Point(100, 10);
                    //points[0] = new Point(50, 100);

                    //Shape s = sf.GetShape("triangle");
                    //s.set(Color.Black, Cx, Cy, points);
                }
                else if (command[count].StartsWith("up")) //PEN UP
                {
                    Match match = regex.Match(command[count]);
                    if (match.Success)
                    {
                        int newY = Int32.Parse(match.Value) + Cy;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, Cx, -newY);
                        Cy = -newY;
                        Console.WriteLine("Val: " + match.Value +
                            " Cx: " + Cx +
                            " Cy: " + Cy);
                    }
                    else
                        Console.WriteLine("Wrong Input");
                }
                else if (command[count].StartsWith("down")) //PEN DOWN cmdLine[count].StartsWith("down")
                {
                    Match match = regex.Match(command[count]);
                    if (match.Success)
                    {
                        int newY = Int32.Parse(match.Value) + Cy;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, Cx, newY);
                        Cy = newY;
                        Console.WriteLine("Val: " + match.Value +
                            " Cx: " + Cx +
                            " Cy: " + Cy);
                    }
                    else
                        Console.WriteLine("Wrong Input");
                }
                else if (command[count].StartsWith("right")) //PEN RIGHT
                {
                    Match match = regex.Match(command[count]);
                    if (match.Success)
                    {
                        int newX = Int32.Parse(match.Value) + Cx;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, newX, Cy);
                        Cx = newX;
                        Console.WriteLine(match.Value + " Pen x: " + Cx + " Pen y: " + Cy);
                    }
                }
                else if (command[count].StartsWith("left")) //PEN LEFT
                {
                    Match match = regex.Match(command[count]);
                    if (match.Success)
                    {
                        int newX = Int32.Parse(match.Value) + Cx;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, -newX, Cy);
                        Cx = -newX;
                        Console.WriteLine(match.Value + " Pen x: " + Cx + " Pen y: " + Cy);
                    }
                }
                else if (command[count].StartsWith("movePen"))
                {
                    Regex pattern = new Regex(@"(?<xcoord>\d+)\s(?<ycoord>\d+)");
                    Match match = pattern.Match(command[count]);
                    //MatchCollection matches = pattern.Matches(cmdLine[count])
                    if (match.Success)
                    {
                        int xcoord = int.Parse(match.Groups["xcoord"].Value);
                        int ycoord = int.Parse(match.Groups["ycoord"].Value);
                        Console.WriteLine("xcoord " + xcoord);
                        Cx = xcoord;
                        Console.WriteLine("ycoord " + ycoord);
                        Cy = ycoord;
                    }
                    else
                        Console.WriteLine("false");
                }
                else if (command[count].StartsWith("loop"))
                {
                    int loopcounter = count;
                    while (!command[loopcounter].StartsWith("endloop")) 
                    {
                        Console.WriteLine(command[loopcounter]);
                        loopcounter++;
                    }
                    Console.WriteLine("Start loop at line " + (count+1));
                    Console.WriteLine("end loop at line " + (loopcounter+1));
                    int NoOfLines = loopcounter - count - 1;
                    //string[] looplist = new string[NoOfLines];
                    //Array.Copy(cmdLine, (count+1), looplist, 0, NoOfLines); //refactor to list
                    List<string> looper = command.GetRange(count + 1, NoOfLines);
                    command.AddRange(looper);
                }
                count++;
            } 
        }

        private void button2_click(object sender, EventArgs e)
        {
            textBox1.Clear();
            count = 0;
        }
    }
}

    internal class TextRange
    {

    }