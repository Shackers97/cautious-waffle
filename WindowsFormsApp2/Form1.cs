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
        Regex regex = new Regex(@"\d+");

        private void button1_click(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.Red);
            string test = textBox1.Text;
            string[] cmdLine = test.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("array length: " + cmdLine.Length);

            while (count < cmdLine.Length)
            {
                if (cmdLine[count].StartsWith("move"))
                {
                    Match match = regex.Match(cmdLine[count]);
                    if (match.Success)
                    {
                        Console.WriteLine(match.Value);
                        //update pen coordinates
                    }
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
                    Regex pattern = new Regex(@"\d+\s\d+");
                    Match testpat = pattern.Match(cmdLine[count]);
                    if (testpat.Success)
                    {
                        Console.WriteLine(testpat.Value);
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