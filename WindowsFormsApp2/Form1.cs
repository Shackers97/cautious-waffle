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

            while (count < cmdLine.Length)
            {
                if (cmdLine[count].StartsWith("rect"))
                {
                    Shape s = sf.GetShape("rectangle");
                    s.set(Color.Black, 50, 50, 100, 100);
                    s.draw(panel1.CreateGraphics());
                }
                else if (cmdLine[count].StartsWith("up")) //PEN UP
                {
                    Match match = regex.Match(cmdLine[count]);
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
                else if (cmdLine[count].StartsWith("down")) //PEN DOWN
                {
                    Match match = regex.Match(cmdLine[count]);
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
                else if (cmdLine[count].StartsWith("right")) //PEN RIGHT
                {
                    Match match = regex.Match(cmdLine[count]);
                    if (match.Success)
                    {
                        int newX = Int32.Parse(match.Value) + Cx;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, newX, Cy);
                        Cx = newX;
                        Console.WriteLine(match.Value + " Pen x: " + Cx + " Pen y: " + Cy);
                    }
                }
                else if (cmdLine[count].StartsWith("left")) //PEN LEFT
                {
                    Match match = regex.Match(cmdLine[count]);
                    if (match.Success)
                    {
                        int newX = Int32.Parse(match.Value) + Cx;
                        panel1.CreateGraphics().DrawLine(p, Cx, Cy, -newX, Cy);
                        Cx = -newX;
                        Console.WriteLine(match.Value + " Pen x: " + Cx + " Pen y: " + Cy);
                    }
                }
                else if (cmdLine[count].StartsWith("movePen"))
                {
                    Regex pattern = new Regex(@"(?<xcoord>\d+)\s(?<ycoord>\d+)");
                    Match match = pattern.Match(cmdLine[count]);
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