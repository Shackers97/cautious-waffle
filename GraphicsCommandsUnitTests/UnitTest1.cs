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

        /// <summary>
        /// Tests the rectangle command with an incorrect input, should return and not draw.
        /// </summary>
        [TestMethod()]
        public void RectangleCommandTestFail()
        {
            Commands commands = new Commands();
            
            commands.x = 0;
            commands.y = 0;
            Pen p = new Pen(Color.Black);
            Graphics g = testpanel.CreateGraphics();

            string testcommand = "rectangle 100 abc"; //incorrect syntax, should fail

            int newcounter = commands.RectangleCommand(testcommand, p, g, testlabel, counter);
            Assert.AreEqual(-2, newcounter);
        }

        /// <summary>
        /// Draws a rectangle, if succeeded then the counter should return the same as input
        /// </summary>
        [TestMethod()]
        public void RectangleCommandTestPass()
        {
            Commands commands = new Commands();
            int newcount = 2; //falsifies the counter to further down the "list"
            commands.x = 0;
            commands.y = 0;
            Pen p = new Pen(Color.Black);
            Graphics g = testpanel.CreateGraphics();

            string testcommand = "rectangle 100 100"; // command should pass

            newcount = commands.RectangleCommand(testcommand, p, g, testlabel, newcount);
            Assert.AreEqual(2, newcount);
        }

        /// <summary>
        /// Tests the current pen position, by drawing to coordinates first
        /// </summary>
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

        /// <summary>
        /// Creates a list of commands, and attempts to duplicate them 5 times
        /// </summary>
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
            Assert.AreEqual(12, testcommands.Count); //Number of final commands in the list, the two copied (5x2) plus the (2) loop commands (= 12)
        }

        /// <summary>
        /// Creates a variable and adds to it, should return the new value.
        /// </summary>
        [TestMethod()]
        public void VariableTest()
        {
            Commands commands = new Commands();

            string variable = "var myvariable = 10";
            string variablecom = "myvariable + 5";
            commands.VariableCommand(variable, testlabel, counter);
            commands.HandleVariable(variablecom, testlabel, counter);

            string expectedout = "variable myvariable equals: 15";
            Assert.AreEqual(expectedout, testlabel.Text);
        }

        /// <summary>
        /// Creates a new variable but adds to a nonexistant one, should return non-existant.
        /// </summary>
        [TestMethod()]
        public void VariableTestFail ()
        {
            Commands commands = new Commands();

            string variable = "var myvariable = 10";
            string variablecom = "testvariable + 5";
            commands.VariableCommand(variable, testlabel, counter);
            commands.HandleVariable(variablecom, testlabel, counter);

            string expectedout = "Variable does not exist";
            Assert.AreEqual(expectedout, testlabel.Text);
        }

        /// <summary>
        /// Running a simple if comparator. Should return the position of endif
        /// </summary>
        [TestMethod()]
        public void IfTest()
        {
            Commands commands = new Commands();
            commands.x = commands.y = 0;
            string varcommand1 = "var myvar = 9";
            string varcommand2 = "myvar + 1";
            commands.VariableCommand(varcommand1, testlabel, counter);
            commands.HandleVariable(varcommand2, testlabel, counter);

            string ifcommand = "if myvar > 11";
            string testcommand = "movepen 25 25"; //Should not pass this command regardless, however not called in the test
            string endif = "endif";

            List<string> comlist = new List<string>();
            comlist.Add(ifcommand);
            comlist.Add(testcommand);
            comlist.Add(endif);

            counter = commands.IfCommand(ifcommand, comlist, testlabel, counter);
            Assert.AreEqual(2, counter);
        }

        /// <summary>
        /// Testing the repeat command, should copy the command and increment it as defined
        /// </summary>
        [TestMethod()]
        public void RepeatTest()
        {
            Commands commands = new Commands();
            List<string> commandslist = new List<string>();
            string com = "repeat 4 circle 20 +5";
            commandslist.Add(com);
            string assertcommand = "circle 30";
            commands.RepeatCommand(com, commandslist, testlabel, counter);

            Assert.AreEqual(5, commandslist.Count); //should add 4 new commands to the list

            Assert.AreEqual(assertcommand, commandslist[2]); //checks if the third position in the list is the expected command
        }
    }
}

