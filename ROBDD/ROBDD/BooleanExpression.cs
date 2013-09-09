using System;
using System.Collections.Generic;
using SingularSys.Jep;
using SingularSys.Jep.Parser;

namespace ROBDD
{
    class BooleanExpression
    {
        public static String AND = "&&";
        public static String OR = "||";
        public static String NOT = "!";
        public static String LEFT_BRACKET = "(";
        public static String RIGHT_BRACKET = ")";

        private String expression;
        private List<String> variableNames;
        private JepInstance jep;
        INode n1 = null;

        public BooleanExpression(String expression)
        {
            this.expression = expression;
            jep = new JepInstance();
            variableNames = GetVariableNamesFromString();//.ToList();
        }

        public List<String> GetVariableNames()
        {
            return variableNames;
        }

        private List<String> GetVariableNamesFromString()
        {
            expression = ReplaceTabsLineFeeds(expression);
            String variables = ReplaceAllOperators(expression);
            String[] variableNames = variables.Split(' ');
            List<String> variablesSet = new List<String>();
            foreach (var Name in variableNames)
            {
                variablesSet.Add(Name);
            }

            variablesSet.Remove("");
            List<String> VariablesList = new List<String>(variablesSet);
            IList<String> roVariavlesList = VariablesList.AsReadOnly();
            return new List<String>(variablesSet);
        }

        private String ReplaceAllOperators(String expr)
        {
            String result = expr;
            result = result.Replace(AND, "");
            result = result.Replace(OR, "");
            result = result.Replace(NOT, "");
            result = result.Replace(LEFT_BRACKET, "");
            result = result.Replace(RIGHT_BRACKET, "");
            return result;
        }

        private String ReplaceTabsLineFeeds(String expr)
        {
            String result = expr;
            result = result.Replace("\r", " ");
            result = result.Replace("\n", " ");
            result = result.Replace("\t", " ");
            return result;
        }

        public bool Evaluate()
        {
            try
            {
                if (n1 == null)
                {
                    n1 = jep.Parse(expression);
                }
                Object evaluate = jep.Evaluate(n1);
                if (evaluate is bool)
                {
                    return (Boolean)evaluate;
                }
                else
                {
                    return !((Double)evaluate == 0.0D);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: " + e.Message);
            }
            return false;
        }

        public void SetVariables(Dictionary<String, Boolean> variableValues)
        {
            foreach (String var in variableValues.Keys)
            {
                jep.AddVariable(var, variableValues[var]);
            }
        }

        public int GetVariablesCount()
        {
            return variableNames.Count;
        }

        public BooleanExpression SetVariableValue(String variable, Boolean value)
        {
            jep.AddVariable(variable, value);
            return this;
        }

        public String GetVariableName(int index)
        {
            return variableNames[index];
        }
    }
}
