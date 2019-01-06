using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsCommandParser
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// The current line position in the List of lines
        /// </summary>
        public int count = 0;

        List<string> command;

        Pen p;
        Pen texPen;
        Graphics g;

        string[] cmdLine;
        string csplit;

        Commands c1 = new Commands();

        public Form1()
        {
            InitializeComponent();
            label1.Text = null;
            
            Bitmap bmp = new Bitmap(@"../../images/texture1.jpg");
            TextureBrush tBrush = new TextureBrush(bmp);
            texPen = new Pen(tBrush, 20);
            Image i = bmp;
            button4.BackgroundImage = i;

            g = panel1.CreateGraphics();
            p = new Pen(Color.Red);
            p.Width = comboBox1.SelectedIndex;
            button3.BackColor = Color.Red;
        }

        private void button1_click(object sender, EventArgs e)
        {
            csplit = textBox1.Text.ToLower(); //all commands lower case
            cmdLine = csplit.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("array length: " + cmdLine.Length);
            command = cmdLine.ToList();
            while (count < command.Count && count != -1)
            {
                if (command[count].StartsWith("rectangle"))
                {
                    count = c1.RectangleCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("circle"))
                {
                    count = c1.CircleCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("triangle"))
                {
                    count = c1.TriangleCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("polygon"))
                {
                    count = c1.PolygonCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("up"))
                {
                    count = c1.UpCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("down"))
                {
                    count = c1.DownCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("right"))
                {
                    count = c1.RightCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("left"))
                {
                    count = c1.LeftCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("movepen"))
                {
                    count = c1.MovePenCommand(command[count], label1, count);
                }
                else if (command[count].StartsWith("drawto"))
                {
                    count = c1.DrawToCommand(command[count], p, g, label1, count);
                }
                else if (command[count].StartsWith("drawtexture"))
                {
                    count = c1.DrawTextureCommand(command[count], texPen, g, label1, count);
                }
                else if (command[count].StartsWith("loop"))
                {
                    count = c1.LoopCommand(command[count], command, label1, count);
                }
                else if (command[count].StartsWith("repeat"))
                {
                    count = c1.RepeatCommand(command[count], command, label1, count);
                }
                else if (command[count].StartsWith("var"))
                {
                    count = c1.VariableCommand(command[count], label1, count);
                }
                else if (command[count].StartsWith("if"))
                {
                    count = c1.IfCommand(command[count], command, label1, count);
                }
                else if (command[count].StartsWith("endloop") || command[count].StartsWith("endif"))
                {
                    //skip these lines 
                }
                else //checks for variables 
                {
                    count = c1.HandleVariable(command[count], label1, count);
                }
            count++;    
            }
        }

        private void export_click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = ".txt files|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                csplit = textBox1.Text; 
                cmdLine = csplit.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                File.WriteAllLines(saveFileDialog1.FileName, cmdLine);
            }
        }

        private void load_click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ".txt files|*.txt|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void button2_click(object sender, EventArgs e) //Clear commands
        {
            textBox1.Clear();
            panel1.Refresh();
            label1.Text = null;
            count = 0;
            c1.Clear();
            command.Clear();
        }

        private void button3_click(object sender, EventArgs e) //Colour Picker
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color c = colorDialog1.Color;
                p.Color = c;
                button3.BackColor = c;
            }
        }

        private void HelpCommand_click(object sender, EventArgs e)
        {
            CommandHelpForm helpform = new CommandHelpForm();
            
            helpform.ShowDialog();
        }

        

        private void SaveImage_click(object sender, EventArgs e)
        {
            
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            Point p1 = panel1.PointToScreen(Point.Empty);
            
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(p1, bmp.Size);
            
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            saveFileDialog2.Filter = ".jpg files|*.jpg| .png files|*.png| All files (*.*)|*.*";

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(saveFileDialog2.FileName);
            }
        }


        private void PenWidthSelection_dropdown(object sender, EventArgs e)
        {
            p.Width = comboBox1.SelectedIndex;
        }

        private void PenWidthSelection_text(object sender, EventArgs e)
        {
            p.Width = float.Parse(comboBox1.SelectedText);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void texturebutton_click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.BMP; *.JPG; *.PNG)|*.bmp; *.jpg; *.png| All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = "../../images/";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                TextureBrush tBrush = new TextureBrush(bmp);
                texPen = new Pen(tBrush, 20);
                Image i = bmp;
                button4.BackgroundImage = i;
            }
        }
    }
}
