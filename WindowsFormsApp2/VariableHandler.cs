using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphicsCommandParser
{
    class VariableHandler
    {
        public List<Variable> localVars = new List<Variable>();

        public VariableHandler()
        {

        }
        public int checkVarValue(string name) //checks and retrieves the value of a variable with string name
        {
            int vcounter = 0;
            int varValue = 0;
            foreach (Variable var in localVars)
            {
                if (name.Equals(localVars[vcounter].GetName()))
                {
                    varValue = localVars[vcounter].GetValue();
                    break;
                }
                vcounter++;
            }
            return varValue;
        }

        public int getVarIndex(string name) //checks and retrieves the position of a variable with string name within a variables list
        {
            int vcounter = 0;
            foreach (Variable var in localVars)
            {
                if (name.Equals(localVars[vcounter].GetName()))
                    return vcounter;
                vcounter++;
            }
            return vcounter;
        }

        public bool VarExists(string name) //checks if variable of string name exists within a variables list
        {
            int vcounter = 0;
            foreach (Variable var in localVars)
            {
                if (name.Equals(localVars[vcounter].GetName()))
                    return true;
                else if (!name.Equals(localVars[vcounter].GetName()))
                    break;
                vcounter++;
            }
            return false;
        }

        public void VariableModify(Match newmatch, string varName, int currentVal, int val)
        {

            if (newmatch.Success && newmatch.Groups["mod"].Value.Equals("+") && VarExists(varName)) //adds value to variable
            {
                currentVal = currentVal + val;
                localVars[getVarIndex(varName)].SetValue(currentVal);
                //label1.Text = localVars[getVarIndex(varName)].ToString();
            }
            else if (newmatch.Success && newmatch.Groups["mod"].Value.Equals("-") && VarExists(varName)) //
            {
                currentVal = currentVal - val; //check for negatives
                localVars[getVarIndex(varName)].SetValue(currentVal);
                //label1.Text = localVars[getVarIndex(varName)].ToString();
            }
            else if (newmatch.Success && newmatch.Groups["mod"].Value.Equals("=") && VarExists(varName))
            {
                currentVal = val;
                localVars[getVarIndex(varName)].SetValue(currentVal);
                //label1.Text = localVars[getVarIndex(varName)].ToString();
            }
            else
            {
                //label1.Text = "error at line " + (count + 1) + ": Variable '" + varName + "' does not exist";
                return;
            }
        }
    }
}
