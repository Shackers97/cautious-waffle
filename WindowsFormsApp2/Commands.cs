using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsCommandParser
{
    /// <summary>
    /// Set of commands to control the drawing and logic of the form
    /// </summary>
    public class Commands
    {
        ShapeFactory sf = new ShapeFactory();
        VariableHandler vh = new VariableHandler();
        
        /// <summary>
        /// Pen Co-ordinates
        /// </summary>
        public int x, y;

        /// <summary>
        /// Draws a rectangle according to the input string
        /// </summary>
        /// <param name="command">The command string</param>
        /// <param name="p">The pen used to draw with the command</param>
        /// <param name="g">The graphics in which the shape will be drawn</param>
        /// <param name="label1">Output label for console</param>
        /// <param name="counter">The current line counter</param>
        /// <returns>The current line in which the command is run. Or -2 if the command fails</returns>
        public int RectangleCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<command>\w+)\s(?<width>\d{1,3})\s(?<height>\d{1,3})");
            Match match = pattern.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("rectangle"))
            {
                int width = int.Parse(match.Groups["width"].Value);
                int height = int.Parse(match.Groups["height"].Value);
                Shape s = sf.GetShape("rectangle");
                s.set(p.Color, x, y, width, height);
                s.draw(g, p);
            }
            else
            {
                label1.Text = "Incorrect syntax for Rectangle: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a Circle according to the input string
        /// </summary>
        /// <param name="command"></param>
        /// <param name="p"></param>
        /// <param name="g"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns>The current line in which the command is run. Or -2 if the command fails</returns>
        public int CircleCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<radius>\d{1,3})");
            Match match = reg.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("circle"))
            {
                int rad = int.Parse(match.Groups["radius"].Value);
                Shape s = sf.GetShape("circle");
                s.set(p.Color, x, y, rad);
                s.draw(g , p);
            }
            else
            {
                label1.Text = "Incorrect syntax for Circle: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a Triangle according to the input string
        /// </summary>
        /// <param name="command"></param>
        /// <param name="p"></param>
        /// <param name="g"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns>The current line in which the command is run. Or -2 if the command fails</returns>
        public int TriangleCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex pat = new Regex(@"(?<command>\w+)\s(?<a>\d{1,3})\s(?<b>\d{1,3})");
            Match match = pat.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("triangle"))
            {
                int a = int.Parse(match.Groups["a"].Value);
                int b = int.Parse(match.Groups["b"].Value);
                Shape s = sf.GetShape("triangle");
                s.set(p.Color, x, y, a, b);
                s.draw(g, p);
            }
            else
            {
                label1.Text = "Incorrect syntax for Triangle: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a polygon from a string input.
        /// </summary>
        /// <param name="command">The string to parse the command from</param>
        /// <param name="p"></param>
        /// <param name="g"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public int PolygonCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex pat = new Regex(@"(?<command>\w+)\s(?<1>\d{1,3}\,\d{1,3})\s(?<2>\d{1,3}\,\d{1,3})\s(?<3>\d{1,3}\,\d{1,3})\s?(?<4>\d{1,3}\,\d{1,3})?\s?(?<5>\d{1,3}\,\d{1,3})?\s?(?<6>\d{1,3}\,\d{1,3})?");
            Match match = pat.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("polygon"))
            {
                string a = match.Groups["1"].Value;
                string b = match.Groups["2"].Value;
                string c = match.Groups["3"].Value;
                string d = match.Groups["4"].Value;
                string e = match.Groups["5"].Value;
                string f = match.Groups["6"].Value;

                Point p1 = ToPoint(a);
                Point p2 = ToPoint(b);
                Point p3 = ToPoint(c);

                List<Point> pl = new List<Point>();
                pl.Add(p1);
                pl.Add(p2);
                pl.Add(p3);

                if (!d.Equals(""))
                {
                    Point p4 = ToPoint(d);
                    pl.Add(p4);
                }
                if (!e.Equals(""))
                {
                    Point p5 = ToPoint(e);
                    pl.Add(p5);
                }
                if (!f.Equals(""))
                {
                    Point p6 = ToPoint(d);
                    pl.Add(p6);
                }

                Point[] points = new Point[pl.Count];
                points = pl.ToArray();
                g.DrawPolygon(p, points);
            }
            else
            {
                label1.Text = "Incorrect syntax for Polygon: Stopping execution";
                return -2;
            }
            return counter;
        }

        private Point ToPoint(string coords)
        {
            string[] t = coords.Split(',');
            int x = int.Parse(t[0]);
            int y = int.Parse(t[1]);
            Point point = new Point(x, y);
            return point;
        }
        /// <summary>
        /// Draws a line upwards on the Y axis
        /// </summary>
        public int UpCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d{1,3})");
            Match match = reg.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("up"))
            {
                int newY = int.Parse(match.Groups["param"].Value) - y;
                g.DrawLine(p, x, y, x, -newY);
                y = -newY;
            }
            else
            {
                label1.Text = "Incorrect syntax for Up: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a line downwards on the Y axis
        /// </summary>
        public int DownCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d{1,3})");
            Match match = reg.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("down"))
            {
                int newY = int.Parse(match.Groups["param"].Value) + y;
                g.DrawLine(p, x, y, x, newY);
                y = newY;
            }
            else
            {
                label1.Text = "Incorrect syntax for Down: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a line left horizontally on the X axis
        /// </summary>
        public int LeftCommand(string command, Pen p, Graphics g, Label label1, int counter) 
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d{1,3})");
            Match match = reg.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("left"))
            {
                int newX = x - int.Parse(match.Groups["param"].Value);
                g.DrawLine(p, x, y, newX, y);
                x = newX;
            }
            else
            {
                label1.Text = "Incorrect syntax for Left: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a line right horizontally on the X axis
        /// </summary>
        public int RightCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d{1,3})");
            Match match = reg.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("right"))
            {
                int newX = int.Parse(match.Groups["param"].Value) + x;
                g.DrawLine(p, x, y, newX, y);
                x = newX;
            }
            else
            {
                label1.Text = "Incorrect syntax for Right: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Moves the pen position to new co-ordinates
        /// </summary>
        /// <param name="command"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public int MovePenCommand(string command, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<command>\w+)\s(?<xcoord>\d{1,3})\s(?<ycoord>\d{1,3})");
            Match match = pattern.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("movepen"))
            {
                int xcoord = int.Parse(match.Groups["xcoord"].Value);
                int ycoord = int.Parse(match.Groups["ycoord"].Value);
                x = xcoord;
                y = ycoord;
            }
            else
            {
                label1.Text = "Incorrect syntax for movePen: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a straight line to chosen co-ordinates
        /// </summary>
        /// <param name="command"></param>
        /// <param name="p"></param>
        /// <param name="g"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public int DrawToCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<command>\w+)\s(?<xcoord>\d+)\s(?<ycoord>\d+)");
            Match match = pattern.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("drawto"))
            {
                int xcoord = int.Parse(match.Groups["xcoord"].Value);
                int ycoord = int.Parse(match.Groups["ycoord"].Value);
                g.DrawLine(p, x, y, xcoord, ycoord);
                x = xcoord;
                y = ycoord;
            }
            else
            {
                label1.Text = "Incorrect syntax for DrawTo: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Draws a straight line, using a texture pen, to chosen co-ordinates
        /// </summary>
        /// <param name="command"></param>
        /// <param name="texPen">The texture pen</param>
        /// <param name="g"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public int DrawTextureCommand(string command, Pen texPen, Graphics g, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<command>\w+)\s(?<xcoord>\d+)\s(?<ycoord>\d+)");
            Match match = pattern.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("drawtexture"))
            {
                int xcoord = int.Parse(match.Groups["xcoord"].Value);
                int ycoord = int.Parse(match.Groups["ycoord"].Value);
                g.DrawLine(texPen, x, y, xcoord, ycoord);
                x = xcoord;
                y = ycoord;
            }
            else
            {
                label1.Text = "Incorrect syntax for DrawTexture: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Loops a selection of commands a specified number of times
        /// </summary>
        /// <param name="newcommand">The string containing the loop command</param>
        /// <param name="command">The list of commands, to count through to calculate the loop</param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public int LoopCommand(string newcommand, List<string> command, Label label1, int counter)
        {
            Regex loopreg = new Regex(@"(?<command>\w+)\s(?<loopnum>\d+)");
            Match match = loopreg.Match(newcommand);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("loop"))
            {
                int loopnum = int.Parse(match.Groups["loopnum"].Value);
                int loopcounter = counter;
                int c = 0;
                while (!command[loopcounter].StartsWith("endloop"))
                {
                    Console.WriteLine(command[loopcounter]);
                    loopcounter++;
                }
                Console.WriteLine("Start loop at line " + (counter + 1));
                Console.WriteLine("end loop at line " + (loopcounter + 1));
                int NoOfLines = loopcounter - counter - 1;
                List<string> looper = command.GetRange(counter + 1, NoOfLines);
                List<string> looper2 = new List<string>();
                while (c != (loopnum - 1))
                {
                    looper2.AddRange(looper);
                    c++;
                }
                command.InsertRange((counter + 1), looper2);
            }
            else
            {
                label1.Text = "Incorrect syntax for Loop: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Repeats a shape drawing
        /// </summary>
        public int RepeatCommand(string newcommand, List<string> command, Label label1, int counter)
        {
            Regex varPattern = new Regex(@"(?<command>\w+)\s(?<num>\d+)\s(?<shape>\w+)\s(?<var>\w+)\s(?<mod>\+|\-)(?<val>\d+)"); //regex With variable
            Regex intPattern = new Regex(@"(?<command>\w+)\s(?<num>\d+)\s(?<shape>\w+)\s(?<basesize>\d+)\s(?<mod>\+|\-)(?<val>\d+)"); //regex without variable
            Match varMatch = varPattern.Match(newcommand);
            Match intMatch = intPattern.Match(newcommand);
            string com = varMatch.Groups["command"].Value;
            string var = varMatch.Groups["var"].Value;
            int repcounter, numreps, increment, value, count2;
            string shape, modifier;

            if (varMatch.Success && com.Equals("repeat") && vh.VarExists(var)) //If input contains variable
            {
                value = vh.checkVarValue(var); ;

                repcounter = 0;
                numreps = int.Parse(varMatch.Groups["num"].Value);
                shape = varMatch.Groups["shape"].Value;
                modifier = varMatch.Groups["mod"].Value;
                increment = int.Parse(varMatch.Groups["val"].Value);
                
                count2 = counter;
                
            }
            else if (intMatch.Success && com.Equals("repeat")) //if input contains value
            {
                value = int.Parse(intMatch.Groups["basesize"].Value);

                repcounter = 0;
                numreps = int.Parse(intMatch.Groups["num"].Value);
                shape = intMatch.Groups["shape"].Value;
                modifier = intMatch.Groups["mod"].Value;
                increment = int.Parse(intMatch.Groups["val"].Value);
                count2 = counter;
            }
            else
            {
                label1.Text = "Incorrect syntax for repeat : Stopping execution";
                return -2;
            }
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
                command.Insert(count2 + 1, newCommand);
                repcounter++;
                count2++;
            }
            return counter;
        }

        /// <summary>
        /// Command to create a new variable, stored in an instance of variablehandler.
        /// </summary>
        public int VariableCommand(string command, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<name>\w+)\s(?<mod>\=)\s(?<value>\d+)");
            Match match = pattern.Match(command);
            if (match.Success)
            {
                string name = match.Groups["name"].Value;
                int value = int.Parse(match.Groups["value"].Value);
                Variable var = new Variable(name, value);
                if (!vh.VarExists(name))
                {
                    vh.localVars.Add(var);
                }
                else
                {
                    label1.Text = "Variable " + name + " already exists and was not created";
                }
            }
            else
            {
                label1.Text = "Incorrect syntax for variable creation: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Checks a variable against a specified value, and executes the following commands if true.
        /// </summary>
        /// <returns>The position of the line to jump to if the check does not pass</returns>
        public int IfCommand(string newcommand, List<String> command, Label label1, int counter) //SYNTAX CHECK
        {
            Regex ifReg = new Regex(@"(?<if>\w+)\s(?<variable>\w+)\s(?<mod>\=|\<|\>)\s(?<argval>\d+)");
            Match match = ifReg.Match(newcommand);

            if (match.Success)
            {
                string varname = match.Groups["variable"].Value;
                string modifier = match.Groups["mod"].Value;
                if (vh.VarExists(varname))
                {
                    int v1 = vh.checkVarValue(varname);
                    int v2 = int.Parse(match.Groups["argval"].Value);
                    bool pass = false;
                    switch (modifier)
                    {
                        case ">":
                            if (v1 > v2)
                                pass = true;
                            break;
                        case "<":
                            if (v1 < v2)
                                pass = true;
                            break;
                        case "=":
                            if (v1 == v2)
                                pass = true;
                            break;
                        default:
                            pass = false;
                            break;
                    }
                    
                    if (!pass) //if does not pass, skip to endif
                    {
                        try // find endif statement
                        {
                            int count2 = counter;
                            while (!command[count2].StartsWith("endif"))
                            {
                                count2++;
                            }
                            counter = count2;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            label1.Text = "endif statement does not exist";
                            return -2;
                        }
                    }
                } 
            }
            else
            {
                label1.Text = "Incorrect syntax for if statement: Stopping execution";
                return -2;
            }
            return counter;
        }

        /// <summary>
        /// Checks an input string for an existing variable, and modifies according to the string
        /// </summary>
        /// <param name="command"></param>
        /// <param name="label1"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public int HandleVariable(string command, Label label1, int counter)
        {
            Regex varReg = new Regex(@"(?<name>\w+)\s(?<mod>\+|\-|\=)\s(?<value>\d+)");
            Match newmatch = varReg.Match(command);

            string varName = newmatch.Groups["name"].Value;
            if (vh.VarExists(varName))
            {
                int currentVal = vh.checkVarValue(varName);
                int val = int.Parse(newmatch.Groups["value"].Value);

                vh.VariableModify(newmatch, varName, currentVal, val);
                int index = vh.getVarIndex(varName);
                label1.Text = vh.localVars[index].ToString();
            }
            else
            {
                label1.Text = "Variable does not exist";
                return -2;
            }
            return counter;
            
        }

        /// <summary>
        /// Clears the variables list, and resets the position of the pen to 0,0
        /// </summary>
        public void Clear()
        {
            x = y = 0;
            vh.localVars.Clear();
        }
    }
}
