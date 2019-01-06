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

        /// <summary>
        /// Checks and retrieves the value of a variable with string name
        /// </summary>
        /// <param name="name">The name of the variable to search for</param>
        /// <returns>The value of the variable if found</returns>
        public int checkVarValue(string name) 
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
        /// <summary>
        /// Checks and retrieves the position of a variable with string name in the variables list
        /// </summary>
        /// <param name="name">The name of the variable to search for</param>
        /// <returns>The int position of the varible in the list</returns>
        public int getVarIndex(string name)
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

        /// <summary>
        /// Checks if variable of string name exists within a variables list
        /// </summary>
        /// <param name="name">The variable to check</param>
        /// <returns>True if the variable exists within the list</returns>
        public bool VarExists(string name)
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

        /// <summary>
        /// Modifies a variable; add, subtract, or equals
        /// </summary>
        /// <param name="newmatch"></param>
        /// <param name="varName"></param>
        /// <param name="currentVal"></param>
        /// <param name="val"></param>
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
