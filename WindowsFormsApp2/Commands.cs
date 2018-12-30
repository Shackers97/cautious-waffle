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
    class Commands
    {
        ShapeFactory sf = new ShapeFactory();
        VariableHandler vh = new VariableHandler();
        public int x, y; //Pen Co-ordinates

        public void RectangleCommand(string command, Graphics g, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<command>\w+)\s(?<width>\d+)\s(?<height>\d+)");
            Match match = pattern.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("rectangle"))
            {
                int width = int.Parse(match.Groups["width"].Value);
                int height = int.Parse(match.Groups["height"].Value);
                Shape s = sf.GetShape("rectangle");
                s.set(Color.Black, x, y, width, height);
                s.draw(g);
            }
            else
            {
                label1.Text = "Incorrect syntax for Rectangle at line " + (counter + 1) + ": Stopping execution";
                return;
            }   
        }
        public void CircleCommand(string command, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<radius>\d+)");
            Match match = reg.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("circle"))
            {
                int rad = int.Parse(match.Groups["radius"].Value);
                Shape s = sf.GetShape("circle");
                s.set(Color.Black, x, y, rad);
                s.draw(g);
            }
            else
            {
                label1.Text = "Incorrect syntax for Circle at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void TriangleCommand(string command, Graphics g, Label label1, int counter)
        {
            Regex pat = new Regex(@"(?<command>\w+)\s(?<a>\d+)\s(?<b>\d+)");
            Match match = pat.Match(command);
            string com = match.Groups["command"].Value;
            if (match.Success && com.Equals("triangle"))
            {
                int a = int.Parse(match.Groups["a"].Value);
                int b = int.Parse(match.Groups["b"].Value);
                Shape s = sf.GetShape("triangle");
                s.set(Color.Black, x, y, a, b);
                s.draw(g);
            }
            else
            {
                label1.Text = "Incorrect syntax for Triangle at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void UpCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
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
                label1.Text = "Incorrect syntax for Up at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public int DownCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
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
                label1.Text = "Incorrect syntax for Down at line " + (counter + 1) + ": Stopping execution";
                return -2;
            }
            return counter;
        }
        public void LeftCommand(string command, Pen p, Graphics g, Label label1, int counter) 
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
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
                label1.Text = "Incorrect syntax for Left at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void RightCommand(string command, Pen p, Graphics g, Label label1, int counter)
        {
            Regex reg = new Regex(@"(?<command>\w+)\s(?<param>\d+)");
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
                label1.Text = "Incorrect syntax for Right at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void MovePenCommand(string command, Label label1, int counter)
        {
            Regex pattern = new Regex(@"(?<command>\w+)\s(?<xcoord>\d+)\s(?<ycoord>\d+)");
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
                label1.Text = "Incorrect syntax for movePen at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void DrawToCommand(string command, Pen p, Graphics g, Label label1, int counter)
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
                label1.Text = "Incorrect syntax for DrawTo at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void DrawTextureCommand(string command, Pen texPen, Graphics g, Label label1, int counter)     //merge with drawto?
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
                label1.Text = "Incorrect syntax for DrawTexture at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public void LoopCommand(string newcommand, List<string> command, Label label1, int counter)
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
                label1.Text = "Incorrect syntax for Loop at line " + (counter + 1) + ": Stopping execution";
                return;
            }
        }
        public int RepeatCommand(string newcommand, List<string> command, Label label1, int counter)
        {
            Regex varPattern = new Regex(@"(?<command>\w+)\s(?<num>\d+)\s(?<shape>\w+)\s(?<var>\w+)\s(?<mod>\+|\-)(?<val>\d+)"); 
            Match varMatch = varPattern.Match(newcommand);
            string com = varMatch.Groups["command"].Value;
            string var = varMatch.Groups["var"].Value;
            if (varMatch.Success && com.Equals("repeat") && vh.VarExists(var))
            {
                int repcounter = 0;
                int numreps = int.Parse(varMatch.Groups["num"].Value);
                string shape = varMatch.Groups["shape"].Value;
                string modifier = varMatch.Groups["mod"].Value;
                int increment = int.Parse(varMatch.Groups["val"].Value);
                int value = vh.checkVarValue(var); ; //base shape size
                int count2 = counter;
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
            }
            else
            {
                label1.Text = "Incorrect syntax for repeat at line " + (counter + 1) + ": Stopping execution";
                return -2;
            }
            return counter;
        }
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
                label1.Text = "Incorrect syntax for variable at line " + (counter + 1) + ": Stopping execution";
                return -2;
            }
            return counter;
        }
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
                    Console.WriteLine("if statement = " + pass);
                    if (!pass) //if does not pass, skip to endif
                    {
                        int count2 = counter;
                        while (!command[count2].StartsWith("endif"))
                        {
                            count2++;
                        }
                        Console.WriteLine("endif statement at line " + (count2 + 1));
                        counter = count2;
                    }
                }
                
            }
            else
            {
                label1.Text = "Incorrect syntax for if statement at line " + (counter + 1) + ": Stopping execution";
                return -2;
            }
            return counter;
        } 
        public void HandleVariable(string command, Label label1)
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
            
        }
        public void Clear()
        {
            x = y = 0;
            vh.localVars.Clear();
        }
    }
}
