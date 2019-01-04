using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphicsCommandParser;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GraphicsCommandParser.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        Panel testpanel = new Panel();
        Label testlabel = new Label();
        int counter = 0;

        [TestMethod()]
        public void RectangleCommandTest()
        {
            Commands commands = new Commands();
            commands.x = 0;
            commands.y = 0;
            Pen p = new Pen(Color.Black);
            Graphics g = testpanel.CreateGraphics();

            string testcommand = "rectangle 100 100";

            commands.RectangleCommand(testcommand, p, g, testlabel, counter);
            Assert.IsNotNull(testlabel);
        }

        [TestMethod()]
        public void PenPositionTest()
        {
            Commands commands = new Commands();
            commands.x = 0;
            commands.y = 0;
            Pen p = new Pen(Color.Black);
            Graphics g = testpanel.CreateGraphics();

            string testcommand = "drawto 240 250";
            commands.DrawToCommand(testcommand, p, g, testlabel, counter);

            Assert.AreEqual(commands.x, 240);
            Assert.AreEqual(commands.y, 250);
        }

        [TestMethod()]
        public void LoopTest()
        {
            Commands commands = new Commands();
            commands.x = 0;
            commands.y = 0;
            Pen p = new Pen(Color.Black);
            Graphics g = testpanel.CreateGraphics();
            List<string> testcommands = new List<string>();
            string t1 = "loop 5";
            string t2 = "down 10";
            string t3 = "right 5";
            string t4 = "endloop";
            testcommands.Add(t1);
            testcommands.Add(t2);
            testcommands.Add(t3);
            testcommands.Add(t4);
            commands.LoopCommand(t1, testcommands, testlabel, counter);
            Assert.AreEqual(12, testcommands.Count); //Number of final commands, tests that the loop command copies the correct commands the correct number of times
        }

        [TestMethod()]
        public void VariableTest()
        {
            Commands commands = new Commands();

            string variable = "var myvariable = 10";
            string variablecom = "myvariable + 5";
            commands.VariableCommand(variable, testlabel, counter);
            commands.HandleVariable(variablecom, testlabel);

            string expectedout = "variable myvariable equals: 15";
            Assert.AreEqual(expectedout, testlabel.Text);
        }

        [TestMethod()]
        public void IfTest()
        {
            Commands commands = new Commands();
            commands.x = commands.y = 0;
            string varcommand1 = "var myvar = 9";
            string varcommand2 = "myvar + 1";
            commands.VariableCommand(varcommand1, testlabel, counter);
            commands.HandleVariable(varcommand2, testlabel);

            string ifcommand = "if myvar > 10"; //
            string testcommand = "movepen 25 25";
            string endif = "endif";

            List<string> comlist = new List<string>();
            comlist.Add(ifcommand);
            comlist.Add(testcommand);
            comlist.Add(endif);

            counter = commands.IfCommand(ifcommand, comlist, testlabel, counter);
            Assert.AreEqual(2, counter);
        }
        [TestMethod()]
        public void RepeatTest()
        {
            Commands commands = new Commands();
            List<string> commandslist = new List<string>();
            string com = "repeat 4 circle 20 +5";
            commandslist.Add(com);
            string assertcommand = "circle 30";
            commands.RepeatCommand(com, commandslist, testlabel, counter);

            Assert.AreEqual(5, commandslist.Count); //should add 4 commands to the list

            Assert.AreEqual(assertcommand, commandslist[2]); //checks if the third command in the list is the correct command
        }
    }
}

